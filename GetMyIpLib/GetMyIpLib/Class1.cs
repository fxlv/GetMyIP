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

        // pass in a Concurrent Queue object
        public static async void GetIpAsync(ConcurrentQueue<string> ipq)
        {
            string response = "";
            HttpClient client = new HttpClient();
            try
            {
                response = await client.GetStringAsync("http://ip.bgp.lv");
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