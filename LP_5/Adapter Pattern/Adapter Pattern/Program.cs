// Target Interface
public interface ITarget
{
    string GetRequest();
}

// Adaptee (Legacy Component)
public class Adaptee
{
    public string GetSpecificRequest()
    {
        return "Specific request data";
    }
}

// Adapter
public class Adapter : ITarget
{
    private readonly Adaptee _adaptee;

    public Adapter(Adaptee adaptee)
    {
        _adaptee = adaptee;
    }

    public string GetRequest()
    {
        return $"Adapted: {_adaptee.GetSpecificRequest()}";
    }
}

// Usage
class Program
{
    static void Main(string[] args)
    {
        Adaptee adaptee = new Adaptee();
        ITarget target = new Adapter(adaptee);

        Console.WriteLine(target.GetRequest());
    }
}
