using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventureTime
{
    public class Game
    {
        public DateTime dt1 = DateTime.Now; // Spar ned tiden som referens för nedräkning
        public int counter = 600;
        public string countDown;
        WorldBuilder world = new WorldBuilder();
        Player player = new Player();

        public Game()
        {
            Console.WriteLine("Du vaknar upp i din säng, lika trött och förvirrad som vanligt \noch undrar vad fan du heter nu igen..");
            Console.Write("Skriv in ditt namn: ");
            player.Name = Console.ReadLine().ToUpper();
            Console.Clear();
            Console.WriteLine($"{player.Name} kollar på klockan och får hjärtat i halsgropen. ");
            
            player.CurrentRoom = world.rooms.Single(x => x.Name == "Sovrum"); // Hämtar det rum som heter "Sovrum" i listan, lägger det i current room. Objektet som överensstämmer returneras i "single".

            bool gameOver = false;
            bool gameWon = false;
            Timer t = new Timer(TimerCallback, null, 0, 1000); //skapar tråd och initierar nedräkning
            dt1 = DateTime.Now + new TimeSpan(0, 0, 10, 0);
            Thread.Sleep(50); // För att minska risken att WriteLinen under denna sker innan Timer-Printen
            Console.WriteLine("Du flyger upp ur sängen, golvet är kallt.");
            Console.WriteLine(countDown);

            while (!gameOver && !gameWon)
            {
                if (counter <= 0)
                {
                    Console.WriteLine("Tiden tog slut! Spelet avslutas om 5 sekunder!");
                    gameOver = true;
                    Thread.Sleep(5000);
                    break;
                }
                if (player.CurrentRoom.YouWon)
                {
                    t.Dispose(); // När spelaren vinner ska nedräkning termineras
                    Console.Clear();
                    Console.WriteLine("Du vann!!");
                    Console.ReadLine();
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
            counter--;
        }

        public void Command()
        {
            string cmd = Console.ReadLine().ToUpper();
            string[] useWithSplitter = cmd.Split(' '); // Delar upp cmd för varje mellanslag i array
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
            else if (cmd.Length >= 6 && cmd.Substring(0, 6) == "ÖPPNA ")
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
                Console.WriteLine(countDown); //varje gång användaren skriver kolla ska nedräkning synas.
            }
            else if (cmd == "HJÄLP")
            {
                Console.WriteLine("\nLista kommandon: \n* Norr   (n) : Du rör dig norrut\n* Söder  (s) : Du rör dig söderut\n* Öster  (ö) : Du rör dig österut\n* Väster (v) : Du rör dig västerut.");
                Console.WriteLine("* Väska  (i) : Visar föremål som spelaren bär på.");
                Console.WriteLine("* Ta Upp     : Tar upp angiviet föremål.");
                Console.WriteLine("* Kasta      : Släpper angivet föremål ur spelarens Väska.");
                Console.WriteLine("* Använd Med : Använder angivet föremål 1 Med angivet föremål 2.");
                Console.WriteLine("* Kolla      : Ger spelaren en rumsbeskrivning.");
                Console.WriteLine("* Kolla På   : Ger spelaren en beskrivning av angivet föremål.");
                Console.WriteLine("* Öppna      : Öppnar angivet föremål.");
                Console.WriteLine("* Stäng      : Stäng föremål.");
                Console.WriteLine("");
            }
            else if (useWithSplitter[0] == "ANVÄND" && useWithSplitter[2] == "MED")
            {
                Item item1 = player.PlayerInventory.SingleOrDefault(x => x.Name == useWithSplitter[1]); //hos playerns inventory, hämta ett item, om det finns, vars namn överensstämmer med arrayen-position 1 eller 3 
                Item item2 = player.PlayerInventory.SingleOrDefault(x => x.Name == useWithSplitter[3]); //om ett sådant föremål inte finns, sätt item 1 eller 2 till null

                if (item1 == null)
                {
                    Console.WriteLine("Du saknar föremål för att utföra det här.");
                }
                else
                {
                    if (useWithSplitter[1] == item1.Name)
                    {
                        foreach (var exit in player.CurrentRoom.Exits)
                        {
                            if (useWithSplitter[3] == exit.Name)
                            {
                                exit.usedWith(item1.Name);
                                return;
                            }
                        }
                        if (item2 == null)
                        {
                            Console.WriteLine("Du saknar föremål för att utföra det här.");
                        }
                        else if (useWithSplitter[3] == item2.Name)
                        {
                            /*Skulle man bara byta ut namn på item.name = till torr strumpa kan man i inventory råka få strykjärn utbytt mot torr stumpa
                             * istället för blöt strumpa utbytt mot torr strumpa*/

                            Item newCombinedItem = item2.usedWith(item1);
                            if (newCombinedItem != null)
                            {
                                player.PlayerInventory.Remove(item1);
                                player.PlayerInventory.Remove(item2);
                                player.PlayerInventory.Add(newCombinedItem);
                            }
                            return;
                        }
                    }
                    Console.WriteLine("Tänkte du rätt nu?!");
                }
            }
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
                        Console.WriteLine(player.Name + " står nu i " + player.CurrentRoom.Name + ".");
                        return;
                    }
                    else if (exit.isOpen == false)
                    {
                        Console.WriteLine(exit.Description);
                        return;
                    }
                }
            }
            Console.WriteLine("Du kan ej gå i den riktningen.");
        }

        public void Open(string cmd)
        {
            bool errorMessage = false;
            foreach (var exit in player.CurrentRoom.Exits)
            {
                if (cmd.Substring(6) == exit.Name)
                {
                    if (exit.isOpen == true && exit.isContainer == true)
                    {
                        player.CurrentRoom = exit.newRoom;

                        Console.Clear();
                        Console.WriteLine(player.Name + " har öppnat " + player.CurrentRoom.Name);
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
                    else if (exit.isOpen == true && exit.isContainer == false)
                    {
                        Console.WriteLine("Dörren är öppen.");
                        errorMessage = true;
                        return;
                    }
                    else if (exit.isOpen == false && exit.isContainer == true)
                    {
                        Console.WriteLine("Dörren är låst.");
                        errorMessage = true;
                        return;
                    }
                }
            }
            if (!errorMessage)
            {
                Console.WriteLine("Är du säker på att du angav rätt föremål att öppna."); 
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
                        Console.WriteLine("\nDu kan inte plocka upp den där.");
                        return;
                    }
                }
            }
            Console.WriteLine("\nDet finns inget där att plocka upp.");
        }

        public void Drop(string cmd)
        {
            foreach (var item in player.PlayerInventory)
            {
                if (cmd.Substring(6) == item.Name.ToUpper())
                {
                    Console.WriteLine(player.Name + " kastade ut " + item.Name + ".");

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
            Console.WriteLine("Det finns inget föremål med beskrivningen" + cmd.Substring(8) + ".");
        }
    }
}