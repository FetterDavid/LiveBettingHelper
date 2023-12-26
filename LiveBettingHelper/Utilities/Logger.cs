using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBettingHelper.Utilities
{
    public class Logger : INotifyPropertyChanged
    {
        private const int MAX_LOG_IN_MEMORY = 1000;
        public Queue<string> LogsInMemory = new Queue<string>();
        public double Progress { get; set; }
        private string _logFilePath = Path.Combine(FileSystem.AppDataDirectory, "logs.txt");

        public event PropertyChangedEventHandler PropertyChanged;

        public void Info(string message)
        {
            SaveLogInFile("I", message);
            SaveLogInMemory("I", message);
            ShowLogInDebug("I", message);
        }

        public void Debug(string message)
        {
            SaveLogInFile("D", message);
            SaveLogInMemory("D", message);
            ShowLogInDebug("D", message);
        }

        public void Warning(string message)
        {
            SaveLogInFile("Warning", message);
            SaveLogInMemory("Warning", message);
            ShowLogInDebug("Warning", message);
        }

        public void Exception(Exception e, string message = "")
        {
            SaveLogInFile("Ex", message + " " + e.Message);
            SaveLogInMemory("Ex", message + " " + e.Message);
            ShowLogInDebug("Ex", message + " " + e.Message);
        }

        public void Error(string message)
        {
            SaveLogInFile("E", message);
            SaveLogInMemory("E", message);
            ShowLogInDebug("E", message);
        }

        public void Fatal(string message)
        {
            SaveLogInFile("F", message);
            SaveLogInMemory("F", message);
            ShowLogInDebug("F", message);
        }

        private void SaveLogInFile(string logType, string message)
        {
            using (StreamWriter sw = new StreamWriter(_logFilePath))
            {
                sw.WriteLine($"{logType}|{DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss")}|{message}");
            }
        }
        /// <summary>
        /// Beallitja a folyamat %-os elkeszultsegi statuszat
        /// </summary>
        public void SetProgress(int current, int max) => SetProgress(100.0 * current / max);
        /// <summary>
        /// Beallitja a folyamat %-os elkeszultsegi statuszat
        /// </summary>
        /// <param name="progress">-1, ha kikapcsolja </param>
        public virtual void SetProgress(double progress)
        {
            if (Progress < 0 || progress < 0 || Math.Abs(Progress - progress) >= 0.25) // optimalizalunk, negyed szazaleknal kisebb elterest kihagyjuk
            {
                Progress = progress;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Progress)));
            }
        }

        private void SaveLogInMemory(string logType, string message)
        {
            if (LogsInMemory.Count() >= MAX_LOG_IN_MEMORY) LogsInMemory.Dequeue();
            LogsInMemory.Enqueue($"{logType}|{DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss")}|{message}");
        }

        private void ShowLogInDebug(string logType, string message)
        {
            System.Diagnostics.Debug.WriteLine($"{logType}|{DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss")}|{message}");
        }
    }
}
