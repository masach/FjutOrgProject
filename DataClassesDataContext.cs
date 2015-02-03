using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationV2
{
    partial class DataClassesDataContext
    {
        partial void DeleteDeptMent(DeptMent instance)
        {
            ExecuteDynamicDelete(instance);
            LoggerHelper.Log("部门删除", "部门名:" + instance.F_name + ",ID：" + instance.F_ID);  
        }

        partial void DeleteUser(User instance)
        {
            ExecuteDynamicDelete(instance);
            LoggerHelper.Log("用户删除", "用户名:" + instance.F_userName + ",ID：" + instance.F_ID);  
        }

        partial void DeletePosApplicant(PosApplicant instance)
        {
            ExecuteDynamicDelete(instance);
            LoggerHelper.Log("岗位申请删除", "用户ID:" + instance.F_UserID + ",ID：" + instance.F_ID);
        }

        partial void DeleteApplicantUnit(ApplicantUnit instance)
        {
            ExecuteDynamicDelete(instance);
            LoggerHelper.Log("单位删除", "项目名:" + instance.F_name + ",ID：" + instance.F_ID);               
        }
    }
}