// Abstract class representing Animal
public abstract class Animal
{
    // Abstract method to be implemented by concrete classes
    public abstract void Speak();
}

// Concrete class representing Dog
public class Dog : Animal
{
    // Implementation of Speak method for Dog
    public override void Speak()
    {
        Console.WriteLine("Woof!");
    }
}

// Concrete class representing Cat
public class Cat : Animal
{
    // Implementation of Speak method for Cat
    public override void Speak()
    {
        Console.WriteLine("Meow!");
    }
}

// Abstract factory class for creating animals
public abstract class AnimalFactory
{
    // Abstract method to be implemented by specific factories
    public abstract Animal CreateAnimal();
}

// Concrete factory for creating Dog
public class DogFactory : AnimalFactory
{
    // Create a Dog instance
    public override Animal CreateAnimal()
    {
        return new Dog();
    }
}

// Concrete factory for creating Cat
public class CatFactory : AnimalFactory
{
    // Create a Cat instance
    public override Animal CreateAnimal()
    {
        return new Cat();
    }
}

// Usage of Factory Method
class Program
{
    static void Main(string[] args)
    {
        // Use DogFactory to create a Dog
        AnimalFactory factory = new DogFactory();
        Animal animal = factory.CreateAnimal();
        animal.Speak(); // Output: Woof!
    }
}
