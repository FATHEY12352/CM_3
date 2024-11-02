// Product class: Car
public class Car
{
    public string Engine { get; set; } // Engine type
    public string Wheels { get; set; } // Wheels type
    public string Transmission { get; set; } // Transmission type

    // Method to display the built car
    public void Show()
    {
        Console.WriteLine($"Car with {Engine}, {Wheels}, {Transmission}");
    }
}

// Builder interface for building different parts of a car
public interface ICarBuilder
{
    void BuildEngine(); // Build the engine
    void BuildWheels(); // Build the wheels
    void BuildTransmission(); // Build the transmission
    Car GetCar(); // Return the built car
}

// Concrete builder for creating a sports car
public class SportsCarBuilder : ICarBuilder
{
    private Car _car = new Car(); // New car object being built

    public void BuildEngine()
    {
        _car.Engine = "V8 Engine"; // Sports car engine
    }

    public void BuildWheels()
    {
        _car.Wheels = "Sport Wheels"; // Sports car wheels
    }

    public void BuildTransmission()
    {
        _car.Transmission = "Manual Transmission"; // Sports car transmission
    }

    // Return the fully built car
    public Car GetCar()
    {
        return _car;
    }
}

// Director class responsible for managing the building process
public class CarDirector
{
    private ICarBuilder _carBuilder;

    // Constructor to set the builder
    public CarDirector(ICarBuilder carBuilder)
    {
        _carBuilder = carBuilder;
    }

    // Build the car using the builder
    public void BuildCar()
    {
        _carBuilder.BuildEngine(); // Build engine
        _carBuilder.BuildWheels(); // Build wheels
        _carBuilder.BuildTransmission(); // Build transmission
    }

    // Get the fully built car
    public Car GetCar()
    {
        return _carBuilder.GetCar();
    }
}

// Usage of Builder Pattern
class Program
{
    static void Main(string[] args)
    {
        // Create a builder for a sports car
        ICarBuilder builder = new SportsCarBuilder();
        CarDirector director = new CarDirector(builder);

        // Build the sports car
        director.BuildCar();
        Car car = director.GetCar();

        // Display the built car
        car.Show(); // Output: Car with V8 Engine, Sport Wheels, Manual Transmission
    }
}



