using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace EducationV2.App_Code
{
    public class ExpertBll
    {
        #region Departments

        public static void DelDepts(List<String> deptIDs)
        {
            DataClassesDataContext dc = new DataClassesDataContext();          
            foreach (String deptID in deptIDs)
            {
                var users = dc.User.Where(_user => _user.F_belongDeptID.Equals(deptID) 
                    && _user.F_Role.Equals(RoleType.OrgDeptDirector) == false
                    && _user.F_Role.Equals(RoleType.OrgDeptAdmin) == false);
                dc.User.DeleteAllOnSubmit(users);

            }
            var depts = dc.DeptMent.Where(_dept => deptIDs.Contains(_dept.F_ID)
                && _dept.F_name.Equals(SpecialUnitAndDept.DEPT_NAME_FJUTORG) == false);
            dc.DeptMent.DeleteAllOnSubmit(depts);
            dc.SubmitChanges();         
        }

        public static void AuthDepts(List<String> DeptIDs)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            var Depts = dc.DeptMent.Where(_Dept => DeptIDs.Contains(_Dept.F_ID));
            foreach (var Dept in Depts)
            {
                Dept.F_status = RoleType.Authoried;              
            }
            dc.SubmitChanges();
        }

        public static void UnAuthDepts(List<String> DeptIDs)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            var Depts = dc.DeptMent.Where(_Dept => DeptIDs.Contains(_Dept.F_ID));
            foreach (var Dept in Depts)
            {
                Dept.F_status = RoleType.UnderAudit;
            }
            dc.SubmitChanges();
        }
#endregion

        #region Users
        public static void DelUsers(List<String> userIDs)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            var users = dc.User.Where( _user => userIDs.Contains( _user.F_ID)
                  && _user.F_Role.Equals(RoleType.OrgDeptDirector) == false);
            dc.User.DeleteAllOnSubmit(users);
            dc.SubmitChanges();
        }

        public static void AuthUsers(List<String> userIDs)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            var users = dc.User.Where(_user => userIDs.Contains(_user.F_ID));
            foreach(var user in users)
            {
                user.F_status = RoleType.Authoried;
                user.F_lastModifyTime = DateTime.Now;
            }
            dc.SubmitChanges();
        }

        public static void UnAuthUsers(List<String> userIDs)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            var users = dc.User.Where(_user => userIDs.Contains(_user.F_ID)
                  && _user.F_Role.Equals(RoleType.OrgDeptDirector) == false);
            foreach (var user in users)
            {
                user.F_status = RoleType.UnderAudit;
                user.F_lastModifyTime = DateTime.Now;
            }
            dc.SubmitChanges();
        }

#endregion

        #region Staff
        public static void DelStaffs(List<String> staffIDs)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            var staffs = dc.Staff.Where(_staff => staffIDs.Contains(_staff.F_StaffID));
            dc.Staff.DeleteAllOnSubmit(staffs);
            dc.SubmitChanges();
        }

        public static void AuthStaffs(List<String> staffIDs)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            var staffs = dc.Staff.Where(_staff => staffIDs.Contains(_staff.F_StaffID));
            foreach (var staff in staffs)
            {
                staff.F_status = RoleType.Authoried;
                staff.F_lastModifyTime = DateTime.Now;
            }
            dc.SubmitChanges();
        }

        public static void UnAuthStaffs(List<String> staffIDs)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            var staffs = dc.Staff.Where(_staff => staffIDs.Contains(_staff.F_StaffID));
            foreach (var staff in staffs)
            {
                staff.F_status = RoleType.UnderAudit;
                staff.F_lastModifyTime = DateTime.Now;
            }
            dc.SubmitChanges();
        }

        #endregion
        
        #region Positions


        public static void DeletePositions( List<String> ids)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            foreach (String id in ids)
            {
                var positions = dc.Position.Where(_ap => ids.Contains(_ap.F_ID));
                dc.Position.DeleteAllOnSubmit(positions);
                dc.SubmitChanges();
            }
        }

        public static void AuthPositions(List<String> userIDs)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            var positions = dc.Position.Where(_pos => userIDs.Contains(_pos.F_ID));
            foreach (var pos in positions)
            {
                pos.F_status = RoleType.Authoried;              
            }
            dc.SubmitChanges();
        }

        public static void UnAuthPositions(List<String> userIDs)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            var positions = dc.Position.Where(_pos => userIDs.Contains(_pos.F_ID));
            foreach (var pos in positions)
            {
                pos.F_status = RoleType.UnderAudit;              
            }
            dc.SubmitChanges();
        }

        public static void SetPositionsEndDate(List<String> userIDs, DateTime dt)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            var positions = dc.Position.Where(_pos => userIDs.Contains(_pos.F_ID));
            foreach (var pos in positions)
            {
                pos.F_endDate = dt;
            }
            dc.SubmitChanges();
        }

        

#endregion

   
    }
}