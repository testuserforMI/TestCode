using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_UI_TestCases
{
    public class Constants
    {
        public class Style
        {
            public struct MenuSelected 
            {
                public static string FontSize = "17px";
                public static string FontStyle = "normal";
                public static string TextTransform = "uppercase";
                public static string Color = "rgba(80, 224, 251, 1)";
            }

            public struct MenuNotSelected
            {
                public static string FontSize = "17px";
                public static string FontStyle = "normal";
                public static string TextTransform = "uppercase";
                public static string Color = "rgba(255, 255, 255, 1)";
            }

            public struct StepSelected
            {
                public static string FontSize = "16px";
                public static string FontStyle = "normal";
                public static string TextTransform = "none";
                public static string Color = "rgba(255, 255, 255, 1)";
            }

            public struct StepNotSelected
            {
                public static string FontSize = "16px";
                public static string FontStyle = "normal";
                public static string TextTransform = "none";
                public static string Color = "rgba(0, 0, 0, 1)";
            }
        }

        public class ErrorMessages
        {
            public struct RegisterPage 
            {
                public const string SameUserName = "User name already exists. Please enter a different user name.";
                public const string UserNameRequired = "The User name field is required.";
                public const string PasswordRequired = "The Password field is required.";
                public const string PasswordMismatch = "The password and confirmation password do not match.";
                public const string PasswordLessThan6Char = "The Password must be at least 6 characters long.";
            }

            public struct LoginPage
            {
                public const string EmptyUserName =  "The User name field is required.";
                public const string EmptyPassword = "The Password field is required.";
                public const string UnRegisteredUser = "The user name or password provided is incorrect.";
            }
        }
        public class StaticStrings
        {
            public struct MyCodaLabPage
            {
                public static List<string> SectionHeaders = new List<string>() { "Competitions I Manage", "Competitions I Participate In", "My Worksheets" };
                public const string ParticipantApproved = "Participation approved";
                public const string ParticipantRejected = "Participation rejected";
                public const string ParticipantPendingApproval = "Participation pending approval";
            }

            public struct Alert
            {
                public const string StepChangeAlert = "You have changed the input value and Do you want to continue";
            }

            public struct ParticipantPage
            {
                public const string ParticipationPendingApproval = "Participation pending approval";
            }
        }
    }
}
