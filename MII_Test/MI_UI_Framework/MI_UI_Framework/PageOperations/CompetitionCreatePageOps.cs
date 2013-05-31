using MI_UI_Framework.Page;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using MI_UI_Framework.Utilities;
using OpenQA.Selenium.Remote;
using System.IO;

namespace MI_UI_Framework.PageOperations
{
    public class CompetitionCreatePageOps
    {
        private static CompetitionCreatePage page; 
        public CompetitionCreatePageOps()
        {
            page = new CompetitionCreatePage();
            PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, page);
        }

        public MetaDataOps MetaDataPage
        {
            get
            {
                return new MetaDataOps();
            }
        }

        public DetailsPageOps DetailsPage
        {
            get
            {
                return new DetailsPageOps();
            }
        }

        public PhaseDetailsPageOps PhaseDetailsPage
        {
            get
            {
                return new PhaseDetailsPageOps();
            }
        }

        public PublishPageOps PublishPage
        {
            get
            {
                return new PublishPageOps();
            }
        }

        public void SelectTab(Tab tab)
        {
            FrameworkBase.Log.LogMessage(string.Format("Selecting tab {0}", tab.ToString()));
            Operations.ClickAction(page.Tabs[(int)tab]);
            WaitForTabSection(tab);
        }

        public void WaitForTabSection(Tab tab)
        {
            IWebElement elementToWait = null;
            switch (tab)
            {
                case Tab.MetaData:
                    elementToWait = new MetaData().Title;
                    break;
                case Tab.PhaseDetails:
                    elementToWait = new PhaseDetails().CompetitionEndDate;
                    break;
                case Tab.Details:
                    elementToWait = new Details().Tabs;
                    break;
                case Tab.Publish:
                    elementToWait = new Publish().PublishChanges;
                    break;
            }

            Utils.Wait(10, elementToWait);
        }

        private IWebElement GetStepControl(Tab tab)
        {
            return page.Tabs[(int)tab];
        }

        public string GetTabControlStyle(Tab tab, MI_UI_Framework.Utilities.Utils.ControlStyle style)
        {
            string controlStyle = Utils.GetControlStyle(GetStepControl(tab), style);
            FrameworkBase.Log.LogMessage(string.Format("Returning the tab:{0} style {1}", tab.ToString(), controlStyle));
            return controlStyle;
        }

        public void ClickSave()
        {
            FrameworkBase.Log.LogMessage("Clicking save button in Competition create page");
            Operations.ClickAction(page.Save);
        }

        public void ClickFinish()
        {
            FrameworkBase.Log.LogMessage("Clicking finish button in Competition create page");
            SeleniumHelper.Browser.WebDriver.ExecuteScript("$(\"#btnSaveConti\").click()");
            Utils.WaitForLoaderExit(MyCodaLabPage.CompetitionListLoader);
            //Operations.ClickElement(page.Next);
        }

        public void ClickNext()
        {
            FrameworkBase.Log.LogMessage("Clicking next button in Competition create page");
            SeleniumHelper.Browser.WebDriver.ExecuteScript("$(\"#btnSaveConti\").click()");
            //Operations.ClickElement(page.Next);
        }

        public void ClickPrevious()
        {
            FrameworkBase.Log.LogMessage("Clicking previous button in Competition create page");
            SeleniumHelper.Browser.WebDriver.ExecuteScript("$(\"#btnSavePrev\").click()");
            //Operations.ClickElement(page.Previous);
        }

        public bool IsUnsavedDataNotificationVisible()
        {
            return Operations.IsElementVisible(page.UnsavedDataNotification);
        }

        public enum Tab : int
        {
            MetaData = 0,
            PhaseDetails = 1,
            Details = 2,
            Publish = 3
        }
    }

    public class MetaDataOps
    {
        private static MetaData page;
        public MetaDataOps()
        {
            page = new MetaData();
            PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, page);
        }

        public bool IsMetaDataTabClicked()
        {
            FrameworkBase.Log.LogMessage("Checking whether the metadata tab is clicked or not");
            return Operations.IsElementVisible(page.Title);
        }

        public void SetTitle(string title)
        {
            FrameworkBase.Log.LogMessage(string.Format("Setting title as {0}", title));
            Operations.SetTextIntoTextBox(page.Title, title);
        }

        public string GetTitle()
        {
            string title = Operations.GetTextFromTextBox(page.Title);
            FrameworkBase.Log.LogMessage(string.Format("Returning the set title: {0}", title));
            return title;
        }

        public string GetDescription()
        {
            return Operations.GetTextFromTextBox(page.ShortDescription);
        }

        public void UploadImage(string imagePath)
        {
            SeleniumHelper.Browser.WebDriver.ExecuteScript("document.getElementById(\"uploadFile\").removeAttribute(\"class\")");
            page.UploadImage.SendKeys(imagePath);
            Utils.WaitForLoaderExit("preloaderInputImg", 20);
        }

        public void SetShortDescription(string description)
        {
            FrameworkBase.Log.LogMessage(string.Format("Setting shot description as {0}", description));
            Operations.SetTextIntoTextBox(page.ShortDescription, description);
        }
    }

    public class DetailsPageOps
    {
        private static Details page;
        public DetailsPageOps()
        {
            page = new Details();
            PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, page);
        }

        public void SelectTab(Tabs tab)
        {
            FrameworkBase.Log.LogMessage(string.Format("Selecting {0} tab in Details page", tab.ToString()));
            Operations.ClickElement(page.Tabs.FindElements(By.TagName("li"))[(int)tab]);
        }

        private int SelectedTabIndex
        {
            get
            {
                int index = 0;
                List<IWebElement> elements = page.Tabs.FindElements(By.TagName("li")).ToList<IWebElement>();
                for (int i = 0; i < elements.Count; i++)
                {
                    if (Operations.GetElementAttribute(elements[i], "class").Contains("active"))
                    {
                        index = i;
                        break;
                    }
                }

                return index;
            }
        }

        private int SelectedLDSubTabIndex
        {
            get
            {
                int index = 0;
                List<IWebElement> elements = page.LDSubTabs.FindElements(By.TagName("li")).ToList<IWebElement>();
                for (int i = 0; i < elements.Count; i++)
                {
                    if (Operations.GetElementAttribute(elements[i], "class").Contains("active"))
                    {
                        index = i;
                        break;
                    }
                }

                return index;
            }
        }

        public void SelectLDSubTab(int index)
        {
            FrameworkBase.Log.LogMessage(string.Format("Selecting learn the details sub tab with index {0}", index));
            Operations.ClickElement(page.LDSubTabs.FindElements(By.TagName("li"))[index]);
        }

        public void CreateLDSubTab(int tabIndex, string textToBeSet)
        {
            FrameworkBase.Log.LogMessage("Enabling a new sub tab in Learn the details section");
            List<IWebElement> t = page.LDSubTabs.FindElements(By.TagName("li")).ToList<IWebElement>();
            FrameworkBase.Log.LogMessage(string.Format("Making the sub tab with index {0} visible", tabIndex));
            Operations.ClickElement(t[tabIndex].FindElement(By.TagName("div")));
            Operations.DoubleClickElement(t[tabIndex].FindElement(By.TagName("label")));
            FrameworkBase.Log.LogMessage(string.Format("Setting the tab title {0}", textToBeSet));
            Operations.SetTextIntoTextBox(t[tabIndex].FindElement(By.TagName("input")), textToBeSet);
            Utils.WaitForLoaderExit("buttonPreloaderInput");
            System.Threading.Thread.Sleep(1000);
        }

        public List<string> GetLDVisibleSubTabs()
        {
            List<string> subtabs = new List<string>();

            FrameworkBase.Log.LogMessage("Getting all the visible sub tabs in Learn the details section of Competition create page");

            foreach (var item in page.LDSubTabs.FindElements(By.TagName("li")))
            {
                string classAttribute = Operations.GetElementAttribute(item, "class");

                if (!classAttribute.Contains("viewStateOff"))
                {
                    subtabs.Add(Operations.GetElementInnerText(item.FindElement(By.TagName("label"))));
                }
            }

            FrameworkBase.Log.LogMessage("Obtained sub tabs are");

            foreach (var s in subtabs)
            {
                FrameworkBase.Log.LogMessage(s);
            }

            return subtabs;
        }

        public void SetTextIntoLDSubTabTextArea(string text)
        {
            FrameworkBase.Log.LogMessage("Setting learn and details sub tab text area");
            Operations.SetTextIntoTextBox(page.SubTabsTextArea, text);
        }

        public string GetTextFromLDSubTabTextArea()
        {
            string details = Operations.GetTextFromTextBox(page.SubTabsTextArea);
            FrameworkBase.Log.LogMessage("Getting details of the sub tab in Learn and details section");
            FrameworkBase.Log.LogMessage(string.Format("Details : {0}", details));
            return details;
        }

        public void CreateParticipateSubTab(int tabIndex, string textToBeSet)
        {
            FrameworkBase.Log.LogMessage("Enabling a new sub tab in Participate section");
            List<IWebElement> t = page.ParticipateSubTabs.FindElements(By.TagName("li")).ToList<IWebElement>();
            FrameworkBase.Log.LogMessage(string.Format("Making the sub tab with index {0} visible", tabIndex));
            Operations.ClickElement(t[tabIndex].FindElement(By.TagName("div")));
            Operations.DoubleClickElement(t[tabIndex].FindElement(By.TagName("label")));
            FrameworkBase.Log.LogMessage(string.Format("Setting the tab title {0}", textToBeSet));
            Operations.SetTextIntoTextBox(t[tabIndex].FindElement(By.TagName("input")), textToBeSet);
            Utils.WaitForLoaderExit("buttonPreloaderInput");
            System.Threading.Thread.Sleep(1000);
        }

        public void SelectParticipateSubTab(int index)
        {
            FrameworkBase.Log.LogMessage(string.Format("Selecting participate sub tab with index {0}", index));
            Operations.ClickElement(page.ParticipateSubTabs.FindElements(By.TagName("li"))[index]);
        }

        public void SetTextIntoParticipateSubTabTextArea(string text)
        {
            FrameworkBase.Log.LogMessage("Setting details for the Participate sub tab");
            FrameworkBase.Log.LogMessage(string.Format("Details : {0}", text));
            Operations.SetTextIntoTextBox(page.ParticipateTextArea, text);
        }

        public string GetTextFromParticipateSubTabTextArea()
        {
            return Operations.GetTextFromTextBox(page.ParticipateTextArea);
        }

        public List<string> GetParticipateVisibleSubTabs()
        {
            List<string> subtabs = new List<string>();

            FrameworkBase.Log.LogMessage("Getting all the visible sub tabs in Participate section of Competition create page");

            foreach (var item in page.ParticipateSubTabs.FindElements(By.TagName("li")))
            {
                string classAttribute = Operations.GetElementAttribute(item, "class");

                if (classAttribute.Contains("active") || classAttribute.Contains("viewStateAlwaysOn"))
                {
                    subtabs.Add(Operations.GetElementInnerText(item.FindElement(By.TagName("label"))));
                }
            }

            foreach (var s in subtabs)
            {
                FrameworkBase.Log.LogMessage(s);
            }

            return subtabs;
        }

        public enum Tabs : int
        {
            LearnTheDetails = 0,
            Participate = 1,
            SeeTheResults = 2
        }
    }

    public class PhaseDetailsPageOps
    {
        private static PhaseDetails page;
        public PhaseDetailsPageOps()
        {
            page = new PhaseDetails();
            PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, page);
        }

        public void ClickAddDataSetForPhase(int phaseIndex)
        {
            FrameworkBase.Log.LogMessage(string.Format("Clicking add button on phase index {0}", phaseIndex));
            string id = string.Format("addDataSetPh{0}", phaseIndex);
            Operations.ClickElement(Elements.GetElementByID(id));
            Utils.WaitForLoaderExit("preloader");
        }

        public void AddNewPhase(CompPhaseDetails details, int phaseIndex)
        {
            SetPhaseTitle(details.Titile, phaseIndex);
            SetPhaseStartDate(details.StartDate, phaseIndex);
            SetPhaseMaxSubmission(details.MaxSubmissions, phaseIndex);

            if (details.DataSets.Count > 0)
            {
                ClickExpandButtonForPhase(phaseIndex);
                foreach (var item in details.DataSets)
                {
                    ClickAddDataSetForPhase(phaseIndex);
                    SetDataSetType(item.DataSetType, phaseIndex);
                    SetDataSetBlobURL(item.BlobURL, phaseIndex);
                    SetDataSetAccessString(item.AccessString, phaseIndex);
                    ClickApplyLinkForDataSet(phaseIndex);
                }
                ClickCollapseButtonForPhase(phaseIndex);
            }
        }

        public void ClickExpandButtonForPhase(int phaseIndex = 0)
        {
            FrameworkBase.Log.LogMessage(string.Format("Clicking expand button of phase with index {0}", phaseIndex));
            Operations.ClickElement(Elements.GetElements(By.XPath(PhaseDetails.ExpandButton))[phaseIndex - 1]);
        }

        public void ClickCollapseButtonForPhase(int phaseIndex = 0)
        {
            FrameworkBase.Log.LogMessage(string.Format("Clicking collapse button of phase with index {0}", phaseIndex));
            Operations.ClickElement(Elements.GetElements(By.XPath(PhaseDetails.CollapseButton))[phaseIndex - 1]);
        }

        public void SetPhaseTitle(string titleToBeSet, int phaseIndex = 0)
        {
            FrameworkBase.Log.LogMessage(string.Format("Setting Phase title as {0} for phase with index {1}", titleToBeSet, phaseIndex));
            Operations.SetTextIntoTextBox(Elements.GetElements(By.XPath(PhaseDetails.PhaseTitle))[phaseIndex - 1], titleToBeSet);
        }

        public void SetPhaseStartDate(string date, int phaseIndex = 0)
        {
            FrameworkBase.Log.LogMessage(string.Format("Setting Phase start date as {0} for phase with index {1}", date, phaseIndex));
            Operations.SetTextIntoTextBox(Elements.GetElements(By.XPath(PhaseDetails.PhaseStartDate))[phaseIndex - 1], date);
        }

        public void SetPhaseMaxSubmission(int maxSubmission, int phaseIndex = 0)
        {
            FrameworkBase.Log.LogMessage(string.Format("Setting Phase maximum submission as {0} for phase with index {1}", maxSubmission, phaseIndex));
            Operations.SetTextIntoTextBox(Elements.GetElements(By.XPath(PhaseDetails.PhaseMaximumSubmission))[phaseIndex - 1], maxSubmission.ToString());
        }

        public void SetDataSetType(DataSetType dstype, int phaseIndex = 0, int dataSetIndex = 0)
        {
            Operations.SelectItemByValue(GetDataSetControls(PhaseDetails.DSTypeID, phaseIndex, dataSetIndex), ((int)dstype).ToString());
        }

        public void SetDataSetBlobURL(string bolburl, int phaseIndex = 0, int dataSetIndex = 0)
        {
            Operations.SetTextIntoTextBox(GetDataSetControls(PhaseDetails.DSBlobURLID, phaseIndex, dataSetIndex), bolburl);
        }

        private IWebElement GetDataSetControls(string controlID, int phaseIndex = 0, int dataSetIndex = 0)
        {
            IList<IWebElement> sections = Elements.GetElements(By.ClassName(PhaseDetails.PhasesClassName));
            IList<IWebElement> phases = sections[phaseIndex].FindElements(By.Id(controlID));

            if (phases.Count <= 0 || phases == null)
            {
                return null;
            }

            return phases[dataSetIndex];
        }

        public void SetDataSetAccessString(string accessString, int phaseIndex = 0, int dataSetIndex = 0)
        {
            Operations.SetTextIntoTextBox(GetDataSetControls(PhaseDetails.DSAccessStringID, phaseIndex, dataSetIndex), accessString);
        }

        public string GetDataSetDownloadURL(int phaseIndex = 0, int dataSetIndex = 0)
        {
            return Operations.GetElementInnerText(GetDataSetControls(PhaseDetails.DSDownloadURLID, phaseIndex, dataSetIndex));
        }

        public void ClickApplyLinkForDataSet(int phaseIndex = 0, int dataSetIndex = 0)
        {
            Operations.ClickElement(GetDataSetControls(PhaseDetails.DSApplyID, phaseIndex, dataSetIndex));
            Utils.WaitForLoaderExit("preloader");
        }

        public void ClickRemoveLinkForDataSet(int phaseIndex = 0, int dataSetIndex = 0)
        {
            Operations.ClickElement(GetDataSetControls(PhaseDetails.DSRemoveID, phaseIndex, dataSetIndex));
        }

        public void ClickUpdateLinkForDataSet(int phaseIndex, int dataSetIndex)
        {
            Operations.ClickElement(GetDataSetControls(PhaseDetails.DSUpdateID, phaseIndex, dataSetIndex));
        }

        public void ClickDeleteLinkForDataSet(int phaseIndex, int dataSetIndex)
        {
            Operations.ClickElement(GetDataSetControls(PhaseDetails.DSDeleteID, phaseIndex, dataSetIndex));
        }

        public List<DataSetDetails> GetAllDataSetDetails(int phaseIndex)
        {
            FrameworkBase.Log.LogMessage(string.Format("Getting data set details for the phase with index {0}", phaseIndex));
            List<DataSetDetails> details = new List<DataSetDetails>();

            IList<IWebElement> sections = Elements.GetElements(By.ClassName(PhaseDetails.DSControlID));

            IWebElement element = null;

            for (int i = 0; i < sections.Count; i++)
            {
                DataSetDetails det = new DataSetDetails();
                DataSetType dtype = DataSetType.AzureBlob;
                element = GetDataSetControls(PhaseDetails.DSTypeID, phaseIndex, i);
                if (element != null)
                {
                    Enum.TryParse<DataSetType>(Operations.SelectedItemValue(element), out dtype);
                    det.DataSetType = dtype;
                    det.BlobURL = Operations.GetElementInnerText(GetDataSetControls(PhaseDetails.DSBlobURLID, phaseIndex, i));
                    det.AccessString = Operations.GetElementInnerText(GetDataSetControls(PhaseDetails.DSAccessStringID, phaseIndex, i));
                    det.DownloadURL = GetDataSetDownloadURL(phaseIndex, i);

                    details.Add(det);
                }
            }

            return details;
        }

        public List<CompPhaseDetails> GetAllPhaseDetails()
        {
            List<CompPhaseDetails> details = new List<CompPhaseDetails>();
            string phaseXpath = PhaseDetails.PhaseCustomXpath.Replace("[{0}]", "");

            for (int i = 1; i < Elements.GetElements(By.XPath(phaseXpath)).Count; i++)
            {
                ClickExpandButtonForPhase(i);
                CompPhaseDetails det = new CompPhaseDetails();
                det.Titile = Operations.GetTextFromTextBox(Elements.GetElements(By.XPath(PhaseDetails.PhaseTitle))[i - 1]);
                det.StartDate = Operations.GetElementInnerText(Elements.GetElements(By.XPath(PhaseDetails.PhaseStartDate))[i - 1]);
                det.MaxSubmissions = Convert.ToInt32(Operations.GetTextFromTextBox(Elements.GetElements(By.XPath(PhaseDetails.PhaseMaximumSubmission))[i - 1]));
                det.DataSets = GetAllDataSetDetails(i);
                ClickCollapseButtonForPhase(i);
                details.Add(det);
            }

            return details;
        }

        public enum DataSetType : int
        {
            AzureBlob = 2,
            AzureBlobSharedAccessSignature = 1
        }
    }

    public class PublishPageOps
    {
        private static Publish page;
        public PublishPageOps()
        {
            page = new Publish();
            PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, page);
        }

        public void CheckPublishCompetition()
        {
            FrameworkBase.Log.LogMessage("Selecting Publish competition check box");
            Operations.CheckCheckBox(page.PublishTheCompetition);
        }

        public void ClickPublishChanges()
        {
            FrameworkBase.Log.LogMessage("Clicking publish button");
            Operations.ClickElement(page.PublishChanges);
        }

        public void PublishCompetition()
        {
            CheckPublishCompetition();
            ClickPublishChanges();
        }

        public bool IsPublishCheckBoxChecked()
        {
            return page.PublishTheCompetition.Selected;
        }
    }
}
