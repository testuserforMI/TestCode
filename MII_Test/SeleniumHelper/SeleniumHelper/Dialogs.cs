using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumHelper
{
    public class Dialogs
    {
        public static string GetAlertText()
        {
            IAlert alert = Browser.WebDriver.SwitchTo().Alert();
            return alert.Text;
        }

        public static void SelectAlertOption(bool accept = true)
        {
            IAlert alert = Browser.WebDriver.SwitchTo().Alert();
            if (accept)
            {
                alert.Accept();
            }
            else
            {
                alert.Dismiss();
            }
        }

        public static void UploadFile(string filePath)
        {
            IntPtr hwnd = FindWindow(null, "File Upload");
            SetPath(hwnd, filePath);
            IntPtr hokBtn = GetDlgItem(hwnd, 1);
            uint id = GetDlgCtrlID(hokBtn);
            SetActiveWindow(hwnd);
            IntPtr res = SendMessage(hokBtn, (int)0x00F5, 0, IntPtr.Zero);
        }

        public static void OpenFile(string fileLocation)
        {
            IntPtr hwnd = FindWindow(null, "Open");
            SetPath(hwnd, fileLocation);
            IntPtr hokBtn = GetDlgItem(hwnd, 1);
            uint id = GetDlgCtrlID(hokBtn);
            SetActiveWindow(hwnd);
            IntPtr res = SendMessage(hokBtn, (int)0x00F5, 0, IntPtr.Zero);
        }

        public static bool IsAlertDialogVisible()
        {
            bool displayed = true;
            IAlert alert = Browser.WebDriver.SwitchTo().Alert();

            if (alert == null)
            {
                displayed = false;
            }

            return displayed;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string className, string windowsName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern uint GetDlgCtrlID(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = false)]
        private static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, string lParam);
        private const uint WM_SETTEXT = 0x000C;

        private static void SetPath(IntPtr handle, string fileLocation)
        {
            IntPtr iptrHWndControl = GetDlgItem(handle, 1148);
            HandleRef hrefHWndTarget = new HandleRef(null, iptrHWndControl);
            SendMessage(hrefHWndTarget, WM_SETTEXT, IntPtr.Zero, fileLocation);
        }
    }
}
