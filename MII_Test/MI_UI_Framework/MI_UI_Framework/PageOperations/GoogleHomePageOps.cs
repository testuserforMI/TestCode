using MI_UI_Framework.Page;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_UI_Framework.PageOperations
{
    public class GoogleHomePageOps
    {
        private static GoogleHomePage page;

        static GoogleHomePageOps()
        {
            page = new GoogleHomePage();
            PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, page);
        }

        public void SetTextIntoSearchBox(string textToBeSearch)
        {
            Operations.SetTextIntoTextBox(page.SearchTextBox, textToBeSearch);
        }

        public void ClickSearchButton()
        {
            Operations.ClickElement(page.SearchButton);
        }

        public bool IsSearchGridVisible()
        {
            return Operations.IsElementVisible(page.SearchResultGrid);
        }
    }
}
