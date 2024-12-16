// Singleton class definition
public class Logger
{
    // Private static variable to hold the single instance of the class
    private static Logger _instance;

    // Object to lock for thread safety
    private static readonly object _lock = new object();

    // Private constructor to prevent direct instantiation
    private Logger() { }

    // Public static method to get the instance of the class
    public static Logger GetInstance()
    {
        // Check if instance is null
        if (_instance == null)
        {
            // Lock to ensure thread safety
            lock (_lock)
            {
                // If still null, create the instance
                if (_instance == null)
                {
                    _instance = new Logger();
                }
            }
        }
        return _instance;
    }

    // Method to log a message
    public void Log(string message)
    {
        Console.WriteLine($"Log: {message}");
    }
}

// Usage of Singleton
class Program
{
    static void Main(string[] args)
    {
        // Get the single instance of Logger
        Logger logger = Logger.GetInstance();
        logger.Log("Singleton pattern example."); // Log a message using the single Logger instance
    }
}
