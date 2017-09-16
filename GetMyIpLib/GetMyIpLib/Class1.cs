using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetMyIpLibrary
{
    public class MyIp
    {
        public string IP;
        public static string ipAddressDetectionUrl = "http://ip.bgp.lv";

        /// <summary>
        /// Simple, blocking method that returns YOUR IP as a string
        /// </summary>
        /// <returns></returns>
        public static string GetIpBlocking()
        {
            HttpClient client = new HttpClient();
            string myIp  = client.GetStringAsync(ipAddressDetectionUrl).Result;
            myIp = StripHtml(myIp);
            return myIp;
        }


        /// <summary>
        /// Async version of pass in a Concurrent Queue object
        /// </summary>
        /// <param name="ipq"></param>
        public static async void GetIpAsync(ConcurrentQueue<string> ipq)
        {
            string response = "";
            HttpClient client = new HttpClient();
            try
            {
                response = await client.GetStringAsync(ipAddressDetectionUrl);
            }
            catch (System.Net.Http.HttpRequestException)
            {
                Console.WriteLine("Could not get the IP address.");
                Environment.Exit(1);
            }
            response = StripHtml(response);
            ipq.Enqueue(response);
        }
        public static string StripHtml(string htmlContent)
        {
            string pattern = "([0-9.]+)";
            Match match = Regex.Match(htmlContent, pattern);
            return match.ToString();
        }
     
    }
}