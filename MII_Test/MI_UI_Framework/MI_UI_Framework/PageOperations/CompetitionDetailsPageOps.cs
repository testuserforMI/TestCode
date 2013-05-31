using MI_UI_Framework.Page;
using MI_UI_Framework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_UI_Framework.PageOperations  
{
    public class CompetitionDetailsPageOps  
    {
        private static CompetitionDetailsPage page; 

        public CompetitionDetailsPageOps()
        {
            page = new CompetitionDetailsPage();
            PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, page);
        }

        public void SelectTab(Tabs tab)
        {
            FrameworkBase.Log.LogMessage(string.Format("Selecting tab {0} in Competition details page", tab.ToString()));
            Operations.ClickElement(page.Tabs.FindElements(By.TagName("li"))[(int)tab]);
        }

        public int SelectedTabIndex
        {
            get
            {
                int index = -1;

                FrameworkBase.Log.LogMessage("Getting the selected tab index in the competition details page");

                List<IWebElement> childs = page.Tabs.FindElements(By.TagName("li")).ToList<IWebElement>();

                foreach (var item in childs)
                {
                    if (StringUtilities.CompareStrings(Operations.GetElementAttribute(item, "class"), "active", true, true))
                    {
                        index = childs.IndexOf(item);
                        break;
                    }
                }

                FrameworkBase.Log.LogMessage(string.Format("Selected tab index is {0}", index));

                return index;
            }
        }

        public LearnTheDetails LearnTheDetailsPage
        {
            get
            {
                return new LearnTheDetails();
            }
        }

        public Participate ParticipatePage
        {
            get
            {
                return new Participate();
            }
        }

        public SeeTheResults SeeTheResultsPage
        {
            get
            {
                return new SeeTheResults();
            }
        }

        public bool IsCompetitionDetailsPageVisible()
        {
            FrameworkBase.Log.LogMessage("Checking whether the competition details page is visible or not");
            return page.Tabs.Displayed;
        }

        public enum Tabs : int
        {
            LearnTheDetails = 0,
            Participate = 1,
            SeeTheResults = 2
        }

        public string GetPhaseDateFromStatusStrip(int phaseIndex = 0)
        {
            string date = Operations.GetElementInnerText(page.DateOnCompStatusStrip[phaseIndex].FindElement(By.TagName("span")));
            FrameworkBase.Log.LogMessage(string.Format("Date in the Competition status strip for phase with index {0} is {1}", phaseIndex , date));
            return date;
        }
    }

    public class LearnTheDetails
    {
        private static CompetitionDetailsPage page;

        internal LearnTheDetails()
        {
            page = new CompetitionDetailsPage();
            PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, page);
        }

        public void SelectSubTab(int tabIndex)
        {
            FrameworkBase.Log.LogMessage(string.Format("Selecting the sub tab in learn the details page with index {0}", tabIndex));
            Operations.ClickElement(page.LearnTheDetailsSubTabs.FindElements(By.TagName("li"))[tabIndex]);
            Utils.WaitForLoaderExit("preloader");
        }

        private int GetSelectedSubTabIndex()
        {
            int index = -1;
            List<IWebElement> childs = page.LearnTheDetailsSubTabs.FindElements(By.TagName("li")).ToList<IWebElement>();

            foreach (var item in childs)
            {
                if (StringUtilities.CompareStrings(Operations.GetElementAttribute(item, "class"), "active", true, true))
                {
                    index = Convert.ToInt32(Operations.GetElementAttribute(item, "tabIndex"));
                    break;
                }
            }

            return index;
        }

        private string GetSelectedSubTabID()
        {
            return string.Format("tab{0}_{0}", GetSelectedSubTabIndex());
        }

        public string GetSelectedTabName()
        {
            int index = GetSelectedSubTabIndex();

            return Operations.GetElementInnerText(page.LearnTheDetailsSubTabs.FindElements(By.TagName("li"))[index]);
        }

        public List<string> GetAllSubTabNames()
        {
            FrameworkBase.Log.LogMessage("Getting all the sub tab names in learn the details page");
            List<string> subtabs = new List<string>();

            foreach (var item in page.LearnTheDetailsSubTabs.FindElements(By.TagName("li")))
            {
                string text = Operations.GetElementInnerText(item);

                if (!string.IsNullOrEmpty(text))
                {
                    subtabs.Add(text);
                }
            }
            FrameworkBase.Log.LogMessage("Sub tabs are:");
            foreach (var s in subtabs)
            {
                FrameworkBase.Log.LogMessage(s);
            }
            return subtabs;
        }

        public List<string> SubTabData
        {
            get
            {
                List<string> data = new List<string>();

                IWebElement ele = page.TabsDataSection[new CompetitionDetailsPageOps().SelectedTabIndex].FindElement(By.Id(GetSelectedSubTabID()));

                foreach (var item in ele.FindElements(By.TagName("p")))
                {
                    data.Add(Operations.GetElementInnerText(item));
                }

                return data;
            }
        }
    }

    public class Participate
    {
        private static CompetitionDetailsPage page;

        internal Participate()
        {
            page = new CompetitionDetailsPage();
            PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, page);
        }

        public void RegisterForChallange()
        {
            FrameworkBase.Log.LogMessage("Registering for challenge");
            new CompetitionDetailsPageOps().SelectTab(CompetitionDetailsPageOps.Tabs.Participate);
            CheckAcceptRules();
            ClickRegister();
            System.Threading.Thread.Sleep(3000);
        }

        public void ClickRegister()
        {
            FrameworkBase.Log.LogMessage("Clicking register button");
            Operations.ClickElement(page.Register);
        }

        public void CheckAcceptRules()
        {
            FrameworkBase.Log.LogMessage("Selecting accept the rules check box");
            Operations.CheckCheckBox(page.AcceptRulesCheckBox);
        }

        public bool IsRegisteringPending()
        {
            FrameworkBase.Log.LogMessage("Checking whether the registration is pending or not");
            string innerText = RegistrationStatus;

            return StringUtilities.CompareStrings(innerText, "Your registration is pending approval", true, true);
        }

        public string RegistrationStatus
        {
            get
            {
                return Operations.GetElementInnerText(page.ParticipationStatus);
            }
        }

        public void ClickLoginOrCreateAccount()
        {
            FrameworkBase.Log.LogMessage("Clicking login or create account button");
            Operations.ClickElement(page.LoginOrCreateAccount);
        }

        public void SelectSubTab(SubTabs tab)
        {
            FrameworkBase.Log.LogMessage(string.Format("Selecting the sub tab {0} in participate page", tab.ToString()));
            int tabIndex = new CompetitionDetailsPageOps().SelectedTabIndex;
            string query = string.Format("//div[@id='tabParticipantContainer']/div[{0}]/nav/ul/li[{1}]", tabIndex, (int)tab);
            Operations.ClickElement(Elements.GetElementByXPath(query));
            Utils.WaitForLoaderExit("preloader");
        }

        public void UploadResults(string filePath)
        {
            FrameworkBase.Log.LogMessage(string.Format("Uploading result file from {0} path", filePath));
            SeleniumHelper.Browser.WebDriver.ExecuteScript("document.getElementById(\"uploadFile\").removeAttribute(\"class\")");
            page.SubmitResults.SendKeys(filePath);
            Utils.WaitForLoaderExit("preloaderInputImg", 60);
        }

        public enum SubTabs : int
        {
            GetData = 1,
            SubmitResults = 2
        }

        private IWebElement GetInnertableRow(string nameOfTheFile)
        {
            int rowIndex = 0;

            List<Dictionary<string, string>> allrows = new List<Dictionary<string, string>>();

            foreach (var item in page.InternalResultTable)
            {
                allrows.Add(Operations.GetTableRowData(item, rowIndex));
            }

            rowIndex = allrows.FindIndex(t => t.Values.Contains(nameOfTheFile));

            IWebElement element = page.InternalResultTable[rowIndex];
            return element;
        }

        public void ExpandOrCollapseRow(string nameOfTheFile)
        {
            FrameworkBase.Log.LogMessage("Selecing expand collapse option of the row");
            IWebElement element = GetInnertableRow(nameOfTheFile);
            Operations.ClickElement(element.FindElement(By.ClassName(CompetitionDetailsPage.CollapseClass)));
        }

        public void SelectResultSubmissionOptions(ResultOption option, string fileName)
        {
            FrameworkBase.Log.LogMessage(string.Format("Clicking submit to leaderboard button of the row with search string {0}", fileName));
            IWebElement element = GetInnertableRow(fileName);
            Operations.ClickElement(element.FindElement(By.XPath("..")).FindElement(By.ClassName("linkSubmitBtn")).FindElements(By.TagName("a"))[(int)option]);
        }

        public void SelectRowLink(RowLinks link, string nameOfTheFile)
        {
            FrameworkBase.Log.LogMessage(string.Format("Selecting row link option {0} with search string {1}", link.ToString(), nameOfTheFile));

            IWebElement element = GetInnertableRow(nameOfTheFile);
            Operations.ClickElement(element.FindElement(By.XPath("..")).FindElement(By.ClassName(CompetitionDetailsPage.LinkBlock)).
                FindElements(By.TagName("a"))[(int)link]);
        }

        public void UploadAndSubmitResultsToLeaderBoard(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            string fileName = info.Name;
            UploadResults(filePath);
            ExpandOrCollapseRow(fileName);
            SelectResultSubmissionOptions(ResultOption.SubmitToLeaderBoard, fileName);
        }

        public void DeleteResultFromLeaderBoard(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            string fileName = info.Name;
            ExpandOrCollapseRow(fileName);
            SelectResultSubmissionOptions(ResultOption.DeleteFromLeaderBoard, fileName);
        }

        public Dictionary<string, string> GetSubmittedRowOnLeaderBoard()
        {
            FrameworkBase.Log.LogMessage("Getting the result values which was submitted to leaderboard");
            Dictionary<string, string> rowValues = new Dictionary<string, string>();

            for (int i = 0; i < page.InternalResultTable.Count; i++)
            {
                string attr = Operations.GetElementAttribute(page.InternalResultTable[i].FindElement(By.ClassName(CompetitionDetailsPage.TickedClass)).
                 FindElement(By.TagName("div")), "class");

                if (StringUtilities.CompareStrings(attr, CompetitionDetailsPage.TickedClassName, true, true))
                {
                    rowValues = Operations.GetTableRowData(page.InternalResultTable[i], 0);
                }
            }

            return rowValues;
        }

        public void SelectPhase(string phaseTitle)
        {
            List<string> phaseTitles = PhaseButtonTitles();
            int index = phaseTitle.IndexOf(phaseTitle);
            Operations.ClickElement(page.PhaseButtons[index]);
        }

        private List<string> PhaseButtonTitles()
        {
            List<string> titles = new List<string>();

            foreach (var item in page.PhaseButtons)
            {
                titles.Add(Operations.GetElementInnerText(item));
            }

            return titles;
        }

        public string ErrorMessageForNoRecords
        {
            get
            {
                return Operations.GetElementInnerText(page.CompTitleForNoRecords);
            }
        }

        public enum RowLinks : int
        {
            ScoringDetails = 0,
            StandardOutput = 1,
            StandardError = 2
        }

        public enum ResultOption : int
        {
            SubmitToLeaderBoard = 0,
            DeleteFromLeaderBoard = 1
        }
    } 

    public class SeeTheResults
    {
        private static CompetitionDetailsPage page;

        internal SeeTheResults()
        {
            page = new CompetitionDetailsPage();
            PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, page);
        }

        public List<string> Columns
        {
            get
            {
                return Operations.GetColumnNames(page.SeeTheResultTable);
            }
        }

        public bool IsRowEntryPresent(string userName)
        {
            bool result = false;

            List<Dictionary<string, string>> allrows = new List<Dictionary<string, string>>();
            allrows = Operations.GetAllRows(page.SeeTheResultTable);

            foreach (var item in allrows)
            {
                foreach (KeyValuePair<string, string> entry in item)
                {
                    if (entry.Key.Equals("USERNAME"))
                    {
                        try
                        {
                            if (entry.Value.Equals(userName))
                            {
                                result = true;
                                break;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }

            return result;
        }

        public Dictionary<string, string> LeaderBoardResultDetails(string userName)
        {
            FrameworkBase.Log.LogMessage("Getting the result details from the leaderboard");
            int rowIndex = Operations.GetRowIndex(page.SeeTheResultTable, userName);
            return Operations.GetTableRowData(page.SeeTheResultTable, rowIndex);
        }
    }
}
