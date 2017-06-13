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

namespace DNC
{
    public partial class IncomingPartTwo : Form
    {
       

        public IncomingPartTwo(String PortName1, Int16 DataBits1, Int32 BaudRate1, StopBits stp1, Parity parity1, Handshake hShake1, Boolean RtsEnabled1, Boolean DtrEnabled1)
        {  InitializeComponent();
            String PortName=PortName1;
            Int16 DataBits=DataBits1;
            Int32 BaudRate=BaudRate1;
            StopBits stp=stp1;
            Parity parity=parity1;
            Handshake hShake=hShake1;
            Boolean RtsEnabled=RtsEnabled1;
            Boolean DtrEnabled=DtrEnabled1;
            SerialPort port = new SerialPort(PortName);

            port.DataBits = DataBits;
            port.BaudRate = BaudRate;
            port.StopBits = stp;
            port.Parity = parity;
            port.Handshake = hShake;
            port.RtsEnable = RtsEnabled;
            port.DtrEnable = DtrEnabled;
            
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            port.Open();
            label2.Text = PortName; 
          
        }

        private void IncomingPartTwo_Load(object sender, EventArgs e)
        {

          

        }


        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
      {

             SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();

          richTextBox1.Text += indata;
             Console.Write(indata);
            
         }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

            richTextBox1.SelectionStart = richTextBox1.Text.Length;

            richTextBox1.ScrollToCaret();
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = String.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "NC files (*.nc)|*.nc|All files (*.*)|*.* ";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName.Length > 0)
            {



                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);



            }
        }
    }
}
