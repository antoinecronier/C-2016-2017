using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using wpfzoo.commandworker;

namespace wpfzoo.logger
{
    public class Logger2
    {
        private const string MESSAGE = "\nWrite to log?";
        private const string ALERTTITLE = "Warning";

        private AlertMode alertMode;
        private LogMode logMode;
        private String name;
        private CommandWorker worker;
        private ManualResetEvent reseter;
        private int idSinceStart = 0;

        public Logger2(string name = "Logger", LogMode logMode = LogMode.NONE, AlertMode alertMode = AlertMode.NONE)
        {
            this.name = name + ": ";
            this.logMode = logMode;
            this.alertMode = alertMode;
            reseter = new ManualResetEvent(false);
            worker = new CommandWorker(this.name);
            worker.Start();
        }

        public void Log(String toLog, String msg = null, String userDirectory = null)
        {
            worker.Enqueue(() =>
            {
                InnerLog(toLog, msg, userDirectory);
                
            }, reseter);
        }

        public void Log(String toLog, LogMode logMode, AlertMode alertMode, String msg = null, String userDirectory = null)
        {
            worker.Enqueue(() =>
            {
                ReLog(toLog, logMode, alertMode);
                
            }, reseter);
        }

        private void InnerLog(string toLog, string msg, string userDirectory)
        {
            if (msg == null)
            {
                msg = toLog;
            }

            if (userDirectory == null)
            {
                userDirectory = Path.GetTempPath() + "\\" +
                        System.Windows.Application.Current.ToString().Split('.')[0];
            }

            switch (this.logMode)
            {
                case LogMode.NONE:
                    break;
                case LogMode.CONSOLE:
                    Console.WriteLine(this.name + toLog);
                    break;
                case LogMode.EXTERNAL:
                    SaveToFile(this.name + toLog, userDirectory);
                    break;
                case LogMode.CURRENT_FOLDER:
                    SaveToFile(this.name + toLog, AppDomain.CurrentDomain.BaseDirectory);
                    break;
                case LogMode.TEMP_FOLDER:
                    SaveToFile(this.name + toLog, Path.GetTempPath() + "\\" +
                        System.Windows.Application.Current.ToString().Split('.')[0]);
                    break;
                default:
                    break;
            }

            switch (this.alertMode)
            {
                case AlertMode.NONE:
                    break;
                case AlertMode.CONSOLE:
                    Console.WriteLine(this.name + msg);
                    break;
                case AlertMode.MESSAGE_BOX:
                    MessageBox.Show(this.name + msg);
                    break;
                case AlertMode.MESSAGE_BOX_CUSTOM:
                    MessageBoxResult result = MessageBox.Show(this.name + msg + MESSAGE, ALERTTITLE, MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (result == MessageBoxResult.Yes)
                    {
                        ReLog(toLog, LogMode.TEMP_FOLDER, AlertMode.NONE);
                    }
                    break;
                case AlertMode.TOAST:
                    break;
                case AlertMode.OVERLAY:
                    break;
                default:
                    break;
            }
        }

        private void ReLog(string toLog, LogMode logMode, AlertMode alertMode)
        {
            AlertMode tempAlert = this.alertMode;
            LogMode tempLog = this.logMode;
            this.alertMode = alertMode;
            this.logMode = logMode;
            this.Log(toLog);
            this.alertMode = tempAlert;
            this.logMode = tempLog;
        }

        private void SaveToFile(string toLog, string userDirectory)
        {
            Directory.CreateDirectory(userDirectory);

            TextWriter file = new StreamWriter(
                userDirectory +
                "\\current_logs", true, UTF8Encoding.UTF8);
            file.WriteLineAsync(System.Windows.Application.Current + " " + DateTime.Now + ": " + toLog);
            file.Close();
        }

        public void Log(Exception toLog, String msg = null, String userDirectory = null)
        {
            worker.Enqueue(() =>
            {
                InnerLog("Message: " + toLog.Message + "\nStacktrace: " + toLog.StackTrace, msg, userDirectory);
                
            }, reseter);
        }

        public void Log(Exception toLog, LogMode logMode, AlertMode alertMode, String msg = null, String userDirectory = null)
        {
            worker.Enqueue(() =>
            {
                ReLog("Message: " + toLog.Message + "\nStacktrace: " + toLog.StackTrace, logMode, alertMode);
                
            }, reseter);
        }
    }
}
