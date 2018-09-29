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

        public Item usedWith(Item item)
        {
            if (item.Name == UsableWith)
            {
                Console.WriteLine("Du satte ihop sakerna.");
                return new Item { Name = item.UpdateName, Description = item.UpdateDescription };
            }
            else Console.WriteLine("Det kommer inte att fungera.");

            return null;
        }

        //OBS måste skriva strykjärn/blöt_strumpa i "rätt ordning"
        //public void usedWith(string item)
        //{
        //    if (item == UsableWith)
        //    {
        //        Name = UpdateName;
        //        Description = UpdateDescription;
        //        Console.WriteLine("Du satte ihop sakerna.");
        //    }
        //    else Console.WriteLine("Det kommer inte att fungera.");
        //}
    }
}