// Subject Interface
public interface IImage
{
    void Display();
}

// Real Subject
public class RealImage : IImage
{
    private string _fileName;

    public RealImage(string fileName)
    {
        _fileName = fileName;
        LoadFromDisk();
    }

    private void LoadFromDisk()
    {
        Console.WriteLine($"Loading image: {_fileName}");
    }

    public void Display()
    {
        Console.WriteLine($"Displaying image: {_fileName}");
    }
}

// Proxy
public class ProxyImage : IImage
{
    private RealImage _realImage;
    private string _fileName;

    public ProxyImage(string fileName)
    {
        _fileName = fileName;
    }

    public void Display()
    {
        if (_realImage == null)
        {
            _realImage = new RealImage(_fileName);
        }
        _realImage.Display();
    }
}

// Usage
class Program
{
    static void Main(string[] args)
    {
        IImage image = new ProxyImage("photo.jpg");

        // Image is loaded only when Display is called
        image.Display();
        image.Display(); // No loading occurs here
    }
}
