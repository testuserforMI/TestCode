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
    internal class GoogleHomePage
    {
        [FindsBy(How = How.Id, Using = "gbqfq")]
        internal IWebElement SearchTextBox;

        [FindsBy(How = How.Id, Using = "gbqfb")]
        internal IWebElement SearchButton;

        [FindsBy(How = How.Id, Using = "center_col")]
        internal IWebElement SearchResultGrid;
    }
}
