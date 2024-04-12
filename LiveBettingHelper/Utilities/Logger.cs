using LiveBettingHelper.Views.Popups;
using System.ComponentModel;

namespace LiveBettingHelper.Utilities
{
    public class Logger : INotifyPropertyChanged
    {
        /// <summary>
        /// A memoriában tartott logok maximum száma
        /// </summary>
        private const int MAX_LOG_IN_MEMORY = 1000;
        /// <summary>
        /// A memóriában tárolt logok sora
        /// </summary>
        public Queue<string> LogsInMemory = new Queue<string>();
        /// <summary>
        /// progress ablak fo szoveg
        /// </summary>
        public string Caption { get; protected set; }
        /// <summary>
        /// progress ablak kis szoveg
        /// </summary>
        public string SubCaption { get; protected set; }
        /// <summary>
        /// a folyamat 0 és 1 közötti elkeszultsegi statusza
        /// </summary>
        public double Progress { get; set; }
        /// <summary>
        /// Property changed handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// A lementett log file elérésiútvonala
        /// </summary>
        private string _logFilePath = Path.Combine(FileSystem.AppDataDirectory, "logs.txt");
        /// <summary>
        /// Logol egy info üzenetet
        /// </summary>
        public void Info(string message)
        {
            SaveLogInFile("I", message);
            SaveLogInMemory("I", message);
            ShowLogInDebug("I", message);
        }
        /// <summary>
        /// Logol egy debug üzenetet
        /// </summary>
        public void Debug(string message)
        {
            SaveLogInFile("D", message);
            SaveLogInMemory("D", message);
            ShowLogInDebug("D", message);
        }
        /// <summary>
        /// Logol egy figyelmeztetést
        /// </summary>
        public void Warning(string message)
        {
            SaveLogInFile("Warning", message);
            SaveLogInMemory("Warning", message);
            ShowLogInDebug("Warning", message);
        }
        /// <summary>
        /// Logol egy kivételt
        /// </summary>
        public void Exception(Exception e, string message = "")
        {
            SaveLogInFile("Ex", message + " " + e.Message);
            SaveLogInMemory("Ex", message + " " + e.Message);
            ShowLogInDebug("Ex", message + " " + e.Message);
            App.PopupManager.ShowPopup(new ErrorPopup(message + " " + e.Message));
        }
        /// <summary>
        /// Logol egy hibát
        /// </summary>
        public void Error(string message)
        {
            SaveLogInFile("E", message);
            SaveLogInMemory("E", message);
            ShowLogInDebug("E", message);
            App.PopupManager.ShowPopup(new ErrorPopup(message));
        }
        /// <summary>
        /// Logol egy végzetes hibát
        /// </summary>
        public void Fatal(string message)
        {
            SaveLogInFile("F", message);
            SaveLogInMemory("F", message);
            ShowLogInDebug("F", message);
        }
        /// <summary>
        /// Beallitja a folyamat %-os elkeszultsegi statuszat
        /// </summary>
        public void SetProgress(double current, double max) => SetProgress(current / max);
        /// <summary>
        /// Beallitja a folyamat %-os elkeszultsegi statuszat
        /// </summary>
        public virtual void SetProgress(double progress)
        {
            Progress = progress;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Progress)));
        }
        /// <summary>
        /// Beallitja (ha van) a progress ablak szoveget es torli a kiegeszito szoveget
        /// </summary>
        public virtual void SetCaption(string caption)
        {
            if (Caption != caption)
            {
                Caption = caption;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Caption)));
            }
            SetSubCaption("");
        }
        /// <summary>
        /// Beallitja (ha van) a progress ablak kicsi szoveget (a fo szoveg alatt megjeleno kiegeszito szoveget)
        /// </summary>
        public virtual void SetSubCaption(string subcaption)
        {
            if (SubCaption != subcaption)
            {
                SubCaption = subcaption;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SubCaption)));
            }
        }
        /// <summary>
        /// Elmenti a log-ot egy szöveges fájlba
        /// </summary>
        private void SaveLogInFile(string logType, string message)
        {
            using (StreamWriter sw = new StreamWriter(_logFilePath))
            {
                sw.WriteLine($"{logType}|{DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss")}|{message}");
                sw.Close();
            }
        }
        /// <summary>
        /// Elmenti a logot a LogsInMemory sorba hogy a memoriából könnyen elérhető legyen
        /// </summary>
        private void SaveLogInMemory(string logType, string message)
        {
            if (LogsInMemory.Count() >= MAX_LOG_IN_MEMORY) LogsInMemory.Dequeue();
            LogsInMemory.Enqueue($"{logType}|{DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss")}|{message}");
        }
        /// <summary>
        /// Kiírja a log-ot a console-ra
        /// </summary>
        private void ShowLogInDebug(string logType, string message)
        {
            System.Diagnostics.Debug.WriteLine($"{logType}|{DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss")}|{message}");
        }
    }
}
