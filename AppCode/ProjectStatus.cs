using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationV2.App_Code
{
    public class ProjectStatus
    {
        /// <summary>
        /// 等待部门审批
        /// </summary>
        public static String UnderDeptAudit = "等待部门审批";
        /// <summary>
        /// 等待学校审批
        /// </summary>
        public static String UnderSchoolAudit = "等待系统管理员审批";

        /// <summary>
        /// 等待教育厅审批
        /// </summary>
        public static String UnderEducationAudit = "等待教育厅审批";
        /// <summary>
        /// 立项
        /// </summary>
        public static String Approval = "立项";

        /// <summary>
        /// 所有审批都通过
        /// </summary>
        public static String Pass = "审核通过";
        /// <summary>
        /// 审批不通过
        /// </summary>
        public static String Deny = "审批没有通过";

        public static String PassEduCheck = "通过教育厅审核";

#region 验收状态


        /// <summary>
        /// 初始为草稿状态，用户可进行修改，管理员无法看到该信息
        /// </summary>
        public static String ApplicantDraft = "草稿";
        /// <summary>
        /// 用户提交，此时用户仍可修改。
        /// </summary>
        public static String ApplicantCommit = "已提交";
     
        public static String ApplicantComplete = "已截止";

#endregion


        public enum EStatus
        {
            // 3xx 代表验收状态
            ApplicantDraft = 11,
            ApplicantCommit = 12,       
            ApplicantComplete = 13,       

        

            // 撤项
            Rescind = 51
        }


        public static bool IsUnitAuthrize(String statue)
        {
            bool result = false;
            if (UnderEducationAudit.Equals(statue) || Approval.Equals(statue))
                result = true;
            return result;
        }

        public static bool IsCommit(String status)
        {
            bool result = false;
            if (status.Equals(ApplicantDraft) == false && status.Equals(Deny) == false)
                result = true;
            return result;
        }
    }
}