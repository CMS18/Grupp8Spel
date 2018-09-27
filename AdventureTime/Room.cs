using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureTime
{
    public class Room 
    {
        public Room()
        {
        RoomInventory = new List<Item>();
        Exits = new List<Exit>();
        }
        public List<Item> RoomInventory { get; set; }
        public List<Exit> Exits { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public void currentDescription()
        {
            
            Console.WriteLine(Description); // Description på rummet

            Console.Write("I rummet hittar du : ");
            for (int i = 0; i < RoomInventory.Count; i++)
            {
                Console.Write(RoomInventory[i].Name + " ");
            }
            Console.Write("\n");

        }
    }
}