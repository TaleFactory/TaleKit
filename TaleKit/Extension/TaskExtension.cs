namespace TaleKit.Extension;

public static class TaskExtension
{
    public static void Then(this Task task, Func<Task> action)
    {
        task.ContinueWith(async x =>
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something happened in pending task");
                Console.WriteLine(ex.Message);
            }
        });
    }
    
    public static Task ThenExecute(this Task task, Action action)
    {
        return task.ContinueWith(x =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something happened in pending task");
                Console.WriteLine(ex.Message);
            }
        });
    }
    
    public static void Then(this Task task, Action action)
    {
        task.ContinueWith(x =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something happened in pending task");
                Console.WriteLine(ex.Message);
            }
        });
    }
}