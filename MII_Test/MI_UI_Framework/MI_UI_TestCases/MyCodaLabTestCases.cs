using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MI_UI_Framework;
using MI_UI_Framework.PageOperations;
using MI_UI_Framework.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Configuration;

namespace MI_UI_TestCases 
{
    [TestClass]
    public class MyCodaLabTestCases : BaseClass  
    {
        bool deleteCompetition;
        string competitionTitle;
        string competitionDescription; 

        [TestInitialize] 
        public void TestInitialize()
        {
            deleteCompetition = false;
            competitionTitle = string.Empty;
            competitionDescription = string.Empty;
        }

        [TestMethod]
        [Description("Test case to verify mycodalab link is highlighted when clicked")]
        [Priority(1)]
        public void Verify_MyCodaLabLinkNavigation()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                Assert.IsTrue(Pages.MyCodaLabPage.IsPageSectionVisible(), "Mycodalab page section is not visible");

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.GetMyCodaLabLinkStyle(Utils.ControlStyle.FontColor), Constants.Style.MenuSelected.Color, true, true), "Font color did not match");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.GetMyCodaLabLinkStyle(Utils.ControlStyle.FontSize), Constants.Style.MenuSelected.FontSize, true, true), "Font size did not match");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.GetMyCodaLabLinkStyle(Utils.ControlStyle.FontStyle), Constants.Style.MenuSelected.FontStyle, true, true), "Font style did not match");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.GetMyCodaLabLinkStyle(Utils.ControlStyle.TextTransform), Constants.Style.MenuSelected.TextTransform, true, true), "Text transform did not match");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify home link style when control is in mycodalab page")]
        [Priority(1)]
        public void Verify_HomeLink_Style_InMyCodaLabPage()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.HomePage.GetHomeLinkFontColor(), Constants.Style.MenuNotSelected.Color, true, true), "Font color did not match");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.HomePage.GetHomeLinkFontSize(), Constants.Style.MenuNotSelected.FontSize, true, true), "Font size did not match");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.HomePage.GetHomeLinkFontStyle(), Constants.Style.MenuNotSelected.FontStyle, true, true), "Font style did not match");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.HomePage.GetHomeLinkTextTransform(), Constants.Style.MenuNotSelected.TextTransform, true, true), "Text transform did not match");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify creation of competition")]
        [Priority(0)]
        public void Verify_CreateNewCompetition()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                this.competitionTitle = StringUtilities.GetRandomPlainString(20);
                this.competitionDescription = StringUtilities.GetRandomPlainString(50);

                Pages.MyCodaLabPage.CreateCompetition(this.competitionTitle, this.competitionDescription);

                Assert.IsTrue(Pages.MyCodaLabPage.IsCompetitionPresent(this.competitionTitle), string.Format("Competition {0} was not found in the list.", this.competitionTitle));

                this.deleteCompetition = true;
            }
            catch(Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify deletion of competition")]
        [Priority(1)]
        public void Verify_DeleteCompetition()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                this.competitionTitle = StringUtilities.GetRandomPlainString(20);
                this.competitionDescription = StringUtilities.GetRandomPlainString(50);

                Pages.MyCodaLabPage.CreateCompetition(this.competitionTitle, this.competitionDescription);
                Pages.MyCodaLabPage.DelteCompetition(this.competitionTitle);

                Assert.IsTrue(!Pages.MyCodaLabPage.IsCompetitionPresent(this.competitionTitle), string.Format("Competition {0} was found in the list.", this.competitionTitle));
            }

            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify that when competition created as public appears both in MyCodaLab and competition page")]
        [Priority(1)]
        public void Verify_CompetitionAsPublic()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                this.competitionTitle = StringUtilities.GetRandomPlainString(20);
                this.competitionDescription = StringUtilities.GetRandomPlainString(50);

                Pages.MyCodaLabPage.CreateCompetition(this.competitionTitle, this.competitionDescription, isPublic: true);

                Assert.IsTrue(Pages.MyCodaLabPage.IsCompetitionPresent(this.competitionTitle), string.Format("Competition {0} was not found in the my coda lab page.", this.competitionTitle));

                Pages.CompetitionPage.ClickCompetitionLink();

                Assert.IsTrue(Pages.CompetitionPage.IsCompetitionPresent(this.competitionTitle), string.Format("Competition {0} was not found in the Competition page.", this.competitionTitle));

                this.deleteCompetition = true;
            }

            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify that when competition created as non public appears in MyCodaLab and not in competition page")]
        [Priority(1)]
        public void Verify_CompetitionAsNonPublic()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                this.competitionTitle = StringUtilities.GetRandomPlainString(20);
                this.competitionDescription = StringUtilities.GetRandomPlainString(50);

                Pages.MyCodaLabPage.CreateCompetition(this.competitionTitle, this.competitionDescription, isPublic: false);

                Assert.IsTrue(Pages.MyCodaLabPage.IsCompetitionPresent(this.competitionTitle), string.Format("Competition {0} was not found in the my coda lab page.", this.competitionTitle));

                Pages.CompetitionPage.ClickCompetitionLink();

                Assert.IsTrue(!Pages.CompetitionPage.IsCompetitionPresent(this.competitionTitle), string.Format("Competition {0} was found in the Competition page.", this.competitionTitle));

                this.deleteCompetition = true;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify that user will be taken to login page when clicked on MyCodaLab link without login")]
        [Priority(1)]
        public void Verify_MyCodaLabLinkWihoutLogin()
        {
            try
            {
                Browser.Start();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                Assert.IsTrue(Pages.LoginPage.IsUserNameFiledVisible(), "Control did not navigate to login page.");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify creation of competition when saved using step links in create wizard")]
        [Priority(1)]
        public void Verify_CreateCompetition_StepNavigation()
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
                Pages.MyCodaLabPage.CreatePage.ClickNext();
                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.PhaseDetails);
                Pages.MyCodaLabPage.CreatePage.ClickSave();
                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.Details);
                Pages.MyCodaLabPage.CreatePage.ClickSave();
                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.Publish);
                Pages.MyCodaLabPage.CreatePage.ClickFinish();

                Assert.IsTrue(Pages.MyCodaLabPage.IsCompetitionPresent(this.competitionTitle), string.Format("Competition {0} was not found in the my coda lab page.", this.competitionTitle));

                this.deleteCompetition = true;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify create competition wizard step link styles")]
        [Priority(2)]
        public void Verify_StepLinkStyle()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                Pages.MyCodaLabPage.ClickCreateNewCompetitionLink();
                Pages.MyCodaLabPage.ClickStartInWizard();

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.FontColor), Constants.Style.StepSelected.Color, true, true), "Font color did not match when metadata was selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.FontStyle), Constants.Style.StepSelected.FontStyle, true, true), "Font style did not match when metadata was selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.FontSize), Constants.Style.StepSelected.FontSize, true, true), "Font size did not match when metadata was selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.TextTransform), Constants.Style.StepSelected.TextTransform, true, true), "Text transform did not match when metadata was selected");

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.FontColor), Constants.Style.StepNotSelected.Color, true, true), "Font color did not match when details was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.FontStyle), Constants.Style.StepNotSelected.FontStyle, true, true), "Font style did not match when details was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.FontSize), Constants.Style.StepNotSelected.FontSize, true, true), "Font size did not match when details was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.TextTransform), Constants.Style.StepNotSelected.TextTransform, true, true), "Text transform did not match when details was not selected");

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.FontColor), Constants.Style.StepNotSelected.Color, true, true), "Font color did not match when preview was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.FontStyle), Constants.Style.StepNotSelected.FontStyle, true, true), "Font style did not match when preview was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.FontSize), Constants.Style.StepNotSelected.FontSize, true, true), "Font size did not match when preview was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.TextTransform), Constants.Style.StepNotSelected.TextTransform, true, true), "Text transform did not match when preview was not selected");

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.FontColor), Constants.Style.StepNotSelected.Color, true, true), "Font color did not match when publish was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.FontStyle), Constants.Style.StepNotSelected.FontStyle, true, true), "Font style did not match when publish was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.FontSize), Constants.Style.StepNotSelected.FontSize, true, true), "Font size did not match when publish was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.TextTransform), Constants.Style.StepNotSelected.TextTransform, true, true), "Text transform did not match when publish was not selected");

                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.Details);

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.FontColor), Constants.Style.StepNotSelected.Color, true, true), "Font color did not match when metadata was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.FontStyle), Constants.Style.StepNotSelected.FontStyle, true, true), "Font style did not match when metadata was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.FontSize), Constants.Style.StepNotSelected.FontSize, true, true), "Font size did not match when metadata was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.TextTransform), Constants.Style.StepNotSelected.TextTransform, true, true), "Text transform did not match when metadata was not selected");

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.FontColor), Constants.Style.StepSelected.Color, true, true), "Font color did not match when details was selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.FontStyle), Constants.Style.StepSelected.FontStyle, true, true), "Font style did not match when details was selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.FontSize), Constants.Style.StepSelected.FontSize, true, true), "Font size did not match when details was selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.TextTransform), Constants.Style.StepSelected.TextTransform, true, true), "Text transform did not match when details was selected");

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.FontColor), Constants.Style.StepNotSelected.Color, true, true), "Font color did not match when preview was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.FontStyle), Constants.Style.StepNotSelected.FontStyle, true, true), "Font style did not match when preview was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.FontSize), Constants.Style.StepNotSelected.FontSize, true, true), "Font size did not match when preview was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.TextTransform), Constants.Style.StepNotSelected.TextTransform, true, true), "Text transform did not match when preview was not selected");

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.FontColor), Constants.Style.StepNotSelected.Color, true, true), "Font color did not match when publish was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.FontStyle), Constants.Style.StepNotSelected.FontStyle, true, true), "Font style did not match when publish was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.FontSize), Constants.Style.StepNotSelected.FontSize, true, true), "Font size did not match when publish was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.TextTransform), Constants.Style.StepNotSelected.TextTransform, true, true), "Text transform did not match when publish was not selected");

                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.PhaseDetails);

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.FontColor), Constants.Style.StepNotSelected.Color, true, true), "Font color did not match when metadata was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.FontStyle), Constants.Style.StepNotSelected.FontStyle, true, true), "Font style did not match when metadata was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.FontSize), Constants.Style.StepNotSelected.FontSize, true, true), "Font size did not match when metadata was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.TextTransform), Constants.Style.StepNotSelected.TextTransform, true, true), "Text transform did not match when metadata was not selected");

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.FontColor), Constants.Style.StepNotSelected.Color, true, true), "Font color did not match when details was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.FontStyle), Constants.Style.StepNotSelected.FontStyle, true, true), "Font style did not match when details was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.FontSize), Constants.Style.StepNotSelected.FontSize, true, true), "Font size did not match when details was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.TextTransform), Constants.Style.StepNotSelected.TextTransform, true, true), "Text transform did not match when details was not selected");

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.FontColor), Constants.Style.StepSelected.Color, true, true), "Font color did not match when preview was selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.FontStyle), Constants.Style.StepSelected.FontStyle, true, true), "Font style did not match when preview was selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.FontSize), Constants.Style.StepSelected.FontSize, true, true), "Font size did not match when preview was selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.TextTransform), Constants.Style.StepSelected.TextTransform, true, true), "Text transform did not match when preview was selected");

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.FontColor), Constants.Style.StepNotSelected.Color, true, true), "Font color did not match when publish was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.FontStyle), Constants.Style.StepNotSelected.FontStyle, true, true), "Font style did not match when publish was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.FontSize), Constants.Style.StepNotSelected.FontSize, true, true), "Font size did not match when publish was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.TextTransform), Constants.Style.StepNotSelected.TextTransform, true, true), "Text transform did not match when publish was not selected");

                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.Publish);

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.FontColor), Constants.Style.StepNotSelected.Color, true, true), "Font color did not match when metadata was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.FontStyle), Constants.Style.StepNotSelected.FontStyle, true, true), "Font style did not match when metadata was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.FontSize), Constants.Style.StepNotSelected.FontSize, true, true), "Font size did not match when metadata was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.MetaData, Utils.ControlStyle.TextTransform), Constants.Style.StepNotSelected.TextTransform, true, true), "Text transform did not match when metadata was not selected");

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.FontColor), Constants.Style.StepNotSelected.Color, true, true), "Font color did not match when details was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.FontStyle), Constants.Style.StepNotSelected.FontStyle, true, true), "Font style did not match when details was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.FontSize), Constants.Style.StepNotSelected.FontSize, true, true), "Font size did not match when details was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Details, Utils.ControlStyle.TextTransform), Constants.Style.StepNotSelected.TextTransform, true, true), "Text transform did not match when details was not selected");

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.FontColor), Constants.Style.StepNotSelected.Color, true, true), "Font color did not match when preview was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.FontStyle), Constants.Style.StepNotSelected.FontStyle, true, true), "Font style did not match when preview was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.FontSize), Constants.Style.StepNotSelected.FontSize, true, true), "Font size did not match when preview was not selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.PhaseDetails, Utils.ControlStyle.TextTransform), Constants.Style.StepNotSelected.TextTransform, true, true), "Text transform did not match when preview was not selected");

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.FontColor), Constants.Style.StepSelected.Color, true, true), "Font color did not match when publish was selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.FontStyle), Constants.Style.StepSelected.FontStyle, true, true), "Font style did not match when publish was selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.FontSize), Constants.Style.StepSelected.FontSize, true, true), "Font size did not match when publish was selected");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.MyCodaLabPage.CreatePage.GetTabControlStyle(CompetitionCreatePageOps.Tab.Publish, Utils.ControlStyle.TextTransform), Constants.Style.StepSelected.TextTransform, true, true), "Text transform did not match when publish was selected");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify the fields of the saved competition in the edit competition page")]
        [Priority(1)]
        public void Verify_SavedCompetition()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                this.competitionTitle = StringUtilities.GetRandomPlainString(20);
                this.competitionDescription = StringUtilities.GetRandomPlainString(50);

                Pages.MyCodaLabPage.CreateCompetition(this.competitionTitle, this.competitionDescription);

                //Pages.MyCodaLabPage.ClickEditCompetition(this.competitionTitle);
                Pages.MyCodaLabPage.SelectCompetitionOptionlink(MyCodaLabPageOps.CompetitionOptionLinks.Edit, this.competitionTitle);

                Assert.IsTrue(StringUtilities.CompareStrings(this.competitionTitle, Pages.MyCodaLabPage.CreatePage.MetaDataPage.GetTitle(), true, true), "Competition title was not as expected in the edit page");
                this.competitionTitle = StringUtilities.GetRandomPlainString(20);
                Pages.MyCodaLabPage.CreatePage.MetaDataPage.SetTitle(this.competitionTitle);

                Assert.IsTrue(StringUtilities.CompareStrings(this.competitionDescription, Pages.MyCodaLabPage.CreatePage.MetaDataPage.GetDescription(), true, true), "Competition description was not as expected in the edit page");
                this.competitionDescription = StringUtilities.GetRandomPlainString(50);
                Pages.MyCodaLabPage.CreatePage.MetaDataPage.SetShortDescription(this.competitionDescription);
                Pages.MyCodaLabPage.CreatePage.ClickNext();

                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.Publish);

                Assert.IsFalse(Pages.MyCodaLabPage.CreatePage.PublishPage.IsPublishCheckBoxChecked(), "Public checkbox was checked in the edit page");
                Pages.MyCodaLabPage.CreatePage.PublishPage.CheckPublishCompetition();
                Pages.MyCodaLabPage.CreatePage.ClickFinish();

                Assert.IsTrue(Pages.MyCodaLabPage.IsCompetitionPresent(this.competitionTitle), "Competition was not found in MyCodalab page after edit");
                Pages.CompetitionPage.ClickCompetitionLink();
                Assert.IsTrue(Pages.CompetitionPage.IsCompetitionPresent(this.competitionTitle), "Competition was not found in Competitions page after making competition public");

                this.deleteCompetition = true;
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify the three section headers of the competition list.")]
        [Priority(2)]
        public void Verify_CompetitionListSectionHeaders()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                List<string> headers = Pages.MyCodaLabPage.GetSectionHeaders();

                CollectionAssert.AreEquivalent(headers, Constants.StaticStrings.MyCodaLabPage.SectionHeaders, "Section headers are not equivalent");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify alert dialog when data in any of the step while creating competition is not saved but trying to navigate to next step")]
        [Priority(2)]
        public void Verify_AlertOperation()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                Pages.MyCodaLabPage.ClickCreateNewCompetitionLink();
                Pages.MyCodaLabPage.ClickStartInWizard();

                string title = StringUtilities.GetRandomPlainString(10);
                Pages.MyCodaLabPage.CreatePage.MetaDataPage.SetTitle(title);
                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.Details);

                Assert.IsTrue(Dialogs.IsAlertDisplayed(), "Alert dialog was not displayed");
                Assert.IsTrue(StringUtilities.CompareStrings(Dialogs.GetAlertText(), Constants.StaticStrings.Alert.StepChangeAlert, true, true), "String displayed in alert window did not match the actual");

                Dialogs.SelectAlertOption();
            }

            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to create more number of competitions")]
        public void CreateMoreCompetition()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                for (int i = 0; i < 1000; i++)
                {
                    try
                    {
                        Pages.MyCodaLabPage.CreateCompetition(isPublic: true);
                    }
                    catch
                    {
                        Pages.MyCodaLabPage.ClickMyCodaLabLink();
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
        [Description("Test case to create more number of competitions")]
        [Priority(1)]
        public void Verify_AddNewSubTabInLearnTheDetails()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                Pages.MyCodaLabPage.ClickCreateNewCompetitionLink();
                Pages.MyCodaLabPage.ClickStartInWizard();

                string title = StringUtilities.GetRandomPlainString(10);
                Pages.MyCodaLabPage.CreatePage.MetaDataPage.SetTitle(title);
                Pages.MyCodaLabPage.CreatePage.ClickSave();
                Pages.MyCodaLabPage.CreatePage.ClickNext();
                Pages.MyCodaLabPage.CreatePage.ClickNext();
                string tabText = StringUtilities.GetRandomPlainString(6);
                Pages.MyCodaLabPage.CreatePage.DetailsPage.CreateLDSubTab(3, tabText);
                Pages.MyCodaLabPage.CreatePage.ClickSave();
                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.Publish);
                Pages.MyCodaLabPage.CreatePage.PublishPage.PublishCompetition();
                Pages.MyCodaLabPage.CreatePage.ClickFinish();
                Assert.IsTrue(Pages.MyCodaLabPage.IsCompetitionPresent(title), "Competition created was not found in the list");
                Pages.MyCodaLabPage.SelectCompetitionOptionlink(MyCodaLabPageOps.CompetitionOptionLinks.Edit, title);

                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.Details);

                List<string> subtabs = Pages.MyCodaLabPage.CreatePage.DetailsPage.GetLDVisibleSubTabs();
                Assert.IsTrue(subtabs.Contains(tabText), "Tab created was not saved");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to make all the invisible tabs visible")]
        [Priority(2)]
        public void Verify_MakeAllTabsVisible()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                Pages.MyCodaLabPage.ClickCreateNewCompetitionLink();
                Pages.MyCodaLabPage.ClickStartInWizard();

                string title = StringUtilities.GetRandomPlainString(10);
                Pages.MyCodaLabPage.CreatePage.MetaDataPage.SetTitle(title);
                Pages.MyCodaLabPage.CreatePage.ClickSave();
                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.Details);

                List<string> newTabsAdded = new List<string>();
                for (int i = 3; i <= 7; i++)
                {
                    string tabText = StringUtilities.GetRandomPlainString(6);
                    Pages.MyCodaLabPage.CreatePage.DetailsPage.CreateLDSubTab(i, tabText);
                    Pages.MyCodaLabPage.CreatePage.ClickSave();
                    newTabsAdded.Add(tabText);
                }

                Pages.MyCodaLabPage.CreatePage.ClickSave();
                Pages.MyCodaLabPage.CreatePage.ClickNext();
                Pages.MyCodaLabPage.CreatePage.PublishPage.PublishCompetition();
                Pages.MyCodaLabPage.CreatePage.ClickFinish();

                Pages.MyCodaLabPage.SelectCompetitionOptionlink(MyCodaLabPageOps.CompetitionOptionLinks.Edit, title);
                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.Details);

                List<string> subtabs = Pages.MyCodaLabPage.CreatePage.DetailsPage.GetLDVisibleSubTabs();
                foreach (var item in newTabsAdded)
                {
                    Assert.IsTrue(subtabs.Contains(item), string.Format("{0} tab was not found in the list", item));
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify that the data in learn the details tab is saved")]
        [Priority(1)]
        public void Verify_LearnTheDetailsEdit()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();

                CompetitionDetails details = Pages.MyCodaLabPage.CreateCompetition(1)[0];
                Pages.MyCodaLabPage.SelectCompetitionOptionlink(MyCodaLabPageOps.CompetitionOptionLinks.Edit, details.Title);

                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.Details);
                string overViewText = StringUtilities.GetRandomPlainString(20);
                Pages.MyCodaLabPage.CreatePage.DetailsPage.SetTextIntoLDSubTabTextArea(overViewText);
                Pages.MyCodaLabPage.CreatePage.ClickSave();
                Pages.MyCodaLabPage.CreatePage.DetailsPage.SelectLDSubTab(1);
                string evaluationText = StringUtilities.GetRandomPlainString(20);
                Pages.MyCodaLabPage.CreatePage.DetailsPage.SetTextIntoLDSubTabTextArea(evaluationText);
                Pages.MyCodaLabPage.CreatePage.ClickSave();
                Pages.MyCodaLabPage.CreatePage.ClickNext();
                Pages.MyCodaLabPage.CreatePage.PublishPage.PublishCompetition();
                Pages.MyCodaLabPage.CreatePage.ClickFinish();

                Pages.MyCodaLabPage.SelectCompetitionOptionlink(MyCodaLabPageOps.CompetitionOptionLinks.Edit, details.Title);
                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.Details);
                Assert.IsTrue(StringUtilities.CompareStrings(overViewText, Pages.MyCodaLabPage.CreatePage.DetailsPage.GetTextFromLDSubTabTextArea(), true, true),
                    "Text in the overview tab was not as expected");

                Pages.MyCodaLabPage.CreatePage.DetailsPage.SelectLDSubTab(1);
                Assert.IsTrue(StringUtilities.CompareStrings(evaluationText, Pages.MyCodaLabPage.CreatePage.DetailsPage.GetTextFromLDSubTabTextArea(), true, true),
                    "Text in the evaluation tab was not as expected");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify the list of competitions I participate in")]
        [Priority(1)]
        public void Verify_CompetitionsIParticipateIn()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                this.competitionTitle = Pages.MyCodaLabPage.CreateCompetition(1)[0].Title;

                Pages.CompetitionPage.ClickCompetitionLink();
                Pages.CompetitionPage.SelectCompetition(this.competitionTitle);
                Pages.CompetitionDetailsPage.SelectTab(CompetitionDetailsPageOps.Tabs.Participate);
                Pages.CompetitionDetailsPage.ParticipatePage.RegisterForChallange();

                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                Pages.MyCodaLabPage.SelectTab(MyCodaLabPageOps.MyCodaLabTabs.CompetitionIParticipateIn);

                Assert.IsTrue(
                    Pages.MyCodaLabPage.IsCompetitionPresent(this.competitionTitle),
                    string.Format("Competition {0} was not found in the Competition I participate in page", this.competitionTitle));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify competition creation with phases")]
        [Priority(1)]
        public void Verify_CompetitionCreateWithPhases()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();

                DataSetDetails dataSetDetails1 = new DataSetDetails() { AccessString = StringUtilities.GetRandomPlainString(10), BlobURL = StringUtilities.GetRandomPlainString(10), DataSetType = PhaseDetailsPageOps.DataSetType.AzureBlob };
                DataSetDetails dataSetDetails2 = new DataSetDetails() { AccessString = StringUtilities.GetRandomPlainString(10), BlobURL = StringUtilities.GetRandomPlainString(10), DataSetType = PhaseDetailsPageOps.DataSetType.AzureBlobSharedAccessSignature };

                List<DataSetDetails> dsDetails = new List<DataSetDetails>();
                dsDetails.Add(dataSetDetails1);
                dsDetails.Add(dataSetDetails2);

                CompPhaseDetails phaseDetails = new CompPhaseDetails() { DataSets = dsDetails, MaxSubmissions = 10, StartDate = DateTime.Now.Date.ToString("MM/dd/yyyy"), Titile = StringUtilities.GetRandomPlainString(10) };

                Pages.MyCodaLabPage.ClickMyCodaLabLink();
                CompetitionDetails details = Pages.MyCodaLabPage.CreateCompetition(phaseDetails);
                this.competitionTitle = phaseDetails.Titile;
                Pages.MyCodaLabPage.SelectCompetitionOptionlink(MyCodaLabPageOps.CompetitionOptionLinks.Edit, details.Title);
                Pages.MyCodaLabPage.CreatePage.SelectTab(CompetitionCreatePageOps.Tab.PhaseDetails);

                List<CompPhaseDetails> actual = Pages.MyCodaLabPage.CreatePage.PhaseDetailsPage.GetAllPhaseDetails();

                Assert.AreEqual<string>(phaseDetails.Titile, actual[0].Titile, "Phase details are not as expected");
                Assert.AreEqual<string>(phaseDetails.StartDate, actual[0].StartDate, "Start date was not as expected");
                Assert.AreEqual<int>(phaseDetails.MaxSubmissions, actual[0].MaxSubmissions, "Maximum submissions are not as expected");
                Assert.AreEqual<MI_UI_Framework.PageOperations.PhaseDetailsPageOps.DataSetType>(phaseDetails.DataSets[0].DataSetType, actual[0].DataSets[0].DataSetType, 
                    "Dataset type is not as expected");
                Assert.AreEqual<string>(phaseDetails.DataSets[0].BlobURL, actual[0].DataSets[0].BlobURL, "Blob URL is not as expected");
                Assert.AreEqual<string>(phaseDetails.DataSets[0].AccessString, actual[0].DataSets[0].AccessString, "Access string is not as expected");
                Assert.IsTrue(!string.IsNullOrEmpty(actual[0].DataSets[0].DownloadURL), "Download URL was not generated");

                Assert.AreEqual<MI_UI_Framework.PageOperations.PhaseDetailsPageOps.DataSetType>(phaseDetails.DataSets[1].DataSetType, actual[0].DataSets[1].DataSetType,
                    "Dataset type is not as expected");
                Assert.AreEqual<string>(phaseDetails.DataSets[1].BlobURL, actual[0].DataSets[1].BlobURL, "Blob URL is not as expected");
                Assert.AreEqual<string>(phaseDetails.DataSets[1].AccessString, actual[0].DataSets[1].AccessString, "Access string is not as expected");
                this.deleteCompetition = true;
            }
           catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            try
            {
                if (ConfigurationManager.AppSettings["UseUI"] == "1" && this.deleteCompetition)
                {
                    Pages.MyCodaLabPage.ClickMyCodaLabLink();
                    Pages.MyCodaLabPage.DelteCompetition(this.competitionTitle);
                }
                else
                {
                    DBUtils.DeleteAllCompetitions();
                }
            }

            catch
            {
                // No catch as clean up should not fail the test case
            }

            finally
            {
                TestCleanUp();
            }
        }
    }
}
