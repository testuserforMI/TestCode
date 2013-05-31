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
    internal class CompetitionCreatePage   
    {
        internal IList<IWebElement> Tabs
        {
            get
            {
                return Elements.GetElements(By.XPath("//div[@class='competitionProcessHeader']/ul/li/a"));
            }
        }

        internal IWebElement Next
        {
            get
            {
                return Elements.GetElementByCSSSelector("input[value=\"next\"]");
            }
        }

        internal IWebElement Save
        {
            get
            {
                return Elements.GetElementByID("btnSave");
            }
        }

        internal IWebElement Previous
        {
            get
            {
                return Elements.GetElementByCSSSelector("input[value=\"previous\"]");
            }
        }

        internal IWebElement UnsavedDataNotification
        {
            get
            {
                return Elements.GetElementByID("publishNotoficationIcon");
            }
        }
    }

    internal class MetaData
    {
        internal IWebElement Title
        {
            get
            {
                return Elements.GetElementByID("Title");
            }
        }

        internal IWebElement ShortDescription
        {
            get
            {
                return Elements.GetElementByID("Description");
            }
        }

        internal IWebElement UploadImage
        {
            get
            {
                return Elements.GetElementByID("uploadFile");
            }
        }
    }

    internal class Details
    {
        internal IWebElement Tabs
        {
            get
            {
                return Elements.GetElementByClassName("competitionsDetailTabTop");
            }
        }

        internal IWebElement LDSubTabs
        {
            get
            {
                return Elements.GetElementByID("textEditorLftTab");
            }
        }

        internal IWebElement SubTabsTextArea
        {
            get
            {
                return Elements.GetElementByID("textEditorTxtArea");
            }
        }

        internal IWebElement ParticipateSubTabs
        {
            get
            {
                return Elements.GetElementByID("CompetitionsDetailLftParticipateNav");
            }
        }

        internal IWebElement ParticipateTextArea
        {
            get
            {
                return Elements.GetElementByID("textEditorTxtArea1");
            }
        }

        internal IWebElement ApplyChanges
        {
            get
            {
                return Elements.GetElementByID("applyChanges");
            }
        }
    }

    internal class PhaseDetails
    {
        internal const string PhaseCustomXpath = "//div[@class='CompetitionTypeDetailrowDate']";
        internal const string PhaseTitle = "//input[contains(@class,'phaseTitle')]";
        internal const string PhaseStartDate = "//div[@class='dateSelectBlock'][1]/input[contains(@id,'StartDate')]";
        internal const string PhaseMaximumSubmission = "//div[@class='dateSelectBlock'][1]/input[contains(@id,'SubmissionLmt')]";
        internal const string ExpandButton = "//div[contains(@id,'datasetimg')]";
        internal const string CollapseButton = "//div[contains(@id,'datasetimg')]";
        internal const string AddDataSetButton = "//div[contains(@class,'phaseToggleBtn')]";
        internal const string EmptyDataSet = "//div[@id='downloadedContainer']/div[@class='phaseDatasetDetailsContainer']";

        internal const string PhasesClassName = "CompetitionTypeDetailrowDate";
        internal const string DSControlID = "downloadedContainer";
        internal const string DSTypeID = "selectOption";
        internal const string DSBlobURLID = "SourceUrl";
        internal const string DSAccessStringID = "SourceAccessInfo";
        internal const string DSDownloadURLID = "DownloadUrl";
        internal const string DSApplyID = "apply";
        internal const string DSDeleteID = "delete";
        internal const string DSUpdateID = "update";
        internal const string DSRemoveID = "remove";

        internal const string DSControl = "jQuery(\".CompetitionTypeDetailrowDate:eq({0}) .downloadedContainer .phaseDatasetDetails\").length";

        internal IWebElement CompetitionEndDate
        {
            get
            {
                return Elements.GetElementByID("competitionEndDate");
            }
        }
    }

    internal class Publish
    {
        internal IWebElement PublishTheCompetition
        {
            get
            {
                return Elements.GetElementByID("Public");
            }
        }

        internal IWebElement PublishChanges
        {
            get
            {
                return Elements.GetElementByID("btnPublish");
            }
        }
    }
}
