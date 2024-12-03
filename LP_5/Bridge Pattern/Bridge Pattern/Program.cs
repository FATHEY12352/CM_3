// Abstraction
public abstract class RemoteControl
{
    protected IDevice Device;

    protected RemoteControl(IDevice device)
    {
        Device = device;
    }

    public abstract void TogglePower(); // Abstract method that must be implemented
}

// Extended Abstraction
public class AdvancedRemoteControl : RemoteControl
{
    public AdvancedRemoteControl(IDevice device) : base(device) { }

    public override void TogglePower()
    {
        if (Device.IsOn())
        {
            Device.TurnOff();
        }
        else
        {
            Device.TurnOn();
        }
    }

    public void SetVolume(int volume)
    {
        Device.SetVolume(volume);
    }
}

// Implementation Interface
public interface IDevice
{
    void TurnOn();
    void TurnOff();
    void SetVolume(int volume);
    bool IsOn();
}

// Concrete Implementation
public class TV : IDevice
{
    private bool _isOn;

    public void TurnOn()
    {
        _isOn = true;
        Console.WriteLine("TV is turned on.");
    }

    public void TurnOff()
    {
        _isOn = false;
        Console.WriteLine("TV is turned off.");
    }

    public void SetVolume(int volume)
    {
        Console.WriteLine($"TV volume set to {volume}.");
    }

    public bool IsOn() => _isOn;
}

// Usage
class Program
{
    static void Main(string[] args)
    {
        IDevice tv = new TV();
        RemoteControl remote = new AdvancedRemoteControl(tv);

        // Toggle Power On
        remote.TogglePower();

        // Set volume
        ((AdvancedRemoteControl)remote).SetVolume(15);

        // Toggle Power Off
        remote.TogglePower();
    }
}
