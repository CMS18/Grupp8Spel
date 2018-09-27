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
            Console.WriteLine("Welcome to adventure time");
            Console.Write("Please enter your name : ");
            player.Name = Console.ReadLine();
            player.CurrentRoom = createWorld.rooms[0];
            //Console.WriteLine(player.CurrentRoom.Exits[0]);
            bool gameOver = false;
            Timer t = new Timer(TimerCallback, null, 0, 1000);
            dt1 = DateTime.Now + new TimeSpan(0, 0, 10, 0);

            player.CurrentRoom.currentDescription();
            Console.WriteLine(countDown);//flytta in i första rummets description, bug på om man är snabbare än 1 sekund.
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

        public void TimerCallback(Object o)
        {
            //Console.WriteLine("In TimerCallBack: " + counter);
            DateTime dt2 = DateTime.Now;
            //Console.WriteLine(dt1.Minute + ":" + dt1.Second);
            //Console.WriteLine(dt1.Second);
            //var dt3 = new TimeSpan();

            //dt3 = dt1.Subtract(dt2);
            //Console.WriteLine(dt3);
            TimeSpan t = dt1 - dt2;
            countDown = string.Format("{0}:{1} tills bussen går!", t.Minutes, t.Seconds);
            //Console.WriteLine(dt2.TimeOfDay.Minutes + ":" + dt2.TimeOfDay.Seconds);
            //Console.WriteLine("du har: " + (dt2.TimeOfDay.Minutes - (dt1.TimeOfDay.Minutes-10))+" minuter kvar" + (dt1.TimeOfDay.Seconds - dt2.TimeOfDay.Seconds));
            //Console.WriteLine(dt2.TimeOfDay);
            //Console.WriteLine(dt2.TimeOfDay);


            counter--;
            //GC.Collect(); //vettefan vad detta är men failar timern kanske den löser det :D
        }

        public void searchDirection(string cmd)
        {
            foreach (var item in player.CurrentRoom.Exits)
            {

                if (item.Direction == cmd)
                {
                    if (item.isOpen == true)
                    {
                        Console.Clear();
                        player.CurrentRoom = item.newRoom;
                        Console.WriteLine(player.Name + " entered " + player.CurrentRoom.Name);
                        return;
                    }
                    else if (item.isOpen == false)
                    {
                        Console.WriteLine(item.Description);
                        return;
                    }
                }
            }

            Console.WriteLine("You can't go there");
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
                        Console.WriteLine("Du kan inte plocka upp den där");
                    }
                }
            }

        }
        public void drop(string cmd)
        {
            foreach (var item in player.PlayerInventory)
            {

                if (cmd.Substring(5) == item.Name.ToUpper())
                {

                    Console.WriteLine(player.Name + " dropped " + item.Name);

                    player.CurrentRoom.RoomInventory.Add(item);
                    player.PlayerInventory.Remove(item);
                    return;


                }
            }

        }
        public void LookAt(string cmd)
        {

            foreach (var item in player.CurrentRoom.RoomInventory)
            {

                if (cmd.Substring(9) == item.Name.ToUpper())
                {
                    Console.WriteLine(item.Description);
                    return;
                }
            }
            foreach (var item in player.PlayerInventory)
            {

                if (cmd.Substring(9) == item.Name.ToUpper())
                {
                    Console.WriteLine(item.Description);
                    return;
                }
            }
            Console.WriteLine("There is no item by the description of " + cmd.Substring(8));

        }

        public void Command()
        {

            string cmd = Console.ReadLine().ToUpper();
            string[] useWithSplitter = cmd.Split(' ');
            if (cmd == "NORR" || cmd == "N")
            {
                searchDirection("NORR");
            }
            else if (cmd == "SÖDER" || cmd == "S")
            {
                searchDirection("SÖDER");
            }
            else if (cmd == "ÖSTER" || cmd == "Ö")
            {
                searchDirection("ÖSTER");
            }
            else if (cmd == "VÄSTER" || cmd == "V")
            {
                searchDirection("VÄSTER");
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
                drop(cmd);
            }
            else if (cmd == "KOLLA")
            {
                Console.Clear();
                player.CurrentRoom.currentDescription();
                Console.WriteLine(countDown);
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
                Console.WriteLine("Did you just make up an item");
            }
        }
    }
}