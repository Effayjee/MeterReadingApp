using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingApp.Helpers
{
    public static class Logger
    {
        private const string LogFilePath = "errors.log";

        public static void LogError(string message)
        {
            try
            {
                File.AppendAllText(LogFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
            }
            catch { }
        }
    }
}
