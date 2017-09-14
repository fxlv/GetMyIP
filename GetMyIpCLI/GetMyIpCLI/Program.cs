using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Text.RegularExpressions;


namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentQueue<string> ipq = new ConcurrentQueue<string>();
            
            Console.WriteLine("Getting your IP, please wait...");
            GetMyIp.Get(ipq);
            Console.Write("Waiting on response from server");
            while (ipq.Count == 0)
            {
                Console.Write(".");
                Thread.Sleep(20);
            }
            Console.WriteLine(".");
            string ipAddress;
            ipq.TryDequeue(out ipAddress);
            ipAddress = StripHtml(ipAddress);
            Console.WriteLine(String.Format("Your IP: {0}", ipAddress));
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();



        }

        public static string StripHtml(string htmlContent)
        {
            string pattern = "([0-9.]+)";
            Match match = Regex.Match(htmlContent, pattern);
            Console.WriteLine(match);
            return match.ToString();
            
        }
    }
}
