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
    internal class CompetitionPage 
    {
        internal IWebElement CompetitionLink
        {
            get
            {
                return Elements.GetElementByXPath("//li[@id='liCompetition']/a");
            }
        }

        internal IWebElement FilterCompContainer
        {
            get
            {
                return Elements.GetElementByXPath("//div[@class='container compBlockFilter']");
            }
        }

        internal IWebElement CompletitionList
        {
            get
            {
                return Elements.GetElementByXPath("//div[@class='container']");
            }
        }
    }
}
