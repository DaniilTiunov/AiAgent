using AiAgent.Control.Controllers;
using AiAgent.DependencyInjection;

namespace AiAgent.Control;

/// <summary>
/// Точка входа в API приложения.
/// </summary>
public static class Program
{
    /// <summary>
    /// Запускает веб-хост приложения.
    /// </summary>
    private static async Task Main(string[] args)
    {
        try
        {
            using var host = Startup.BuildHost(
                args,
                typeof(ChatController).Assembly);

            await host.RunAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}