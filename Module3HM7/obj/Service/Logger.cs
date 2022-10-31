namespace Module3HM7;

public class Logger
{
    private LogItem _head;
    private LogItem _current;

    public LogItem Storage
    {
        get { return _head; }
    }

    private static Logger _instance;


    public static Logger GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Logger();
        }

        return _instance;
    }
    public void Log(LogItem item)
    {
        if (_head == null)
        {
            _head = item;
            _current = _head;
        }
        else
        {
            _current.Next = item;
            _current = _current.Next;
        }
    }
}