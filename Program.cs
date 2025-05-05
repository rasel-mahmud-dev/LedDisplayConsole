using System;
using Microsoft.Owin.Hosting;

namespace RsDisplayConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"Server running at {baseAddress}");
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}