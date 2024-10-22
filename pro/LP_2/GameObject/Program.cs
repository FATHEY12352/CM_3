using System;
using System.Reflection.Metadata;
using System.Reflection;
using System.Threading.Channels;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

// الفئة الأساسية التي تمثل الكائنات في اللعبة
public abstract class GameObject
{
    public int Id { get; private set; } // معرف الكائن
    public string Name { get; private set; } // اسم الكائن
    public int X { get; private set; } // إحداثيات X
    public int Y { get; private set; } // إحداثيات Y

    // المُنشئ لتعيين القيم
    public GameObject(int id, string name, int x, int y)
    {
        Id = id;
        Name = name;
        X = x;
        Y = y;
    }
}

//id, name, (x, y) : These are private fields that store the object's ID, name, and coordinates.
//The constructor GameObject(int id, string name, int x, int y) initializes the object with values.
//GetId(), GetName(), GetX(), and GetY() are public methods to retrieve the values of these private fields.

// الفئة المجردة التي تمثل الوحدات مثل الجنود
public abstract class Unit : GameObject
{
    private float hp; // الصحة
    private bool alive; // حالة الوحدة (حية أو ميتة)

    // المُنشئ لتعيين القيم
    public Unit(int id, string name, int x, int y, float hp) : base(id, name, x, y)
    {
        this.hp = hp;
        this.alive = true;
    }

    public bool IsAlive() => alive; // التحقق مما إذا كانت الوحدة حية

    public float GetHp() => hp; // الحصول على قيمة الصحة

    public void ReceiveDamage(float damage) // استلام الضرر
    {
        hp -= damage;
        if (hp <= 0) alive = false; // إذا كانت الصحة أقل من أو تساوي الصفر، تعتبر ميتة
    }
}
//Inheritance: Unit inherits from GameObject, meaning it gets all the properties (id, name, x, y) from GameObject.
//hp: A private field representing the unit’s health.
//GetHp(): Returns the unit's current health.
//IsAlive(): Returns true if the unit's health (hp) is greater than zero.
//ReceiveDamage(float damage): Decreases the unit's health by the damage received. It also ensures health doesn’t go below zero.

// واجهة للهجوم على وحدات أخرى
public interface IAttacker
{
    void Attack(Unit unit);
}

//Interface: An interface in C# is like a contract that guarantees any class implementing this interface must define the Attack() method. 
//It doesn't define how the attack works, just that the method must exist.

// واجهة للحركة
public interface IMoveable
{
    void Move(int newX, int newY);
}

// فئة تمثل القوسيين، ترث من Unit وتطبق واجهتي IAttacker وIMoveable
public class Archer : Unit, IAttacker, IMoveable
{
    public Archer(int id, string name, int x, int y, float hp) : base(id, name, x, y, hp) { }

    public void Attack(Unit unit)
    {
        if (unit.IsAlive()) // إذا كانت الوحدة المستهدفة حية
        {
            unit.ReceiveDamage(10); // تطبيق الضرر
            Console.WriteLine($"{Name} يهاجم {unit.Name}");
        }
    }

    public void Move(int newX, int newY) // الحركة إلى إحداثيات جديدة
    {
        Console.WriteLine($"{Name} MOVE TO ({newX}, {newY})");
    }
}

//Inheritance: Archer inherits from Unit, meaning it can access health, position, and other unit-related methods.
//Interfaces:
//IAttacker: Allows the Archer to attack other units. The Attack(Unit target) method calls ReceiveDamage() on the target.
//IMoveable: Allows the Archer to move to a new position using the Move() method, which updates its coordinates.


//// فئة مجردة تمثل المباني
public abstract class Building : GameObject
{
    private bool built; // حالة البناء

    public Building(int id, string name, int x, int y) : base(id, name, x, y)
    {
        built = false; // افتراضي غير مبني
    }

    public bool IsBuilt() => built; // التحقق مما إذا كان المبنى مبنيًا

    public void Build() // بناء المبنى
    {
        built = true;
        Console.WriteLine($"{Name} bild dn.");
    }
}

//nheritance: Building inherits from GameObject, so it has properties like id, name, x, and y.
//isBuilt: A private boolean field that stores whether the building is constructed or not.
//IsBuilt(): A method that returns whether the building has been built.


// فئة تمثل الحصون، ترث من Building وتطبق واجهة IAttacker
public class Fort : Building, IAttacker
{
    public Fort(int id, string name, int x, int y) : base(id, name, x, y) { }

    public void Attack(Unit unit)
    {
        if (unit.IsAlive()) // إذا كانت الوحدة المستهدفة حية
        {
            unit.ReceiveDamage(20); // تطبيق الضرر من الحصن
            Console.WriteLine($"{Name} atac {unit.Name}");
        }
    }
}
//Inheritance: Fort inherits from Building, which means it can use all building-related methods and properties (like IsBuilt()).
//Interface: Fort implements IAttacker and can attack other units with the Attack() method.


// فئة تمثل المنازل المتنقلة، ترث من Building وتطبق واجهة IMoveable
public class MobileHouse : Building, IMoveable
{
    public MobileHouse(int id, string name, int x, int y) : base(id, name, x, y) { }

    public void Move(int newX, int newY) // الحركة إلى إحداثيات جديدة
    {
        Console.WriteLine($"{Name} mpve to ({newX}, {newY})");
    }
}

//nheritance: MobileHome inherits from Building, which gives it all the properties and methods related to buildings.
//Interface: By implementing the IMoveable interface, MobileHome can move to new coordinates using the Move() method.

// البرنامج الرئيسي لاختبار الكود
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // تغيير الترميز

        Archer archer = new Archer(1, "Archer1", 0, 0, 100); // إنشاء قوسى جديد
                   // Archer archer = new Archer(...): This creates a new archer with an ID of 1, name "Archer1", initial position(0, 0), and health of 100.
                  //Fort fort = new Fort(...): This creates a new fort with an ID of 2, name "Fort1", and initial position(5, 5).
        Fort fort = new Fort(2, "Fort1", 5, 5); // إنشاء حصن جديد

        archer.Move(7, 10); // حركة القوسى إلى إحداثيات جديدة
        //This line changes the archer's position to the new coordinates (3, 3). This demonstrates how the Move() method works for units like Archer that implement the IMoveable interface.
        fort.Attack(archer); // هجوم الحصن على القوسى
                             //The fort uses its Attack() method to attack the archer.The archer takes damage as defined by the fort's attack method, which will reduce the archer's health.
        archer.ReceiveDamage(80); // Deal 100 damage directly to kill the archer

        // إضافة تحقق من الصحة بعد الهجوم
        Console.WriteLine($"Al-Qusay's health after the attack: {archer.GetHp()}"); // تحقق من الصحة بعد الهجوم.
        Console.WriteLine($"Is the Sagittarius alive ؟ {archer.IsAlive()}"); // تحقق مما إذا كان القوسى حيًا.
         //archer.GetHp(): This retrieves the archer’s current health after the attack, and the result is printed.
        //archer.IsAlive(): This checks if the archer is still alive (if the health is greater than 0). If the archer’s health is 0 or below, the archer is considered dead.

        Console.ReadLine(); 
    }
}