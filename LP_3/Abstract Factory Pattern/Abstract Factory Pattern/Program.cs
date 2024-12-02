// Abstract product interface for Button
public interface IButton
{
    void Render(); // Render method for the button
}

// Abstract product interface for Checkbox
public interface ICheckbox
{
    void Render(); // Render method for the checkbox
}

// Concrete implementation of Windows Button
public class WindowsButton : IButton
{
    // Render a Windows-style button
    public void Render()
    {
        Console.WriteLine("Render Windows Button.");
    }
}

// Concrete implementation of Mac Button
public class MacButton : IButton
{
    // Render a Mac-style button
    public void Render()
    {
        Console.WriteLine("Render Mac Button.");
    }
}

// Concrete implementation of Windows Checkbox
public class WindowsCheckbox : ICheckbox
{
    // Render a Windows-style checkbox
    public void Render()
    {
        Console.WriteLine("Render Windows Checkbox.");
    }
}

// Concrete implementation of Mac Checkbox
public class MacCheckbox : ICheckbox
{
    // Render a Mac-style checkbox
    public void Render()
    {
        Console.WriteLine("Render Mac Checkbox.");
    }
}

// Abstract factory interface for GUI components
public interface IGUIFactory
{
    IButton CreateButton();    // Method to create a button
    ICheckbox CreateCheckbox(); // Method to create a checkbox
}

// Concrete factory for creating Windows GUI components
public class WindowsFactory : IGUIFactory
{
    public IButton CreateButton()
    {
        return new WindowsButton(); // Return Windows-style button
    }

    public ICheckbox CreateCheckbox()
    {
        return new WindowsCheckbox(); // Return Windows-style checkbox
    }
}

// Concrete factory for creating Mac GUI components
public class MacFactory : IGUIFactory
{
    public IButton CreateButton()
    {
        return new MacButton(); // Return Mac-style button
    }

    public ICheckbox CreateCheckbox()
    {
        return new MacCheckbox(); // Return Mac-style checkbox
    }
}

// Usage of Abstract Factory
class Program
{
    static void Main(string[] args)
    {
        // Use WindowsFactory to create Windows-style components
        IGUIFactory factory = new WindowsFactory();
        IButton button = factory.CreateButton(); // Create a Windows Button
        ICheckbox checkbox = factory.CreateCheckbox(); // Create a Windows Checkbox

        button.Render(); // Output: Render Windows Button.
        checkbox.Render(); // Output: Render Windows Checkbox.
    }
}
