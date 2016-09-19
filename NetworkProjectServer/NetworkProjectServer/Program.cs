﻿using System;
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
        private static List<Player> playerList = new List<Player>();
        private static List<Enemy> enemies = new List<Enemy>();
        private static List<Player> objsToRemove = new List<Player>();
        private static int _port = 5557;
        private static TcpListener _server;
        private static bool _isrunning;
        private static Object laas = new Object();


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

            String sData = null;

            IPEndPoint endPoint = (IPEndPoint)client.Client.RemoteEndPoint;
            Player p = new Player(endPoint);
            playerList.Add(p);



            IPEndPoint localendPoint = (IPEndPoint)client.Client.LocalEndPoint;
            Random rnd = new Random();

            int hemmeligtTal = rnd.Next(1, 99);


            while (client.Connected)
            {
                //sWriter.WriteLine("Det er ikke din tur");

                try
                {
                    lock (laas)
                    {
                        //sWriter.WriteLine("Det er din tur");

                        sData = sReader.ReadLine();
                    }
                }
                catch (Exception e)
                {


                    Console.WriteLine("Client on port " + p.PlayerEP.Port.ToString() + " left the game,\n there are " + playerList.Count.ToString() + ":(");

                    foreach (Player item in playerList)
                    {
                        string removePlayer = p.PlayerEP.Port.ToString();

                        if (item.PlayerEP.Port.ToString() == removePlayer)
                        {
                            objsToRemove.Add(item);
                        }
                    }
                    foreach (Player pl in objsToRemove)
                    {
                        playerList.Remove(pl);
                    }
                    objsToRemove.Clear();

                    Console.WriteLine(playerList.Count.ToString());
                    Thread.CurrentThread.Abort();
                }


               
                try
                {
                    sWriter.Flush();
                }
                catch (Exception e)
                { }

                Console.WriteLine("The server contains " + playerList.Count.ToString() + " players.");
                Console.WriteLine("Server on " + localendPoint.Address.ToString() + " port: " + localendPoint.Port.ToString());
                Console.WriteLine("Client on port " + p.PlayerEP.Port.ToString() + "> " + hemmeligtTal.ToString());
                // Her kunne man skrive noget tilbage til klienten
                // Det gør vi da bare senere - ha!
                //sWriter.WriteLine(sData.ToUpper());
                //sWriter.Flush();


            }
        }
    }
}
