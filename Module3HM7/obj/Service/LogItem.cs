namespace Module3HM7;

public class LogItem
{
    public LogItem Next { get; set; }

    public DateTime datetime { get; }

    public string Message { get; }

    public LogType Type { get; }

    public LogItem(DateTime _datetimetime, string message, LogType type)
    {
        datetime = _datetimetime;
        Message = message;
        Type = type;
    }
}