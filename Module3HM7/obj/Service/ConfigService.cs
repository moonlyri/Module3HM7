namespace Module3HM7;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;


public class ConfigService
{
    private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
    public string Log { get; set; }
    public string BackUp { get; set; }
    public int Count { get; set; }
    public ConfigService()
    {
        LoadConfig().GetAwaiter().GetResult();
    }

    public async Task LoadConfig()
    {
        if (!File.Exists("config.json"))
        {
            var streamWriter = new StreamWriter("config.json");
            streamWriter.WriteLine("{}");
            streamWriter.Close();
        }

        var configFile = File.ReadAllText("config.json");
        JsonConvert.DeserializeObject(configFile);

        await Task.Run(async () =>
        {
            if (Count == 0)
            {
                Count = 10;
                await SaveConfigAsync();
            }
        });

        await Task.Run(async () =>
        {
            if (Log == null)
            {
                Log = ("logger.json");
                await SaveConfigAsync();
            }
        });

        await Task.Run(async () =>
        {
            if (BackUp == null)
            {
                BackUp = ("backup.json");
                await SaveConfigAsync();
            }
        });
    }

    public async Task SaveConfigAsync()
    {
        await _semaphoreSlim.WaitAsync();
        var json = JsonConvert.SerializeObject(Formatting.Indented);
        await File.WriteAllTextAsync("config.json", json);
        _semaphoreSlim.Release();
    }
}