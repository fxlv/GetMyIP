using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Concurrent;



namespace GetMyIpGUI
{
    public partial class GetMyIp : Form
    {
       


        public GetMyIp()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            //
        }
        private async void labelMe2()
        {

            label2.Text = "Getting your IP...";
           
            label2.Text = Program.getMyIp();
               
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            labelMe2();
        }
    }
}
