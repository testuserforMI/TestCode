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
    public class CompetitionPageOps : CompetitionListOps  
    {
        private static CompetitionPage page;

        public CompetitionPageOps()
        {
            page = new CompetitionPage();
            PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, page);
        }

        public void ClickCompetitionLink()
        {
            FrameworkBase.Log.LogMessage("Clicking Competitions link");
            Operations.ClickElement(page.CompetitionLink);
            Utils.WaitForLoaderExit(MyCodaLabPage.CompetitionListLoader);
        }

        public bool IsFilterCompVisible()
        {
            FrameworkBase.Log.LogMessage("Checking whether the filter comp control is visible or not");
            return Operations.IsElementVisible(page.FilterCompContainer);
        }

        public string GetCompetitionLinkFontStyle()
        {
            FrameworkBase.Log.LogMessage("Getting competition link font style");
            string style = Operations.GetCSSFontStyle(page.CompetitionLink);
            FrameworkBase.Log.LogMessage(string.Format("Font Style: {0}", style));
            return style;
        }

        public string GetCompetitionLinkFontColor()
        {
            FrameworkBase.Log.LogMessage("Getting competition link font color");
            string style = Operations.GetCSSFontColor(page.CompetitionLink);
            FrameworkBase.Log.LogMessage(string.Format("Font Color: {0}", style));
            return style;
        }

        public string GetCompetitionLinkFontSize()
        {
            FrameworkBase.Log.LogMessage("Getting competition link font size");
            string size = Operations.GetCSSFontSize(page.CompetitionLink);
            FrameworkBase.Log.LogMessage(string.Format("Font Size: {0}", size));
            return size;
        }

        public string GetCompetitionLinkFontWeight()
        {
            FrameworkBase.Log.LogMessage("Getting competition link font weight");
            string weight = Operations.GetCSSFontWeight(page.CompetitionLink);
            FrameworkBase.Log.LogMessage(string.Format("Font weight: {0}", weight));
            return weight;
        }

        public string GetCompetitionLinkTextTransform()
        {
            return Operations.GetCSSTextTransform(page.CompetitionLink);
        }

        public void SelectCompetition(string title)
        {
            FrameworkBase.Log.LogMessage(string.Format("Selecting competition {0} in Competition page", title));
            int competitionIndex = GetCompetitionIndex(title);

            string xpath = string.Format("//div[@class='competitionTile'][{0}]/article/div[@class='articleTextArea']", competitionIndex);

            Operations.ClickElement(Elements.GetElementByXPath(xpath));
        }

        internal override void Initialize()
        {
            FrameworkBase.Log.LogMessage("Initializing Competitions list control in Competition page");
            base.index = -1;
            base.baseQueryPath = "";
        }
    }
}
