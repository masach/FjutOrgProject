using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationV2.App_Code;

namespace EducationV2
{
    public class LoggerHelper
    {
        internal static void Log(string type, string content)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            Logger logger = new Logger();
            logger.F_ID = Guid.NewGuid().ToString();
            logger.F_content = content;
            logger.F_datetime = DateTime.Now;
            logger.F_type = type;  
            if (null != HttpContext.Current)
            {
                System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
                logger.F_userID = session[SessionMgm.UserID].ToString();
                logger.F_ipAddr = session[SessionMgm.IPAddr].ToString();
            }
            dc.Logger.InsertOnSubmit(logger);
            dc.SubmitChanges();


        }
    }
}