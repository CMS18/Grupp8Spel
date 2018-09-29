using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureTime
{
    public class Item
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string UpdateName { get; set; }

        public string UpdateDescription { get; set; }

        public bool CanPickUp { get; set; }

        public string UsableWith { get; set; }

        public void usedWith(string item)
        {
            if (item == UsableWith)
            {
                Name = UpdateName;
                Description = UpdateDescription;
                Console.WriteLine("Du satte ihop sakerna.");
            }
            else Console.WriteLine("Det kommer inte att fungera.");
        }
    }
}