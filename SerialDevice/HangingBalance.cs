using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

namespace SerialDevice
{
    public class HangingBalance : DeviceBase
    {
        private List<byte> m_ReadBuffer = new List<byte>(); //存放数据缓存，如果数据到达数量少于指定长度，等待下次接受
        private byte[] mDeviceNo = new byte[4] { 0x41, 0xd6,0x44, 0x77 };
        private const int MINACKLENTH = 8;//回应最小长度

        protected const int FRESHCMDCHECKBYTELENGTH = 10;                    //刷新命令的回应只有10个字节


        protected string m_PluggedPortName = string.Empty;
        protected AutoResetEvent m_FreshEvent = new AutoResetEvent(false);
        protected Hashtable bufferByCom = new Hashtable();      //不同串口，不同的buffer
        private List<string> m_OccupancyComList = new List<string>();
        protected byte[] m_FreshCmd;


        //public HangingBalance()
        //{
        //    this._deviceType = DeviceType.HangingBalance;
        //    _detectByteLength = 10;                                     //读取AD测量值,数据长度
        //    SetDetectBytes(new byte[] { 0x41, 0xd6, 0x44, 0x77, 0x04, 0xe0, 0x00, 0xb6, 0x03});      //设置检测命令
        //    m_FreshCmd = new byte[]   { 0x41, 0xd6, 0x44, 0x77, 0x04, 0xe0, 0x00, 0xb6, 0x03 };
        //    Init(9600, 8, StopBits.One, Parity.None, "");
        //}

        public HangingBalance(uint deviceNo = 0)
        {
            SetDeviceNo(deviceNo);
            this._deviceType = DeviceType.HangingBalance;
            _detectByteLength = 10;                                     //读取AD测量值,数据长度
            byte[] detectBytes = new byte[] { mDeviceNo[0], mDeviceNo[1], mDeviceNo[2], mDeviceNo[3], 0x04, 0xE0, 0x00, 0x00/*crc-8*/, 0x03 };
            m_FreshCmd = new byte[] { mDeviceNo[0], mDeviceNo[1], mDeviceNo[2], mDeviceNo[3], 0x04, 0xE0, 0x00, 0x00, 0x03 };
            byte crc8 = GetCRC8(detectBytes);
            detectBytes[7] = crc8;
            m_FreshCmd[7] = crc8;
            SetDetectBytes(detectBytes);      //设置检测命令
            Init(9600, 8, StopBits.One, Parity.None, "");
        }

        /// <summary>
        /// 界面上设置了新的序号，需要重新设置检测命令
        /// </summary>
        public void UpdateDetectBytes()
        {
            byte[] detectBytes = new byte[] { mDeviceNo[0], mDeviceNo[1], mDeviceNo[2], mDeviceNo[3], 0x04, 0xE0, 0x00, 0x00/*crc-8*/, 0x03 };
            byte crc8 = GetCRC8(detectBytes);
            detectBytes[7] = crc8;
            SetDetectBytes(detectBytes);
        }


        /// <summary>
        /// 除去最后两位，其余相加
        /// </summary>
        public byte GetCRC8(byte[] buffer)
        {
            int length = buffer.Length;
            byte crc8 = 0;
            for (int i = 0; i < length - 2; i++)
            {
                crc8 += buffer[i];
            }
            return crc8;
        }

        /// <summary>
        /// 设备编号是一个以2开头的10位整型数，占用4个字节头部。低字节在前
        /// </summary>
        /// <param name="deviceNo"></param>
        public void SetDeviceNo(uint deviceNo)
        {
            if (deviceNo == 0)
            {
                mDeviceNo = new byte[] { 0x41, 0xd6, 0x44, 0x77 };
            }
            else
            {
                mDeviceNo[0] = (byte)(deviceNo & 0x00FF);
                mDeviceNo[1] = (byte)(deviceNo >> 8 & 0x00FF);
                mDeviceNo[2] = (byte)(deviceNo >> 16 & 0x00FF);
                mDeviceNo[3] = (byte)(deviceNo >> 24 & 0x00FF);
            }
        }

        public void SetDeviceNo(byte[] DeviceNoBytes)
        {
            if (DeviceNoBytes.Length != 4)
                return;

            if (DeviceNoBytes[0] == 0 && DeviceNoBytes[1] == 0 && DeviceNoBytes[2] == 0 && DeviceNoBytes[3] == 0)
            {
                mDeviceNo = new byte[] { 0x41, 0xd6, 0x44, 0x77 };
            }
            else
            {
                mDeviceNo[0] = DeviceNoBytes[0];
                mDeviceNo[1] = DeviceNoBytes[1];
                mDeviceNo[2] = DeviceNoBytes[2];
                mDeviceNo[3] = DeviceNoBytes[3];
            }
        }

        public override void Get()
        {
            try
            {
                this._communicateDevice.SendData(this._detectCommandBytes);
            }
            catch(Exception ex)
            {

            }
        }


        public string FreshCom(string portName = "")
        {
            m_PluggedPortName = "";
            m_FreshEvent.Reset();
            string connectedCom = string.Empty;
            string[] portNames = new string[] { portName };
            List<Thread> threadPool = new List<Thread>();
            List<SerialBase> serialPortPool = new List<SerialBase>();
            bufferByCom.Clear();
            foreach (string port in portNames)
            {
                if (m_OccupancyComList.FindIndex((x) => { return string.Compare(x, port, true) == 0; }) >= 0)
                {
                    continue;
                }
                //开启多线程，每个串口开一个
                bufferByCom.Add(port, new List<byte>());
                Thread freshThread = new Thread(new ParameterizedThreadStart(CheckPlugged));
                SerialBase serialPort = new SerialBase(port,9600,8,StopBits.One,Parity.None);
                serialPort.DataReceived += OnFreshDataReceived;
                serialPortPool.Add(serialPort);
                freshThread.Start(serialPort);
                threadPool.Add(freshThread);
            }
            for (int i = 0; i < threadPool.Count; i++)
            {
                threadPool[i].Join();
            }
            if (m_FreshEvent.WaitOne(2000/*WAITFOREVENTTIMEOUT*/))
            {
            }
            for (int i = 0; i < serialPortPool.Count; i++)
            {
                serialPortPool[i].Close();
            }
            for (int i = 0; i < threadPool.Count; i++)
            {
                threadPool[i].Abort();
            }
            return connectedCom = m_PluggedPortName;
        }

        protected void CheckPlugged(object com)
        {
            SerialBase serialPort = com as SerialBase;
            serialPort.ReadCount = FRESHCMDCHECKBYTELENGTH;
            bool bOpen = serialPort.Open();
            if (!bOpen)
                return;
            SendFreshCmd(serialPort);
        }

        protected void OnFreshDataReceived(object sender, DataTransEventArgs e)
        {
            List<byte> buffer = (List<byte>)bufferByCom[e.PortName];
            if (e.EventData.Length < FRESHCMDCHECKBYTELENGTH)
            {
                buffer.AddRange(e.EventData);
                if (buffer.Count >= FRESHCMDCHECKBYTELENGTH)
                {
                    if (CompareResponseByte(buffer.ToArray()))
                    {
                        buffer.Clear();
                        m_PluggedPortName = e.PortName;
                        m_FreshEvent.Set();
                    }
                }
            }
            else
            {
                if (CompareResponseByte(e.EventData))
                {
                    buffer.Clear();
                    m_PluggedPortName = e.PortName;
                    m_FreshEvent.Set();
                }
            }
        }

        protected void SendFreshCmd(SerialBase serialPort)
        {
            serialPort.SendData(m_FreshCmd);
        }

        protected bool CompareResponseByte(byte[] eventData)
        {
            bool bEqual = true;
            if (eventData.Length != FRESHCMDCHECKBYTELENGTH)
                bEqual = false;
            else
                bEqual = true;
            return bEqual;
        }



        public void ReadWeight()
        {
            this._communicateDevice.SendData(this._detectCommandBytes);
        }

        public void SetCaliParamater(List<byte> buffer)
        {
            this._communicateDevice.SendData(buffer.ToArray());
        }

        public void SetZeroCalibration(List<byte> buffer)
        {
            this._communicateDevice.SendData(buffer.ToArray());
        }

        public void SetEnableDebugModel(List<byte> buffer)
        {
            this._communicateDevice.SendData(buffer.ToArray());
        }

        public override void Set(byte[] buffer)
        {
        }

        /// <summary>
        /// 只能用于检测串口，收到串口设备数据包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnDetectDataReceived(object sender, DataTransmissionEventArgs args)
        {
            //List<byte> buffer = (List<byte>)_bufferByCom[args.PortName];
            //if (buffer == null)
            //    return;
            //buffer.AddRange(args.EventData);
            //if (buffer.Count >= _detectByteLength && buffer[9] == 0x03)
            //{
            //    _bufferByCom.Clear();//找到串口，清除缓存
            //    base.OnDetectDataReceived(sender, args);
            //    System.Diagnostics.Debug.WriteLine("OnDetectDataReceived Invoked : " + args.PortName);
            //}
        }
        
        public bool IsValidHead(byte[] headBuffer)
        {
            if (headBuffer == null || headBuffer.Length < 4)
                return false;
            if (headBuffer[0] == mDeviceNo[0]
                && headBuffer[1] == mDeviceNo[1]
                && headBuffer[2] == mDeviceNo[2]
                && headBuffer[3] == mDeviceNo[3])
                return true;
            return false;
        }

        public bool IsValidHead(List<byte> headBuffer)
        {
            if (headBuffer == null || headBuffer.Count < 4)
                return false;
            if (headBuffer[0] == mDeviceNo[0]
                && headBuffer[1] == mDeviceNo[1]
                && headBuffer[2] == mDeviceNo[2]
                && headBuffer[3] == mDeviceNo[3])
                return true;
            return false;
        }

        public bool FindHead()
        {
            if (IsValidHead(m_ReadBuffer))
                return true;

            m_ReadBuffer.RemoveAt(0);
            if (m_ReadBuffer.Count < 4)
                return false;
            return FindHead();
        }


        public BaseCommand Unpackage()
        {
            byte[] buffer = null;
            BaseCommand cmd = null;
            if (FindHead())
            {
                if(m_ReadBuffer.Count > 5)
                {
                    byte dataLength = m_ReadBuffer[4];//数据长度
                    if(m_ReadBuffer.Count >= dataLength + 5)//拥有一个完整的包
                    {
                        buffer = new byte[dataLength + 5];
                        m_ReadBuffer.CopyTo(0, buffer, 0, dataLength + 5);
                        m_ReadBuffer.RemoveRange(0, dataLength + 5);
                        switch(buffer[5])
                        {
                            case (byte)CommandType.E0:
                                cmd = new CommandE0(buffer);
                                break;
                            default:
                                cmd = new BaseCommand(buffer);
                                break;
                        }
                        
                    }
                }
            }
            return cmd;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public override void ReceiveData(object sender, DataTransmissionEventArgs args)
        {
            BaseCommand cmd = null;
            lock (m_ReadBuffer)
            {
                m_ReadBuffer.AddRange(args.EventData); 
                if (m_ReadBuffer.Count >= MINACKLENTH)
                {
                    int endIndex = m_ReadBuffer.FindLastIndex((x) => { return x == 0x03; });
                    //帧尾找不到，返回
                    if (endIndex < MINACKLENTH-1)
                    {
                        return;
                    }
                    else
                    {
                        cmd = Unpackage();
                    }
                }
                else
                {
                    return;
                }
            }

            HangingBalanceEventArgs data = new HangingBalanceEventArgs(cmd, args.PortName);
            base.ReceiveData(sender, data);
        }

    }
    
}
