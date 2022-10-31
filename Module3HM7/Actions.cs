namespace Module3HM7;

public class Status
{
        private Logger _logger = Logger.GetInstance();
        public void Info()
        {
            var item = new LogItem(DateTime.UtcNow, " Log info:", LogType.Info);
            _logger.Log(item);
        }
        
        public void Warning()
        {
            var item = new LogItem(DateTime.UtcNow, " Warning!:", LogType.Warning);
            _logger.Log(item);
        }

        public void Error()
        {
            var item = new LogItem(DateTime.UtcNow, " Error:", LogType.Error);
            _logger.Log(item);
        }

    
}