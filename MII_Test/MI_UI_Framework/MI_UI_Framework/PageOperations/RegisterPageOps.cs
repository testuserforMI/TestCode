using MI_UI_Framework.Page;
using MI_UI_Framework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_UI_Framework.PageOperations
{
    public class RegisterPageOps 
    {
        private static RegisterPage page;
         
        public RegisterPageOps()
        {
            page = new RegisterPage();
            PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, page);
        }

        public void SetUserName(string userName)
        {
            FrameworkBase.Log.LogMessage(string.Format("Setting user name field with value {0}", userName));
            Operations.SetTextIntoTextBox(page.UserName, userName);
        }

        public void SetPassword(string password)
        {
            FrameworkBase.Log.LogMessage(string.Format("Setting password field with value {0}", password));
            Operations.SetTextIntoTextBox(page.Password, password);
        }

        public void SetConfirmPassword(string confirmPassword)
        {
            FrameworkBase.Log.LogMessage(string.Format("Setting confirm password field with value {0}", confirmPassword));
            Operations.SetTextIntoTextBox(page.ConfirmPassword, confirmPassword);
        }

        public void ClickRegister()
        {
            FrameworkBase.Log.LogMessage("Clicking register button");
            Operations.ClickButton(page.Register);
        }

        public Register RegisterNewUser(string userName = "", string password = "")
        {
            FrameworkBase.Log.LogMessage("Registering new user");
            Register newuser = new Register();
            if (string.IsNullOrEmpty(userName))
            {
                userName = StringUtilities.GetRandomPlainString(5);
            }

            if (string.IsNullOrEmpty(password))
            {
                password = StringUtilities.GetRandomPlainString(7);
            }

            SetUserName(userName);
            SetPassword(password);
            SetConfirmPassword(password);
            ClickRegister();

            newuser.UserName = userName;
            newuser.Password = password;

            return newuser;
        }

        public List<string> GetErrorMessages()
        {
            List<string> errormessages = new List<string>();

            foreach (var item in page.ErrorControl.FindElements(By.TagName("li")))
            {
                errormessages.Add(Operations.GetElementInnerText(item));
            }

            return errormessages;
        }

        public bool IsUserNameFieldVisible()
        {
            return Operations.IsElementVisible(page.UserName);
        }
    }

    public class Register
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
