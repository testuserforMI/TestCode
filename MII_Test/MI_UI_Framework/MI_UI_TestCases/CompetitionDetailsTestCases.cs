using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MI_UI_Framework;
using MI_UI_Framework.Utilities;
using MI_UI_Framework.PageOperations;
using System.Collections.Generic;
using System.Configuration;
//using ChallengesDetails = MI_UI_Framework.PageOperations.CompetitionDetailsPageOps.ChallengesDetails;

namespace MI_UI_TestCases
{
    [TestClass]
    public class CompetitionDetailsTestCases : BaseClass 
    {
        private string competitionTitle; 
        private bool deleteCompetition;  

        [TestInitialize]
        public void Initialize()
        {
            competitionTitle = string.Empty;
            this.deleteCompetition = false;
        }

        [TestMethod]
        [Priority(1)]
        [Description("Test case to verify the navigation to competition details page when any competition is clicked in Competitions page")]
        public void CompetitionDetails_Verify_CompetitionLinkNavigation()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                EnterCompetitionDetailsPage();

                Assert.IsTrue(Pages.CompetitionDetailsPage.IsCompetitionDetailsPageVisible(), "Control did not navigate to competition details page");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }
        
        [TestMethod]
        [Priority(2)]
        [Description("Test case to verify the new tab added in the competition create page")]
        public void CompetitionDetails_Verify_NewTabAdded()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                Pages.MyCodaLabPage.ClickCreateNewCompetitionLink();
                Pages.MyCodaLabPage.ClickStartInWizard();

                this.competitionTitle = StringUtilities.GetRandomPlainString(10);
                Pages.MyCodaLabPage.CreatePage.MetaDataPage.SetTitle(this.competitionTitle);
                Pages.MyCodaLabPage.CreatePage.ClickSave();
                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.Details);
                string tabText = StringUtilities.GetRandomPlainString(6);
                Pages.MyCodaLabPage.CreatePage.DetailsPage.CreateLDSubTab(3, tabText);
                Pages.MyCodaLabPage.CreatePage.ClickSave();
                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.Publish);
                Pages.MyCodaLabPage.CreatePage.PublishPage.PublishCompetition();
                Pages.MyCodaLabPage.CreatePage.ClickFinish();
                Pages.CompetitionPage.ClickCompetitionLink();
                Assert.IsTrue(Pages.CompetitionPage.IsCompetitionPresent(this.competitionTitle), "Competition created was not found in the list");
                EnterCompetitionDetailsPage();
                Assert.IsTrue(Pages.CompetitionDetailsPage.LearnTheDetailsPage.GetAllSubTabNames().Contains(tabText), "Tab added was not appearing in the list");
            }
            catch(Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Priority(2)]
        [Description("Test case to verify the login page appear when tried to register without loggin in")]
        public void CompetitionDetails_Verify_LoginPromptForRegister()
        {
            try
            {
                Browser.Start();
                CompetitionDetails details = Pages.MyCodaLabPage.CreateCompetition(1)[0];
                Pages.CompetitionPage.ClickCompetitionLink();
                this.competitionTitle = details.Title;
                EnterCompetitionDetailsPage();
                Pages.CompetitionDetailsPage.ParticipatePage.RegisterForChallange();
                Assert.IsTrue(Pages.LoginPage.IsUserNameFiledVisible(), "Control did not prompt for username and password when user tried to register for competition");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Priority(1)]
        [Description("Test case to verify deleting the results submitted to leaderboard")]
        public void Verify_SubmitResult_ToLeaderBoard()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                EnterCompetitionDetailsPage();
                Pages.CompetitionDetailsPage.SelectTab(CompetitionDetailsPageOps.Tabs.Participate);
                Pages.CompetitionDetailsPage.ParticipatePage.CheckAcceptRules();
                Pages.CompetitionDetailsPage.ParticipatePage.ClickRegister();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                Pages.MyCodaLabPage.SelectCompetitionOptionlink(MyCodaLabPageOps.CompetitionOptionLinks.Participants, this.competitionTitle);
                Pages.MyCodaLabPage.ChangeStatus(MyCodaLabPageOps.RequestTo.Approve, Pages.HomePage.GetLoggedInUserName());
                EnterCompetitionDetailsPage();
                Pages.CompetitionDetailsPage.SelectTab(CompetitionDetailsPageOps.Tabs.Participate);
                Pages.CompetitionDetailsPage.ParticipatePage.SelectSubTab(Participate.SubTabs.SubmitResults);
                string filePath = Utils.GenerateResultFile(TestContext.TestDeploymentDir,"result");
                Pages.CompetitionDetailsPage.ParticipatePage.UploadAndSubmitResultsToLeaderBoard(filePath);
                Pages.CompetitionDetailsPage.SelectTab(CompetitionDetailsPageOps.Tabs.SeeTheResults);
                Assert.IsTrue(
                    Pages.CompetitionDetailsPage.SeeTheResultsPage.IsRowEntryPresent(Pages.HomePage.GetLoggedInUserName()), "Submitted submission number was not found in the table");
            }
            catch(Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Priority(1)]
        [Description("Test case to verify deleting the results submitted to leaderboard")]
        public void Verify_DeleteResult_FromLeaderBoard()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                EnterCompetitionDetailsPage();
                Pages.CompetitionDetailsPage.SelectTab(CompetitionDetailsPageOps.Tabs.Participate);
                Pages.CompetitionDetailsPage.ParticipatePage.RegisterForChallange();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                Pages.MyCodaLabPage.SelectCompetitionOptionlink(MyCodaLabPageOps.CompetitionOptionLinks.Participants, this.competitionTitle);
                Pages.MyCodaLabPage.ChangeStatus(MyCodaLabPageOps.RequestTo.Approve, Pages.HomePage.GetLoggedInUserName());
                EnterCompetitionDetailsPage();
                Pages.CompetitionDetailsPage.SelectTab(CompetitionDetailsPageOps.Tabs.Participate);
                Pages.CompetitionDetailsPage.ParticipatePage.SelectSubTab(Participate.SubTabs.SubmitResults);
                string filePath = Utils.GenerateResultFile(TestContext.TestDeploymentDir, "result");
                Pages.CompetitionDetailsPage.ParticipatePage.UploadAndSubmitResultsToLeaderBoard(filePath);
                Pages.CompetitionDetailsPage.SelectTab(CompetitionDetailsPageOps.Tabs.SeeTheResults);
                Assert.IsTrue(
                    Pages.CompetitionDetailsPage.SeeTheResultsPage.IsRowEntryPresent(Pages.HomePage.GetLoggedInUserName()), "Submitted submission number was not found in the table");
                Pages.CompetitionDetailsPage.SelectTab(CompetitionDetailsPageOps.Tabs.Participate);
                Pages.CompetitionDetailsPage.ParticipatePage.SelectSubTab(Participate.SubTabs.SubmitResults);
                Pages.CompetitionDetailsPage.ParticipatePage.DeleteResultFromLeaderBoard(filePath);
                Pages.CompetitionDetailsPage.SelectTab(CompetitionDetailsPageOps.Tabs.SeeTheResults);
                Assert.IsFalse(
                    Pages.CompetitionDetailsPage.SeeTheResultsPage.IsRowEntryPresent(Pages.HomePage.GetLoggedInUserName()), "Submitted submission number was not found in the table");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Priority(1)]
        [Description("Test case to verify details of the results submitted to the leaderboard")]
        public void Verify_SumittedResultDetails_InLeaderBoard()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                EnterCompetitionDetailsPage();
                Pages.CompetitionDetailsPage.SelectTab(CompetitionDetailsPageOps.Tabs.Participate);
                Pages.CompetitionDetailsPage.ParticipatePage.RegisterForChallange();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                Pages.MyCodaLabPage.SelectCompetitionOptionlink(MyCodaLabPageOps.CompetitionOptionLinks.Participants, this.competitionTitle);
                Pages.MyCodaLabPage.ChangeStatus(MyCodaLabPageOps.RequestTo.Approve, Pages.HomePage.GetLoggedInUserName());
                EnterCompetitionDetailsPage();
                Pages.CompetitionDetailsPage.SelectTab(CompetitionDetailsPageOps.Tabs.Participate);
                Pages.CompetitionDetailsPage.ParticipatePage.SelectSubTab(Participate.SubTabs.SubmitResults);
                string filePath = Utils.GenerateResultFile(TestContext.TestDeploymentDir, "result");
                Pages.CompetitionDetailsPage.ParticipatePage.UploadAndSubmitResultsToLeaderBoard(filePath);
                Dictionary<string, string> resultDetailsInParticipatePage = Pages.CompetitionDetailsPage.ParticipatePage.GetSubmittedRowOnLeaderBoard();
                Pages.CompetitionDetailsPage.SelectTab(CompetitionDetailsPageOps.Tabs.SeeTheResults);
                Dictionary<string, string> resultDetailsInLeaderBoard = Pages.CompetitionDetailsPage.SeeTheResultsPage.LeaderBoardResultDetails(Pages.HomePage.GetLoggedInUserName());
                Assert.AreEqual<string>(resultDetailsInParticipatePage["SUBMISSION"], resultDetailsInLeaderBoard["SUBMISSION"],
                    string.Format("Submission number was not same in both the page. \\n ParticipatePage : {0} \\n SeeTheResults page : {1}",
                    resultDetailsInParticipatePage["SUBMISSION"], resultDetailsInLeaderBoard["SUBMISSION"]));

                Assert.AreEqual<string>(resultDetailsInParticipatePage["DATE"], resultDetailsInLeaderBoard["SUBMITTED AT"],
                    string.Format("Submission date was not same in both the page. \\n ParticipatePage : {0} \\n SeeTheResults page : {1}",
                    resultDetailsInParticipatePage["DATE"], resultDetailsInLeaderBoard["SUBMITTED AT"]));

                Assert.AreEqual<string>(resultDetailsInParticipatePage["SCORE"], resultDetailsInLeaderBoard["SCORE"],
                    string.Format("Score was not same in both the page. \\n ParticipatePage : {0} \\n SeeTheResults page : {1}",
                    resultDetailsInParticipatePage["SCORE"], resultDetailsInLeaderBoard["SCORE"]));

            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Priority(1)]
        [Description("Test case to verify details of the results submitted to the leaderboard by differnt user and viewed by different user")]
        public void Verify_ResultSubmissionDetails_ForDifferentUser()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                string loggedInUserName = Pages.HomePage.GetLoggedInUserName();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                EnterCompetitionDetailsPage();
                Pages.CompetitionDetailsPage.SelectTab(CompetitionDetailsPageOps.Tabs.Participate);
                Pages.CompetitionDetailsPage.ParticipatePage.CheckAcceptRules();
                Pages.CompetitionDetailsPage.ParticipatePage.ClickRegister();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                Pages.MyCodaLabPage.SelectCompetitionOptionlink(MyCodaLabPageOps.CompetitionOptionLinks.Participants, this.competitionTitle);
                Pages.MyCodaLabPage.ChangeStatus(MyCodaLabPageOps.RequestTo.Approve, Pages.HomePage.GetLoggedInUserName());
                EnterCompetitionDetailsPage();
                Pages.CompetitionDetailsPage.SelectTab(CompetitionDetailsPageOps.Tabs.Participate);
                Pages.CompetitionDetailsPage.ParticipatePage.SelectSubTab(Participate.SubTabs.SubmitResults);
                string filePath = Utils.GenerateResultFile(TestContext.TestDeploymentDir, "result");
                Pages.CompetitionDetailsPage.ParticipatePage.UploadAndSubmitResultsToLeaderBoard(filePath);
                Dictionary<string, string> resultDetailsInParticipatePage = Pages.CompetitionDetailsPage.ParticipatePage.GetSubmittedRowOnLeaderBoard();

                Pages.HomePage.ClickLogOff();
                Pages.LoginPage.Login(ConfigurationManager.AppSettings["UN1"], ConfigurationManager.AppSettings["UN1Password"]);

                Pages.CompetitionPage.ClickCompetitionLink();
                Pages.CompetitionPage.SelectCompetition(this.competitionTitle);
                Pages.CompetitionDetailsPage.SelectTab(CompetitionDetailsPageOps.Tabs.SeeTheResults);
                Dictionary<string, string> resultDetailsInLeaderBoard = Pages.CompetitionDetailsPage.SeeTheResultsPage.LeaderBoardResultDetails(loggedInUserName);

                Assert.AreEqual<string>(resultDetailsInParticipatePage["SUBMISSION"], resultDetailsInLeaderBoard["SUBMISSION"],
                    string.Format("Submission number was not same in both the page. \\n ParticipatePage : {0} \\n SeeTheResults page : {1}",
                    resultDetailsInParticipatePage["SUBMISSION"], resultDetailsInLeaderBoard["SUBMISSION"]));

                Assert.AreEqual<string>(resultDetailsInParticipatePage["DATE"], resultDetailsInLeaderBoard["SUBMITTED AT"],
                    string.Format("Submission date was not same in both the page. \\n ParticipatePage : {0} \\n SeeTheResults page : {1}",
                    resultDetailsInParticipatePage["DATE"], resultDetailsInLeaderBoard["SUBMITTED AT"]));

                Assert.AreEqual<string>(resultDetailsInParticipatePage["SCORE"], resultDetailsInLeaderBoard["SCORE"],
                    string.Format("Score was not same in both the page. \\n ParticipatePage : {0} \\n SeeTheResults page : {1}",
                    resultDetailsInParticipatePage["SCORE"], resultDetailsInLeaderBoard["SCORE"]));

                Assert.AreEqual<string>(resultDetailsInLeaderBoard["USERNAME"], loggedInUserName,
                    string.Format("Username was not as expected in the leaderboard table. Actual :{0} Expected : {1}", resultDetailsInLeaderBoard["USERNAME"], loggedInUserName));
            }
            catch(Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        private void EnterCompetitionDetailsPage()
        {
            if (string.IsNullOrEmpty(this.competitionTitle))
            {
                //Pages.MyCodaLabPage.ClickMyCodaLabLink();
                this.competitionTitle = new MyCodaLabPageOps().CreateCompetition(1)[0].Title;//StringUtilities.GetRandomPlainString(10);
                //Pages.MyCodaLabPage.CreateCompetition(this.competitionTitle, isPublic: true);
                this.deleteCompetition = true;
            }

            System.Threading.Thread.Sleep(1000);
            Pages.CompetitionPage.ClickCompetitionLink();
            Pages.CompetitionPage.SelectCompetition(this.competitionTitle);
        }

        [TestCleanup]
        public void CleanUp()
        {
            try
            {
                if (this.deleteCompetition)
                {
                    if (ConfigurationManager.AppSettings["UseUI"] != "1")
                    {
                        DBUtils.DeleteCompetition(this.competitionTitle);
                    }
                    else
                    {
                        Pages.MyCodaLabPage.ClickMyCodaLabLink();
                        Pages.MyCodaLabPage.DelteCompetition(this.competitionTitle);
                    }
                    //Pages.MyCodaLabPage.ClickMyCodaLabLink();
                    //Pages.MyCodaLabPage.DelteCompetition(this.competitionTitle);
                    this.competitionTitle = string.Empty;
                }

                TestCleanUp();
            }
            catch
            {
                // No catch as clean up should not fail the test case
            }
        }
    }
}
