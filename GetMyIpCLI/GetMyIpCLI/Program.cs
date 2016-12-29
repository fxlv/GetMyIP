using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentQueue<string> ipq = new ConcurrentQueue<string>();
            
            Console.WriteLine("Getting your IP, please wait...");
            get(ipq);
            Console.Write("Waiting om response from server");
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
        static async void get(ConcurrentQueue<string> ipq)
        {
            string response = "";
            HttpClient client = new HttpClient();
            try {
                response = await client.GetStringAsync("http://ip.bgp.lv");
            }
            catch (System.Net.Http.HttpRequestException)
            {
                Console.WriteLine("Could not get the IP address.");
                Environment.Exit(1);
            }
            ipq.Enqueue(response);
        }
    }
}
