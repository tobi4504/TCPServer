using CLass_Libary;
using System;
using System.Text.Json;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            FootBallPlayer syntaxFinder = new FootBallPlayer(2, "Boris", 30, 2);
           // Console.WriteLine(syntaxFinder.ToString());
            string Syntax = JsonSerializer.Serialize(syntaxFinder);
            Console.WriteLine(Syntax);
            Server worker = new Server();
            worker.Start();

            Console.ReadLine();
        }
    }
}
