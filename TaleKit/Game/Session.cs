﻿using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TaleKit.Extension;
using TaleKit.Game.Entities;
using TaleKit.Game.Event;
using TaleKit.Network;
using TaleKit.Network.Packet;
using TaleKit.Network.Packet.Interaction;

namespace TaleKit.Game;

public class Session
{
    private readonly INetwork network;
    private readonly PacketFactory factory;
    private readonly ProcessorRegistry registry;
    private readonly ConcurrentDictionary<Type, ConcurrentBag<IEventProcessor>> processors = new();

    public event Action<IPacket> PacketReceived;
    public event Action<IPacket> PacketSend;
    public event Action Disconnected;
    
    public Session(INetwork network, IActionBridge bridge)
    {
        var services = new ServiceCollection()
            .AddSingleton<PacketFactory>()
            .AddSingleton<ProcessorRegistry>()
            .AddImplementingTypes<IPacketProcessor>()
            .AddSingleton<INetwork>(network)
            .AddSingleton<Session>(this)
            .BuildServiceProvider();
        
        this.network = network;
        this.factory = services.GetRequiredService<PacketFactory>();
        this.registry = services.GetRequiredService<ProcessorRegistry>();
        this.network.PacketSend += OnPacketSend;
        this.network.PacketReceived += OnPacketReceived;
        this.network.Disconnected += () => Disconnected?.Invoke();

        Character = new Character(bridge, network);
    }

    public Character Character { get; set; }

    public void SendPacket(string packet)
    {
        this.network.SendPacket(packet);
    }

    public void RecvPacket(string packet)
    {
        this.network.RecvPacket(packet);
    }

    public void Close()
    {
        this.network.Disconnect();
    }

    public Session AddProcessor<T>() where T : IEventProcessor
    {
        var processor = Activator.CreateInstance<T>();
        AddProcessor(processor);
        return this;
    }

    public void AddProcessor<T>(T processor) where T : IEventProcessor
    {
        var eventProcessors = processors.GetValueOrDefault(processor.EventType);
        if (eventProcessors is null)
        {
            processors[processor.EventType] = eventProcessors = new ConcurrentBag<IEventProcessor>();
        }
        
        eventProcessors.Add(processor);
    }

    public void AddProcessor<T>(Action<T> task) where T : IEvent
    {
        AddProcessor(new SimpleProcessor<T>(task));
    }

    public void Emit<T>(T e) where T : IEvent
    {
        var eventProcessors = processors.GetValueOrDefault(e.GetType());
        if (eventProcessors is null)
        {
            return;
        }

        foreach (var processor in eventProcessors)
        {
            processor.Process(e);
        }
    }

    private void OnPacketReceived(string packet)
    {
        try
        {
            var typedPacket = factory.CreatePacket(packet);
            if (typedPacket is null)
            {
                return;
            }
        
            PacketReceived?.Invoke(typedPacket);

            var processor = registry.GetProcessor(PacketDirection.Receive, typedPacket.GetType());
            if (processor is null)
            {
                return;
            }
        
            processor.Process(this, typedPacket);
        }
        catch (Exception e)
        {
            Log.Error(e, "Failed to process packet");
        }
    }

    private void OnPacketSend(string packet)
    {
        try
        {
            var typedPacket = factory.CreatePacket(packet);
            if (typedPacket is null)
            {
                return;
            }

            PacketSend?.Invoke(typedPacket);

            var processor = registry.GetProcessor(PacketDirection.Send, typedPacket.GetType());
            if (processor is null)
            {
                return;
            }

            processor.Process(this, typedPacket);
        } 
        catch (Exception e)
        {
            Log.Error(e, "Failed to process packet");
        }
    }
}