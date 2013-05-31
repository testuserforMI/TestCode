using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumHelper
{
    public class Elements
    {
        /// <summary>
        /// Method to explicitly wait for the control
        /// </summary>
        /// <param name="element">Element for which the explicit wait has to done</param>
        public static void ExplicitWaitForElement(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(Browser.WebDriver, TimeSpan.FromSeconds(20));
            wait.Until<bool>(d => { return element.Displayed; });
        }

        /// <summary>
        /// Method to get the element based on the element ID attribute
        /// </summary>
        /// <param name="id">ID of the element</param>
        /// <returns>IWebElement object which matches the passed ID</returns>
        public static IWebElement GetElementByID(string id, int noOfTries = 20)
        {
            return GetElement(By.Id(id));
        }

        /// <summary>
        /// Method to get the element based on the element name attribute
        /// </summary>
        /// <param name="id">Name attribute of the element</param>
        /// <returns>IWebElement object which matches the passed name</returns>
        public static IWebElement GetElementByName(string name, int noOfTries = 20)
        {
            return GetElement(By.Name(name));
        }

        /// <summary>
        /// Method to get the element based on the element Xpath
        /// </summary>
        /// <param name="id">Xpath of the element</param>
        /// <returns>IWebElement object which matches the passed XPath</returns>
        public static IWebElement GetElementByXPath(string xpath, int noOfTries = 20)
        {
            return GetElement(By.XPath(xpath));
        }

        /// <summary>
        /// Method to get the element based on the element linked text
        /// </summary>
        /// <param name="id">linked text of the element</param>
        /// <returns>IWebElement object which matches the passed linked text</returns>
        public static IWebElement GetElementByLinkedText(string text, int noOfTries = 20)
        {
            return GetElement(By.LinkText(text));
        }

        public static IWebElement GetElementByCSSSelector(string csspath, int noOfTries = 20)
        {
            return GetElement(By.CssSelector(csspath));
        }

        public static IWebElement GetElementByClassName(string className, int noOfTries = 20)
        {
            return GetElement(By.ClassName(className));
        }

        public static IWebElement GetElementByJavaScript(string script, int noOfTries = 20)
        {
            return (IWebElement)Browser.WebDriver.ExecuteJavaScriptAndGetValue<IWebElement>(script);
        }

        /// <summary>
        /// Method to get the element
        /// </summary>
        /// <param name="by">Element to be searched by</param>
        /// <returns>IWebElement object</returns>
        private static IWebElement GetElement(By by, int noOfTries = 20)
        {
            IWebElement element = null;
            for (int i = 0; i < noOfTries; i++)
            {
                element = WaitForElement(by);
                if (element != null)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }

            return element;
        }

        private static IWebElement WaitForElement(By by, int noOfTries = 20)
        {
            WebDriverWait wait = new WebDriverWait(Browser.WebDriver, TimeSpan.FromSeconds(200));
            IWebElement myDynamicElement = wait.Until<IWebElement>((d) =>
            {
                try
                {
                    return d.FindElement(by);
                }
                catch
                {
                    return null;
                }
            });

            return myDynamicElement;
        }

        /// <summary>
        /// Method to get the element
        /// </summary>
        /// <param name="by">Element to be searched by</param>
        /// <returns>IWebElement object</returns>
        public static IList<IWebElement> GetElements(By by, int noOfTries = 20)
        {
            IList<IWebElement> element = null;
            for (int i = 0; i < noOfTries; i++)
            {
                element = WaitForElements(by);
                if (element != null)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }

            return element;
        }

        private static IList<IWebElement> WaitForElements(By by, int noOfTries = 20)
        {
            WebDriverWait wait = new WebDriverWait(Browser.WebDriver, TimeSpan.FromSeconds(200));

            IList<IWebElement> myDynamicElement = wait.Until<IList<IWebElement>>((d) =>
            {
                try
                {
                    return d.FindElements(by).ToList<IWebElement>();
                }
                catch
                {
                    return null;
                }
            });

            return myDynamicElement;
        }
    }
}
