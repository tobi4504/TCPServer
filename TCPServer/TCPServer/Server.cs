using CLass_Libary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace TCPServer
{
    public class Server
    {
        private const int PORT = 2121;
        static List<FootBallPlayer> listOfPlayers = new List<FootBallPlayer>();
        private bool done = false;

        public Server()
        {

        }
        public void Start()
        {
            TcpListener listner = new TcpListener(IPAddress.Any,PORT);
            listner.Start();

            while(true)
            {
                TcpClient socket = listner.AcceptTcpClient();
                Task.Run(
                    () =>
                    {
                        TcpClient tmpSocket = socket;
                        DoClient(tmpSocket);
                    }
                    );
            }
        }

        private void DoClient(TcpClient socket)
        {
            using (StreamReader sr = new StreamReader(socket.GetStream()))
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            {
                sw.AutoFlush = true;
                
                while(!done)
                { 
                String playerString = sr.ReadLine();
                    //try
                    //{

                        if (playerString.ToLower() == "hentalle")
                        {
                            foreach (var p in listOfPlayers)
                            {
                                sw.WriteLine(p.ToString());
                            }
                        }
                        else if (playerString.Contains("ID"))
                        {
                            sw.WriteLine("Enter the id of the player you wish to find");
                            playerString = sr.ReadLine();
                            listOfPlayers.Where(p => p.Id == Convert.ToInt32(playerString));
                        }
                        else if (playerString.ToLower() == "gem")
                        {
                            sw.WriteLine("Input your Player");
                            playerString = sr.ReadLine();
                            FootBallPlayer player = JsonSerializer.Deserialize<FootBallPlayer>(playerString);
                            listOfPlayers.Add(player);
                            //Console.WriteLine("Received car json string " + playerString);
                            //Console.WriteLine("Received car : " + player);
                        }
                        else if (playerString.ToLower() == "im done")
                        {
                            done = true;
                        }
                        else
                        {
                            sw.WriteLine("Your input was invalid");
                        }
                    //}
                    //catch (NullReferenceException e)
                    //{
                    //    sw.WriteLine("you have to insert something");
                    //}
                    //catch(Exception e)
                    //{
                    //    sw.WriteLine(e.Message + "Pls contact your sever provider");
                    //}
                }


            }
            socket?.Close();
        }

    }
}
