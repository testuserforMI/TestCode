using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_UI_Framework.Utilities
{
    public class Dialogs
    {
        public static string GetAlertText()
        {
            return SeleniumHelper.Dialogs.GetAlertText();
        }

        public static void SelectAlertOption(bool accept = true)
        {
            SeleniumHelper.Dialogs.SelectAlertOption(accept);
        }

        public static void UploadFile(string filePath)
        {
            SeleniumHelper.Dialogs.UploadFile(filePath);
        }

        public static void OpenFile(string fileLocation)
        {
            SeleniumHelper.Dialogs.OpenFile(fileLocation);
        }

        public static bool IsAlertDisplayed()
        {
            return SeleniumHelper.Dialogs.IsAlertDialogVisible();
        }
    }
}
