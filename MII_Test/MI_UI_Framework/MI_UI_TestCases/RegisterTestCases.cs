using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MI_UI_Framework;
using MI_UI_Framework.Utilities;

namespace MI_UI_TestCases
{
    [TestClass]
    public class RegisterTestCases : BaseClass
    {
        [TestMethod]  
        [Description("Test case to verify registration of new user")]
        [Priority(0)]
        public void Verify_RegisterUser()
        {
            try
            {
                Browser.Start();
                Pages.HomePage.ClickRegisterLink();

                string userName = StringUtilities.GetRandomPlainString(5);
                string password = StringUtilities.GetRandomPlainString(7);
                Pages.RegisterPage.SetUserName(userName);
                Pages.RegisterPage.SetPassword(password);
                Pages.RegisterPage.SetConfirmPassword(password);
                Pages.RegisterPage.ClickRegister();

                string loggedinuser = Pages.HomePage.GetLoggedInUserName();

                Assert.IsTrue(StringUtilities.CompareStrings(loggedinuser, userName, true, true),
                    string.Format("Logged in user is not same as registered username. Registered user:{0}, Logged in user:{1}", userName, loggedinuser));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify error message when existing username is used to register")]
        [Priority(2)]
        public void Verify_Register_SameUserName()
        {
            try
            {
                Browser.Start();
                Pages.HomePage.ClickRegisterLink();

                string userName = StringUtilities.GetRandomPlainString(5);
                string password = StringUtilities.GetRandomPlainString(7);
                Pages.RegisterPage.SetUserName(userName);
                Pages.RegisterPage.SetPassword(password);
                Pages.RegisterPage.SetConfirmPassword(password);
                Pages.RegisterPage.ClickRegister();

                Pages.HomePage.ClickLogOff();
                Pages.HomePage.ClickRegisterLink();

                Pages.RegisterPage.SetUserName(userName);
                Pages.RegisterPage.SetPassword(password);
                Pages.RegisterPage.SetConfirmPassword(password);
                Pages.RegisterPage.ClickRegister();

                Assert.IsTrue(Pages.RegisterPage.GetErrorMessages().Contains(Constants.ErrorMessages.RegisterPage.SameUserName), "Error message for same user name was not as expected");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify error message when username filed is kept empty during register")]
        [Priority(2)]
        public void Verify_Register_EmptyUserName()
        {
            try
            {
                Browser.Start();
                Pages.HomePage.ClickRegisterLink();

                string password = StringUtilities.GetRandomPlainString(7);
                Pages.RegisterPage.SetUserName(string.Empty);
                Pages.RegisterPage.SetPassword(password);
                Pages.RegisterPage.SetConfirmPassword(password);
                Pages.RegisterPage.ClickRegister();

                Assert.IsTrue(Pages.RegisterPage.GetErrorMessages().Contains(Constants.ErrorMessages.RegisterPage.UserNameRequired), "Error message when username field was kept empty, was not as expected");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify error message when password filed is kept empty during register")]
        [Priority(2)]
        public void Verify_Register_EmptyPassword()
        {
            try
            {
                Browser.Start();
                Pages.HomePage.ClickRegisterLink();

                string userName = StringUtilities.GetRandomPlainString(7);
                Pages.RegisterPage.SetUserName(userName);
                Pages.RegisterPage.SetPassword(string.Empty);
                Pages.RegisterPage.SetConfirmPassword(string.Empty);
                Pages.RegisterPage.ClickRegister();

                Assert.IsTrue(Pages.RegisterPage.GetErrorMessages().Contains(Constants.ErrorMessages.RegisterPage.PasswordRequired), "Error message when password field was kept empty, was not as expected");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify error message when password and confirm password did not match")]
        [Priority(2)]
        public void Verify_Register_PasswordMismatch()
        {
            try
            {
                Browser.Start();
                Pages.HomePage.ClickRegisterLink();

                Pages.RegisterPage.SetUserName(StringUtilities.GetRandomPlainString(7));
                Pages.RegisterPage.SetPassword(StringUtilities.GetRandomPlainString(7));
                Pages.RegisterPage.SetConfirmPassword(StringUtilities.GetRandomPlainString(7));
                Pages.RegisterPage.ClickRegister();

                Assert.IsTrue(Pages.RegisterPage.GetErrorMessages().Contains(Constants.ErrorMessages.RegisterPage.PasswordMismatch), "Error message when password and confirm password did not match, was not as expected");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        [TestMethod]
        [Description("Test case to verify error message when password entered is less than 6 characters")]
        [Priority(2)]
        public void Verify_Register_PasswordLessThan6Characters()
        {
            try
            {
                Browser.Start();
                Pages.HomePage.ClickRegisterLink();

                string password = StringUtilities.GetRandomPlainString(5);
                Pages.RegisterPage.SetUserName(StringUtilities.GetRandomPlainString(7));
                Pages.RegisterPage.SetPassword(password);
                Pages.RegisterPage.SetConfirmPassword(password);
                Pages.RegisterPage.ClickRegister();

                Assert.IsTrue(Pages.RegisterPage.GetErrorMessages().Contains(Constants.ErrorMessages.RegisterPage.PasswordLessThan6Char), "Error message when password and confirm password was entered with less than 6 characters, was not as expected");
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }
    }
}
