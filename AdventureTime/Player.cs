using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureTime
{
    public class Player
    {
        public List<Item> PlayerInventory { get; set; } 

        public Player ()
        {
            PlayerInventory = new List<Item>();
        }
        public string Name { get; set; }

        public Room CurrentRoom { get; set; }

        public void ShowInventory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  " +Name +"s SAKER\n  --------------");
            foreach (var item in PlayerInventory)
            {
                Console.WriteLine("   "+item.Name);
            }
            Console.WriteLine("\n  --------------\n");
            Console.ResetColor();
            
        }
        public void combine()
        { }
    }
}