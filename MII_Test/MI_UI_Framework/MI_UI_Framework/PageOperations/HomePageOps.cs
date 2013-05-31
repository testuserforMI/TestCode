using MI_UI_Framework.Page;
using MI_UI_Framework.Utilities;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_UI_Framework.PageOperations    
{
    public class HomePageOps
    {
        private static HomePage page; 

        public HomePageOps()
        {
            page = new HomePage();
            PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, page);
        }

        public void ClickRegisterLink()
        {
            FrameworkBase.Log.LogMessage("Clicking Register link in home page");
            Operations.ClickElement(page.Register);
        }

        public string GetLoggedInUserName()
        {
            FrameworkBase.Log.LogMessage("Getting logged in username");
            string username = Operations.GetElementInnerText(page.LoggedInUser);
            FrameworkBase.Log.LogMessage(string.Format("Logged in UserName : {0}", username));
            return username;
        }

        public bool IsLoginLinkVisible()
        {
            return Operations.IsElementVisible(page.Login);
        }

        public void ClickLogin()
        {
            FrameworkBase.Log.LogMessage("Checking whether the login link is visible or not");
            Operations.ClickElement(page.Login);
        }

        public void ClickLogOff()
        {
            FrameworkBase.Log.LogMessage("Clicking log off link");
            Utils.Wait(5, page.LogOff);
            Operations.ClickElement(page.LogOff);
        }

        public bool IsArticleSectionVisible()
        {
            return Operations.IsElementVisible(page.PageSection);
        }

        public string GetHomeLinkFontStyle()
        {
            return Operations.GetCSSFontStyle(page.Home);
        }

        public string GetHomeLinkFontColor()
        {
            return Operations.GetCSSFontColor(page.Home);
        }

        public string GetHomeLinkFontSize()
        {
            return Operations.GetCSSFontSize(page.Home);
        }

        public string GetHomeLinkFontWeight()
        {
            return Operations.GetCSSFontWeight(page.Home);
        }

        public string GetHomeLinkTextTransform()
        {
            return Operations.GetCSSTextTransform(page.Home);
        }

        public bool IsRegisterLinkVisible()
        {
            return Operations.IsElementVisible(page.Register);
        }
    }
}
