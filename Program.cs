namespace TaskTracker;

class Program
{
    static void Main()
    {
        try
        {
            var app = new TaskApp();
            app.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}