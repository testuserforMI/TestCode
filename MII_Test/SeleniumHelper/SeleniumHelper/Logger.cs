using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumHelper
{
    public class Logger
    {
        private static string _logPath;
        private static TextWriter writer;
        private static StreamReader reader;

        public static string FilePath
        {
            get
            {
                return _logPath;
            }
            set
            {
                _logPath = value;
                if (!File.Exists(value))
                {
                    FileStream f = File.Create(value);
                    f.Close();
                }
            }
        }

        public static void LogMessage(string logMessage)
        {
            if (string.IsNullOrEmpty(Logger.FilePath))
            {
                throw new Exception("File path was not set");
            }

            writer = File.AppendText(Logger.FilePath);
            writer.WriteLine(logMessage);
            writer.Close();
            DumpLog();
        }

        private static void DumpLog()
        {
            reader = File.OpenText(FilePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
            }
            reader.Close();
        }
    }
}
