using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserType = SeleniumHelper.Enums.BrowserType;
using Selenium;
using OpenQA.Selenium.Android;

namespace SeleniumHelper 
{
    public static class Browser   
    {
        private static ScreenShotRemoteWebDriver webDriver;

        private static ISelenium selenim;

        /// <summary>
        /// Gets the IWebDriver instance
        /// </summary>
        public static ScreenShotRemoteWebDriver WebDriver
        {
            get { return webDriver; }
        }

        public static ISelenium Selenium
        {
            get { return selenim; }
        }

        /// <summary>
        /// Method to start the application with the specified type of browser, platform and URL
        /// </summary>
        public static void Start(BrowserType browserType, PlatformType platformType, string serverUrl, string driverPath, string applicationURL)
        {
            string browser = browserType.ToString().ToLower();
            DesiredCapabilities compat;
            switch (browserType)
            {
                case BrowserType.InternetExplorer:
                    compat = DesiredCapabilities.InternetExplorer();
                    break;
                case BrowserType.Chrome:
                    compat = DesiredCapabilities.Chrome();
                    break;
                case BrowserType.Safari:
                    compat = DesiredCapabilities.Safari();
                    break;
                case BrowserType.Firefox:
                default:
                    compat = DesiredCapabilities.Firefox();
                    break;
            }
            
            compat.IsJavaScriptEnabled = true;
            compat.Platform = new Platform(platformType);
            webDriver = new ScreenShotRemoteWebDriver(compat);
            webDriver.Manage().Window.Maximize();
            webDriver.Url = applicationURL;
        }

        public static void Start(BrowserType btype, PlatformType ptype, string driverPath, string url)
        {
            selenim = new DefaultSelenium("localhost", 4444, btype.ToString(), url);

            Init(btype, url, ptype, driverPath);

            selenim.Start();

            selenim.WindowMaximize();

            webDriver.Manage().Window.Maximize();

            if (btype != BrowserType.Chrome && btype != BrowserType.Safari)
            {
                webDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(60000));
            }

            webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(60000));

            selenim.WaitForPageToLoad("120000");

            webDriver.Url = url;
            //selenim.Open(url);
        }

        private static void Init(BrowserType type, string url, PlatformType platform, string dirverPath)
        {
            BrowserInfo browserString = BrowserFinder.Find(type);
            DesiredCapabilities capabilities = new DesiredCapabilities();

            switch (type)
            {
                case BrowserType.InternetExplorer:

                    InternetExplorerOptions op = new InternetExplorerOptions();
                    op.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    op.InitialBrowserUrl = url;
                    op.AddAdditionalCapability("Platform", platform.ToString());

                    InternetExplorerDriverService services = InternetExplorerDriverService.CreateDefaultService(dirverPath);
                    services.Start();

                    //webDriver = new InternetExplorerDriver(services, op);
                    break;

                case BrowserType.Firefox:
                    capabilities = DesiredCapabilities.Firefox();
                    capabilities.Platform = new Platform(platform);

                    //webDriver = new RemoteWebDriver(capabilities);

                    break;

                case BrowserType.Chrome:

                    ChromeDriverService service = ChromeDriverService.CreateDefaultService(dirverPath);

                    service.Start();

                    ChromeOptions opts = new ChromeOptions();
                    opts.AddAdditionalCapability("Platform", platform.ToString());

                    //webDriver = new ChromeDriver(service, opts);
                    break;
                case BrowserType.Android:
                    //webDriver = new AndroidDriver();
                    break;

                case BrowserType.Safari:
                    SafariDriverServer server = new SafariDriverServer();
                    server.Start();

                    SafariDriverExtension ext = new SafariDriverExtension();
                    ext.Install();

                    SafariOptions opt = new SafariOptions();
                    opt.AddAdditionalCapability("Platform", platform.ToString());

                    capabilities = DesiredCapabilities.Safari();
                    capabilities.Platform = new Platform(platform);

                    //webDriver = new SafariDriver(opt);

                    break;
                default:
                    capabilities = DesiredCapabilities.Firefox();
                    capabilities.Platform = new Platform(platform);
                    //webDriver = new RemoteWebDriver(DesiredCapabilities.Firefox());
                    break;
            }

            selenim = new WebDriverBackedSelenium(webDriver, url);
        }

        public static void StopDriver()
        {
            try
            {
                WebDriver.Close();

                webDriver.Quit();
            }
            catch
            {
                // Not throwing any exception as this method is been called in test cleanup. And test case should not fail because of cleanup
            }
        }

        internal class BrowserInfo
        {
            public string ExeName { get; set; }

            public string ExePath { get; set; }

            public override string ToString()
            {
                if (!string.IsNullOrEmpty(ExePath))
                {
                    return string.Format("*{0} {1}", ExeName, ExePath);
                }
                else
                {
                    return string.Format("*{0}", ExeName);
                }
            }
        }

        internal class BrowserFinder
        {
            public static BrowserInfo Find(BrowserType type)
            {
                BrowserInfo info = new BrowserInfo();

                switch (type)
                {
                    case BrowserType.InternetExplorer:

                        info.ExeName = "iexplore";

                        break;

                    case BrowserType.Firefox:

                        info.ExeName = "firefox";

                        break;

                    case BrowserType.Chrome:

                        info.ExeName = "googlechrome";

                        info.ExePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

                        //TDO: Need to find the path from registry key
                        break;
                    case BrowserType.Safari:
                        info.ExeName = "safari";
                        break;
                }

                return info;
            }
        }

        public static T ExecuteJavaScriptAndGetValue<T>(this ScreenShotRemoteWebDriver driver, string script)
        {
            object v = driver.ExecuteScript(string.Format("return {0}", script));
            return (T)v;
        }

        public static void ExecuteJavaScript(this ScreenShotRemoteWebDriver driver, string script)
        {
            driver.ExecuteScript(script);
        }
    }

    public class ScreenShotRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
    {
        public ScreenShotRemoteWebDriver(ICapabilities capabilities)
            : base(capabilities)
        {
        }

        public ScreenShotRemoteWebDriver(Uri RemoteAdress, ICapabilities capabilities)
            : base(RemoteAdress, capabilities)
        {
        }

        /// <summary>
        /// Gets a <see cref="Screenshot"/> object representing the image of the page on the screen.
        /// </summary>
        /// <returns>A <see cref="Screenshot"/> object containing the image.</returns>
        public Screenshot GetScreenshot()
        {
            // Get the screenshot as base64.
            Response screenshotResponse = this.Execute(DriverCommand.Screenshot, null);
            string base64 = screenshotResponse.Value.ToString();

            // ... and convert it.
            return new Screenshot(base64);
        }
    }
}
