using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Collections.Concurrent;
using GetMyIpLibrary;


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

        public static string getMyIp()
        {
            MyIp myIp = new MyIp();
            var ip = myIp.GetIpBlocking();
            return ip;
        }

    }
}
