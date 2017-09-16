using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Text.RegularExpressions;
using GetMyIpLibrary;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentQueue<string> ipq = new ConcurrentQueue<string>();
            
            Console.WriteLine("Getting your IP, please wait...");
            GetMyIpLibrary.MyIp.GetIpAsync(ipq);
            Console.Write("Waiting on response from server");
            while (ipq.Count == 0)
            {
                Console.Write(".");
                Thread.Sleep(20);
            }
            Console.WriteLine(".");
            string ipAddress;
            ipq.TryDequeue(out ipAddress);
            Console.WriteLine(String.Format("Your IP: {0}", ipAddress));
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
