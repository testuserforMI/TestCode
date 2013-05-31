using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MI_UI_Framework;
using MI_UI_Framework.PageOperations;
using MI_UI_Framework.Utilities;

namespace MI_UI_TestCases
{
    [TestClass]
    public class CompetitionTestCases : BaseClass
    {
        [TestMethod]
        [Priority(1)]
        [Description("Test case to verify whether the Competition tab is highlighted when clicked")]
        public void Verify_CompetitionLinkNavigation()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.CompetitionPage.ClickCompetitionLink();

                Assert.IsTrue(Pages.CompetitionPage.IsFilterCompVisible(), "Filter comp section is not visible");

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.CompetitionPage.GetCompetitionLinkFontColor(), Constants.Style.MenuSelected.Color, true, true), "Font color did not match");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.CompetitionPage.GetCompetitionLinkFontSize(), Constants.Style.MenuSelected.FontSize, true, true), "Font size did not match");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.CompetitionPage.GetCompetitionLinkFontStyle(), Constants.Style.MenuSelected.FontStyle, true, true), "Font style did not match");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.CompetitionPage.GetCompetitionLinkTextTransform(), Constants.Style.MenuSelected.TextTransform, true, true), "Text transform did not match");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Priority(1)]
        [Description("Test case to verify that home tab is not highlighted when competition tab is clicked")]
        public void Verify_HomeLink_Style_InCompetitionPage()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Pages.CompetitionPage.ClickCompetitionLink();

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
    }
}
