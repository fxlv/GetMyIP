﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NMock;
using RichardSzalay.MockHttp;
using System.Configuration;
namespace GetMyIpLibrary
{
    public interface IMyIp
    {
        string GetIpBlocking();
        string StripHtml(string htmlContent);

    }
    public class MyIp : IMyIp
    {
        public string ipAddressDetectionUrl = "";
        public HttpClient client = new HttpClient();

        public MyIp()
        {
            // set up ipSource with the value from App config
            var settings = new GetMyIpLib.Properties.Settings();
            ipAddressDetectionUrl = $"http://{settings.ipSource}";
        }



    /// <summary>
    /// Simple, blocking method that returns YOUR IP as a string
    /// </summary>
    /// <returns></returns>
        public string GetIpBlocking()
        {
            string myIp  = client.GetStringAsync(ipAddressDetectionUrl).Result;
            myIp = StripHtml(myIp);
            return myIp;
        }


        /// <summary>
        /// Async version of pass in a Concurrent Queue object
        /// </summary>
        /// <param name="ipq"></param>
        public async void GetIpAsync(ConcurrentQueue<string> ipq)
        {
            string response = "";
            HttpClient client = new HttpClient();
            try
            {
                response = await client.GetStringAsync(ipAddressDetectionUrl);
            }
            catch (System.Net.Http.HttpRequestException)
            {
                Console.WriteLine("failed to get the IP address.");
            }
            response = StripHtml(response);
            ipq.Enqueue(response);
        }

        public string StripHtml(string htmlContent)
        {
            string pattern = "([0-9.]+)";
            Match match = Regex.Match(htmlContent, pattern);
            return match.ToString();
        }
     
    }
}