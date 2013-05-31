using MI_UI_Framework.Utilities;
using OpenQA.Selenium;
using SeleniumHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_UI_Framework.PageOperations 
{
    public abstract class CompetitionListOps 
    {
        internal int index = -1;
        internal string baseQueryPath;

        internal abstract void Initialize();

        public List<CompetitionInList> GetAllCompetitions() 
        {
            Initialize();

            FrameworkBase.Log.LogMessage("Getting all the competitions");

            List<CompetitionInList> competitions = new List<CompetitionInList>();

            List<IWebElement> elements = Elements.GetElements(By.XPath(string.Format("//{0}div[@class='competitionTile']", baseQueryPath))).ToList<IWebElement>();

            int count = elements.Count;
            bool breakLoop = false;

            System.Threading.Thread.Sleep(2000);

            FrameworkBase.Log.LogMessage("Scrolling the window till all the competitions are loaded");

            do
            {
                Operations.ScrollWindow();
                System.Threading.Thread.Sleep(1000);
                elements = Elements.GetElements(By.XPath(string.Format("//{0}div[@class='competitionTile']", baseQueryPath))).ToList<IWebElement>();
                if (count == elements.Count)
                {
                    breakLoop = true;
                }
                else
                {
                    count = elements.Count;
                }

            } while (!breakLoop);

            FrameworkBase.Log.LogMessage("Gathering the competition details");

            if (elements.Count > 0)
            {
                for (int i = 1; i <= elements.Count; i++)
                {
                    string baseXpath = string.Format("//{0}div[@class='competitionTile'][{1}]", baseQueryPath, i);
                    string title = Operations.GetElementInnerText(Elements.GetElementByXPath(string.Format("{0}/article/div[@class='articleTextArea']/div/div[1]/h3", baseXpath)));
                    string owner = Operations.GetElementInnerText(Elements.GetElementByXPath(string.Format("{0}/article/div[@class='articleTextArea']/div/div[1]/label", baseXpath)));
                    string description = Operations.GetElementInnerText(Elements.GetElementByXPath(string.Format("{0}/article/div[@class='articleTextArea']/div/p", baseXpath)));

                    competitions.Add(new CompetitionInList() { Title = title, CreatedBy = owner, Description = description });
                }
            }
            return competitions;
        }

        public CompetitionInList GetCompetition(string title)
        {
            FrameworkBase.Log.LogMessage(string.Format("Getting competition with title {0}", title));
            return (from a in GetAllCompetitions()
                    where a.Title.Equals(title)
                    select a).First();
        }

        internal int GetCompetitionIndex(string title)
        {
            List<CompetitionInList> allcomeptitions = GetAllCompetitions();
            int index = -1;
            foreach (var item in allcomeptitions)
            {
                if (StringUtilities.CompareStrings(title, item.Title, true, true))
                {
                    index = allcomeptitions.IndexOf(item);
                    break;
                }
            }

            return index + 1;
        }

        public bool IsCompetitionPresent(string title)
        {
            FrameworkBase.Log.LogMessage(string.Format("Checking whether the competition {0} is present or not", title));
            int index = GetCompetitionIndex(title);

            if (index <= 0)
            {
                FrameworkBase.Log.LogMessage(string.Format("Competition with title {0} was not found in the page", title));
                return false;
            }

            FrameworkBase.Log.LogMessage(string.Format("Competition with title {0} not found in the page", title));
            return true;
        }

        public string GetCompetitionImageSource(string title)
        {
            int competitionIndex = GetCompetitionIndex(title);

            string xpath = string.Format("//{0}div[@class='articleImageContainer'][{1}]/div/figure", baseQueryPath, competitionIndex);

            return Operations.GetBackgroundImageURL(Elements.GetElementByXPath(xpath));
        }
    }
}
