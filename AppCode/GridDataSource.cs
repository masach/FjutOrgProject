using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.ComponentModel;
using System.Collections.Specialized;
using EducationV2.App_Code;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Web.SessionState;

namespace EducationV2
{

    public class GridDataSource
    {


        private bool IsAccept(String status)
        {
            bool result = false;
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetUsersInDept()
        {
            HttpSessionState Session = HttpContext.Current.Session;
            DataClassesDataContext dc = new DataClassesDataContext();
            String role = Session[SessionMgm.Role].ToString();
            IQueryable<ViewUserInDept> userIndepts;
            List<String> userIDs = Global.GetSuitUsers(Session);
            userIndepts = dc.ViewUserInDept.Where(_vu => userIDs.Contains(_vu.F_ID));
            return UtilHelper.ToDataTable(userIndepts);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetStaffsInDept()
        {
            //HttpSessionState Session = HttpContext.Current.Session;
            DataClassesDataContext dc = new DataClassesDataContext();
            //String role = Session[SessionMgm.Role].ToString();
            IQueryable<ViewStaffInDept> userIndepts = dc.ViewStaffInDept;
            //List<String> userIDs = Global.GetSuitUsers(Session);
            //userIndepts = dc.ViewUserInDept.Where(_vu => userIDs.Contains(_vu.F_ID));
            return UtilHelper.ToDataTable(userIndepts);
        }       


        [DataObjectMethod(DataObjectMethodType.Select)]
        DataTable GetDepts()
        {
            HttpSessionState Session = HttpContext.Current.Session;
            DataClassesDataContext dc = new DataClassesDataContext();
            var depts = dc.DeptMent.Where(dm => dm.F_unitID.Equals(Session[SessionMgm.UnitID].ToString()));
            return UtilHelper.ToDataTable(depts);
        }
    }
}