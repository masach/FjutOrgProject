using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationV2.App_Code
{
    public class RoleType
    {
        public static String Applicant = "申请人员";
        public static String OrgDeptAdmin = "系统管理员";
        public static String OrgDeptDirector = "组织部部长";

        public static String DeptAdmin = "部门主管";        
        public static String TeamAdmin = "团队负责人";
        public static String EduAdmin = "教育厅管理人员";
        public static String ProjectMember = "项目成员"; 

        public static String Draft = "未提交";
        public static String UnderAudit = "待审核";
        public static String Authoried = "审核通过";
    }
}