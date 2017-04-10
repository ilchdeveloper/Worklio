using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace Worklio.Logger
{
    public interface ILogger
    {
        void Write(string logMessage);
        void Write(Exception ex);
    }
    public class Logger : ILogger
    {
        private static readonly object _syncObject = new object();
        private static readonly Lazy<ILogger> instance = new Lazy<ILogger>(() => new Logger(), LazyThreadSafetyMode.ExecutionAndPublication);
        private TextWriter _textWriter;
        private static DateTime _startLog;
        private static string _fileName;
        public static ILogger Instance => instance.Value;
        public static string SPath()
        {
            return ConfigurationManager.AppSettings["logPath"];
        }

        private Logger()
        {
            _startLog = DateTime.Now;
            _fileName = SPath() + @"\log_" + _startLog.ToString("MM-dd-yyyy") + ".log";
            if (!Directory.Exists(SPath()))
            {
                Directory.CreateDirectory(SPath());
            }

            _textWriter = TextWriter.Synchronized(File.AppendText(_fileName));
        }
        public void Write(string logMessage)
        {
            try
            {
                CreateFileIfDateChanged(DateTime.Now);
                Log(logMessage, _textWriter);
            }
            catch (IOException ex)
            {
                if(ex != null)
                    _textWriter.Close();
            }
        }

        private void Log(string logMessage, TextWriter w)
        {
            lock (_syncObject)
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.WriteLine("Message: {0}", logMessage);
                w.WriteLine("-------------------------------");
                w.Flush();
            }
        }
        private  void CreateFileIfDateChanged(DateTime logDate)
        {
            if (_startLog.Date == logDate.Date) { return; }
            _startLog = logDate;
             if (_textWriter != null)
            {
                _textWriter.Close();                
            }
            _textWriter = TextWriter.Synchronized(File.AppendText(SPath() + @"\log_" + _startLog.ToString("MM-dd-yyyy") + ".log"));
        }

        public void Write(Exception ex)
        {
            try
            {
                CreateFileIfDateChanged(DateTime.Now);
                Log(ex.Message, _textWriter);
            }
            catch (IOException exp)
            {
                if (exp != null)
                    _textWriter.Close();
            }
        }
    }
}