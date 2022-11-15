using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Module3HM7;

public class Program
{
    public static async Task Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<Logger>()
            .AddTransient<FileService>()
            .AddTransient<BackUp>()
            .AddTransient<ConfigService>()
            .AddTransient<Starter>()
            .BuildServiceProvider();

        var start = serviceProvider.GetService<Starter>();
        await start.Run();
    }
}