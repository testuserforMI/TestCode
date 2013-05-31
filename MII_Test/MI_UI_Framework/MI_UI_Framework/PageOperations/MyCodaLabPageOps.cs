using MI_UI_Framework.Page;
using MI_UI_Framework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_UI_Framework.PageOperations 
{
    public class MyCodaLabPageOps : CompetitionListOps   
    {
        private static MyCodaLabPage page;
 
        public MyCodaLabPageOps()
        {
            page = new MyCodaLabPage();
            PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, page);
        }

        public void ClickMyCodaLabLink(bool checkLoader = true)
        {
            FrameworkBase.Log.LogMessage("Clicking MyCodaLab link");
            Operations.ClickElement(page.MyCodaLabLink);
            if (checkLoader)
            {
                Utils.WaitForLoaderExit(MyCodaLabPage.CompetitionListLoader);
            }
        } 

        public bool IsPageSectionVisible()
        {
            FrameworkBase.Log.LogMessage("Checking whether the mycodalab page section is visible");
            return Operations.IsElementVisible(page.PageSection);
        }

        public string GetMyCodaLabLinkStyle(MI_UI_Framework.Utilities.Utils.ControlStyle style)
        {
            FrameworkBase.Log.LogMessage("Getting mycodalab link style");
            string stylevalue = Utils.GetControlStyle(page.MyCodaLabLink, style);
            FrameworkBase.Log.LogMessage(string.Format("{0}: {1}", style.ToString(), stylevalue));
            return stylevalue;
        }

        public void ClickDeleteButton()
        {
            FrameworkBase.Log.LogMessage("Clicking delete button");
            Operations.ClickButton(page.DeleteCompetitionButton);
        }

        public void DelteCompetition(string competitionTitle)
        {
            SelectCompetitionOptionlink(CompetitionOptionLinks.Delete, competitionTitle);
            ClickDeleteButton();
        }

        public void ClickBackToListInDeletePage()
        {
            Operations.ClickElement(page.BackToListInDeletePage);
        }

        public bool IsCreateNewCompetitionButtonVisible()
        {
            FrameworkBase.Log.LogMessage("Checking whether the create new competition button is visible or not");
            return Operations.IsElementVisible(page.CreateNewCompetition);
        }

        public void ClickCreateNewCompetitionLink()
        {
            FrameworkBase.Log.LogMessage("Clicking create new competition button");
            Operations.ClickElement(page.CreateNewCompetition);
        }

        public void ClickStartInWizard()
        {
            FrameworkBase.Log.LogMessage("Clicking start button in the competition creation wizard");
            Operations.ClickElement(page.CreateCompetitionWizardStart);
        }

        public CompetitionCreatePageOps CreatePage
        {
            get
            {
                return new CompetitionCreatePageOps();
            }
        }

        public List<CompetitionDetails> CreateCompetition(int numberOfCompetition)
        {
            FrameworkBase.Log.LogMessage(string.Format("Creating {0} competitions", numberOfCompetition));
            List<CompetitionDetails> details = new List<CompetitionDetails>();

            if (ConfigurationManager.AppSettings["UseUI"] == "1")
            {
                for (int i = 0; i < numberOfCompetition; i++)
                {
                    details.Add(CreateCompetition(isPublic:true));
                }
            }
            else
            {
                details = DBUtils.InsertCompetitions(numberOfCompetition);
            }

            ClickMyCodaLabLink();

            return details;
        }

        public CompetitionDetails CreateCompetition(string title = "", string description = "", string createdBy = "", bool isPublic =false)
        {
            FrameworkBase.Log.LogMessage("Creating new Competition");
            
            ClickMyCodaLabLink();
            if (string.IsNullOrEmpty(title))
            {
                title = StringUtilities.GetRandomPlainString(10);
            }

            if (string.IsNullOrEmpty(description))
            {
                description = StringUtilities.GetRandomPlainString(30);
            }

            if (string.IsNullOrEmpty(createdBy))
            {
                createdBy = Pages.HomePage.GetLoggedInUserName();
            }

            ClickCreateNewCompetitionLink();
            ClickStartInWizard();
            CreatePage.MetaDataPage.SetTitle(title);
            CreatePage.MetaDataPage.SetShortDescription(description);
            CreatePage.ClickSave();
            CreatePage.ClickNext();
            CreatePage.PhaseDetailsPage.SetPhaseStartDate(DateTime.Now.Date.ToString("MM/dd/yyyy"), 1);
            CreatePage.ClickSave();
            CreatePage.ClickNext();
            CreatePage.ClickNext();
            if (isPublic)
            {   
                CreatePage.PublishPage.CheckPublishCompetition();
            }
            CreatePage.ClickFinish();

            FrameworkBase.Log.LogMessage("Competition created with the below details");
            FrameworkBase.Log.LogMessage(string.Format("Title : {0}", title));
            FrameworkBase.Log.LogMessage(string.Format("Description : {0}", description));

            return new CompetitionDetails() { Title = title, CreatedBy = createdBy, Description = description, IsPublic = isPublic };
        }

        public CompetitionDetails CreateCompetition(CompPhaseDetails phaseDetails, bool isPublic = true)
        {
            CompetitionDetails details = new CompetitionDetails();
            FrameworkBase.Log.LogMessage("Creating new Competition");
            if (!IsCreateNewCompetitionButtonVisible())
            {
                ClickMyCodaLabLink();
            }
        
            details.Title = StringUtilities.GetRandomPlainString(10);
            details.Description = StringUtilities.GetRandomPlainString(30);
            details.CreatedBy = Pages.HomePage.GetLoggedInUserName();

            ClickCreateNewCompetitionLink();
            ClickStartInWizard();
            CreatePage.MetaDataPage.SetTitle(details.Title);
            CreatePage.MetaDataPage.SetShortDescription(details.Description);
            CreatePage.ClickSave();
            CreatePage.ClickNext();
            CreatePage.WaitForTabSection(CompetitionCreatePageOps.Tab.PhaseDetails);

            CreatePage.PhaseDetailsPage.AddNewPhase(phaseDetails, 1);
            CreatePage.ClickSave();

            CreatePage.SelectTab(CompetitionCreatePageOps.Tab.Publish);
            if (isPublic)
            {
                CreatePage.PublishPage.CheckPublishCompetition();
                CreatePage.ClickFinish();
            }

            return details;
        }

        public void ClickUploadDataList()
        {
            FrameworkBase.Log.LogMessage("Clicking upload data list button");
            Operations.ClickElement(page.UploadDataList);
        }

        public List<string> GetSectionHeaders()
        {
            List<string> sectionheaders = new List<string>();

            foreach (var item in Elements.GetElements(By.XPath("//div[@class='container myCodalabBlock myCodalabTabContainer']/ul[1]/li")))
            {
                string s = Operations.GetElementInnerText(item);
                if (!string.IsNullOrEmpty(s))
                {
                    sectionheaders.Add(s);
                }
            }

            return sectionheaders;
        }

        public Dictionary<string, string> GetUserDetailsInMyCodaLabPage()
        {
            FrameworkBase.Log.LogMessage("Getting user details in MyCodaLab page");
            Dictionary<string, string> userDetails = new Dictionary<string, string>();

            userDetails.Add("Name", Operations.GetElementInnerText(page.UserDetailsBlock.FindElement(By.TagName("h3"))));

            userDetails.Add("FigureURL", Operations.GetBackgroundImageURL(page.UserDetailsBlock.FindElement(By.TagName("figure"))));

            userDetails.Add("Description", Operations.GetElementInnerText(page.UserDetailsBlock.FindElement(By.TagName("p"))));

            return userDetails;
        }

        private int FindParticipantIndex(string participantName)
        {
            int index = 0;

            for (int i = 1; i <= page.ParticipantsList.Count; i++)
            {
                string userName = Operations.GetElementInnerText(page.ParticipantsList[i].FindElement(By.TagName("h3")));
                if (StringUtilities.CompareStrings(userName, participantName, true, true))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public void UpdateParticipantStatus(string participantName, RequestTo status = RequestTo.Approve)
        {
            int index = FindParticipantIndex(participantName);

            if (index != 0)
            {
                List<IWebElement> options = page.ParticipantsList[index].FindElements(By.TagName("a")).ToList<IWebElement>();
                Operations.ClickElement(options[(int)status]);
            }
        }

        public enum RequestTo : int
        {
            Approve = 0,
            Reject = 1
        }

        public enum MyCodaLabTabs : int
        {
            CompetitionIManage = 0,
            CompetitionIParticipateIn = 1,
            MyDatasets = 2
        }

        public void SelectTab(MyCodaLabTabs tab)
        {
            FrameworkBase.Log.LogMessage(string.Format("Selecting tab {0} in MyCodaLab page", tab.ToString()));
            Operations.ClickElement(page.MyCodaLabPageTabs.FindElements(By.TagName("li"))[(int)tab]);
            Utils.WaitForLoaderExit(MyCodaLabPage.CompetitionListLoader);
        }

        private int GetParticipantIndex(string participantName)
        {
            List<ParticipantDetails> allParticipants = GetAllParticipants();
            int index = -1;
            foreach (var item in allParticipants)
            {
                if (StringUtilities.CompareStrings(item.ParticipantName, participantName, true, true))
                {
                    index = allParticipants.IndexOf(item);
                    break;
                }
            }

            return index + 1;
        }

        public List<ParticipantDetails> GetAllParticipants()
        {
            FrameworkBase.Log.LogMessage("Getting all participand details in MyCodaLabPage");
            List<ParticipantDetails> allparticipants = new List<ParticipantDetails>();

            if (page.ParticipantsList.Count > 0)
            {
                for (int i = 0; i <page.ParticipantsList.Count; i++)
                {
                    string userName = Operations.GetElementInnerText(page.ParticipantsList[i].FindElement(By.TagName("h3")));
                    string statusMessage = Operations.GetElementInnerText(page.ParticipantsList[i].FindElement(By.TagName("p")));
                    allparticipants.Add(new ParticipantDetails() { ParticipantName = userName, StatusMessage = statusMessage });
                }
            }

            return allparticipants;
        }

        public void ChangeStatus(RequestTo request, string participantName)
        {
            FrameworkBase.Log.LogMessage(string.Format("Changing the participant: {0} status to {1}", participantName, request.ToString()));
            int index = GetParticipantIndex(participantName);

            Operations.ClickElement(page.ParticipantsList[index - 1].FindElements(By.TagName("a"))[(int) request]);
        }

        public bool IsParticipantNamePresent(string participantName)
        {
            FrameworkBase.Log.LogMessage(string.Format("Checking whether the participant {0} is present or not", participantName));

            List<ParticipantDetails> allparticipants = GetAllParticipants();

            foreach (var item in allparticipants)
            {
                if (StringUtilities.CompareStrings(item.ParticipantName, participantName, true, true))
                {
                    FrameworkBase.Log.LogMessage("Participant was found");
                    return true;
                }
            }

            FrameworkBase.Log.LogMessage("Participant was not found");
            return false;
        }

        public void SelectCompetitionOptionlink(CompetitionOptionLinks link, string competitionTitle)
        {
            Initialize();
            FrameworkBase.Log.LogMessage(string.Format("Selecting competition link {0} options", link.ToString()));
            int index = GetCompetitionIndex(competitionTitle);
            string xpathForEdit = string.Format("//{0}div[@class='competitionTile'][{1}]/article/div[@class='articleTextArea']/div/div[@class='competitionActions']/a[{2}]", base.baseQueryPath, index, (int)link);
            IWebElement editLink = Elements.GetElementByXPath(xpathForEdit);
            Operations.ClickElement(editLink);
        }

        public string GetParticipantStatus(string competitionTitle)
        {
            FrameworkBase.Log.LogMessage("Getting participant status");
            Initialize();
            int index = GetCompetitionIndex(competitionTitle);
            string xpathForstatus = string.Format("//{0}div[@class='competitionTile'][{1}]/article/div[@class='articleTextArea']/div/div[@class='competitionActions']/label", base.baseQueryPath, index);
            IWebElement status = Elements.GetElementByXPath(xpathForstatus);
            return Operations.GetElementInnerText(status);
        }

        public void SetRejectionReason(string reason)
        {
            Operations.SetTextIntoTextBox(page.RejectionReasonTextArea, reason);
        }

        public void SelectRejectionOption(RejectionOption option)
        {
            Operations.ClickElement(page.RejectionOptions.FindElements(By.TagName("a"))[(int)option]);
        }

        internal override void Initialize()
        {
            FrameworkBase.Log.LogMessage("Initializing competition list in MyCodaLab page");
            List<IWebElement> elements = page.MyCodaLabPageTabs.FindElements(By.TagName("li")).ToList<IWebElement>();

            for (int i = 0; i < elements.Count; i++)
            {
                if (Operations.GetElementAttribute(elements[i], "class").Contains("highlightactive"))
                {
                    base.index = i + 1;
                    base.baseQueryPath = string.Format("ul[@class='myCodalabTabContent']/li[{0}]/", base.index);
                    break;
                }
            }
        }

        public enum CompetitionOptionLinks : int
        {
            Delete = 1,
            Edit = 2,
            Participants = 3
        }

        public enum RejectionOption : int
        {
            Ok = 1,
            Cancel = 0
        }
    }

    public class CompetitionInList
    {
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
    }

    public class CompetitionDetails
    {
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public List<CompPhaseDetails> Phases { get; set; }
    }

    public class ParticipantDetails
    {
        public string ParticipantName { get; set; }
        public string StatusMessage { get; set; }
    }

    public class CompPhaseDetails
    {
        public string Titile { get; set; }
        public string StartDate { get; set; }
        public int MaxSubmissions { get; set; }
        public List<DataSetDetails> DataSets { get; set; }
    }
        
    public class DataSetDetails
    {
        public MI_UI_Framework.PageOperations.PhaseDetailsPageOps.DataSetType DataSetType { get; set; }
        public string BlobURL { get; set; }
        public string AccessString { get; set; }
        public string DownloadURL { get; set; }
    }
}
