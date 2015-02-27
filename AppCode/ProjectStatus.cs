using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationV2.App_Code
{
    public class ProjectStatus
    {


#region 状态文本
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
            ApplicantDraft = 11,
            ApplicantCommit = 12,       
            ApplicantComplete = 13      
        }


        //public static bool IsUnitAuthrize(String statue)
        //{
        //    bool result = false;
        //    if (UnderEducationAudit.Equals(statue) || Approval.Equals(statue))
        //        result = true;
        //    return result;
        //}

        //public static bool IsCommit(String status)
        //{
        //    bool result = false;
        //    if (status.Equals(ApplicantDraft) == false && status.Equals(Deny) == false)
        //        result = true;
        //    return result;
        //}
    }
}