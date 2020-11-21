using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Misc = ComunicationProtocol.Misc;
using System.Configuration;
using SerialDevice;
using System.Timers;

namespace PTool
{
    public partial class Calibration : UserControl
    {
        //1.请放入500克砝码，并静置5秒后，点击校准
        private uint mDeviceNo = 0; //产品序列号，只能写一次
        private byte[] DeviceNoBytes = new byte[4]{ 0x41, 0xd6, 0x44, 0x77 };
        private const string mTips = "{0}.请放入{1}克砝码，点击校准";
        private const string mAdTips = "AD值：{0}";
        private List<int> mWeights = new List<int>();//砝码配置值，5个
        private List<int> mAdWeights = new List<int>();//AD测量值
        private List<ushort> mTempAdWeights = new List<ushort>();//AD测量值临时存放，等比较误差后再放到mAdWeights中
        private ushort mAdWeightFirst = 0;
        private ushort mAdWeightSecond = 0;
        private List<Label> mStepInfoLabels = new List<Label>();
        private HangingBalance m_DetectBalance = new HangingBalance(0);
        private string mPortName = string.Empty;
        private System.Timers.Timer mTimer = new System.Timers.Timer();//设置读取AD数据的时钟
        private int mHasReadTimes = 0;//只能读两次，两次结果误差在范围内就算合格
        private ushort mStandardError = 0;//误差在范围
        private int mCurrentStep = 0;//当前正在进行到第几步，1，2，3，4，5。0:代表结束

        public delegate void DelegateEnableContols(bool bEnabled);
        public delegate void DelegateStopTimer();
        public delegate void DelegateShowNextCalibrateTips();

        public Calibration()
        {
            InitializeComponent();
            mStepInfoLabels.Add(lbStep1);
            mStepInfoLabels.Add(lbStep2);
            mStepInfoLabels.Add(lbStep3);
            mStepInfoLabels.Add(lbStep4);
            mStepInfoLabels.Add(lbStep5);
            LoadConfig();
            InitStepInfo();
            m_DetectBalance.DeviceDataRecerived += OnDeviceDataReceived;
            mTimer.Interval = 3000;
            mTimer.Elapsed += OnTimer;
        }


        private void OnDeviceDataReceived(object sender, EventArgs e)
        {
            HangingBalanceEventArgs args = e as HangingBalanceEventArgs;
            if (args.Command == null)
                return;
            SerialDevice.CommandType type = args.Command.mCmdType;
            if (type == SerialDevice.CommandType.E0)
            {
                CommandE0 e0 = args.Command as CommandE0;
                ushort ad = e0.GetAD();

                if (mHasReadTimes == 1)
                    mAdWeightFirst = ad;

                if (mHasReadTimes == 2)
                    mAdWeightSecond = ad;

                if (mHasReadTimes == 2)
                {
                    if (Math.Abs(mAdWeightSecond - mAdWeightFirst) <= mStandardError)
                    {
                        mAdWeights.Add((mAdWeightSecond + mAdWeightFirst) / 2);
                        StopTimer();
                        ShowNextCalibrateTips();
                        EnableControls(true);
                    }
                    else
                    {
                        //不合格要重新来，否则时钟不会停
                        mHasReadTimes = 0;
                        mTempAdWeights.Clear();
                    }

                }
            }
        }

        private void LoadConfig()
        {
            mWeights.Clear();
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string strStandardError = ConfigurationManager.AppSettings.Get("StandardError");
                if (!ushort.TryParse(strStandardError, out mStandardError))
                    mStandardError = 5;

                string strWeights = ConfigurationManager.AppSettings.Get("Weights");
                int weight = 0;
                foreach (var w in strWeights.Split(','))
                {
                    if (int.TryParse(w, out weight))
                        mWeights.Add(weight);
                    else
                    {
                        mWeights.Add(0);
                        Logger.Instance().ErrorFormat("配置文件错误，请检查砝码参数是否正确");
                        MessageBox.Show("配置文件错误，请检查砝码参数是否正确");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件参数配置错误，请先检查该文件后再重新启动程序!" + ex.Message);
            }
        }

        /// <summary>
        /// 初始化校准步骤文字说明
        /// </summary>
        private void InitStepInfo()
        {
            int count = mWeights.Count;
            int labelCount = mStepInfoLabels.Count;
            for (int i = 0; i < count && i < labelCount; i++)
            {
                mStepInfoLabels[i].Text = string.Format(mTips, i + 1, mWeights[i]);
            }
        }

        public void SetPortName(string portName)
        {
            mPortName = portName;
        }

        public void SetDeviceNumber(uint deviceNo)
        {
            mDeviceNo = deviceNo;
            DeviceNoBytes = PressureForm.GetDeviceNumberBytes();//设备序号字节
        }

        private void StartTimer()
        {
            mHasReadTimes = 0;
            StopTimer();
            mTimer.AutoReset = true;
            mTimer.Enabled = true;
            mTimer.Start();
        }

        private void StopTimer()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DelegateStopTimer(StopTimer), null);
                return;
            }

            mHasReadTimes = 0;
            mTempAdWeights.Clear();
            mTimer.Enabled = false;
            mTimer.Stop();
        }

        /// <summary>
        /// 每3秒钟读一次数据，总共读两次，两次结果的误差要在一定范围内才算有效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            if (mHasReadTimes >= 2)
            {
                StopTimer();
                return;
            }
            m_DetectBalance.ReadWeight();
            ++mHasReadTimes;
        }

        public bool Init()
        {
            if (string.IsNullOrEmpty(mPortName))
            {
                return false;
            }
            m_DetectBalance.Init(mPortName);
            return true;
        }

        public bool Open()
        {
            if (!m_DetectBalance.IsOpen())
            {
                return m_DetectBalance.Open();
            }
            return true;
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            m_DetectBalance.Close();
        }

        private void btnCalibrate1_Click(object sender, EventArgs e)
        {
            mAdWeights.Clear();
            if(!m_DetectBalance.IsOpen())
            {
                if (Init() && Open())
                {

                }
                else
                {
                    MessageBox.Show("初始化串口失败!");
                    return;
                }
            }
            StartTimer();
            mCurrentStep = 1;
            SetResultMessage("正在校准......");
            lbAdWeight1.Text = "";
            lbAdWeight2.Text = "";
            lbAdWeight3.Text = "";
            lbAdWeight4.Text = "";
            lbAdWeight5.Text = "";
            btnCalibrate1.Enabled = false;
        }

        private void btnCalibrate2_Click(object sender, EventArgs e)
        {
            mCurrentStep = 2;
            OnCalibrateClick(sender, e);
        }

        private void btnCalibrate3_Click(object sender, EventArgs e)
        {
            mCurrentStep = 3;
            OnCalibrateClick(sender, e);
        }

        private void btnCalibrate4_Click(object sender, EventArgs e)
        {
            mCurrentStep = 4;
            OnCalibrateClick(sender, e);
        }

        private void btnCalibrate5_Click(object sender, EventArgs e)
        {
            mCurrentStep = 5;
            OnCalibrateClick(sender, e);
        }

        private void OnCalibrateClick(object sender, EventArgs e)
        {
            Open();
            StartTimer();
            SetResultMessage("正在校准......");
            Button btn = sender as Button;
            btn.Enabled = false;
        }

        private void SetResultMessage(string message)
        {
            lbResult.Text = message;
            lbResult.ForeColor = Color.White;
        }

        private void EnableControls(bool bEnable = true)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DelegateEnableContols(EnableControls), new object[] { bEnable });
                return;
            }

            switch (mCurrentStep)
            {
                case 0:
                    btnCalibrate1.Enabled = false;
                    btnCalibrate2.Enabled = false;
                    btnCalibrate3.Enabled = false;
                    btnCalibrate4.Enabled = false;
                    btnCalibrate5.Enabled = false;
                    break;
                case 1:
                    btnCalibrate1.Enabled = false;
                    btnCalibrate2.Enabled = true;
                    btnCalibrate3.Enabled = false;
                    btnCalibrate4.Enabled = false;
                    btnCalibrate5.Enabled = false;
                    break;
                case 2:
                    btnCalibrate1.Enabled = false;
                    btnCalibrate2.Enabled = false;
                    btnCalibrate3.Enabled = true;
                    btnCalibrate4.Enabled = false;
                    btnCalibrate5.Enabled = false;
                    break;
                case 3:
                    btnCalibrate1.Enabled = false;
                    btnCalibrate2.Enabled = false;
                    btnCalibrate3.Enabled = false;
                    btnCalibrate4.Enabled = true;
                    btnCalibrate5.Enabled = false;
                    break;
                case 4:
                    btnCalibrate1.Enabled = false;
                    btnCalibrate2.Enabled = false;
                    btnCalibrate3.Enabled = false;
                    btnCalibrate4.Enabled = false;
                    btnCalibrate5.Enabled = true;
                    break;
                case 5:
                    btnCalibrate1.Enabled = true;
                    btnCalibrate2.Enabled = false;
                    btnCalibrate3.Enabled = false;
                    btnCalibrate4.Enabled = false;
                    btnCalibrate5.Enabled = false;
                    break;
            }


        }

        /// <summary>
        /// 串口连接成功，可以开始校准了
        /// </summary>
        /// <param name="bEnable"></param>
        public void EnableCalibrate(bool bEnable)
        {
            btnCalibrate1.Enabled = bEnable;
            btnCalibrate2.Enabled = false;
            btnCalibrate3.Enabled = false;
            btnCalibrate4.Enabled = false;
            btnCalibrate5.Enabled = false;
        }

        private void ShowNextCalibrateTips()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DelegateShowNextCalibrateTips(ShowNextCalibrateTips), null);
                return;
            }
            switch (mCurrentStep)
            {
                case 0:
                    SetResultMessage("");
                    break;
                case 1:
                    SetResultMessage("请进行第二步校准");
                    if(mAdWeights.Count > 0)
                        lbAdWeight1.Text = string.Format(mAdTips, mAdWeights[0].ToString());
                    break;
                case 2:
                    SetResultMessage("请进行第三步校准");
                    if (mAdWeights.Count > 1)
                        lbAdWeight2.Text = string.Format(mAdTips, mAdWeights[1].ToString());
                    break;
                case 3:
                    SetResultMessage("请进行第四步校准");
                    if (mAdWeights.Count > 2)
                        lbAdWeight3.Text = string.Format(mAdTips, mAdWeights[2].ToString());
                    break;
                case 4:
                    SetResultMessage("请进行第五步校准");
                    if (mAdWeights.Count > 3)
                        lbAdWeight4.Text = string.Format(mAdTips, mAdWeights[3].ToString());
                    break;
                case 5:
                    SetResultMessage("校准结束！");
                    if (mAdWeights.Count > 4)
                        lbAdWeight5.Text = string.Format(mAdTips, mAdWeights[4].ToString());
                    break;
            }
            if(mCurrentStep ==5 )
            {
                WriteParamater2Device();
            }
        }

        /// <summary>
        /// 设置重量校准参数
        /// </summary>
        private void WriteParamater2Device()
        {
            if (mWeights.Count != 5 || mAdWeights.Count != 5)
            {
                MessageBox.Show("数据采集出错！");
                return;
            }
            double[] arrX = { mWeights[0], mWeights[1], mWeights[2], mWeights[3], mWeights[4] };
            double[] arrY = { mAdWeights[0], mAdWeights[1], mAdWeights[2], mAdWeights[3], mAdWeights[4] };
            //double[] arrX = { 1, 2, 3, 4, 5 };
            //double[] arrY = { 10, 20, 30, 40, 50 };
            double[] paramaters = CalculatePoly(arrX, arrY, 1);

            int a = (int)(paramaters[1]);
            int b = (int)(paramaters[0]);

            CommandE4 cmd = new CommandE4();
            cmd.SetDeviceNumber(DeviceNoBytes);
            List<byte> buffer = cmd.SetCalibrateCommand(a, b);
            m_DetectBalance.SetCaliParamater(buffer);
        }

        /// <summary>
        ///y =  a0 + a1 * x + a2 * x *x
        /// </summary>
        /// <param name="arrX"></param>
        /// <param name="arrY"></param>
        /// <param name="length"></param>
        /// <param name="dimension"></param>
        /// <returns>a0,a1,a2,a3</returns>
        private double[] CalculatePoly(double[] arrX, double[] arrY, int dimension = 2)
        {
            return Polynomial.MultiLine(arrX, arrY, arrX.Length, dimension);
        }

        /// <summary>
        /// 重量零点标定
        /// </summary>
        public void DoZeroCalibration()
        {
            if (!m_DetectBalance.IsOpen())
            {
                if (Init() && Open())
                {

                }
                else
                {
                    MessageBox.Show("初始化串口失败!");
                    return;
                }
            }

            if ( m_DetectBalance.IsOpen())
            {
                CommandE3 cmd = new CommandE3();
                cmd.SetDeviceNumber(DeviceNoBytes);
                List<byte> buffer = cmd.CreateZeroCaliCommand();
                m_DetectBalance.SetZeroCalibration(buffer);
            }
            else
            {
                MessageBox.Show("串口未连接！");
            }
           
        }

        /// <summary>
        /// 生成进入或退出调试
        /// </summary>
        public void EnableDebugModel(bool debug = true)
        {
            if (!m_DetectBalance.IsOpen())
            {
                if (Init() && Open())
                {

                }
                else
                {
                    MessageBox.Show("初始化串口失败!");
                    return;
                }
            }

            if ( m_DetectBalance.IsOpen())
            {
                CommandDF cmd = new CommandDF();
                cmd.SetDeviceNumber(DeviceNoBytes);
                List<byte> buffer = cmd.CreateDebugCommand(debug);
                m_DetectBalance.SetEnableDebugModel(buffer);
            }
            else
            {
                MessageBox.Show("串口未连接！");
            }
        }

    }
}
