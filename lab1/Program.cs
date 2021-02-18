using System;
using System.Collections.Generic;

/*
 * Lab 1
 * Variant 1
 * 
 * Реалізувати гру, де можуть бути воїни різних типів: 
 * стрільці, піхота, кінні війська, танкові війська. 
 * 
 * Всі типи повинні мати ряд однакових характеристик, наприклад, рівень здоров’я, тощо. 
 * 
 * Крім того, кожен з типів повинен мати свої відмінності, 
 * наприклад, зовнішній вигляд, міць, швидкість пересування тощо
 */

/*
 * Prototype is a creational design pattern 
 * that allows cloning objects, even complex ones, without coupling to their specific classes.
 */

namespace lab1
{
    class Program
    {
        // Declare Unit field for the future prototype objects
        // If the game is complex, may need to create multiple such variables
        static Unit prototype;

        static void Main(string[] args)
        {
            Console.WriteLine("\nThe countries prepare for battle...\n");
            PrintLine();
            Console.WriteLine($"{"country",15}{"type",25}{"health",10}{"speed",10}{"damage",10}");
            PrintLine();

            List<Unit> frenchArmy = new List<Unit>();

            Unit.currentCountry = "France";

                // Create Tank instance with default parameters
                prototype = new Tank();
                // Clone Tank prototype instance two times into the army collection,

                /* 
                 * the Clone() method is in the loop in AddUnits()
                 */

                AddUnits(ref frenchArmy, prototype, 2);

                prototype = new Artillery();
                AddUnits(ref frenchArmy, prototype, 2);

                // Create custom Cavalry instance with specified parameters
                // Royal Cavalry forces "Garde du Corps"
                prototype = new Cavalry("Royal ", 1.8f, 2.5f);
                AddUnits(ref frenchArmy, prototype, 1);

                // Common Cavalry forces
                prototype = new Cavalry();
                AddUnits(ref frenchArmy, prototype, 4);

                prototype = new Infantry();
                AddUnits(ref frenchArmy, prototype, 1);
                PrintLine();

            List<Unit> germanArmy = new List<Unit>();
            Unit.currentCountry = "Germany";

                // Panzer Tank forces
                prototype = new Tank("Panzer ", 2.3f, 1.4f);
                AddUnits(ref germanArmy, prototype, 2);

                // Blitzkrieg Tank forces
                prototype = new Tank("Blitzkrieg ", 4.5f, 2.2f);
                AddUnits(ref germanArmy, prototype, 3);

                // Motorized Infantry
                prototype = new Infantry("Motorized ", 1.8f);
                AddUnits(ref germanArmy, prototype, 6);
                PrintLine();

            // Test Fight() method for individual units and show results
            Console.WriteLine("\nThe battle begins...");

            Random randomMobilizer = new Random();
            BattleSimulator(frenchArmy, randomMobilizer);
            BattleSimulator(germanArmy, randomMobilizer);

            Console.WriteLine("The battle ends...\nThe health of the participating units is lower:\n");

            PrintLine();
            foreach (Unit i in frenchArmy) i.GetInfo();
            PrintLine();
            foreach (Unit i in germanArmy) i.GetInfo();
            PrintLine();

            Console.Read();
        }

        // Delimiter for the console
        public static void PrintLine()
        {
            Console.WriteLine(new string('-', 70));
        }

        // Clone a number of prototype units into the army list and show their stats
        private static void AddUnits(ref List<Unit> army, Unit prototype, int number)
        {
            for (int i = 0; i < number; i++)
            {
                army.Add(prototype.Clone());
                army[^1].GetInfo();
            }
        }

        // Simulate some army units doing Fight()
        private static void BattleSimulator(List<Unit> army, Random random)
        {
            int mobilizedUnits = random.Next(3, army.Count + 1);
            int mobilizedOffset = random.Next(army.Count - mobilizedUnits + 1);
            for (int i = mobilizedOffset; i < mobilizedUnits; i++)
            {
                army[i].Fight();
            }
        }
    }

    // Prototype Unit
    abstract class Unit
    {
        public static string currentCountry { get; set; }

        public string   country { get; protected set; }
        public string   type { get; protected set; }
        public int      health { get; protected set; }
        public int      speed { get; protected set; }
        public int      damage { get; protected set; }

        static Random randomBattler = new Random();

        // Constructor of the common default stats for every unit
        public Unit()
        {
            country = currentCountry;
            health = 100;
        }

        // Clone() returns a shallow copy of the current object
        // If the class has a property with reference type,
        // may need to create DeepClone() to be able to
        // modify the cloned object without affecting the original object
        public virtual Unit Clone()
        {
            return (Unit)MemberwiseClone();
        }

        // Show the stats of the object
        public void GetInfo()
        {
            Console.WriteLine($"{country,15}{type,25}{health,10}{speed,10}{damage,10}");
        }

        // Simulate losing health of the object after the battle
        public void Fight()
        {
            health -= randomBattler.Next(100);
        }
    }

    // Concrete Unit type 1
    class Infantry : Unit
    {
        // Constructor with optional parameters
        public Infantry(string prefix = "", float speedMultiplier = 1.0f, float damageMultiplier = 1.0f)
        {
            type = prefix + "Infantry";
            speed = (int)(90 * speedMultiplier);
            damage = (int)(40 * damageMultiplier);
        }

    }

    // Concrete Unit type 2
    class Cavalry : Unit
    {
        // Constructor with optional parameters
        public Cavalry(string prefix = "", float speedMultiplier = 1.0f, float damageMultiplier = 1.0f)
        {
            type = prefix + "Cavalry";
            speed = (int)(300 * speedMultiplier);
            damage = (int)(20 * damageMultiplier);
        }

    }

    // Concrete Unit type 3
    class Artillery : Unit
    {
        // Constructor with optional parameters
        public Artillery(string prefix = "", float speedMultiplier = 1.0f, float damageMultiplier = 1.0f)
        {
            type = prefix + "Artillery";
            speed = (int)(50 * speedMultiplier);
            damage = (int)(200 * damageMultiplier);
        }

    }

    // Concrete Unit type 4
    class Tank : Unit
    {
        // Constructor with optional parameters
        public Tank(string prefix = "", float speedMultiplier = 1.0f, float damageMultiplier = 1.0f)
        {
            type = prefix + "Tank";
            speed = (int)(150 * speedMultiplier);
            damage = (int)(120 * damageMultiplier);
        }

    }

}
