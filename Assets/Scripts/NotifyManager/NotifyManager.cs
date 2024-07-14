public class NotifyManager
{
    private static NotifyManager _instance;

    public static NotifyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new NotifyManager();
            }
            return _instance;
        }
    }

    public string NotifyString { get; set; }

    private NotifyManager()
    {
        // Any initialization code
    }

    public string GetString()
    {
        return NotifyString;
    }

    public void SetString(string notifyString)
    {
        NotifyString = notifyString;
    }
}