using System.Text;

namespace Module3HM7;

public class Logger
{
    private readonly ConfigService _configService;
    private readonly StringBuilder _logs;
    private static int _counter = 0;
    private readonly FileService _fileService;
    private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);


    public event Action<string> BackUp;

    public async Task Log(LogType logType, StreamWriter streamWriter, string message)
    {
        await _semaphoreSlim.WaitAsync();
        _counter++;
        var log = $"{DateTime.UtcNow}: {logType}: {message}";
        await _fileService.WriteFile(streamWriter, log);
        _logs.AppendLine(log);
        if (_counter % _configService.Count == 0)
        {
            BackUp?.Invoke(_logs.AppendLine().ToString());
        }

        _semaphoreSlim.Release();
    }
}