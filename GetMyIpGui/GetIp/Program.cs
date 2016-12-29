using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Collections.Concurrent;


namespace GetMyIpGUI
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GetMyIp());

        }

        public static async Task getMyIpAsync(ConcurrentQueue<string> ipq)
        {
            HttpClient client = new HttpClient();
            string contents = await client.GetStringAsync("http://ip.bgp.lv");
            ipq.Enqueue(contents);
        }

    }
}
