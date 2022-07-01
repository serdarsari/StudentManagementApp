using StudentManagement.Service.Enums;

namespace StudentManagement.Service.LogService
{
    public interface ILoggerService
    {
        public void Log(string message, CustomLogLevel logLevel);
        void Log(string message, CustomLogLevel logLevel, string stackTrace);
        void Write(string message, CustomLogLevel logLevel, TextWriter w, string stackTrace);
    }
}
