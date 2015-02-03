using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using EducationV2.App_Code;
using System.Threading;

namespace EducationV2
{
    public class Global : System.Web.HttpApplication
    {

        //这里使用静态保持对这处Timer实例的引用，以免GC   
        private static System.Threading.Timer timer = null;

        void Application_Start(object sender, EventArgs e)
        {
            // 启动计时器，每天凌晨时计算哪些岗位申请业务关闭
            // 计算现在到目标时间要过的时间段。   
            DateTime LuckTime = DateTime.Now.Date.Add(new TimeSpan(23, 30, 0));
            TimeSpan span = LuckTime - DateTime.Now;
            if (span < TimeSpan.Zero)
            {
                span = LuckTime.AddDays(1d) - DateTime.Now;
            }

            //按需传递的状态或者对象。   
            object state = new object();
            //定义计时器   
            timer = new System.Threading.Timer(
                new TimerCallback(Task_ChangeApplicantState), 
                state,
                /*span*/ TimeSpan.Zero, 
                TimeSpan.FromTicks(TimeSpan.TicksPerDay));
        }



        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码

        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码

        }

        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
            // 或 SQLServer，则不会引发该事件。

        }


        //这里是指定时间执行的代码，必须是静态的。   
        private static void Task_ChangeApplicantState(object state)
        {
            LoggerHelper.Log("定时处理", "定时修改申请状态");
            // 计算哪些岗位申请业务关闭
            DataClassesDataContext dc = new DataClassesDataContext();
            // 用于跟踪LINQ 冲突错误
            //dc.Log = new System.IO.StringWriter();
            IEnumerable<ViewPosApl> datasource = from x in dc.ViewPosApl.AsEnumerable()
                         where x.F_statusEnum.Equals((short)ProjectStatus.EStatus.ApplicantCommit)
                             && (
                                   (x.F_endDate1 == null ? false : (x.F_endDate1.Value.Date > DateTime.Now.Date))
                                || (x.F_endDate2 == null ? false : (x.F_endDate2.Value.Date > DateTime.Now.Date))
                                || (x.F_endDate3 == null ? false : (x.F_endDate3.Value.Date > DateTime.Now.Date))
                                )
                         select x;
            //string temp = dc.Log.ToString();
            foreach (ViewPosApl vapl in datasource)
            {
                PosApplicant apl = dc.PosApplicant.SingleOrDefault(_apl => _apl.F_ID.Equals(vapl.F_ID));
                apl.F_status = ProjectStatus.ApplicantComplete;
                apl.F_statusEnum = (short)ProjectStatus.EStatus.ApplicantComplete;
            }
            dc.SubmitChanges();
        }   

        public static List<String> GetMinUsers(HttpSessionState session)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            String role = session[SessionMgm.Role].ToString();
            String userID = session[SessionMgm.UserID].ToString();
            List<String> result = new List<string>();
            User user = dc.User.SingleOrDefault(_user => _user.F_ID.Equals(userID));

            if (role.Equals(RoleType.Applicant))
            {
                result.Add(userID);
            }
            else if (role.Equals(RoleType.DeptAdmin))
            {
                var users = dc.User.Where(_user => _user.F_belongDeptID.Equals(user.F_belongDeptID) && 
                    (_user.F_Role.Equals(RoleType.DeptAdmin) || _user.F_Role.Equals(RoleType.Applicant) ));
                foreach (var t in users)
                    result.Add(t.F_ID);
            }
            else if (role.Equals(RoleType.OrgDeptAdmin))
            {
                //var users = dc.User.Where(_user => _user.F_belongUnitID.Equals(user.F_belongUnitID) && _user.F_Role.Equals(RoleType.EduAdmin) == false ) ;
                //foreach (var t in users)
                //    result.Add(t.F_ID);
            }
            else if (role.Equals(RoleType.EduAdmin))
            {
                var users = from _user in dc.User
                            where _user.F_Role.Equals(RoleType.OrgDeptAdmin) || _user.F_Role.Equals(RoleType.EduAdmin)
                            select _user.F_ID;
                foreach (var t in users)
                    result.Add(t);
            }
            return result;

        }

        public static List<string> GetSuitUsers(HttpSessionState session)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            String role = session[SessionMgm.Role].ToString();
            String userID = session[SessionMgm.UserID].ToString();
            List<String> result = new List<string>();
            User user = dc.User.SingleOrDefault(_user => _user.F_ID.Equals(userID));

            if (role.Equals(RoleType.Applicant))
            {
                result.Add(userID);
            }
            else
            {
                var users = dc.User;
                foreach (var t in users)
                    result.Add(t.F_ID);
            }          
            return result;
        }

    }
}
