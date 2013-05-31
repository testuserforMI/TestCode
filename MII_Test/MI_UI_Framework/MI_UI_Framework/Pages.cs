using MI_UI_Framework.PageOperations;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumHelper;

namespace MI_UI_Framework
{
    public static class Pages
    {
        public static RegisterPageOps RegisterPage
        {
            get
            {
                var pageOps = new RegisterPageOps();
                //PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, pageOps);
                return pageOps;
            }
        }

        public static HomePageOps HomePage
        {
            get
            {
                var pageOps = new HomePageOps();
                //PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, pageOps);
                return pageOps;
            }
        }

        public static LoginPageOps LoginPage
        {
            get
            {
                var pageOps = new LoginPageOps();
                //PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, pageOps);
                return pageOps;
            }
        }

        public static CompetitionPageOps CompetitionPage
        {
            get
            {
                var pageOps = new CompetitionPageOps();
                //PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, pageOps);
                return pageOps;
            }
        }

        public static MyCodaLabPageOps MyCodaLabPage
        {
            get
            {
                var pageOps = new MyCodaLabPageOps();
                //PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, pageOps);
                return pageOps;
            }
        }

        public static CompetitionDetailsPageOps CompetitionDetailsPage
        {
            get
            {
                var pageOps = new CompetitionDetailsPageOps();
                //PageFactory.InitElements(SeleniumHelper.Browser.WebDriver, pageOps);
                return pageOps;
            }
        }
    }
}
