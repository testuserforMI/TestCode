using OpenQA.Selenium;
using OpenQA.Selenium.Android;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using Selenium;
using System;
using System.Configuration;
using BrowserType = SeleniumHelper.Enums.BrowserType;

namespace MI_UI_Framework
{
    public class Browser
    {
        /// <summary>
        /// Method to start the application with the specified type of browser, platform and URL
        /// </summary>
        public static void Start()
        {
            BrowserType browser = BrowserType.Firefox;
            Enum.TryParse<BrowserType>(ConfigurationManager.AppSettings["BrowserType"], out browser);

            PlatformType ptype = PlatformType.Windows;
            Enum.TryParse<PlatformType>(ConfigurationManager.AppSettings["Platform"], out ptype);

            FrameworkBase.Log.LogMessage(string.Format("Starting {0} browser with platform as {1} & URL {2}....", browser.ToString(), ptype.ToString(), ConfigurationManager.AppSettings["ApplicationURL"]));
            //SeleniumHelper.Browser.Start(browser, ptype, ConfigurationManager.AppSettings["DriverDirectory"], ConfigurationManager.AppSettings["ApplicationURL"]);
            SeleniumHelper.Browser.Start(browser, ptype, "http://localhost:4444/wb/console", ConfigurationManager.AppSettings["DriverDirectory"], ConfigurationManager.AppSettings["ApplicationURL"]);
            FrameworkBase.Log.LogMessage("Browser is launched");
            FrameworkBase.Log.LogMessage("----------------------------------------------------------");
        }

        public static void StopDriver()
        {
            SeleniumHelper.Browser.StopDriver();
        }
    }
}
