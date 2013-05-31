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
    internal class HomePage 
    {
        internal IWebElement Register
        {
            get
            {
                return Elements.GetElementByID("registerLink");
            }
        }

        internal IWebElement LoggedInUser
        {
            get
            {
                return Elements.GetElementByXPath("//a[@class='username']");
            }
        }

        internal IWebElement Login
        {
            get
            {
                return Elements.GetElementByID("loginLink");
            }
        }
        
        internal IWebElement LogOff
        {
            get
            {
                return Elements.GetElementByLinkedText("Log off");
            }
        }

        internal IWebElement PageSection
        {
            get
            {
                return Elements.GetElementByCSSSelector("p");
            }
        }

        internal IWebElement Home
        {
            get
            {
                return Elements.GetElementByXPath("//li[@id='liHome']/a");
            }
        }
    }
}
