using Microsoft.Extensions.DependencyInjection;
using W10.Data;
using W10.Services;

namespace W10;

public static class Program
{
    private static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        Startup.ConfigureServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var gameEngine = serviceProvider.GetService<GameEngine>();
        gameEngine?.Run();
    }
}