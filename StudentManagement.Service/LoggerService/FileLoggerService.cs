using StudentManagement.Service.Enums;

namespace StudentManagement.Service.LoggerService
{
    public class FileLoggerService : ILoggerService
    {
        private readonly string _today = DateTime.Now.ToString("dd-MM-yyyy");
        public void Log(string message, CustomLogLevel logLevel)
        {
            Directory.CreateDirectory("Logs");
            using (StreamWriter w = File.AppendText($@"Logs\{_today}_log.txt"))
            {
                Write(message, logLevel, w, null);
            }
        }

        public void Log(string message, CustomLogLevel logLevel, string? stackTrace)
        {
            Directory.CreateDirectory("Logs");
            using (StreamWriter w = File.AppendText($@"Logs\{_today}_log.txt"))
            {
                Write(message, logLevel, w, stackTrace);
            }
        }

        public void Write(string message, CustomLogLevel logLevel, TextWriter w, string? stackTrace)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  : LogLevel");
            w.WriteLine($"  :{logLevel}");
            w.WriteLine("  : StackTrace");
            w.WriteLine($"  :{stackTrace}");
            w.WriteLine("  : Message");
            w.WriteLine($"  :{message}");
            w.WriteLine("-------------------------------");
        }
    }
}
