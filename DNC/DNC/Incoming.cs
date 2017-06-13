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
using System.IO;

namespace DNC
{
    public partial class Incoming : Form
    {


        Boolean RtsEnabled = false;
        Boolean DtrEnabled = false;
        String PortName;
        Int16 DataBits;
        Int32 BaudRate;
        StopBits stp;
        Handshake hShake;
        Parity parity;
        delegate void SetTextCallback(string text);

        SerialPort port = new SerialPort();



        public Incoming()
        { //String incomingConf= Path.Combine(Directory.GetCurrentDirectory(), "\\in.txt");
          //   String[] lines = File.ReadAllLines(incomingConf);
          //String portName = lines[0];

            InitializeComponent();
            //     port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            Check();



            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\in.txt";
            if (!File.Exists(path)||(File.Exists(path)&&File.ReadAllText(path)==String.Empty))
            {
                FileStream fs = File.Create("in.txt");
                fs.Close();
                StreamWriter sr = new StreamWriter(path, false);
                String[] prtnames = SerialPort.GetPortNames();

                sr.WriteLine(prtnames[0]);
                sr.WriteLine("300");
                sr.WriteLine("None");
                sr.WriteLine("7");
                sr.WriteLine("One");
                sr.WriteLine("None");

                sr.Close();

               
            }
            
            // MessageBox.Show(path);
            // String path = Path.Combine(Directory.GetCurrentDirectory(), "\\in.txt");
            else if(File.Exists(path)&&File.ReadAllText(path)!=String.Empty)
            {
                String[] lines = File.ReadAllLines(path);
                PortName = lines[0];
                BaudRate = Convert.ToInt32(lines[1]);
                hShake = (Handshake)Enum.Parse(typeof(Handshake), lines[2]);
                DataBits = Convert.ToInt16(lines[3]);
                stp = (StopBits)Enum.Parse(typeof(StopBits), lines[4]);
                parity = (Parity)Enum.Parse(typeof(Parity), lines[5]);
                int selectedPortIndex = -1;
                for (int x = 0; x < comboBox1.Items.Count; x++)
                {

                    if (comboBox1.Items[x].ToString() == PortName.ToString())
                    {
                        selectedPortIndex = x;
                    }

                }
                if (selectedPortIndex != -1)
                {
                    comboBox1.SelectedIndex = selectedPortIndex;
                }
                else
                {
                    comboBox1.SelectedIndex = 0;
                }
                for (int x = 0; x < cboBaudRate.Items.Count; x++)
                {
                    if (cboBaudRate.Items[x].ToString() == lines[1])
                    {
                        cboBaudRate.SelectedIndex = x;
                    }
                    else { continue; }
                }
                for (int x = 0; x < cboHandShaking.Items.Count; x++)
                {
                    if (cboHandShaking.Items[x].ToString() == lines[2])
                    {
                        cboHandShaking.SelectedIndex = x;
                    }
                    else { continue; }
                }
                for (int x = 0; x < cboDataBits.Items.Count; x++)
                {
                    if (cboDataBits.Items[x].ToString() == lines[3])
                    {
                        cboDataBits.SelectedIndex = x;
                    }
                    else { continue; }
                }
                for (int x = 0; x < cboStopBits.Items.Count; x++)
                {
                    if (cboStopBits.Items[x].ToString() == lines[4])
                    {
                        cboStopBits.SelectedIndex = x;
                    }
                    else { continue; }
                }
                for (int x = 0; x < cboParity.Items.Count; x++)
                {
                    if (cboParity.Items[x].ToString() == lines[5])
                    {
                        cboParity.SelectedIndex = x;
                    }
                    else { continue; }
                }
               
            }

            //    private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
            //    {

            //       SerialPort sp = (SerialPort)sender;
            //       string indata = sp.ReadExisting();

            //       richTextBox1.Text += indata;
            //       Console.Write(indata);



            // BaudRate = Convert.ToInt32(cboBaudRate.Text);
            //  DataBits = Convert.ToInt16(cboDataBits.Text);
            //   stp = (StopBits)Enum.Parse(typeof(StopBits), cboStopBits.Text);
            //   hShake = (Handshake)Enum.Parse(typeof(Handshake), cboHandShaking.Text);
            //   parity = (Parity)Enum.Parse(typeof(Parity), cboParity.Text);
            //     }

        }

        public void passValue() {


        }

        private void button1_Click(object sender, EventArgs e)
        {

            PortName = comboBox1.GetItemText(comboBox1.SelectedItem).ToString();

            BaudRate = Convert.ToInt32(cboBaudRate.Text);
            DataBits = Convert.ToInt16(cboDataBits.Text);
            stp = (StopBits)Enum.Parse(typeof(StopBits), cboStopBits.Text);
            hShake = (Handshake)Enum.Parse(typeof(Handshake), cboHandShaking.Text);
            parity = (Parity)Enum.Parse(typeof(Parity), cboParity.Text);


            String writePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\in.txt";

            if (!File.Exists(writePath))
            {
                FileStream fs = File.Create("in.txt");
                StreamWriter sr = new StreamWriter(writePath, false);
                String[] prtnames = SerialPort.GetPortNames();

                sr.WriteLine(prtnames[0]);
                sr.WriteLine("300");
                sr.WriteLine("None");
                sr.WriteLine("7");
                sr.WriteLine("One");
                sr.WriteLine("None");



                fs.Close();
            }

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(writePath, false))


            {
                file.WriteLine(comboBox1.GetItemText(comboBox1.SelectedItem).ToString());
                file.WriteLine(cboBaudRate.Text);
                file.WriteLine(cboHandShaking.Text);
                file.WriteLine(cboDataBits.Text);
                file.WriteLine(cboStopBits.Text);
                file.WriteLine(cboParity.Text);


            }

           

            IncomingPartTwo frm = new IncomingPartTwo( PortName,  DataBits,  BaudRate,  stp,  parity,  hShake,  RtsEnabled,  DtrEnabled);
            richTextBox1.Visible = true;
            this.Hide();
           
            frm.Closed += (s, args) => this.Close();
            frm.Show();
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            comboBox1.Visible = false;
            cboBaudRate.Visible = false;
            cboDataBits.Visible = false;
            cboHandShaking.Visible = false;
            cboParity.Visible = false;
            cboStopBits.Visible = false;
            button2.Visible = true;
            button1.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            button3.Visible = true;



            //  port.PortName = PortName;
            //     port.BaudRate = BaudRate;
            //    port.DataBits = DataBits;
            //     port.StopBits = stp;
            //     port.Handshake = hShake;
            //     port.Parity = parity;
            //     port.DtrEnable = DtrEnabled;
            //  port.RtsEnable = RtsEnabled;
            //     

            //  port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            //   port.Open();
            //     port.ReadTimeout = (100000);
            // MessageBox.Show(port.PortName + " is open");


        }
        

     



        public void Check()
        {






            string[] ArrayComPortsNames = null;
            int index = -1;
            string ComPortName = null;
            ArrayComPortsNames = SerialPort.GetPortNames();

            try
            {
                do
                {
                    index += 1;
                    comboBox1.Items.Add(ArrayComPortsNames[index]);
                   
                }

                while (!((ArrayComPortsNames[index] == ComPortName)
                              || (index == ArrayComPortsNames.GetUpperBound(0))));
                Array.Sort(ArrayComPortsNames);

                //want to get first out
                if (index == ArrayComPortsNames.GetUpperBound(0))
                {
                    ComPortName = ArrayComPortsNames[0];
                }
                comboBox1.Text = ArrayComPortsNames[0];
              
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

            }

            cboBaudRate.Items.Add(300);
            cboBaudRate.Items.Add(600);
            cboBaudRate.Items.Add(1200);
            cboBaudRate.Items.Add(2400);
            cboBaudRate.Items.Add(9600);
            cboBaudRate.Items.Add(14400);
            cboBaudRate.Items.Add(19200);
            cboBaudRate.Items.Add(38400);
            cboBaudRate.Items.Add(57600);
            cboBaudRate.Items.Add(115200);
            cboBaudRate.Items.ToString();
            //get first item print in text
            cboBaudRate.Text = cboBaudRate.Items[0].ToString();


            cboHandShaking.Items.Add("None");
            cboHandShaking.Items.Add("XOnXOff");
            cboHandShaking.Items.Add("RequestToSend");
            cboHandShaking.Items.Add("RequestToSendXOnXOff");
            cboHandShaking.Text = cboHandShaking.Items[0].ToString();

            cboParity.Items.Add("None");
            cboParity.Items.Add("Even");
            cboParity.Items.Add("Mark");
            cboParity.Items.Add("Odd");
            cboParity.Items.Add("Space");

            //get the first item print in the text

            cboParity.Text = cboParity.Items[0].ToString();

            cboDataBits.Items.Add(7);
            cboDataBits.Items.Add(8);
            //get the first item print it in the text 
            cboDataBits.Text = cboDataBits.Items[0].ToString();

            cboStopBits.Items.Add("One");
            cboStopBits.Items.Add("OnePointFive");
            cboStopBits.Items.Add("Two");
            //get the first item print in the text
            cboStopBits.Text = cboStopBits.Items[0].ToString();



            if (checkBox1.Checked == true)
            {
                RtsEnabled = true;

            }
            else { RtsEnabled = false; }

            if (checkBox1.Checked == true)
            {
                DtrEnabled = true;

            }
            else { DtrEnabled = false; }


        }

       


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

            richTextBox1.SelectionStart = richTextBox1.Text.Length;

            richTextBox1.ScrollToCaret();
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

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = String.Empty;
        }

      

        
    }
}
