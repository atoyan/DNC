using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;

namespace DNC
{
    public partial class Launcher : Form
    {
        public Launcher()
        {
            InitializeComponent();
           


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Outgoing frm = new Outgoing();
            

           
            frm.Closed += (s, args) => this.Close();
          
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Incoming frm2 = new Incoming();
            frm2.Closed += (s, args) => this.Close();
            frm2.Show();
        }
    }
}
