using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_UI_Framework.Page
{
    internal class LoginPage 
    {
        internal IWebElement UserName
        {
            get
            {
                return Elements.GetElementByID("UserName");
            }
        }

        internal IWebElement Password
        {
            get
            {
                return Elements.GetElementByID("Password");
            }
        }

        internal IWebElement Login
        {
            get
            {
                return Elements.GetElementByCSSSelector("input[type=\"submit\"]");
            }
        }


        internal IList<IWebElement> LoginPageErrors
        {
            get
            {
                return Elements.GetElements(By.ClassName("field-validation-error"));
            }
        }

        internal IWebElement SummaryError
        {
            get
            {
                return Elements.GetElementByClassName("validation-summary-errors");
            }
        }

        internal IWebElement RegisterLink
        {
            get
            {
                return Elements.GetElementByCSSSelector("p > a");
            }
        }
    }
}
