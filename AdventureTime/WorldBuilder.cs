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
            Room roomHall = new Room()
            {
                Name = "Hall",
                Description = "En sliten byrå står i ena hörnet och krokar för din \nhemnyckel och paraply sitter på väggen \nbredvid dörren. Under dörren ligger en matta. "
            };
            // Items vv

            //OITEMS: Matta(SKOR), Byrå, krokar(PARAPLY).
            //MattaD: Det är en "Welcome Home" - matta med röd smutsig text, på mattan står ett par vita sneakers.
            //ByråD: Det är en byrå som du fått av din gamla moster, du gillar den inte men hon skulle bli ledsen om du gjorde dig av med den.
            //KrokarD: Det är två rostiga krokar, på den ena hänger ett paraply och den andra, där nyckeln brukar hänga, är tom.
            //EITEMS: Paraply, skor
            //ParaplyD: Ett slitet paraply med företagsloggan på.
            //SkorD"look matta" finns bool= true, syns ej när du kollar på hallen men när du kollar på mattan.



            Item kofot = new Item() { Name = "KOFOT", Description = "En kofot används för att bryta upp dörrar och att dra ut spikar", CanPickUp = true, UsableWith = "TÄNDARE", UpdateName = "ELDFOT", UpdateDescription = "En brinnande kofot" };
            // Items ^^


            Room roomVardagsrum = new Room() { Name = "Vardagsrum", Description = "You look around and realize that the planet you're on\nis very different from what you're used to. The ship's\nengine seems to be broken" };
            // Items vv 
            Item tändare = new Item() { Name = "TÄNDARE", Description = "En trimmad ciggarettändare", CanPickUp = true };
            // Items ^^


            Room roomKök = new Room() { Name = "Kök", Description = "I ena delen av köket står ett köksbord med två stolar och \npå andra finns en micro, ett kylskåp och en frys." };
            Item micro = new Item() { Name = "MICRO", Description = "En micro i vit plast som gulnat lite med åren men som funkar \nriktigt bra.", CanPickUp = true };
            Item frys = new Item() { Name = "FRYS", Description = "Frysdören har fryst igen och går inte att öppna.", CanPickUp = true, UsableWith = "PARAPLY", UpdateName = "ÖPPEN FRYS", UpdateDescription = "Dörren öppnas, inuti ligger din andra strumpa." };
            Item strumpa = new Item() { Name = "STRUMPA", Description = "Det är en blå skjorta, ett par jeans och \nEN blå högerstrumpa.", CanPickUp = true };
            Item köksbord = new Item() { Name = "KÖKSBORD", Description = "Det är ett runt köksbord, det var ett taktiskt val \ndå du alltid slog i hörnen på ditt förra.", CanPickUp = false };
            Item stolar = new Item() { Name = "STOLAR", Description = "Två stolar i trä som du gjorde på träslöjden i 9an, du är \nfaktiskt ganska stolt över dem.", CanPickUp = false };
            Item kylskåp = new Item() { Name = "KYLSKÅP", Description = "Kylskåpet är mer eller mindre tomt, allt den \ninnehåller är en gammal ketchup som gick ut \nför två månader sen.", CanPickUp = false };



            Room roomSovrum = new Room() { Name = "Sovrum", Description = "Det är ditt sovrum, den obäddade sängen står mot \nväggen och bredvid ligger en hög med smutstvätt. \nGenom fönstret sipprar ett svagt ljus in \noch lyser upp stolen i hörnet." };
            Item kläder = new Item() { Name = "KLÄDER", Description = "Det är en blå skjorta, ett par \njeans och EN blå högerstrumpa.", CanPickUp = true };
            Item stol = new Item() { Name = "STOL", Description = "En ranglig IKEAstol med KLÄDER ovanpå.", CanPickUp = false };
            Item säng = new Item() { Name = "SÄNG", Description = "En standardvit obäddad 90 säng som \ndu köpt billigt på blocket.", CanPickUp = false };
            Item fönster = new Item() { Name = "FÖNSTER", Description = "Regnet öser ned utanför fönstret.", CanPickUp = false };

            // Items vv

            // Rooms ^^



            // Exits vv
            Exit sovrumToHall = new Exit() { newRoom = roomHall, Direction = "SÖDER", isOpen = true };
            Exit hallToSovrum = new Exit() { newRoom = roomSovrum, Direction = "NORR", isOpen = true };

            Exit hallToVardagsRum = new Exit() { newRoom = roomVardagsrum, Direction = "VÄSTER", isOpen = true };
            Exit vardagsrumToHall = new Exit() { newRoom = roomHall, Direction = "ÖSTER", isOpen = true };

            //Exit vardagsrumToKök = new Exit() { Name = "DOOR", Description = "It's a common wooden door, it seems to be jammed", newRoom = roomKök, Direction = "SÖDER", isOpen = false, UsableWith = "KOFOT" };
            //Exit kökToVardagsrum = new Exit() { newRoom = roomVardagsrum, Direction = "NORR", isOpen = true };
            // Exits ^^



            //ADD ITEM TO ROOM :
            roomHall.RoomInventory.Add(kofot);
            roomKök.RoomInventory.Add(tändare);
            roomSovrum.RoomInventory.Add(kläder);
            roomSovrum.RoomInventory.Add(stol);
            roomSovrum.RoomInventory.Add(säng);
            roomSovrum.RoomInventory.Add(fönster);


            // ADD EXIT TO ROOM :
            roomSovrum.Exits.Add(sovrumToHall);
            roomHall.Exits.Add(hallToSovrum);
            roomHall.Exits.Add(hallToVardagsRum);
            roomVardagsrum.Exits.Add(vardagsrumToHall);
            //roomVardagsrum.Exits.Add(vardagsrumToKök);
            //roomKök.Exits.Add(kökToVardagsrum);

            // ADD A ROOM :
            rooms.Add(roomSovrum);
            rooms.Add(roomHall);
            rooms.Add(roomVardagsrum);
            rooms.Add(roomKök);

        }





    }
}