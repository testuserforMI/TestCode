using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MI_UI_Framework;
using System.Configuration;
using MI_UI_Framework.Utilities;
using MI_UI_Framework.PageOperations;
using System.Collections.Generic;

namespace MI_UI_TestCases
{
    [TestClass]
    public class MultiUserTestCases : BaseClass
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void Verify_RequestCompetition()
        {
            try
            {
                Browser.Start();
                CompetitionDetails details = CreateCompetition(ConfigurationManager.AppSettings["UN1"]);
                Pages.LoginPage.Login(ConfigurationManager.AppSettings["UN2"], ConfigurationManager.AppSettings["UN2Password"]);

                Pages.CompetitionPage.ClickCompetitionLink();
                Pages.CompetitionPage.SelectCompetition(details.Title);
                Pages.CompetitionDetailsPage.ParticipatePage.RegisterForChallange();
                Assert.IsTrue(Pages.CompetitionDetailsPage.ParticipatePage.IsRegisteringPending(), "Registration was not sent for approval");

                Pages.HomePage.ClickLogOff();
                Pages.LoginPage.Login(ConfigurationManager.AppSettings["UN1"], ConfigurationManager.AppSettings["UN1Password"]);

                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                Pages.MyCodaLabPage.SelectCompetitionOptionlink(MyCodaLabPageOps.CompetitionOptionLinks.Participants, details.Title);

                List<ParticipantDetails> allparticipants = Pages.MyCodaLabPage.GetAllParticipants();
                foreach (var item in allparticipants)
                {
                    if (StringUtilities.CompareStrings(ConfigurationManager.AppSettings["UN2"], item.ParticipantName, true, true))
                    {
                        Assert.IsTrue(StringUtilities.CompareStrings(item.StatusMessage, Constants.StaticStrings.ParticipantPage.ParticipationPendingApproval, true, true),
                            "Status message was not as expected");
                    }
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        public void Verify_ApproveCompetition()
        {
            try
            {
                Browser.Start();
                CompetitionDetails details = CreateCompetition(ConfigurationManager.AppSettings["UN1"]);
                Pages.LoginPage.Login(ConfigurationManager.AppSettings["UN2"], ConfigurationManager.AppSettings["UN2Password"]);

                Pages.CompetitionPage.ClickCompetitionLink();
                Pages.CompetitionPage.SelectCompetition(details.Title);
                Pages.CompetitionDetailsPage.ParticipatePage.RegisterForChallange();

                Pages.HomePage.ClickLogOff();
                Pages.LoginPage.Login(ConfigurationManager.AppSettings["UN1"], ConfigurationManager.AppSettings["UN1Password"]);

                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                Pages.MyCodaLabPage.SelectCompetitionOptionlink(MyCodaLabPageOps.CompetitionOptionLinks.Participants, details.Title);
                Pages.MyCodaLabPage.ChangeStatus(MyCodaLabPageOps.RequestTo.Approve, ConfigurationManager.AppSettings["UN2"]);

                Pages.HomePage.ClickLogOff();
                Pages.LoginPage.Login(ConfigurationManager.AppSettings["UN2"], ConfigurationManager.AppSettings["UN2Password"]);

                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                Pages.MyCodaLabPage.SelectTab(MyCodaLabPageOps.MyCodaLabTabs.CompetitionIParticipateIn);

                Assert.IsTrue(Pages.MyCodaLabPage.IsCompetitionPresent(details.Title), "Competition was not present in the \"Competition I participate in\" page");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.GetParticipantStatus(details.Title), Constants.StaticStrings.MyCodaLabPage.ParticipantApproved, true, true),
                    "Competition status was not as approved in \"Competition I participate in\" page");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        public void Verify_RejectCompetition()
        {
            try
            {
                Browser.Start();
                CompetitionDetails details = CreateCompetition(ConfigurationManager.AppSettings["UN1"]);
                Pages.LoginPage.Login(ConfigurationManager.AppSettings["UN2"], ConfigurationManager.AppSettings["UN2Password"]);

                Pages.CompetitionPage.ClickCompetitionLink();
                Pages.CompetitionPage.SelectCompetition(details.Title);
                Pages.CompetitionDetailsPage.ParticipatePage.RegisterForChallange();

                Pages.HomePage.ClickLogOff();
                Pages.LoginPage.Login(ConfigurationManager.AppSettings["UN1"], ConfigurationManager.AppSettings["UN1Password"]);

                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                Pages.MyCodaLabPage.SelectCompetitionOptionlink(MyCodaLabPageOps.CompetitionOptionLinks.Participants, details.Title);
                Pages.MyCodaLabPage.ChangeStatus(MyCodaLabPageOps.RequestTo.Reject, ConfigurationManager.AppSettings["UN2"]);
                Pages.MyCodaLabPage.SelectRejectionOption(MyCodaLabPageOps.RejectionOption.Ok);

                Pages.HomePage.ClickLogOff();
                Pages.LoginPage.Login(ConfigurationManager.AppSettings["UN2"], ConfigurationManager.AppSettings["UN2Password"]);

                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                Pages.MyCodaLabPage.SelectTab(MyCodaLabPageOps.MyCodaLabTabs.CompetitionIParticipateIn);

                Assert.IsTrue(Pages.MyCodaLabPage.IsCompetitionPresent(details.Title), "Competition was not present in the \"Competition I participate in\" page");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.GetParticipantStatus(details.Title), Constants.StaticStrings.MyCodaLabPage.ParticipantRejected, true, true),
                    "Competition status was not as rejected in \"Competition I participate in\" page");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        public void Verify_PendingApproval()
        {
            try
            {
                Browser.Start();
                CompetitionDetails details = CreateCompetition(ConfigurationManager.AppSettings["UN1"]);
                Pages.LoginPage.Login(ConfigurationManager.AppSettings["UN2"], ConfigurationManager.AppSettings["UN2Password"]);

                Pages.CompetitionPage.ClickCompetitionLink();
                Pages.CompetitionPage.SelectCompetition(details.Title);
                Pages.CompetitionDetailsPage.ParticipatePage.RegisterForChallange();

                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                Pages.MyCodaLabPage.SelectTab(MyCodaLabPageOps.MyCodaLabTabs.CompetitionIParticipateIn);

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.GetParticipantStatus(details.Title), Constants.StaticStrings.MyCodaLabPage.ParticipantPendingApproval, true, true),
                    "Competition status was not as pending in \"Competition I participate in\" page");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public CompetitionDetails CreateCompetition(string userName)
        {
            CompetitionDetails details = new CompetitionDetails();
            if (ConfigurationManager.AppSettings["UseUI"] == "1")
            {
                if (!Pages.HomePage.GetLoggedInUserName().Contains(userName))
                {
                    Pages.HomePage.ClickLogOff();
                    Pages.LoginPage.Login(ConfigurationManager.AppSettings["UN1"], ConfigurationManager.AppSettings["UN1Password"]);
                }

                details = Pages.MyCodaLabPage.CreateCompetition(isPublic: true);
                Pages.HomePage.ClickLogOff();
            }
            else
            {
                details = DBUtils.InsertCompetitions(ConfigurationManager.AppSettings["UN1"])[0];
            }

            return details;
        }

        [TestCleanup]
        public void CleanUp()
        {
            try
            {
                if (ConfigurationManager.AppSettings["UseUI"] == "1")
                {
                    if (!Pages.HomePage.GetLoggedInUserName().Contains(ConfigurationManager.AppSettings["UN1"]))
                    {
                        Pages.HomePage.ClickLogOff();
                        Pages.LoginPage.Login(ConfigurationManager.AppSettings["UN1"], ConfigurationManager.AppSettings["UN1Password"]);
                    }

                    Pages.MyCodaLabPage.ClickMyCodaLabLink();

                    foreach (var item in Pages.MyCodaLabPage.GetAllCompetitions())
                    {
                        Pages.MyCodaLabPage.DelteCompetition(item.Title);
                    }
                }
                else
                {
                    DBUtils.DeleteAllCompetitions();   
                }

                base.TestCleanUp();
            }
            catch
            {
                // No catch as clean up should not fail the test case
            }
        } 
    }
}
