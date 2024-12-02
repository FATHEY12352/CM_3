using System;
using System.Collections.Generic;

// ====================
// Part 1: Strategy Pattern
// ====================

// Interface for sorting strategies
public interface ISortStrategy
{
    void Sort(List<int> list); // Method to sort a list
}

// BubbleSort class implementing the sorting strategy
public class BubbleSort : ISortStrategy
{
    public void Sort(List<int> list)
    {
        // Bubble sort algorithm implementation
        for (int i = 0; i < list.Count - 1; i++) // Loop through the list
            for (int j = 0; j < list.Count - i - 1; j++) // Inner loop for comparison
                if (list[j] > list[j + 1]) // If current element is greater than next
                {
                    // Swap elements if they are in the wrong order
                    int temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
    }
}

// QuickSort class implementing the sorting strategy
public class QuickSort : ISortStrategy
{
    public void Sort(List<int> list)
    {
        // Quick sort algorithm implementation
        if (list.Count <= 1) return; // Base case for recursion

        int pivot = list[list.Count / 2]; // Choose pivot element from the middle
        List<int> less = new List<int>(), greater = new List<int>(); // Lists for partitioning

        foreach (var item in list) // Partitioning the list into less and greater than pivot
            if (item < pivot) less.Add(item);
            else if (item > pivot) greater.Add(item);

        Sort(less); // Recursively sort the 'less' list
        Sort(greater); // Recursively sort the 'greater' list

        // Clear original list and combine sorted parts
        list.Clear();
        list.AddRange(less);
        list.Add(pivot);
        list.AddRange(greater);
    }
}

// Context class for using a sorting strategy
public class SortContext
{
    private ISortStrategy _sortStrategy; // Current sorting strategy

    public SortContext(ISortStrategy sortStrategy) => _sortStrategy = sortStrategy; // Constructor to set initial strategy

    public void SetSortStrategy(ISortStrategy sortStrategy) => _sortStrategy = sortStrategy; // Method to change strategy

    public void Sort(List<int> list) => _sortStrategy.Sort(list); // Method to sort using current strategy
}

// ====================
// Part 2: Chain of Responsibility Pattern
// ====================

// Abstract Handler class for request processing
public abstract class Handler
{
    protected Handler? NextHandler; // Next handler in the chain

    public void SetNext(Handler nextHandler) => NextHandler = nextHandler; // Method to set next handler

    public abstract void HandleRequest(string request); // Abstract method to handle requests
}

// Concrete handler A that processes request "A"
public class ConcreteHandlerA : Handler
{
    public override void HandleRequest(string request)
    {
        if (request == "A") // If request is "A"
            Console.WriteLine("Handler A handled request A"); // Handle it here
        else
            NextHandler?.HandleRequest(request); // Pass to next handler if it can't handle it
    }
}

// Concrete handler B that processes request "B"
public class ConcreteHandlerB : Handler
{
    public override void HandleRequest(string request)
    {
        if (request == "B") // If request is "B"
            Console.WriteLine("Handler B handled request B"); // Handle it here
        else
            NextHandler?.HandleRequest(request); // Pass to next handler if it can't handle it
    }
}

// ====================
// Part 3: Iterator Pattern
// ====================

// Interface for iterators that provides access to elements in a collection
public interface IIterator<T>
{
    T Next(); // Method to get the next element
    bool HasNext(); // Method to check if more elements exist
}

// Concrete iterator implementation for iterating over a collection
public class ConcreteIterator<T> : IIterator<T>
{
    private List<T> _collection; // Collection being iterated over
    private int _currentIndex = 0; // Current index in the collection

    public ConcreteIterator(List<T> collection) => _collection = collection; // Constructor to set collection

    public T Next() => _collection[_currentIndex++]; // Return current item and increment index

    public bool HasNext() => _currentIndex < _collection.Count; // Check if there are more items left to iterate over
}

// Collection class that holds items and creates an iterator for them
public class Collection<T>
{
    private List<T> _items = new List<T>(); // Internal storage for items

    public void Add(T item) => _items.Add(item); // Method to add an item to the collection

    public IIterator<T> CreateIterator() => new ConcreteIterator<T>(_items); // Create and return an iterator for the collection
}

// ====================
// Main Program Demonstrating All Patterns 
// ====================
class Program
{
    static void Main(string[] args)
    {
        // Demonstration of Strategy Pattern:

        var numbers = new List<int> { 5, 3, 8, 1, 2 }; // Sample data

        var context = new SortContext(new BubbleSort()); // Create context with BubbleSort strategy

        context.Sort(numbers); // Sort using BubbleSort strategy
        Console.WriteLine("Bubble Sorted: " + string.Join(", ", numbers)); // Output sorted result

        context.SetSortStrategy(new QuickSort()); // Change strategy to QuickSort

        numbers = new List<int> { 5, 3, 8, 1, 2 }; // Reset sample data for new sorting

        context.Sort(numbers); // Sort using QuickSort strategy
        Console.WriteLine("Quick Sorted: " + string.Join(", ", numbers)); // Output sorted result

        // Demonstration of Chain of Responsibility Pattern:

        Handler handlerA = new ConcreteHandlerA(); // Create handler A
        Handler handlerB = new ConcreteHandlerB(); // Create handler B

        handlerA.SetNext(handlerB); // Set up chain: A -> B

        handlerA.HandleRequest("A"); // Request handled by A
        handlerA.HandleRequest("B"); // Request handled by B
        handlerA.HandleRequest("C"); // Request not handled by any handler

        // Demonstration of Iterator Pattern:

        var collection = new Collection<string>(); // Create a new collection

        collection.Add("Item 1"); // Add items to the collection
        collection.Add("Item 2");
        collection.Add("Item 3");

        var iterator = collection.CreateIterator(); // Create an iterator for the collection

        while (iterator.HasNext()) // Iterate through all items in the collection
            Console.WriteLine(iterator.Next()); // Output each item using iterator's Next method
    }
}



