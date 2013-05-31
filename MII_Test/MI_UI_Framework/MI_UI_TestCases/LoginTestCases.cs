using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MI_UI_Framework;
using MI_UI_Framework.PageOperations;
using MI_UI_Framework.Utilities;
using System.Collections.Generic;

namespace MI_UI_TestCases
{
    [TestClass]
    public class LoginTestCases : BaseClass
    {
        [TestMethod]
        [Priority(0)]
        [Description("Test case to verify whether login to the application is successfully done or not")]
        public void Verify_Login() 
        {
            try
            {
                Browser.Start();
                Pages.HomePage.ClickRegisterLink();
                Register newuser = Pages.RegisterPage.RegisterNewUser();
                Pages.HomePage.ClickLogOff();
                Pages.LoginPage.Login(newuser.UserName, newuser.Password);

                string loggedinuser = Pages.HomePage.GetLoggedInUserName();
                Assert.IsTrue(StringUtilities.CompareStrings(loggedinuser, newuser.UserName, true, true),
                string.Format("Logged in user is not as expected. Registered user:{0}, Logged in user:{1}", newuser.UserName, loggedinuser));
            }
            catch(Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Priority(1)]
        [Description("Test case to verify whether after login to application, home tab will be highlighted by default")]
        public void Verify_LoginNavigation() 
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login();
                Assert.IsTrue(Pages.HomePage.IsArticleSectionVisible(), "Article section is not visible");

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.HomePage.GetHomeLinkFontColor(), Constants.Style.MenuSelected.Color, true, true), "Font color did not match");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.HomePage.GetHomeLinkFontSize(), Constants.Style.MenuSelected.FontSize, true, true), "Font size did not match");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.HomePage.GetHomeLinkFontStyle(), Constants.Style.MenuSelected.FontStyle, true, true), "Font style did not match");
                Assert.IsTrue(StringUtilities.CompareStrings(Pages.HomePage.GetHomeLinkTextTransform(), Constants.Style.MenuSelected.TextTransform, true, true), "Text transform did not match");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Priority(1)]
        [Description("Test case to verify log off")]
        public void Verify_LogOff()
        {
            try
            {
                Browser.Start();
                Pages.HomePage.ClickRegisterLink();
                Register newuser = Pages.RegisterPage.RegisterNewUser();
                Pages.HomePage.ClickLogOff();

                Pages.MyCodaLabPage.ClickMyCodaLabLink(false);
                Assert.IsTrue(Pages.LoginPage.IsUserNameFiledVisible(), "Log off did not happen properly as control did not find the username field");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Priority(2)]
        [Description("Test case to verify error messages when username and password field are set empty")]
        public void Verify_ErrorForEmptyUserNameAndPassword()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login("", "");

                List<string> errormessage = Pages.LoginPage.GetErrorMessages();

                Assert.IsTrue(errormessage.Contains(Constants.ErrorMessages.LoginPage.EmptyUserName), "Error message for empty username did not match");
                Assert.IsTrue(errormessage.Contains(Constants.ErrorMessages.LoginPage.EmptyPassword), "Error message for empty password did not match");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Priority(2)]
        [Description("Test case to verify error messages when username field is set empty")]
        public void Verify_ErrorForEmptyUserName()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login("", StringUtilities.GetRandomPlainString(7));

                List<string> errormessage = Pages.LoginPage.GetErrorMessages();

                Assert.IsTrue(errormessage.Contains(Constants.ErrorMessages.LoginPage.EmptyUserName), "Error message for empty username did not match");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Priority(2)]
        [Description("Test case to verify error messages when password field is set empty")]
        public void Verify_ErrorForEmptyPassword()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login(StringUtilities.GetRandomPlainString(7), "");

                List<string> errormessage = Pages.LoginPage.GetErrorMessages();

                Assert.IsTrue(errormessage.Contains(Constants.ErrorMessages.LoginPage.EmptyPassword), "Error message for empty password did not match");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Priority(2)]
        [Description("Test case to verify error messages when password field is set empty")]
        public void Verify_ErrorForInvalidUserNameAndPassword()
        {
            try
            {
                Browser.Start();
                Pages.LoginPage.Login(StringUtilities.GetRandomPlainString(7), StringUtilities.GetRandomPlainString(7));

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.LoginPage.GetSummaryError(), Constants.ErrorMessages.LoginPage.UnRegisteredUser, true, true),
                    "Error message for unregisterd or invalid user was not matching");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Priority(2)]
        [Description("Test case to verify error messages when password field is set empty")]
        public void Verify_ErrorForInvalidPassword()
        {
            try
            {
                Browser.Start();
                Pages.HomePage.ClickRegisterLink();
                Register newuser = Pages.RegisterPage.RegisterNewUser();
                Pages.HomePage.ClickLogOff();

                Pages.LoginPage.Login(newuser.UserName, StringUtilities.GetRandomPlainString(7));

                Assert.IsTrue(StringUtilities.CompareStrings(Pages.LoginPage.GetSummaryError(), Constants.ErrorMessages.LoginPage.UnRegisteredUser, true, true),
                    "Error message for when invalid password was entered did not match");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Priority(2)]
        [Description("Test case to verify register link in login page")]
        public void Verify_RegisterLinkNavigationInLoginPage()
        {
            try
            {
                Browser.Start();
                Pages.HomePage.ClickLogin();
                Pages.LoginPage.ClickRegisterLink();

                Assert.IsTrue(Pages.RegisterPage.IsUserNameFieldVisible(),
                    "User name filed in the register page was not visible when register link was clicked from login page");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }
    }
}
