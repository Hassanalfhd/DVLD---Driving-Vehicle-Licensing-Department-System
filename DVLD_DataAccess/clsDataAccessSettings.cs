using System;
using System.Configuration;


namespace DVLD_DataAccess
{
   public static class clsDataAccessSettings
    {
       //public static string ConnectionString = "Server=.;Database=DVLD_Project_Final;User Id=sa;Password=sa123456;";
       
       //public static string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

       public static string ConnectionString = ConfigurationManager.ConnectionStrings["MyDbConnectionString"].ConnectionString;

        public static void LogError(Exception ex)
        {
            string source = "DVLD_App";
            try
            {
                if (!System.Diagnostics.EventLog.SourceExists(source))
                {
                    System.Diagnostics.EventLog.CreateEventSource(source, "Application");
                }
                System.Diagnostics.EventLog.WriteEntry(source, ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            catch { /* فشل التسجيل لا يجب أن يعطل البرنامج */ }
        }
    }
}
