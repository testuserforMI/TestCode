using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumHelper
{
    public class Operations  
    {
        #region Common operations
        /// <summary>
        /// Method to check whether the element is visible or not
        /// </summary>
        /// <param name="element">Element to be checked for visibility status</param>
        /// <returns>True if the element is visible in UI</returns>
        public static bool IsElementVisible(IWebElement element)
        {
            return element.Displayed;
        }

        /// <summary>
        /// Method to check whether the element exists in the HTML or not
        /// </summary>
        /// <param name="element">Element to be checked for existence status</param>
        /// <returns>True if element exists</returns>
        public static bool IsElementExists(IWebElement element)
        {
            bool result = false;

            if (element.Size != null)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Method to check whether the element is disabled or not
        /// </summary>
        /// <param name="element">Element to be checked for disabled status</param>
        /// <returns>True if the element is disabled</returns>
        public static bool IsElementDisabled(IWebElement element)
        {
            bool status = true;

            if (GetElementAttribute(element, "class").Contains("disabled") ||
                    GetElementAttribute(element, "disabled").ToLower().Contains("disabled") ||
                    GetElementAttribute(element, "disabled").ToLower().Contains("true") ||
                    GetElementAttribute(element, "class").ToLower().Contains("inactive"))
            {
                status = true;
            }
            else
            {
                status = false;
            }

            return status;
        }

        /// <summary>
        /// Method to check whether the element is enabled or not
        /// </summary>
        /// <param name="element">Element to be checked for enabled status</param>
        /// <returns>True if the element is enabled</returns>
        public static bool IsElementEnabled(IWebElement element)
        {
            return element.Enabled;
        }

        /// <summary>
        /// Method to get the element inner text
        /// </summary>
        /// <param name="element">Element to get the inner text property</param>
        /// <returns>Inner text of the element</returns>
        public static string GetElementInnerText(IWebElement element)
        {
            string text = string.Empty;

            if (!string.IsNullOrEmpty(element.Text))
            {
                text = element.Text;
            }
            else if (!string.IsNullOrEmpty(GetElementAttribute(element, "text")))
            {
                text = GetElementAttribute(element, "text");
            }
            else if (!string.IsNullOrEmpty(GetElementAttribute(element, "innertext")))
            {
                text = GetElementAttribute(element, "innertext");
            }
            else if (!string.IsNullOrEmpty(GetElementAttribute(element, "value")))
            {
                text = GetElementAttribute(element, "value");
            }

            return text;
        }

        /// <summary>
        /// Method to get the attribute value of element
        /// </summary>
        /// <param name="element">Element of which the attribute value need to be fetched</param>
        /// <param name="attribute">Attribute name of the element</param>
        /// <returns>Attribute value of the element</returns>
        public static string GetElementAttribute(IWebElement element, string attribute)
        {
            return element.GetAttribute(attribute);
        }

        /// <summary>
        /// Method to click on element
        /// </summary>
        /// <param name="element">Element on which the click operation need to be performed</param>
        public static void ClickElement(IWebElement element)
        {
            IsElementVisible(element);
            element.Click();
        }

        public static void ClickAction(IWebElement element)
        {
            IsElementVisible(element);
            Actions ac = new Actions(Browser.WebDriver);
            ac.Click(element);
            ac.Perform();
        }
        #endregion Common operations

        #region TextBox operations
        /// <summary>
        /// Method to set text into the text box
        /// </summary>
        /// <param name="element">Textbox element to which the text need to be set</param>
        /// <param name="text">Text to be set</param>
        public static void SetTextIntoTextBox(IWebElement element, string text)
        {
            ClickAction(element);
            element.Clear();
            element.SendKeys(text);
        }

        /// <summary>
        /// Method to get the set text from the text box element
        /// </summary>
        /// <param name="element">Element from which the text need to be fetched</param>
        /// <returns>Text that is set on the text box</returns>
        public static string GetTextFromTextBox(IWebElement element)
        {
            string text = string.Empty;

            if (!string.IsNullOrEmpty(element.Text))
            {
                text = element.Text;
            }

            else if (!string.IsNullOrEmpty(GetElementAttribute(element, "value")))
            {
                text = GetElementAttribute(element, "value");
            }

            return text;
        }
        #endregion TextBox operations

        #region Radiobutton operations
        /// <summary>
        /// Method to turn on the radio button
        /// </summary>
        /// <param name="element">Radio button element</param>
        public static void ONRadioButton(IWebElement element)
        {
            if (!element.Selected ||
                !string.Equals(GetElementAttribute(element, "checked"), "true"))
            {
                element.Click();
            }
        }

        /// <summary>
        /// Method to turn OFF the radio button
        /// </summary>
        /// <param name="element">Radio button element</param>
        public static void OFFRadioButton(IWebElement element)
        {
            if (element.Selected || string.Equals(GetElementAttribute(element, "checked"), "true"))
            {
                element.Click();
            }
        }
        #endregion Radiobutton operations

        #region Checkbox Operations
        /// <summary>
        /// Method to check the checkbox
        /// </summary>
        /// <param name="element">IWebElement object of checkbox</param>
        public static void CheckCheckBox(IWebElement element)
        {
            if (!element.Selected)
            {
                element.Click();
            }
        }

        /// <summary>
        /// Method to uncheck the checkbox
        /// </summary>
        /// <param name="element">IWebElement object of checkbox</param>
        public static void UnCheckCheckBox(IWebElement element)
        {
            if (element.Selected)
            {
                element.Click();
            }
        }
        #endregion Checkbox Operations

        #region Combobox
        /// <summary>
        /// Method to select an item from the combo box depending on the text displayed
        /// </summary>
        /// <param name="element">IWebElement object of combo box</param>
        /// <param name="text">Text of the option to be selected</param>
        public static void SelectItemByText(IWebElement element, string text)
        {
            SelectElement selectElement = new SelectElement(element);
            selectElement.SelectByText(text);
        }

        /// <summary>
        /// Method to select an item from the combo box depending on the value of the option
        /// </summary>
        /// <param name="element">IWebElement object of combo box</param>
        /// <param name="text">value of the option to be selected</param>
        public static void SelectItemByValue(IWebElement element, string value)
        {
            SelectElement selectElement = new SelectElement(element);
            selectElement.SelectByValue(value);
        }

        /// <summary>
        /// Method to select an item from the combo box depending on the index of the option
        /// </summary>
        /// <param name="element">IWebElement object of combo box</param>
        /// <param name="text">Index of the option to be selected</param>
        public static void SelectItemByIndex(IWebElement element, int index)
        {
            SelectElement selectElement = new SelectElement(element);
            selectElement.SelectByIndex(index);
        }

        /// <summary>
        /// Method to get the selected option text in the combo box
        /// </summary>
        /// <param name="element">IWebElement object of combo box</param>
        /// <returns>Text of the selected option</returns>
        public static string SelectedItemText(IWebElement element)
        {
            SelectElement selectElement = new SelectElement(element);
            IWebElement option = selectElement.SelectedOption;

            return GetElementAttribute(option, "text");
        }

        /// <summary>
        /// Method to get the selected option value
        /// </summary>
        /// <param name="element">IWebElement object of combo box</param>
        /// <returns>Value of the selected option</returns>
        public static string SelectedItemValue(IWebElement element)
        {
            SelectElement selectElement = new SelectElement(element);
            IWebElement option = selectElement.SelectedOption;

            return GetElementAttribute(option, "value");
        }

        /// <summary>
        /// Method to get all option values of the combo box
        /// </summary>
        /// <param name="element">IWebElement object of combo box</param>
        /// <returns>All the list option values</returns>
        public static List<string> GetAllOptionValues(IWebElement element)
        {
            List<string> values = new List<string>();
            SelectElement selecteItem = new SelectElement(element);

            foreach (var item in selecteItem.Options)
            {
                string value = GetElementAttribute(item, "value");
                if (!string.IsNullOrEmpty(value))
                {
                    values.Add(value);
                }
            }

            return values;
        }

        /// <summary>
        /// Method to get all option text of the combo box
        /// </summary>
        /// <param name="element">IWebElement object of combo box</param>
        /// <returns>All the list option texts</returns>
        public static List<string> GetAllOptionText(IWebElement element)
        {
            List<string> values = new List<string>();
            SelectElement selecteItem = new SelectElement(element);

            foreach (var item in selecteItem.Options)
            {
                string value = item.Text;

                if (string.IsNullOrEmpty(value))
                {
                    value = GetElementAttribute(item, "text");
                }

                else if (string.IsNullOrEmpty(value))
                {
                    value = GetElementAttribute(item, "innertext");
                }
                if (!string.IsNullOrEmpty(value))
                {
                    values.Add(value);
                }
            }

            return values;
        }

        /// <summary>
        /// Method to get the option count
        /// </summary>
        /// <param name="element">IWebElement object of combo box</param>
        /// <returns>Count of the options in the combo box</returns>
        public static int GetOptionCount(IWebElement element)
        {
            SelectElement selecteItem = new SelectElement(element);
            return selecteItem.Options.Count;
        }
        #endregion Combobox

        #region Table
        /// <summary>
        /// Method to get column names of the table
        /// </summary>
        /// <param name="element">IWebElement object of table</param>
        /// <returns>List of column names</returns>
        public static List<string> GetColumnNames(IWebElement element)
        {
            List<string> columnNames = new List<string>();
            int index = 0;
            int columnCount = element.FindElements(By.TagName("th")).Count;

            while (columnCount <= 0)
            {
                element = element.FindElement(By.XPath(".."));
                columnCount = element.FindElements(By.TagName("th")).Count;
            }

            foreach (var item in element.FindElements(By.TagName("th")))
            {
                string innerText = GetElementInnerText(item);

                if (string.IsNullOrEmpty(innerText))
                {
                    columnNames.Add(string.Format("BlankColumn{0}", index));
                    index++;
                }
                else
                {
                    columnNames.Add(innerText);
                }
            }

            return columnNames;
        }

        /// <summary>
        /// Method to get the table row
        /// </summary>
        /// <param name="element">IWebElement object of table</param>
        /// <param name="rowIndex">Table row index</param>
        /// <returns>Entire row as string</returns>
        public static string GetTableRow(IWebElement element, int rowIndex)
        {
            var rows = element.FindElements(By.TagName("tr"));
            return rows[rowIndex].Text;
        }

        /// <summary>
        /// Method to get the table row depending on the column index
        /// </summary>
        /// <param name="element">IWebElement object of table</param>
        /// <param name="rowIndex">Row index of the data in table</param>
        /// <param name="columnIndex">Column index of the row</param>
        /// <returns>Data matching the row index and column index</returns>
        public static string GetTableRow(IWebElement element, int rowIndex, int columnIndex)
        {
            var rows = element.FindElements(By.TagName("tr"));
            var columnValue = rows[rowIndex].FindElements(By.TagName("td"));
            if (columnValue.Count > 0)
            {
                return GetElementInnerText(columnValue[columnIndex]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Method to get the table row depending on the column name
        /// </summary>
        /// <param name="element">IWebElement object of table</param>
        /// <param name="rowIndex">Row index from which the data need to be fetched</param>
        /// <param name="columnName">Column name of the table from which data need to be fetched</param>
        /// <returns>Data matching the row index and column name</returns>
        public static string GetTableRow(IWebElement element, int rowIndex, string columnName)
        {
            List<string> columnNames = GetColumnNames(element);
            int columnIndex = columnNames.IndexOf(columnName);

            return GetTableRow(element, rowIndex, columnIndex);
        }

        /// <summary>
        /// Method to get the total row count
        /// </summary>
        /// <param name="element">IWebElement object of table</param>
        /// <returns>Count of number of rows in the table</returns>
        public static int GetTableRowCount(IWebElement element)
        {
            var rows = element.FindElements(By.TagName("tr"));
            return rows.Count;
        }

        /// <summary>
        /// Method to get the total column count
        /// </summary>
        /// <param name="element">IWebElement object of table</param>
        /// <returns>Count of number of columns in the table</returns>
        public static int GetColumnCount(IWebElement element)
        {
            var colums = element.FindElements(By.TagName("th"));
            return colums.Count;
        }

        /// <summary>
        /// Method to check whether the searching data in present in the table
        /// </summary>
        /// <param name="element">IWebElement object of table</param>
        /// <param name="searchString">String that need to be searched in the table</param>
        /// <returns>True if the data is found in the table</returns>
        public static bool IsDataPresent(IWebElement element, string searchString)
        {
            string text = element.Text;
            bool result = true;

            if (string.IsNullOrEmpty(text))
            {
                text = GetElementAttribute(element, "text");
            }

            else if (string.IsNullOrEmpty(text))
            {
                text = GetElementAttribute(element, "innertext");
            }

            if (!text.Equals(searchString))
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Method to get the row index of the search string
        /// </summary>
        /// <param name="element">IWebElement object of table</param>
        /// <param name="searchString">String for which the table need to be searched</param>
        /// <returns>Row index of the string matching. If not found then returned -1</returns>
        public static int GetRowIndex(IWebElement element, string searchString)
        {
            List<Dictionary<string, string>> allrows = GetAllRows(element);

            foreach (var item in allrows)
            {
                foreach (KeyValuePair<string, string> k in item)
                {
                    if (string.Equals(k.Value, searchString))
                    {
                        return allrows.IndexOf(item);
                    }
                }
            }

            return 0;
        }

        public static Dictionary<string, string> GetTableRowData(IWebElement element, int rowIndex)
        {
            Dictionary<string, string> rowData = new Dictionary<string, string>();

            List<string> columnNames = GetColumnNames(element);

            foreach (var item in columnNames)
            {
                if (!rowData.ContainsKey(item))
                {
                    int columnIndex = columnNames.IndexOf(item);
                    string row = GetTableRow(element, rowIndex, columnIndex);
                    rowData.Add(item, row);
                }
            }

            return rowData;
        }

        public static List<Dictionary<string, string>> GetAllRows(IWebElement tableElement)
        {
            List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();

            for (int i = 0; i < GetTableRowCount(tableElement); i++)
            {
                Dictionary<string, string> row = new Dictionary<string, string>();
                row = GetTableRowData(tableElement, i);
                rows.Add(row);
            }

            return rows;
        }

        public static List<string> GetColumnData(IWebElement tableElement, string columnName)
        {
            List<string> columnData = new List<string>();

            foreach (var item in GetAllRows(tableElement))
            {
                var keyvaluePair = item.Single(x => x.Key == columnName);
                columnData.Add(keyvaluePair.Value);
            }

            return columnData;
        }

        public static IWebElement GetTableRowElement(IWebElement tableElement, int rowIndex)
        {
            var rows = tableElement.FindElements(By.TagName("tr"));
            return rows[rowIndex];
        }

        #endregion Table

        #region Link
        /// <summary>
        /// Method to click link
        /// </summary>
        /// <param name="element">IWebElement object of table</param>
        public static void ClickLink(IWebElement element)
        {
            ClickElement(element);
        }

        /// <summary>
        /// Method to get the text on the link
        /// </summary>
        /// <param name="element">IWebElement object of table</param>
        /// <returns>Text present on the link</returns>
        public static string GetLinkText(IWebElement element)
        {
            return GetElementInnerText(element);
        }
        #endregion Link

        #region Button
        /// <summary>
        /// Method to click button
        /// </summary>
        /// <param name="element">IWebElement object of table</param>
        public static void ClickButton(IWebElement element)
        {
            ClickElement(element);
        }
        #endregion

        #region element styles
        /// <summary>
        /// Method to get the font style of the element
        /// </summary>
        /// <param name="element">Element for which font style need to be accessed</param>
        /// <returns>Font style</returns>
        public static string GetCSSFontFamily(IWebElement element)
        {
            return element.GetCssValue("font-family");
        }

        /// <summary>
        /// Method to get the font weight of the element
        /// </summary>
        /// <param name="element">Element for which the font weight need to be accessed</param>
        /// <returns>Font weight of the element</returns>
        public static string GetCSSFontWeight(IWebElement element)
        {
            return element.GetCssValue("font-weight");
        }

        /// <summary>
        /// Method to get the font color
        /// </summary>
        /// <param name="element">Element for which the font color need to be accessed</param>
        /// <returns>Font color of the element</returns>
        public static string GetCSSFontColor(IWebElement element)
        {
            string color = element.GetCssValue("color");

            return color;
        }

        public static string GetCSSFontSize(IWebElement element)
        {
            return element.GetCssValue("font-size");
        }

        public static string GetCSSFontStyle(IWebElement element)
        {
            return element.GetCssValue("font-style");
        }

        public static string GetCSSTextTransform(IWebElement element)
        {
            return element.GetCssValue("text-transform"); ;
        }

        public static string GetBackgroundImageURL(IWebElement element)
        {
            return element.GetCssValue("background-image");
        }
        #endregion

        public static void ScrollWindow()
        {
            string script = "window.scrollTo(0,Math.max(document.documentElement.scrollHeight," + "document.body.scrollHeight,document.documentElement.clientHeight));";
            Browser.WebDriver.ExecuteJavaScript(script);
        }

        public static void DoubleClickElement(IWebElement element)
        {
            Actions builder = new Actions(Browser.WebDriver);
            IAction doubleClick = builder.DoubleClick(element).Build();
            doubleClick.Perform(); 
        }

        public static void CaptureScreenShot(string filePathToBeSaved)
        {
            if (!Directory.Exists(Directory.GetParent(filePathToBeSaved).FullName))
            {
                Directory.CreateDirectory(Directory.GetParent(filePathToBeSaved).FullName);
            }
            Screenshot ss = ((ITakesScreenshot)Browser.WebDriver).GetScreenshot();
            ss.SaveAsFile(filePathToBeSaved, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}
