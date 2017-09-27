using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GetMyIpLibrary;

namespace ConsoleApplication3
{
    class Program
    {
        private static string GetIp()
        {
            ConcurrentQueue<string> ipq = new ConcurrentQueue<string>();
            MyIp myip = new MyIp();
            myip.GetIpAsync(ipq);
            Console.Write("Waiting on response from server");
            while (ipq.Count == 0)
            {
                Console.Write(".");
                Thread.Sleep(20);
            }
            Console.WriteLine(".");
            string ipAddress;
            ipq.TryDequeue(out ipAddress);
            return ipAddress;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Getting your IP, please wait...");
            string ipAddress = GetIp();
            Console.WriteLine("Your IP: {0}", ipAddress);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}