using System.Text;
using System.Text.Json.Serialization;
using System.Transactions;

namespace Module3HM7;

public class FileService 
{
   private SemaphoreSlim _slim = new SemaphoreSlim(1);
   private string _logPath;

   public FileService(ConfigService configService)
   {
      _logPath = configService.Log;

      if (!Directory.Exists(_logPath))
      {
         Directory.CreateDirectory(_logPath);
      }
   }

   public async Task WriteFile(StreamWriter _streamWriter, string text)
   {
      await _slim.WaitAsync();
      await _streamWriter.WriteLineAsync(text);
      await _streamWriter.FlushAsync();
      _slim.Release();
   }

}