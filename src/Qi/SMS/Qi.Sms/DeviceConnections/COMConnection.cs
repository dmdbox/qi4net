using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using Qi.Sms.Protocol;
using Qi.Sms.Protocol.SendCommands;
using log4net;

namespace Qi.Sms.DeviceConnections
{
    public sealed class ComConnection : IDeviceConnection
    {
        private readonly StringBuilder _buffer = new StringBuilder();
        private readonly object _lockItem = "";
        private readonly ILog _log = LogManager.GetLogger(typeof(ComConnection));
        private readonly SerialPort _serialPort;
        public ComConnection(string portName, int baudRate)
        {
            if (portName == null)
                throw new ArgumentNullException("portName");
            PortName = portName;
            BaudRate = baudRate;
            _serialPort = new SerialPort(portName, BaudRate)
                              {
                                  DtrEnable = true,
                                  RtsEnable = true,
                                  Handshake = Handshake.None
                              };
            _serialPort.DataReceived += SerialPortDataReceived;
        }

        public string PortName { get; private set; }

        public int BaudRate { get; private set; }

        #region IDeviceConnection Members

        public event EventHandler<DeviceCommandEventHandlerArgs> ReceivedEvent;

        public void Dispose()
        {
            if (_serialPort != null)
            {
                _serialPort.Close();
                _serialPort.Dispose();
            }
        }

        public void Open()
        {
            try
            {
                _serialPort.Open();
                IsConnected = true;
            }
            catch (Exception ex)
            {
                throw new DeviceConnectionException(ex.Message, ex);
            }
        }

        public void Close()
        {
            try
            {
                _serialPort.Close();
                IsConnected = false;
            }
            catch (Exception ex)
            {
                throw new DeviceConnectionException(ex.Message, ex);
            }
        }

        public SerialPort SerialPort
        {
            get { return _serialPort; }
        }

        public bool IsConnected { get; private set; }

        public string Send(AbstractCommand command)
        {
            lock (_lockItem)
            {
                try
                {
                    _buffer.Remove(0, _buffer.Length);
                    ThreadPool.QueueUserWorkItem(delegate { _serialPort.WriteLine(command.CompleteCommand()); });
                    return WaitReturn(command);
                }
                finally
                {
                    _buffer.Remove(0, _buffer.Length);
                }
            }
        }

        #endregion

        public void InvokeSend(AbstractCommand command, int sleepMilSeconds)
        {
            lock (_lockItem)
            {
                _buffer.Remove(0, _buffer.Length);
                _serialPort.WriteLine(command.CompleteCommand());
                Thread.Sleep(sleepMilSeconds);
                _buffer.Remove(0, _buffer.Length);
            }
        }

        private string WaitReturn(AbstractCommand command)
        {
            var now = DateTime.Now;
            while (true)
            {
                if ((DateTime.Now - now).TotalSeconds > 12)
                {
                    _log.Warn("waiting timeout.");
                    break;
                }
                if (command.Init(_buffer.ToString()))
                {
                    break;
                }
                Thread.Sleep(100);
            }
            string result = _buffer.ToString();
            _log.InfoFormat("receive command \r\n {0}", result);
            return result;
        }

        public void Send(string command)
        {
            _serialPort.WriteLine(command + "\r\n");
        }

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serialPort = (SerialPort)sender;
            while (serialPort.BytesToRead > 0)
            {
                string re = serialPort.ReadExisting();
                _buffer.Append(re);
                if (_buffer != null)
                {
                    _buffer.Append(re);
                }
            }

            var result = CmtiCommand.GetSmsIndex(_buffer.ToString());
            if (result.Length != 0)
            {
                OnReceiveSms(result);
            }
        }

        private void OnReceiveSms(int[] command)
        {
            if (ReceivedEvent != null)
            {
                lock (_lockItem)
                {
                    ReceivedEvent(this, new DeviceCommandEventHandlerArgs(command));
                }
            }
        }
    }
}