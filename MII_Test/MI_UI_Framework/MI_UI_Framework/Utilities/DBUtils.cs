using MI_UI_Framework.PageOperations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_UI_Framework.Utilities
{
    public class DBUtils   
    {
        public static SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["DefaultConnection"].ToString());

        static DBUtils()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public static List<CompetitionDetails> InsertCompetitions(int count = 1, bool isPublic = true)
        {
            string userName  = string.Empty;
            if (!string.IsNullOrEmpty(Pages.HomePage.GetLoggedInUserName()))
            {
                userName = Pages.HomePage.GetLoggedInUserName();
            }
            else
            {
                userName = GetRandomUserName();
            }
            return InsertCompetitions(userName, count, isPublic);
        }

        public static List<CompetitionDetails> InsertCompetitions(string userName, int count = 1, bool isPublic = true)
        {
            List<CompetitionDetails> details = new List<CompetitionDetails>();
            int userId = GetUserId(userName);
            
            string values = string.Empty;
            for (int i = 0; i < count; i++)
            {
                string title = StringUtilities.GetRandomString(10);
                string description = StringUtilities.GetRandomString(20);
                string changeData = string.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now);
                values = string.Format("{6}(1, '{0}','{1}','',null,{2}, '{3}','{4}', '{0}','{1}','',null,{2}, '{3}','{4}',{5},0),", title, description, userId, userName, changeData, Convert.ToInt32(isPublic), values);
                details.Add(new CompetitionDetails() { Title = title, Description = description, CreatedBy = userName, IsPublic = isPublic });
            }

            values = values.Remove(values.Length - 1);

            string sqlQuery = string.Format("INSERT into {0}.[dbo].[Competitions] values {1}", connection.Database, values);

            SqlCommand cmd = new SqlCommand(sqlQuery, connection);

            cmd.ExecuteScalar();

            foreach (var item in details)
            {
                int competitionId = GetCompetitionID(item.Title);
                InsertIntoCompetitionUserTable(competitionId, userId);
                InsertIntoCompetitionpageTable(competitionId);
                CompPhaseDetails phdetails1 = GetPhaseDetails();
                InsertIntoCompetitionPhasesTable(competitionId, 1, 1, phdetails1.Titile, phdetails1.StartDate, phdetails1.MaxSubmissions);

                CompPhaseDetails phdetails2 = GetPhaseDetails();
                InsertIntoCompetitionPhasesTable(competitionId, 1, 2, phdetails2.Titile, phdetails2.StartDate, phdetails2.MaxSubmissions);
            }

            return details;
        }

        private static CompPhaseDetails GetPhaseDetails()
        {
            CompPhaseDetails details = new CompPhaseDetails();

            details.Titile = StringUtilities.GetRandomString(10);
            details.StartDate = DateTime.Now.Date.ToString("MM/dd/yyyy");
            details.MaxSubmissions = 10;

            return details;
        }

        public static void DeleteAllCompetitions()
        {
            string sqlQuery = string.Format("DELETE FROM [{0}].[dbo].[Competitions] ", connection.Database);
            SqlCommand cmd = new SqlCommand(sqlQuery, connection);
            cmd.ExecuteScalar();
        }

        private static void InsertIntoCompetitionpageTable(int competitionId)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append(string.Format("INSERT into {0}.[dbo].[CompetitionPages] values", connection.Database));
            sqlQuery.Append(string.Format("({0},1,1,'Overview',1,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},1,2,'Evaluation',1,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},1,3,'Terms and Conditions',1,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},1,4,'untitled',0,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},1,5,'untitled',0,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},1,6,'untitled',0,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},1,7,'untitled',0,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},1,8,'untitled',0,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},1,9,'Get data',1,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},1,10,'Submit results',1,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));

            sqlQuery.Append(string.Format("({0},2,1,'Overview',1,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},2,2,'Evaluation',1,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},2,3,'Terms and Conditions',1,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},2,4,'untitled',0,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},2,5,'untitled',0,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},2,6,'untitled',0,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},2,7,'untitled',0,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},2,8,'untitled',0,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},2,9,'Get data',1,'','{1}'),",competitionId, StringUtilities.GetRandomPlainString(10)));
            sqlQuery.Append(string.Format("({0},2,10,'Submit results',1,'','{1}')",competitionId, StringUtilities.GetRandomPlainString(10)));

            SqlCommand cmd = new SqlCommand(sqlQuery.ToString(), connection);
            cmd.ExecuteScalar();
        }

        public static string GetRandomUserName()
        {   
            string sqlQuery = string.Format("SELECT TOP 1 UserName FROM [{0}].[dbo].[Users]", connection.Database);
            SqlCommand cmd = new SqlCommand(sqlQuery, connection);
            return cmd.ExecuteScalar().ToString().Trim();
        }

        private static int GetUserId(string userName)
        {
            string sqlQuery = string.Format("SELECT [{0}].[dbo].[Users].UserId FROM [{0}].[dbo].[Users] where [{0}].[dbo].[Users].UserName='{1}'", connection.Database, userName);
            SqlCommand cmd = new SqlCommand(sqlQuery, connection);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public static bool IsUserPresent(string userName) 
        {
            int userID = GetUserId(userName);
            return userID == 0 ? false : true;
        }

        private static int GetCompetitionID(string title)
        {
            string sqlQuery = string.Format("SELECT [{0}].[dbo].[Competitions].CompetitionId FROM [{0}].[dbo].[Competitions] where [{0}].[dbo].[Competitions].Info_Title='{1}'", connection.Database, title);
            SqlCommand cmd = new SqlCommand(sqlQuery, connection);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private static void InsertIntoCompetitionUserTable(int competitionId, int userID)
        {
            string sqlQuery = string.Format("INSERT into {0}.[dbo].[CompetitionOwners] values ({1},{2})", connection.Database, competitionId, userID);
            SqlCommand cmd = new SqlCommand(sqlQuery, connection);
            cmd.ExecuteScalar();
        }

        public static void DeleteCompetition(string title)
        {
            string sqlQuery = string.Format("DELETE FROM [{0}].[dbo].[Competitions] where [{0}].[dbo].[Competitions].Info_Title='{1}'", connection.Database, title);
            SqlCommand cmd = new SqlCommand(sqlQuery, connection);
            cmd.ExecuteScalar();
        }

        public static void InsertIntoCompetitionPhasesTable(int competitionId, int category, int number, string lable, string startDate, int maxSubmission)
        {
            string sqlQuery = string.Format("INSERT into {0}.[dbo].[CompetitionPhases] values ({1},{2},{3},'{4}','{5}',{6})", connection.Database, competitionId, category, number, lable, startDate, maxSubmission);
            SqlCommand cmd = new SqlCommand(sqlQuery, connection);
            cmd.ExecuteScalar();
        }
    }
}
