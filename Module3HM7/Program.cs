using Module3HM7;
using NLog.Fluent;

class Program
{
    static TaskCompletionSource<bool> taskcompletionsourse = new TaskCompletionSource<bool>();
    static void Main(string[] args)
    {
        FileService file = new FileService();
        file.Writer();
        file.DoBackup += async () => file.Backup();
        file.WriteFile();
        taskcompletionsourse.SetResult(true);
        taskcompletionsourse.Task.GetAwaiter().GetResult();

    }
}
    