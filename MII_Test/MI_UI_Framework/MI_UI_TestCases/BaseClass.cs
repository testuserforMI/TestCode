using MI_UI_Framework;
using MI_UI_Framework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.IO;

namespace MI_UI_TestCases
{
    [TestClass]
    [DeploymentItem("Drivers","Drivers")]
    public class BaseClass     
    {
        private TestContext m_testContext;
        private static bool isInitailized = false;
        public TestContext TestContext
        {
            get { return m_testContext; }
            set { m_testContext = value; }
        }

        [TestInitialize]
        public void InitializeBeforeAllTest()
        {
            DirectoryInfo info = new DirectoryInfo(TestContext.TestRunDirectory);
            string folderName = info.Name;
            string loggerPath = string.Format("Logger\\{0}", folderName);

            FrameworkBase fb = new FrameworkBase();
            fb.SetLoggerPath(TestContext.TestName, string.Format("{0}{1}", Path.GetTempPath(), loggerPath));

            if (!isInitailized)
            {
                RegisterUser(ConfigurationManager.AppSettings["RegularUser"], ConfigurationManager.AppSettings["RegularUserPassword"]);
                RegisterUser(ConfigurationManager.AppSettings["UN1"], ConfigurationManager.AppSettings["UN1Password"]);
                RegisterUser(ConfigurationManager.AppSettings["UN2"], ConfigurationManager.AppSettings["UN2Password"]);
                isInitailized = true;
                UpdateConfig();
            }
        }

        private static void RegisterUser(string userName, string password)
        {
            if (!DBUtils.IsUserPresent(userName))
            {
                Browser.Start();
                Pages.HomePage.ClickRegisterLink();
                Pages.RegisterPage.RegisterNewUser(userName, password);
                Browser.StopDriver();
            }
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            Browser.StopDriver();
            FrameworkBase.Log = null;
        }

        public void UpdateConfig()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection settings = config.AppSettings.Settings;

            settings["DriverDirectory"].Value = string.Format("{0}\\Drivers\\", TestContext.TestDeploymentDir);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
        }

        public void LogException(Exception ex)
        {
            FrameworkBase.Log.LogMessage("-----------------------------------------------------------------------");
            FrameworkBase.Log.LogMessage("Exception was caught with the below reasons");
            FrameworkBase.Log.LogMessage(ex.StackTrace);
            FrameworkBase.Log.CaptureImage(TestContext.TestName);
        }
    }
}