using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using ClosedXML.Excel;
using CCWin;
using ApplicationClient;
using Misc = ComunicationProtocol.Misc;
using SerialDevice;

namespace PTool
{
    public partial class PressureForm : Form
    {
        private bool moving = false;
        private Point oldMousePosition;
        public static uint mDeviceNo = 0; //产品序列号，只能写一次

        private HangingBalance m_DetectBalance = new HangingBalance(mDeviceNo);
        public PressureForm()
        {
            InitializeComponent();
        }

        private void PressureForm_Load(object sender, EventArgs e)
        {
            LoadConfig();
            BalanceSerialPort.Items.AddRange(SerialPort.GetPortNames());
            m_DetectBalance.DeviceDataRecerived += OnDetectBalanceDataReceived;
        }

        private void LoadConfig()
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string strDeviceNo = ConfigurationManager.AppSettings.Get("DeviceNo");
                tbDeviceNumber.Text = strDeviceNo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件参数配置错误，请先检查该文件后再重新启动程序!" + ex.Message);
            }
        }

        private void OnDetectBalanceDataReceived(object sender, EventArgs e)
        {
            HangingBalanceEventArgs args = e as HangingBalanceEventArgs;
           // args.PortName
            
        }

        private void SaveLastToolingNo()
        {
            //Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //cfa.AppSettings.Settings["Tool1"].Value = tbToolingNo.Text;
            //cfa.AppSettings.Settings["Tool2"].Value = tbToolingNo2.Text;
            //cfa.Save();
        }

        private void BalanceSerialPort_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (m_DetectBalance == null)
                m_DetectBalance = new HangingBalance(mDeviceNo);

            string name = m_DetectBalance.FreshCom(BalanceSerialPort.Items[BalanceSerialPort.SelectedIndex].ToString());
            if (string.IsNullOrEmpty(name))
            {
                picConnectState.Image = global::HangBalance.Properties.Resources.error2;
                mCalibration.EnableCalibrate(false);
            }
            else
            {
                picConnectState.Image = global::HangBalance.Properties.Resources.ok2;
                mCalibration.SetPortName(name);
                mCalibration.EnableCalibrate(true);
            }
        }

        #region 鼠标移动窗口
        private void tlpTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                return;
            }
            oldMousePosition = e.Location;
            moving = true;
        }

        private void tlpTitle_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
        }

        private void tlpTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && moving)
            {
                Point newPosition = new Point(e.Location.X - oldMousePosition.X, e.Location.Y - oldMousePosition.Y);
                this.Location += new Size(newPosition);
            }
        }
        #endregion

        private void picCloseWindow_Click(object sender, EventArgs e)
        {
            SaveDeviceNumber();
            mCalibration.Close();
            this.Close();
        }

        /// <summary>
        /// 只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbDeviceNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\b')
            {
                e.Handled = false;
                return;
            }
            if (e.KeyChar > '9' || e.KeyChar < '0')
                e.Handled = true;
            else
                e.Handled = false;
        }

        /// <summary>
        /// 保存设备编号
        /// </summary>
        private void SaveDeviceNumber()
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings["DeviceNo"].Value = tbDeviceNumber.Text;
            cfa.Save();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbDeviceNumber.Text) || tbDeviceNumber.Text.Length != 10)
            {
                MessageBox.Show("请输入正确的10位序列号");
            }
            else
            {
                if(!uint.TryParse(tbDeviceNumber.Text, out mDeviceNo))
                {
                    MessageBox.Show("请输入序列号必须是数字");
                }
                else
                {
                    mCalibration.SetDeviceNumber(mDeviceNo);
                    m_DetectBalance.UpdateDetectBytes();
                }
            }
        }

        public static byte[] GetDeviceNumberBytes()
        {
            //0放低位, 4放最高位
            byte[] buffer = new byte[4];
            buffer[0] = (byte)(mDeviceNo & 0x000000FF);
            buffer[1] = (byte)(mDeviceNo >> 8 & 0x000000FF);
            buffer[2] = (byte)(mDeviceNo >> 16 & 0x000000FF);
            buffer[3] = (byte)(mDeviceNo >> 24 & 0x000000FF);
            return buffer;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            tbDeviceNumber.Clear();
            mDeviceNo = 0;
            mCalibration.SetDeviceNumber(mDeviceNo);
            m_DetectBalance.UpdateDetectBytes();
        }

        private void picSetting_Click(object sender, EventArgs e)
        {
            contextMenu.Show((Control)sender, 0, 20);
        }

        private void menuZeroCali_Click(object sender, EventArgs e)
        {
            mCalibration.DoZeroCalibration();
        }

        private void menuDebug_Click(object sender, EventArgs e)
        {
            mCalibration.EnableDebugModel();
        }
    }


}
