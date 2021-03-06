﻿using System;
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
            BuildWorld(); //kör all kod nedan
        }

        public void BuildWorld()
        {
            Room roomSovrum = new Room() { Name = "Sovrum", Description = "Det är ditt sovrum, den obäddade sängen står mot \nväggen, bredvid sängen ligger en hög med smutstvätt. \nGenom fönstret sipprar ett svagt ljus in \noch lyser upp stolen med kläder i hörnet." };
            Item kläder = new Item() { Name = "KLÄDER", Description = "Det är en blå skjorta, ett par jeans och EN blå strumpa, \nden andra strumpan saknas..", CanPickUp = true };
            Item stol = new Item() { Name = "STOL", Description = "En ranglig IKEAstol med KLÄDER ovanpå.", CanPickUp = false };
            Item säng = new Item() { Name = "SÄNG", Description = "En standardvit obäddad 90 säng som \ndu köpt billigt på blocket.", CanPickUp = false };
            Item fönster = new Item() { Name = "FÖNSTER", Description = "Regnet öser ned utanför fönstret.", CanPickUp = false };


            Room roomHall = new Room() { Name = "Hall", Description = "En sliten byrå står i ena hörnet och krokar för din \nhemnyckel och för ditt paraply sitter på väggen. \nUnder dörren ligger en matta där ett par skor står." };
            Item paraply = new Item() { Name = "PARAPLY", Description = "Ett typiskt svart paraply, med en företagslogga på", CanPickUp = true };
            Item skor = new Item() { Name = "SKOR", Description = "Vita sneakers.", CanPickUp = true };
            Item matta = new Item() { Name = "MATTA", Description = "Det är en \"Welcome Home\"-matta med röd smutsig text.", CanPickUp = false };
            Item byrå = new Item() { Name = "BYRÅ", Description = "Det är en byrå som du fått av din gamla moster, du \ngillar den inte men hon skulle bli ledsen om \ndu gjorde dig av med den.", CanPickUp = false };
            Item krokar = new Item() { Name = "KROKAR", Description = "Det är två rostiga krokar, på den ena hänger ett \nparaply och den andra, där nyckeln brukar hänga, \när tom.", CanPickUp = false };
            

            Room roomKök = new Room() { Name = "Kök", Description = "I ena delen av köket står ett köksbord med två stolar, andra sidan finns ett kylskåp och en frys." };
            Item köksbord = new Item() { Name = "KÖKSBORD", Description = "Det är ett runt köksbord, det var ett taktiskt val \ndå du alltid slog i hörnen på ditt förra.", CanPickUp = false };
            Item stolar = new Item() { Name = "STOLAR", Description = "Två stolar i trä som du gjorde på träslöjden i 9an, du är \nfaktiskt ganska stolt över dem.", CanPickUp = false };
            Item kylskåp = new Item() { Name = "KYLSKÅP", Description = "Kylskåpet är mer eller mindre tomt, allt den \ninnehåller är en gammal ketchup som gick ut \nför två månader sen.", CanPickUp = false };
            

            Room containerFrys = new Room() { Name = "FRYS", Description = "Inuti frysen ligger din hemnyckel och en gammal förpackning med glass." };
            Item nyckel = new Item() { Name = "NYCKEL", Description = "En kantig nyckel i metall.", CanPickUp = true, UsableWith = "YTTERDÖRR" };


            Room roomTvättstuga = new Room() { Name = "TVÄTTSTUGA", Description = "Här finns en tvättkorg och en tvättmaskin, på tvättmaskinen står ett strykjärn. " };
            Item Strykjärn = new Item() { Name = "STRYKJÄRN", Description = "Det är ett lila strykjärn.", CanPickUp = true, UsableWith = "BLÖT_STRUMPA", UpdateName = "TORR_STRUMPA", UpdateDescription = "En fluffig torr strumpa." };
            Item Tvättmaskin = new Item() { Name = "TVÄTTMASKIN", Description = "Det är en kantstött tvättmaskin som föregående ägare lämnade kvar.", CanPickUp = false };
            Item Tvättkorg = new Item() { Name = "TVÄTTKORG", Description = "Tvättkorgen är sliten och håller på att spricka i sömmarna.", CanPickUp = false };

            
            Room roomVardagsrum = new Room() { Name = "Vardagsrum", Description = "En soffa och ett litet soffbord med en tidning ovanpå står mot ena väggen, \nmittemot står en stor tv och i hörnet ligger en blöt strumpa." };
            Item Blöt_Strumpa = new Item() { Name = "BLÖT_STRUMPA", Description = "Det är en dyngsur blå strumpa.", CanPickUp = true, UsableWith = "STRYKJÄRN", UpdateName = "TORR_STRUMPA", UpdateDescription = "En fluffig torr strumpa." };
            Item Soffa = new Item() { Name = "SOFFA", Description = "Det är en gul nedsutten soffa.", CanPickUp = false };
            Item Bord = new Item() { Name = "BORD", Description = "Det är ett välanvänt bord med skavda kanter.", CanPickUp = false };
            Item Tidning = new Item() { Name = "TIDNING", Description = "Det är en slarvigt ihopviken tidning med dagens nyheter.", CanPickUp = false };
            Item TV = new Item() { Name = "TV", Description = "TVn är förvånansvärt modern med svart tunn ram.", CanPickUp = false };


            Room roomYtterdörr = new Room() { Name = "DU VANN!!!!", YouWon = true };


            // Exits vv
            Exit sovrumToHall = new Exit() { Name = "HALLDÖRR", newRoom = roomHall, Direction = "SÖDER", isOpen = true, isContainer = false };
            Exit hallToSovrum = new Exit() { Name = "SOVRUMSDÖRR", newRoom = roomSovrum, Direction = "NORR", isOpen = true, isContainer = false };

            Exit hallToVardagsRum = new Exit() { Name = "VARDAGSRUMSDÖRR", newRoom = roomVardagsrum, Direction = "VÄSTER", isOpen = true, isContainer = false };
            Exit vardagsrumToHall = new Exit() { Name = "HALLDÖRR", newRoom = roomHall, Direction = "ÖSTER", isOpen = true, isContainer = false };

            Exit kökToHall = new Exit() { Name = "HALLDÖRR", newRoom = roomHall, Direction = "VÄSTER", isOpen = true, isContainer = false };
            Exit hallToKök = new Exit() { Name = "KÖKSDÖRR", newRoom = roomKök, Direction = "ÖSTER", isOpen = true, isContainer = false };

            Exit kökToFrys = new Exit() { Name = "FRYSDÖRR", Description = "Dörren verkar vara igenfrusen", newRoom = containerFrys, Direction = "ÖPPNA", isOpen = false, isContainer = true, UsableWith = "PARAPLY" };
            Exit frysToKök = new Exit() { Name = "KÖKSDÖRR", newRoom = roomKök, Direction = "STÄNG", isOpen = true, isContainer = false };

            Exit kökToTvättstuga = new Exit() { Name = "TVÄTTSTUGEDÖRR", newRoom = roomTvättstuga, Direction = "NORR", isOpen = true, isContainer = false };
            Exit tvättstugaToKök = new Exit() { Name = "KÖKSDÖRR", newRoom = roomKök, Direction = "SÖDER", isOpen = true, isContainer = false };

            Exit hallToYtterdörr = new Exit() { Name = "YTTERDÖRR", newRoom = roomYtterdörr, Direction = "SÖDER", isOpen = false, isContainer = false, UsableWith = "NYCKEL" };
            

            //ADD ITEM TO ROOM :
            roomSovrum.RoomInventory.Add(kläder);
            roomSovrum.RoomInventory.Add(stol);
            roomSovrum.RoomInventory.Add(säng);
            roomSovrum.RoomInventory.Add(fönster);

            roomHall.RoomInventory.Add(paraply);
            roomHall.RoomInventory.Add(skor);
            roomHall.RoomInventory.Add(matta);
            roomHall.RoomInventory.Add(byrå);
            roomHall.RoomInventory.Add(krokar);

            roomKök.RoomInventory.Add(köksbord);
            roomKök.RoomInventory.Add(stolar);
            roomKök.RoomInventory.Add(kylskåp);

            roomTvättstuga.RoomInventory.Add(Strykjärn);
            roomTvättstuga.RoomInventory.Add(Tvättmaskin);
            roomTvättstuga.RoomInventory.Add(Tvättkorg);

            containerFrys.RoomInventory.Add(nyckel);

            roomVardagsrum.RoomInventory.Add(Blöt_Strumpa);
            roomVardagsrum.RoomInventory.Add(Soffa);
            roomVardagsrum.RoomInventory.Add(Bord);
            roomVardagsrum.RoomInventory.Add(Tidning);
            roomVardagsrum.RoomInventory.Add(TV);
            

            // ADD EXIT TO ROOM :
            roomSovrum.Exits.Add(sovrumToHall);         //ok
            roomHall.Exits.Add(hallToSovrum);           //ok
            roomHall.Exits.Add(hallToVardagsRum);       //ok
            roomVardagsrum.Exits.Add(vardagsrumToHall); //ok
            roomKök.Exits.Add(kökToHall);               //ok
            roomHall.Exits.Add(hallToKök);              //ok
            roomKök.Exits.Add(kökToFrys);               //ok
            containerFrys.Exits.Add(frysToKök);         //ok
            roomKök.Exits.Add(kökToTvättstuga);         //ok
            roomTvättstuga.Exits.Add(tvättstugaToKök);  //ok
            roomHall.Exits.Add(hallToYtterdörr);        //ok
            

            // ADD A ROOM :
            rooms.Add(roomSovrum);
            rooms.Add(roomHall);
            rooms.Add(roomVardagsrum);
            rooms.Add(roomKök);
            rooms.Add(roomTvättstuga);
            rooms.Add(containerFrys);
            rooms.Add(roomYtterdörr);
        }
    }
}