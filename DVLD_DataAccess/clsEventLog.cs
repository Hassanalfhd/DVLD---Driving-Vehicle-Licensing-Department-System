using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DVLD_DataAccess
{
   public abstract class clsEventLog
    {

       public static void WriteEntryExceptionToEventViewer(string Message, EventLogEntryType Type)
       {
           string sourceApp = "DVLD";
           string logName = "Application";
           if (!EventLog.Exists(sourceApp))
           {
               EventLog.CreateEventSource(sourceApp, logName);
           }


           EventLog.WriteEntry(sourceApp, Message, Type);
       }

    }
}
