using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureTime
{
    public class WorldBuilder
    {  
        public List<Item> items { get; set; }
        public List<Room> rooms { get; set; }

        public WorldBuilder()
        {
            items = new List<Item>();
            rooms = new List<Room>();
            BuildWorld();
          
        }
        public void BuildWorld()
        {

            // Rooms vv
            Room roomHall = new Room() { Name = "Hall", Description = "Du står i en svagt belyst hall, fönstrena utåt är trasiga. På golvet ligger glassskärvor utspridda åt alla håll. " };
                // Items vv
                
                Item kofot = new Item() { Name = "KOFOT", Description = "En kofot används för att bryta upp dörrar och att dra ut spikar" ,CanPickUp =true, UsableWith = "TÄNDARE" , UpdateName = "ELDFOT", UpdateDescription="En brinnande kofot"};
                // Items ^^

      
            Room roomVardagsrum = new Room() { Name = "Vardagsrum", Description = "You look around and realize that the planet you're on\nis very different from what you're used to. The ship's\nengine seems to be broken" };
                 // Items vv 
                Item tändare = new Item() { Name = "TÄNDARE", Description = "En trimmad ciggarettändare", CanPickUp = true };
                // Items ^^


            Room roomKök = new Room() { Name = "Kök", Description = "Här finns det ett träd vid en å" };
                // Items vv 

                // Items vv

            // Rooms ^^



            // Exits vv
            Exit hallToVardagsRum = new Exit() { newRoom =roomVardagsrum, Direction = "ÖSTER", isOpen = true };
            Exit vardagsrumToHall = new Exit() { newRoom = roomHall, Direction = "VÄSTER", isOpen = true };
            
            Exit vardagsrumToKök = new Exit() { Name ="DOOR", Description ="It's a common wooden door, it seems to be jammed", newRoom = roomKök, Direction = "SÖDER", isOpen = false, UsableWith = "KOFOT" };
            Exit kökToVardagsrum = new Exit() { newRoom = roomVardagsrum, Direction = "NORR", isOpen = true };
            // Exits ^^



            //ADD ITEM TO ROOM :
            roomHall.RoomInventory.Add(kofot);
            roomKök.RoomInventory.Add(tändare);

            // ADD EXIT TO ROOM :
            roomHall.Exits.Add(hallToVardagsRum);
            roomVardagsrum.Exits.Add(vardagsrumToHall);
            roomVardagsrum.Exits.Add(vardagsrumToKök);
            roomKök.Exits.Add(kökToVardagsrum);

            // ADD A ROOM :
            rooms.Add(roomHall);
            rooms.Add(roomVardagsrum);
            rooms.Add(roomKök);
            
        }



        

    }
}