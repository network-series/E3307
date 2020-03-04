using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class serialPort : Form
    {
        public serialPort()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;//设置该属性 为false
        }
        public delegate void HandleInterfaceUpdataDelegate(string text); //委托，此为重点 
        private HandleInterfaceUpdataDelegate interfaceUpdataHandle;

        private void Form1_Load(object sender, EventArgs e)
        {
            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            if (keyCom != null)
            {
                string[] sSubKeys = keyCom.GetValueNames();
                cmbPort.Items.Clear();
                foreach (string sName in sSubKeys)
                {
                    string sValue = (string)keyCom.GetValue(sName);
                    cmbPort.Items.Add(sValue);
                }
                if (cmbPort.Items.Count > 0)
                    cmbPort.SelectedIndex = 0;
            }

            cmbBaud.Text = "115200";
            cmbStop.Text = "One";
            cmbParity.Text = "None";
            cmbData.Text = "8";
        }

        bool isOpened = false;//串口状态标志
        private void button2_Click(object sender, EventArgs e)
        {
            if (!isOpened)
            {
                serialPort1.PortName = cmbPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(cmbBaud.Text, 10);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text.Trim());
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStop.Text.Trim());
                serialPort1.DataBits = Convert.ToInt32(cmbData.Text.Trim());
                try
                {
                    serialPort1.Open();     //打开串口
                    button2.Text = "关闭串口";
                    cmbPort.Enabled = false;//关闭使能
                    cmbBaud.Enabled = false;
                    cmbParity.Enabled = false;
                    cmbStop.Enabled = false;
                    cmbData.Enabled = false;
                    isOpened = true;
                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(post_DataReceived);//串口接收处理函数
                }
                catch
                {
                    MessageBox.Show("串口打开失败！");
                }
            }
            else
            {
                try
                {              
                    serialPort1.Close();     //关闭串口
                    button2.Text = "打开串口";
                    cmbPort.Enabled = true;//打开使能
                    cmbBaud.Enabled = true;
                    cmbParity.Enabled = true;
                    cmbStop.Enabled = true;
                    cmbData.Enabled = true;
                    isOpened = false;
                }
                catch
                {
                    MessageBox.Show("串口关闭失败！");
                }
            }

        }
        private void post_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            string str = serialPort1.ReadExisting();//字符串方式读
            ReceiveTbox.Text = "";//先清除上一次的数据
            ReceiveTbox.Text += str;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //发送数据
            if (serialPort1.IsOpen)
            {//如果串口开启
                if (SendTbox.Text.Trim() != "")//如果框内不为空则
                {
                    serialPort1.Write(SendTbox.Text.Trim());//写数据
                }
                else
                {
                    MessageBox.Show("发送框没有数据");
                }
            }
            else
            {
                MessageBox.Show("串口未打开");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
