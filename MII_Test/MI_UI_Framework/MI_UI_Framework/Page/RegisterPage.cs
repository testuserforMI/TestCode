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
    internal class RegisterPage
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

        internal IWebElement ConfirmPassword
        {
            get
            {
                return Elements.GetElementByID("ConfirmPassword");
            }
        }

        internal IWebElement Register
        {
            get
            {
                return Elements.GetElementByXPath("//fieldset/input");
            }
        }

        internal IWebElement ErrorControl
        {
            get
            {
                return Elements.GetElementByXPath("//div[@class='validation-summary-errors']/ul");
            }
        }
    }
}
