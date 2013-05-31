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
    internal class MyCodaLabPage 
    {
        internal IWebElement PageSection
        {
            get
            {
                return Elements.GetElementByCSSSelector("p");
            }
        }

        internal IWebElement MyCodaLabLink
        {
            get
            {
                return Elements.GetElementByXPath("//li[@id='liMycodeLab']/a");
            }
        }

        internal IWebElement FilterCompetitionByStatus
        {
            get
            {
                return Elements.GetElementByXPath("//select[@class='gradient'][0]");
            }
        }

        internal IWebElement FilterCompetitionByTechnique
        {
            get
            {
                return Elements.GetElementByXPath("//select[@class='gradient'][1]");
            }
        }

        internal IWebElement FilerCompetitionKeywords
        {
            get
            {
                return Elements.GetElementByXPath("//input[@class='gradient']");
            }
        }

        internal IWebElement CreateNewCompetition
        {
            get
            {
                return Elements.GetElementByXPath("//div[@class='grayGradientBtn compBlockFilter'][1]/a");
            }
        }

        internal IWebElement UploadDataList
        {
            get
            {
                return Elements.GetElementByXPath("//div[@class='createCopm compBlockFilter'][2]/a");
            }
        }

        internal IWebElement CreateCompetitionWizardStart
        {
            get
            {
                return Elements.GetElementByCSSSelector("input[type=\"submit\"]");
            }
        }

        internal IWebElement CompletitionList
        {
            get
            {
                return Elements.GetElementByXPath("//div[@class='container']");
            }
        }

        internal IWebElement DeleteCompetitionButton
        {
            get
            {
                return Elements.GetElementByCSSSelector("input[type=\"submit\"]");
            }
        }

        internal IWebElement BackToListInDeletePage
        {
            get
            {
                return Elements.GetElementByXPath("//div[@class='delButtonContainer']/a");
            }
        }

        internal IWebElement UserDetailsBlock
        {
            get
            {
                return Elements.GetElementByClassName("container compBlockFilter");
            }
        }

        internal IList<IWebElement> ParticipantsList
        {
            get
            {
                return Elements.GetElements(By.ClassName("competitionUserBlock"));
            }
        }

        internal IWebElement MyCodaLabPageTabs
        {
            get
            {
                return Elements.GetElementByClassName("myCodalabTabArea");
            }
        }

        internal IWebElement RejectionReasonTextArea
        {
            get
            {
                return Elements.GetElementByID("reason");
            }
        }

        internal IWebElement RejectionOptions
        {
            get
            {
                return Elements.GetElementByClassName("popupContainerBorder");
            }
        }

        internal const string CompetitionListLoader = "competitionTilePreload";
    }
}
