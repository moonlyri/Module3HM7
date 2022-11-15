namespace Module3HM7;

public class BackUp
{
    private readonly FileService _fileService;
    private StreamWriter _streamWriter;
    private string _backUpPath;

    public BackUp(FileService fileService, ConfigService configService)
    {
        _backUpPath = configService.BackUp;

        if (!Directory.Exists(_backUpPath))
        {
            Directory.CreateDirectory(_backUpPath);
        }

        _fileService = fileService;
    }

    public async void AddBackUpAsync(string text)
    {
        string nameFile = DateTime.UtcNow.ToString("yyyyMMdd_HH_mm_ss_fffff");
        nameFile = Path.Combine(_backUpPath, $"{nameFile}.txt");
        _streamWriter = new StreamWriter(nameFile);
        await _fileService.WriteFile(_streamWriter, text);
    }
}