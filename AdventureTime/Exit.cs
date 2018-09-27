﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureTime
{
    public class Exit
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public string Direction { get; set; }

        public bool isOpen { get; set; }

        public Room newRoom { get; set; }

        public string UsableWith { get; set; }

        public void usedWith(string item)
        {
            if (item == UsableWith)
            {
                isOpen = true;
                Console.WriteLine("Du öppnade dörren");
            }
            else Console.WriteLine("Det kommer inte att gå");
        }
    }
}
