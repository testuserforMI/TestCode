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
    internal class CompetitionDetailsPage
    {
        internal IWebElement ParticipationStatus
        {
            get
            {
                return Elements.GetElementByID("registerCompetitionLabel");
            }
        }

        internal IWebElement AcceptRulesCheckBox
        {
            get
            {
                return Elements.GetElementByID("chkRegister");
            }
        }

        internal IWebElement Tabs
        {
            get
            {
                return Elements.GetElementByClassName("competitionsDetailTabTop");
            }
        }

        internal IWebElement TabSection
        {
            get
            {
                return Elements.GetElementByClassName("subContainerBlock");
            }
        }

        internal IWebElement LearnTheDetailsSubTabs
        {
            get
            {
                return Elements.GetElementByClassName("CompetitionsDetailLft");
            }
        }

        internal IList<IWebElement> TabsDataSection
        {
            get
            {
                return Elements.GetElements(By.ClassName("CompetitionsDetailRit"));
            }
        }

        internal IWebElement SeeTheResultTable
        {
            get
            {
                return Elements.GetElementByID("seeTheResults");
            }
        }

        internal IWebElement Register
        {
            get
            {
                return Elements.GetElementByID("registerCompetition");
            }
        }

        internal IWebElement LoginOrCreateAccount
        {
            get
            {
                return Elements.GetElementByXPath("//div[@class='blueRegButton']/a[1]");
            }
        }

        internal IWebElement SubmitResults
        {
            get
            {
                return Elements.GetElementByID("uploadFile");
            }
        }

        internal IWebElement ResultTable
        {
            get
            {
                return Elements.GetElementByID("resultSubmissionResults");
            }
        }

        internal IList<IWebElement> InternalResultTable
        {
            get
            {
                return Elements.GetElements(By.XPath("//table[@class='resultSubResultsContainer toggleTble']"));
            }
        }

        internal IList<IWebElement> DateOnCompStatusStrip
        {
            get
            {
                return Elements.GetElements(By.XPath("//section[@class='challStatusStripSection']/section/label"));
            }
        }

        internal IList<IWebElement> PhaseButtons
        {
            get
            {
                return Elements.GetElements(By.XPath("//section[@id='phaseToggleBtnContainerSubmitPage']/div/a"));
            }
        }

        internal IWebElement CompTitleForNoRecords
        {
            get
            {
                return Elements.GetElementByXPath("//section[@class='competitionTileNoRecord']/P");
            }
        }

        internal const string RowOptionsControlID = "preSubmissionToggle";
        internal const string SubmitToLeaderBoardClassName = "blueSubmitButton leaderboardUpdate";
        internal const string DeleteFromLeaderBoard = "blueSubmitButton leaderboardDelete";
        internal const string LinkBlock = "linkBlock";
        internal const string TickedClass = "ticked";
        internal const string CollapseClass = "expColl";
        internal const string TickedClassName = "leaderboardOk leaderboardVisible";
    }
}
