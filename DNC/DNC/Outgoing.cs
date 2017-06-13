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
    public partial class Outgoing : Form
    {
        SerialPort ComPort1 = new SerialPort();
       
      //  SerialPort sp;
      
        SerialPort port=new SerialPort();
        delegate void SetTextCallback(string text);
        
       
        string path;
        string InputData = String.Empty;
    

        public Outgoing()
        {
            String PortName;
            Int16 DataBits;
            Int32 BaudRate;
            StopBits stp;
            Parity parity;
            Handshake hShake;
            Boolean RtsEnabled;
            Boolean DtrEnabled;
            InitializeComponent();
            Check();

            

            //   textBox1.Text = PortName;
            //     port.PortName = PortName;
            //    port.BaudRate = BaudRate;
            //    port.DataBits = DataBits;
            //    port.StopBits = stp;
            //     port.Handshake = hShake;
            //     port.Parity = parity;





        }

    
       

        




        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created By Andranik Atoyan");
        }


        public void Check() {


            
           
         

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
                ComPort1.RtsEnable = true;

            }
            if (checkBox1.Checked == true)
            {
                ComPort1.DtrEnable = true;

            }

            
        }

      
       

        private void button1_Click(object sender, EventArgs e)
        {
            if (path == null)
            {
                MessageBox.Show("Please Choose File");
            }
            else
            {
                
                
                    if (checkBox1.Checked == true)
                    {

                        ComPort1.RtsEnable = true;
                    }
                    else
                    {
                        ComPort1.RtsEnable = false;
                    }

                    if (checkBox2.Checked == true)
                    {

                        ComPort1.DtrEnable = true;
                    }

                    else
                    {
                        ComPort1.DtrEnable = false;
                    }


                    ComPort1.WriteBufferSize=1000000;
                ComPort1.WriteTimeout = 1000000;

                    ComPort1.PortName = comboBox1.GetItemText(comboBox1.SelectedItem).ToString();
                    ComPort1.BaudRate = Convert.ToInt32(cboBaudRate.Text);
                    ComPort1.DataBits = Convert.ToInt16(cboDataBits.Text);
                    ComPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cboStopBits.Text);
                    ComPort1.Handshake = (Handshake)Enum.Parse(typeof(Handshake), cboHandShaking.Text);
                    ComPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cboParity.Text);
                    ComPort1.Open();


                    try
                    {

                    //  ComPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                   // ComPort1.Write(File.OpenText(path).ReadToEnd());
                     FileStream readfile1 = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                              BinaryReader binary = new BinaryReader(readfile1, Encoding.GetEncoding(1252));
                         ComPort1.Write(binary.ReadBytes((int)readfile1.Length), 0, (int)readfile1.Length);
                    
                    //ComPort1.Write(new BinaryReader(readfile1).ReadBytes((int)readfile1.Length), 0, (int)readfile1.Length);





                }
                    



                    catch (Exception x) {
                    MessageBox.Show(x.ToString());

                    }
                    

                }


            ComPort1.Close();
        }

        

      



        private void button2_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                path = file.FileName;
            }
        }

        private void Outgoing_Load(object sender, EventArgs e)
        {

            String PortName;
            Int16 DataBits;
            Int32 BaudRate;
            StopBits stp;
            Parity parity;
            Handshake hShake;
            Boolean RtsEnabled;
            Boolean DtrEnabled;
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\out.txt";
            String writePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\out.txt";
            if (!File.Exists(writePath) || (File.Exists(writePath) && File.ReadAllText(writePath) == String.Empty))
            {
                FileStream fs = File.Create("out.txt");
                fs.Close();
                StreamWriter sr = new StreamWriter(writePath, false);
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
           else if (File.Exists(path) && File.ReadAllText(path) != String.Empty)
            {
                String[] lines = File.ReadAllLines(path);
                PortName = lines[0];
                BaudRate = Convert.ToInt32(lines[1]);
                hShake = (Handshake)Enum.Parse(typeof(Handshake), lines[2]);
                DataBits = Convert.ToInt16(lines[3]);
                stp = (StopBits)Enum.Parse(typeof(StopBits), lines[4]);
                parity = (Parity)Enum.Parse(typeof(Parity), lines[5]);
                String comparePortNames;
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
        }
        private void Outgoing_FormClosing(object sender, FormClosingEventArgs e)
        {
          
            String writePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\out.txt";
            if (!File.Exists(writePath) || (File.Exists(writePath) && File.ReadAllText(writePath) == String.Empty))
            {
                FileStream fs = File.Create("out.txt");
                fs.Close();
                StreamWriter sr = new StreamWriter(writePath, false);
                String[] prtnames = SerialPort.GetPortNames();

                sr.WriteLine(prtnames[0]);
                sr.WriteLine("300");
                sr.WriteLine("None");
                sr.WriteLine("7");
                sr.WriteLine("One");
                sr.WriteLine("None");

                sr.Close();

                
            }
            // MessageBox.Show(writePath.ToString());
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
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    }

