using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventureTime
{
    public class Game
    {
        public DateTime dt1 = DateTime.Now;
        public int counter = 600;
        public string countDown;
        WorldBuilder createWorld = new WorldBuilder();
        Player player = new Player();

        public Game()
        {
            //Console.WriteLine("Welcome to adventure time");
            Console.WriteLine("Du vaknar upp i din säng, lika trött och förvirrad som vanligt \noch undrar vad fan du heter nu igen..");
            Console.Write("Skriv in ditt namn: ");
            player.Name = Console.ReadLine().ToUpper();
            Console.Clear();
            Console.WriteLine($"{player.Name} kollar på klockan och får hjärat i halsgropen, ");
            //player.CurrentRoom = createWorld.rooms[0];
            player.CurrentRoom = createWorld.rooms.Single(x => x.Name == "Sovrum"); // Hämtar det rum som heter "Sovrum", lägger det i current room.

            bool gameOver = false;
            Timer t = new Timer(TimerCallback, null, 0, 1000);
            dt1 = DateTime.Now + new TimeSpan(0, 0, 10, 0);
            Thread.Sleep(50); //För att minska risken att WriteLinen under denna sker innan Timer-Printen
            Console.WriteLine("Du flyger upp ur sängen, golvet är kallt.");
            //Console.WriteLine(countDown);//flytta in i första rummets description, bug på om man är snabbare än 1 sekund.

            while (!gameOver)
            {
                //Console.WriteLine("hello");
                if (counter <= 0)
                {
                    Console.WriteLine("Tiden tog slut! Spelet avslutas om 5 sekunder!");
                    gameOver = true;
                    Thread.Sleep(5000);
                    break;
                }
                Command();
            }
        }

        public void TimerCallback(Object o)//Kör i en separat tråd asynkront, det vill säga parallellt
        {
            DateTime dt2 = DateTime.Now;
            TimeSpan t = dt1 - dt2;
            countDown = string.Format("{0}:{1} tills bussen går!", t.Minutes, t.Seconds);
            if (t.Seconds != 0 && t.Seconds % 59 == 0)
            {
                Console.WriteLine(countDown);
            }
            counter--;
        }

        public void SearchDirection(string cmd)
        {
            foreach (var exit in player.CurrentRoom.Exits)
            {

                if (exit.Direction == cmd)
                {
                    if (exit.isOpen == true && exit.isContainer == false)
                    {
                        Console.Clear();
                        player.CurrentRoom = exit.newRoom;
                        Console.WriteLine(player.Name + " står nu i " + player.CurrentRoom.Name);
                        return;
                    }
                    else if (exit.isOpen == false)
                    {
                        Console.WriteLine(exit.Description);
                        return;
                    }
                }
            }


            Console.WriteLine("Du kan ej gå i den riktningen");
        }

        public void Open(string cmd)
        {
            
            foreach (var exit in player.CurrentRoom.Exits)
            {
                
               
                    
                   if (cmd.Substring(6) == exit.Name)
                    {
                        
                        if (exit.isOpen == true && exit.isContainer == true)
                        {
                            player.CurrentRoom = exit.newRoom;

                           
                            Console.Clear();
                            Console.WriteLine(player.Name + " Har öppnat " + player.CurrentRoom.Name);
                            Console.WriteLine(player.CurrentRoom.Name + " innehåll:");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("__________________\n");
                            Console.ResetColor();
                            foreach (var item2 in player.CurrentRoom.RoomInventory)
                            {
                                Console.WriteLine(item2.Name);
                            }
                            
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("__________________");
                            Console.ResetColor();

                            return;
                        }
                        else if ( exit.isOpen == true && exit.isContainer == false)
                        {
                            Console.WriteLine("Dörren är öppen");
                            return;
                        }
                        else if (exit.isOpen == false && exit.isContainer == true)
                        {
                            Console.WriteLine("Dörren är låst");
                            return;
                        }

                    }
                    else if (cmd.Substring(6) != exit.Name)
                    { Console.WriteLine("Är du säker på att du angav rätt föremål att öppna"); }
                
            }
        }
        public void PickUp(string cmd)
        {
            foreach (var item in player.CurrentRoom.RoomInventory)
            {

                if (cmd.Substring(7) == item.Name.ToUpper())
                {
                    if (item.CanPickUp == true)
                    {
                        Console.WriteLine(player.Name + " plockade upp " + item.Name);
                        player.PlayerInventory.Add(item);
                        player.CurrentRoom.RoomInventory.Remove(item);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("\nDu kan inte plocka upp den där");
                        return;
                    }
                }
            }
            Console.WriteLine("\nDet finns inget där att plocka upp");

        }
        public void Drop(string cmd)
        {
            foreach (var item in player.PlayerInventory)
            {

                if (cmd.Substring(6) == item.Name.ToUpper())
                {

                    Console.WriteLine(player.Name + " kastade ut " + item.Name);

                    player.CurrentRoom.RoomInventory.Add(item);
                    player.PlayerInventory.Remove(item);
                    return;


                }
            }

        }
        public void LookAt(string cmd)
        {
            Console.WriteLine();
            foreach (var item in player.CurrentRoom.RoomInventory)
            {

                if (cmd.Substring(9) == item.Name.ToUpper())
                {
                    Console.WriteLine(item.Description + "\n");
                    return;
                }
            }
            foreach (var item in player.PlayerInventory)
            {

                if (cmd.Substring(9) == item.Name.ToUpper())
                {
                    Console.WriteLine(item.Description + "\n");
                    return;
                }
            }
            Console.WriteLine("Det finns inget föremål med beskrivningen" + cmd.Substring(8));

        }

        public void Command()
        {

            string cmd = Console.ReadLine().ToUpper();
            string[] useWithSplitter = cmd.Split(' ');
            if (cmd == "NORR" || cmd == "N")
            {
                SearchDirection("NORR");
            }
            else if (cmd == "SÖDER" || cmd == "S")
            {
                SearchDirection("SÖDER");
            }
            else if (cmd == "ÖSTER" || cmd == "Ö")
            {
                SearchDirection("ÖSTER");
            }
            else if (cmd == "VÄSTER" || cmd == "V")
            {
                SearchDirection("VÄSTER");
            }
            else if (cmd.Length >= 7 && cmd.Substring(0, 7) == "TA UPP ") // Subsstring kollar första 7 charsen i strängen
            {
                PickUp(cmd);
            }
            else if (cmd == "VÄSKA" || cmd == "I")
            {
                player.ShowInventory();
            }
            else if (cmd.Length >= 9 && cmd.Substring(0, 9) == "KOLLA PÅ ")
            {
                LookAt(cmd);
            }
            else if (cmd.Length >= 6 && cmd.Substring(0, 6) == "KASTA ")
            {
                Drop(cmd);
            }
            else if (cmd.Length>= 6 && cmd.Substring(0,6) == "ÖPPNA ")
            {
                Open(cmd);
            }
            else if (cmd == "STÄNG")
            {
                SearchDirection(cmd);
            }
            else if (cmd == "KOLLA")
            {
                Console.Clear();
                player.CurrentRoom.currentDescription();
                Console.WriteLine(countDown);
            }
            else if (cmd == "HJÄLP")
            {
                Console.WriteLine("\nLista kommandon: \n* Norr   (n) : Du rör dig norrut\n* Söder  (s) : Du rör dig söderut\n* Öster  (ö) : Du rör dig österut\n* Väster (v) : Du rör dig västerut");
                Console.WriteLine("* Väska  (i) : Visar föremål som spelaren bär på");
                Console.WriteLine("* Ta Upp     : Tar upp angiviet föremål");
                Console.WriteLine("* Kasta      : Släpper angivet föremål ur spelarens Väska");
                Console.WriteLine("* Använd Med : Använder angivet föremål 1 Med angivet föremål 2");
                Console.WriteLine("* Kolla      : Ger spelaren en rumsbeskrivning");
                Console.WriteLine("* Kolla På   : Ger spelaren en beskrivning av angivet föremål");
                Console.WriteLine("");

            }


            else if (useWithSplitter[0] == "ANVÄND" && useWithSplitter[2] == "MED")
            {
                //Console.WriteLine(useWithSplitter[1]);
                //Console.WriteLine(useWithSplitter[3]);
                foreach (var item in player.PlayerInventory)
                {
                    //Console.WriteLine(item.Name);
                    if (useWithSplitter[1] == item.Name)
                    {
                        foreach (var exit in player.CurrentRoom.Exits)
                        {
                            //Console.WriteLine(exit);
                            if (useWithSplitter[3] == exit.Name)
                            {
                                exit.usedWith(useWithSplitter[1]);
                                return;
                            }

                        }

                        foreach (var item2 in player.PlayerInventory)
                        {
                            if (useWithSplitter[3] == item2.Name)
                            {
                                item2.usedWith(useWithSplitter[1]);
                                return;
                            }

                        }
                    }
                }
                Console.WriteLine("Tänkte du rätt nu?!");
            }
        }
    }
}