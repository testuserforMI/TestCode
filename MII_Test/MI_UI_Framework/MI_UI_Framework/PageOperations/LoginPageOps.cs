using MI_UI_Framework.Page;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_UI_Framework.PageOperations    
{
    public class LoginPageOps
    {
        private static LoginPage page;

        public LoginPageOps()
        {
            page = new LoginPage();
            PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, page);
        }

        public void SetUserName(string username)
        {
            FrameworkBase.Log.LogMessage(string.Format("Setting UserName : {0}", username));
            Operations.SetTextIntoTextBox(page.UserName, username);
        }

        public void SetPassword(string password)
        {
            FrameworkBase.Log.LogMessage(string.Format("Setting Password : {0}", password));
            Operations.SetTextIntoTextBox(page.Password, password);
        }

        public void ClickLogin()
        {
            FrameworkBase.Log.LogMessage("Clicking login button in Login page");
            Operations.ClickElement(page.Login);
        }

        public bool IsUserNameFiledVisible()
        {
            return Operations.IsElementVisible(page.UserName);
        }

        public void Login()
        {
            // These hard coding will be removed once we start accessing from data base
            string userName = ConfigurationManager.AppSettings["RegularUser"];
            string password = ConfigurationManager.AppSettings["RegularUserPassword"]; 

            Login(userName, password);
        }

        public void Login(string userName, string password)
        {
            Pages.HomePage.ClickLogin();
            SetUserName(userName);
            SetPassword(password);
            ClickLogin();
        }

        public List<string> GetErrorMessages()
        {
            List<string> errormessages = new List<string>();

            foreach (var item in page.LoginPageErrors)
            {
                string error = Operations.GetElementInnerText(item);
                if (!string.IsNullOrEmpty(error))
                {
                    errormessages.Add(error);
                }
            }

            return errormessages;
        }

        public string GetSummaryError()
        {
            return Operations.GetElementInnerText(page.SummaryError);
        }

        public void ClickRegisterLink()
        {
            FrameworkBase.Log.LogMessage("Clicking register link in the login window");
            Operations.ClickElement(page.RegisterLink);
        }
    }
}
