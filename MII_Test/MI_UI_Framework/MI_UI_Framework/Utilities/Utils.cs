using OpenQA.Selenium;
using SeleniumHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MI_UI_Framework.Utilities 
{
    public class Utils
    {
        public delegate void ExectionSteps();

        public delegate bool ExpectedPageSteps();

        public static void Wait(int seconds, IWebElement element)
        {
            for (int i = 0; i < seconds; i++)
            {
                if (Operations.IsElementVisible(element))
                {
                    break;
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        public static void WaitForLoaderExit(string loaderClassName, int seconds = 10)
        {
            IWebElement element = Elements.GetElementByClassName(loaderClassName);
            for (int i = 0; i < seconds; i++)
            {
                try
                {
                    if (element.Displayed)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                }
            }
        }

        public static bool RetryOperation(ExectionSteps executionSteps, ExpectedPageSteps expected, int retries)
        {
            for (int i = 0; i < retries; i++)
            {
                executionSteps();

                if (expected())
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetControlStyle(IWebElement element, ControlStyle style)
        {
            return element.GetCssValue(GetEnumDescription(style));
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public enum ControlStyle
        {
            [Description("font-family")]
            FontFamily,

            [Description("font-weight")]
            FontWeight,

            [Description("color")]
            FontColor,

            [Description("font-size")]
            FontSize,

            [Description("font-style")]
            FontStyle,

            [Description("text-transform")]
            TextTransform,

            [Description("background-image")]
            BackgroundImageURL
        }

        public static void GenerateTextImage(string imageText = "MyCodaLab")
        {
            Font f = new Font(FontFamily.GenericSerif, 50);
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(imageText, f);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(Color.Beige);

            //create a brush for the text
            Brush textBrush = new SolidBrush(Color.Black);

            drawing.DrawString(imageText, f, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();
            img.Save(string.Format("{0}.jpg", imageText), ImageFormat.Jpeg);
        }

        public static string GenerateResultFile(string path, string fileNameWithNoExtension)
        {
            path = string.Format("{0}\\{1}.txt", path, fileNameWithNoExtension);
            // Content of the method will be updated when the format of the result file is finalized
            TextWriter writer;
            StreamReader reader;
            if (!File.Exists(path))
            {
                FileStream f = File.Create(path);
                f.Close();
            }

            writer = File.AppendText(path);
            writer.WriteLine(StringUtilities.GetRandomPlainString(20));
            writer.Close();

            reader = File.OpenText(path);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
            }
            reader.Close();

            return path;
        }

        public static void SetSystemDate(string date)
        {
            string command = string.Format("date {0}", date);
            ProcessStartInfo procStartInfo = new ProcessStartInfo(
                                            "cmd.exe",
                                            "/c " + command);
            procStartInfo.UseShellExecute = true;
            procStartInfo.CreateNoWindow = true;
            procStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            procStartInfo.Verb = "runas";
            procStartInfo.Arguments = "/env /user:" + "Administrator" + " cmd " + "/c " + command;

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
        }
    }

    public class Logger
    {
        private string loggerDirectoryPath = string.Empty;
        public Logger(string fileName, string logDirectory)
        {
            if (!logDirectory.EndsWith("\\"))
            {
                logDirectory = logDirectory + "\\";
            }

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            loggerDirectoryPath = string.Format("{0}Logger\\", logDirectory);
            if (!Directory.Exists(loggerDirectoryPath))
            {
                Directory.CreateDirectory(loggerDirectoryPath);
            }

            string LogFile = string.Format("{0}{1}.txt", loggerDirectoryPath, fileName);

            SeleniumHelper.Logger.FilePath = LogFile;
        }

        public void LogMessage(string message)
        {
            SeleniumHelper.Logger.LogMessage(message);
        }

        public void CaptureImage(string imageName)
        {
            string imageDirectoryPath = string.Format("{0}\\Screenshots\\", loggerDirectoryPath);

            if (!Directory.Exists(imageDirectoryPath))
            {
                Directory.CreateDirectory(imageDirectoryPath);
            }

            SeleniumHelper.Operations.CaptureScreenShot(string.Format("{0}{1}", imageDirectoryPath, imageName));
        }
    }
} 
