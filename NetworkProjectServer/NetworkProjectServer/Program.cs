using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace NetworkProjectServer
{
    class Program
    {
        private static Command cmd = new Command();
        private static Queue<Player> playerQueue = new Queue<Player>();
        private static List<Player> playerReady = new List<Player>();
        private static List<Player> allPlayers = new List<Player>();
        private static List<Enemy> enemies = new List<Enemy>();
        private static List<Player> objsToRemove = new List<Player>();
        private static int _port = 5557;
        private static TcpListener _server;
        private static bool _isrunning;
        private static Object laas = new Object();

        internal static List<Enemy> Enemies
        {
            get
            {
                return enemies;
            }

            set
            {
                enemies = value;
            }
        }

        public static List<Player> PlayerReady
        {
            get
            {
                return playerReady;
            }

            set
            {
                playerReady = value;
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "Server";
            Console.WriteLine("Waiting for players ...");
            Tcpserver(_port);
        }

        static void Tcpserver(int port)
        {
            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();
            _isrunning = true;
            LoopClients();
        }

        public static void LoopClients()
        {
            while (_isrunning)
            {
                TcpClient newClient = _server.AcceptTcpClient();
                IPEndPoint endPoint = (IPEndPoint)newClient.Client.RemoteEndPoint;
                Console.WriteLine("New player joind on IP: " + endPoint.ToString());

                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newClient);
            }
        }

        public static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream ns = client.GetStream();


            StreamReader sReader = new StreamReader(ns, Encoding.ASCII);
            StreamWriter sWriter = new StreamWriter(ns, Encoding.ASCII);
            RestHandler handler = new RestHandler();


            String sData = null;

            IPEndPoint endPoint = (IPEndPoint)client.Client.RemoteEndPoint;

            Player p = new Player(endPoint);
            //  reads input about username 
            string brugernavn = sReader.ReadLine();
          Person person=handler.GetUser(0, brugernavn);

            //end
            playerQueue.Enqueue(p);
            allPlayers.Add(p);

            IPEndPoint localendPoint = (IPEndPoint)client.Client.LocalEndPoint;


            while (client.Connected)
            {
                try
                {
                    lock (laas)
                    {
                        sWriter.WriteLine("It is your turn. Write your command");
                        p = playerQueue.Dequeue();
                        Console.WriteLine("Start of game");
                        Console.WriteLine("There are " + playerQueue.Count.ToString() + " in the queue");
                        playerReady.Add(p);
                        allPlayers.Remove(p);
                        Console.WriteLine("There are: " + playerReady.Count.ToString() + " players in the game");



                        sData = sReader.ReadLine();
                        if (playerReady[0].health <= 0 && sData != null)
                        {
                            sWriter.WriteLine("You died, reconnect to start over.");

                            /////////////////////
                            ////husk at tænke over at looope clienter så de ikke behøver at genstarte   exe 
                            /// LoopClients();
                            //////////////////////

                            sWriter.WriteLine("Your highscore will be saved.");
                            KickDeadPlayer(p);
                        }
                        string playerCmd = sData;
                        switch (playerCmd.ToLower())
                        {
                            case "attack":
                                if (enemies.Count == 1)
                                {
                                    sWriter.WriteLine("You attacked " + enemies[0].name);
                                    cmd.Combat(playerReady[0], enemies[0]);
                                    sWriter.WriteLine("Your health " + playerReady[0].health.ToString());
                                    sWriter.WriteLine("Enemy health " + enemies[0].health.ToString());
                                    if (enemies[0].health <= 0 && enemies[0] != null)
                                    {
                                        sWriter.WriteLine("The " + enemies[0].name + " died");
                                        enemies.Clear();
                                    }
                                }

                                else if (enemies.Count == 0)
                                {
                                    sWriter.WriteLine("There is no enemy to attack, use Move to find an enemy");
                                }
                                break;
                            case "move":
                                if (enemies.Count == 0)
                                {
                                    sWriter.WriteLine("you meet " + cmd.MeetEnemy());
                                }
                                else
                                {
                                    sWriter.WriteLine("You are already in combat");
                                }
                                break;
                            case "drink":
                                sWriter.WriteLine("You drank a potion");
                                cmd.DrinkPotion(playerReady[0]);
                                sWriter.WriteLine("You now have: " + playerReady[0].health.ToString() + " health");
                                sWriter.WriteLine("You have " + playerReady[0].potions.ToString() + " potions left");
                                break;
                            default:
                                sWriter.WriteLine("Your command is invalid");
                                break;
                        }
                    }
                    playerQueue.Enqueue(p);
                    Console.WriteLine("There are " + playerQueue.Count.ToString() + " in the queue");
                    PlayerReady.Clear();
                    Console.WriteLine("There are: " + playerReady.Count.ToString() + " players in the game");
                    Console.WriteLine("End of game");
                    allPlayers.Add(p);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    foreach (Player item in playerReady)
                    {
                        string removePlayer = p.PlayerEP.Port.ToString();

                        if (item.PlayerEP.Port.ToString() == removePlayer)
                        {
                            objsToRemove.Add(item);
                        }
                    }
                    playerReady.Clear();
                    objsToRemove.Clear();

                    Console.WriteLine("Client on port " + p.PlayerEP.Port.ToString() + " left the game,\nthere are: " + playerQueue.Count.ToString() + " players");
                    Console.WriteLine(e);
                    sWriter.WriteLine("Something ruined the server");
                    Thread.CurrentThread.Abort();
                }



                try
                {
                    sWriter.Flush();
                }
                catch (Exception e)
                {

                }

                Console.WriteLine("The server contains " + playerQueue.Count.ToString() + " players.");
                Console.WriteLine("Server on " + localendPoint.Address.ToString() + " port: " + localendPoint.Port.ToString());
                // Her kunne man skrive noget tilbage til klienten
                // Det gør vi da bare senere - ha!
                //sWriter.WriteLine(sData.ToUpper());
                //sWriter.Flush();


            }
        }
        public static void KickDeadPlayer(Player p)
        {
            foreach (Player item in playerReady)
            {
                string removePlayer = p.PlayerEP.Port.ToString();

                if (item.PlayerEP.Port.ToString() == removePlayer)
                {
                    objsToRemove.Add(item);
                }
            }
            playerReady.Clear();
            objsToRemove.Clear();
        }
        public static void writeOtherPlayer(TcpClient client)
        {
            for (int i = 0; i < allPlayers.Count; i++)
            {
                if (allPlayers[i].PlayerEP == playerReady[0].PlayerEP)
                {
                    //byte derp =Convert.ToByte( response);
                    //byte[] responseData = responseData = Encoding.ASCII.GetBytes(data, 0, bytes); 
                    // client.Client.Send(Encoding.ASCII.GetBytes("Hello to you, remote host."+client.Client.RemoteEndPoint.ToString()));
                    client.Client.SendTo(Encoding.ASCII.GetBytes("Hello to you, remote host." + client.Client.RemoteEndPoint.ToString()), allPlayers[i].PlayerEP);
                }

            }
        }


        /// <summary>
        /// En test som ikke virkede, hvor man skulle kunne skrive til de specifikke spillere
        /// </summary>
        /// <param name="p">       de spillere som ikke er i låsen</param>
        /// <param name="client">  den instandsierede TcPClient</param>
        /// <param name="message"> den string besked som man vil sende til de andre spillere</param>
        public static void WriteToPlayer(Player p, TcpClient client, string message)
        {
            for (int i = 0; i < allPlayers.Count; i++)
            {
                client.Client.SendTo(Encoding.ASCII.GetBytes(message + client.Client.RemoteEndPoint.ToString()), allPlayers[i].PlayerEP);
            }
        }
    }
}
