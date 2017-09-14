using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class GetMyIp
{
    public static async void Get(ConcurrentQueue<string> ipq)
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
        ipq.Enqueue(response);
    }
}
