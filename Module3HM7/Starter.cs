namespace Module3HM7;

public class Starter
{
    private StreamWriter _streamWriter;
    private string _logName = "Log.txt";
    public Starter(Logger logger, BackUp backUpService, ConfigService configService)
    {
        Logger = logger;
        BackUpService = backUpService;
        _streamWriter = new StreamWriter(Path.Combine(configService.Log, _logName));
    }

    public Logger Logger { get; }
    public BackUp BackUpService { get; }
    public async Task Run()
    {
        Logger.BackUp += BackUpService.AddBackUpAsync;
        var task1 = Task.Run(async () => await MakeLogAsync(50));
        await Task.WhenAll(task1);
    }

    public async Task MakeLogAsync(int countLogs)
    {
        var rnd = new Random();
        for (var i = 0; i < countLogs; i++)
        {
            await Logger.Log((LogType)rnd.Next(3), _streamWriter, $"{i}");
        }
    }
}