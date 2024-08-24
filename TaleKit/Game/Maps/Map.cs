using System.Collections.Concurrent;
using TaleKit.Game.Entities;

namespace TaleKit.Game.Maps;

public class Map
{
    private readonly ConcurrentDictionary<int, Monster> monsters = new();
    private readonly ConcurrentDictionary<int, Drop> drops = new();
    private readonly ConcurrentDictionary<int, Player> players = new();
    private readonly ConcurrentDictionary<int, Npc> npcs = new();
    
    private readonly ConcurrentDictionary<int, Portal> portals = new();
    private readonly ConcurrentDictionary<int, Timespace> timespaces = new();
    private readonly ConcurrentDictionary<int, Shop> shops = new();

    public IEnumerable<Monster> Monsters => monsters.Values;
    public IEnumerable<Drop> Drops => drops.Values;
    public IEnumerable<Player> Players => players.Values;
    public IEnumerable<Npc> Npcs => npcs.Values;
    public IEnumerable<Timespace> Timespaces => timespaces.Values;
    public IEnumerable<Portal> Portals => portals.Values;
    public IEnumerable<Shop> Shops => shops.Values;
    
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required byte[] Grid { get; init; }
    public required int Height { get; init; }
    public required int Width { get; init; }
    
    private byte this[int x, int y] => Grid.Skip(4 + y * Width + x).Take(1).FirstOrDefault();
    
    public void AddPortal(Portal portal)
    {
        portals[portal.Id] = portal;
    }

    public void AddShop(Shop shop)
    {
        shops[shop.Id] = shop;
    }

    public Shop GetShopByOwner(Npc npc)
    {
        return shops.GetValueOrDefault(npc.Id);
    }

    public void AddTimespace(Timespace timespace)
    {
        timespaces[timespace.Id] = timespace;
    }

    public Timespace GetTimespace(int timespaceId)
    {
        return timespaces.GetValueOrDefault(timespaceId);
    }

    public void AddEntity<T>(T entity) where T : Entity
    {
        switch (entity)
        {
            case Monster monster:
                monsters[monster.Id] = monster;
                break;
            case Player player:
                players[player.Id] = player;
                break;
            case Drop drop:
                drops[drop.Id] = drop;
                break;
            case Npc npc:
                npcs[npc.Id] = npc;
                break;
        }
    }

    public bool Contains<T>(T entity)  where T : Entity
    {
        switch (entity)
        {
            case Monster monster:
                return monsters.ContainsKey(monster.Id);
            case Player player:
                return players.ContainsKey(player.Id);
            case Drop drop:
                return drops.ContainsKey(drop.Id);
            case Npc npc:
                return npcs.ContainsKey(npc.Id);
        }

        return false;
    }

    public void RemoveEntity<T>(T entity) where T : Entity
    {
        switch (entity)
        {
            case Monster monster:
                monsters.Remove(monster.Id, out _);
                break;
            case Player player:
                players.Remove(player.Id, out _);
                break;
            case Drop drop:
                drops.Remove(drop.Id, out _);
                break;
            case Npc npc:
                npcs.Remove(npc.Id, out _);
                break;
        }
    }

    public T GetEntity<T>(EntityType entityType, int entityId) where T : Entity
    {
        Entity entity;
        switch (entityType)
        {
            case EntityType.Monster:
                entity = monsters.GetValueOrDefault(entityId);
                break;
            case EntityType.Drop:
                entity = drops.GetValueOrDefault(entityId);
                break;
            case EntityType.Player:
                entity = players.GetValueOrDefault(entityId);
                break;
            case EntityType.Npc:
                entity = npcs.GetValueOrDefault(entityId);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
        }

        if (entity is null)
        {
            return default;
        }

        return (T)entity;
    }

    public bool IsWalkable(Position position)
    {
        if (Grid.Length == 0)
        {
            return true;
        }
            
        if (position.X > Width || position.X < 0 || position.Y > Height || position.Y < 0)
        {
            return false;
        }

        byte value = this[position.X, position.Y];

        return value == 0 || value == 2 || value >= 16 && value <= 19;
    }
}