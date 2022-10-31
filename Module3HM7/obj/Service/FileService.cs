using System.Text;
using System.Text.Json.Serialization;
using System.Transactions;

namespace Module3HM7;

public class FileService 
{
   private static string path = "logger.txt";
   private static string backup = "loggerbackup.txt";
   private StreamWriter _streamWriter = new StreamWriter(path);
   private StreamWriter _writerbackup = new StreamWriter(backup);
   private SemaphoreSlim _slim = new SemaphoreSlim(1);
   public event Action DoBackup;
   
   public async void Writer()
   {
      if (!File.Exists(path))
      {
         File.Create(path);
      }
      
      Status status = new Status();
      var randomize = new Random();
      for (int i = 0; i <= 100; i++)
      {
         int random = randomize.Next(3);
         if (random == 0)
         {
            status.Info();
         }
         else if (random == 1)
         {
            status.Warning();
         }
         else
         {
            status.Error();
         }
      }
      
      var text = new StringBuilder();
      var current = Logger.GetInstance().Storage;
         while (current != null)
         {
            text.AppendLine(current.datetime.ToUniversalTime().ToString() + " Type:" + current.Type + " Message" + current.Message);

            current = current.Next;
         }

         await File.WriteAllTextAsync(path, text.ToString());
   }


   public async Task Backup()
   {
      if (!Directory.Exists(backup))
      {
         Directory.CreateDirectory(backup);
      }

      await using (_writerbackup)
      {
         string[] log = Directory.GetFiles(path);
            for (int i = 0; i < 100; i++)
            {
               if (log.Length >= 10)
               {
                  DoBackup.Invoke();
                  try
                  {
                     
                     File.Copy(path, backup, true);

                  }
                  catch (Exception CopyFailed)
                  {
                     Console.WriteLine(CopyFailed.Message);
                  }
               }
            }
      }
      
   }

   public async Task WriteFile()
   {
      await _slim.WaitAsync();
      await _streamWriter.WriteLineAsync(path);
      await _writerbackup.WriteLineAsync(backup);
      await _streamWriter.FlushAsync();
      await _writerbackup.FlushAsync();
      _slim.Release();
   }

}