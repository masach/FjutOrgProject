using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EducationV2.App_Code;
using System.Text;

namespace EducationV2
{
    public partial class main : SystemLogin
    {
        protected StringBuilder sb = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litMenu.Text = LoadLeftMenu();
                labRole.Text = Session[SessionMgm.Role].ToString();
                labUserName.Text = Session[SessionMgm.RealName].ToString();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();

            Response.Write("<script>window.parent.location.href='Default.aspx';</script>");
        }

        public string LoadLeftMenu()
        {
            String role = Session[SessionMgm.Role].ToString();
            String status = GetStatus();
            List<ExtjsComponent> components = new List<ExtjsComponent>();
            StringBuilder sb = new StringBuilder();
            //非申报都是管理员
            if (!role.Equals(RoleType.Applicant))
            {
                sb.Append("<div title=\"部门信息管理\">");
                sb.Append("<ul class=\"panel-ul\">");
                
                sb.Append("<li onclick=\"javascript:addTab('部门信息管理','frmDeptManage.aspx');\">部门信息管理</li>");
                sb.Append("<li onclick=\"javascript:addTab('用户信息管理','frmUserManage.aspx');\">用户信息管理</li>");                
               
                sb.Append("</ul></div>");

                sb.Append("<div title=\"职工档案管理\">");
                sb.Append("<ul class=\"panel-ul\">");
                sb.Append("<li onclick=\"javascript:addTab('职工档案管理','frmStaffManage.aspx');\">职工档案管理</li>");
                sb.Append("</ul></div>");
            }
            //申报管理，只要经过审核就可以申报
            if (status.Equals(RoleType.Authoried))
            {
                sb.Append("<div title=\"岗位申请管理\">");
                sb.Append("<ul class=\"panel-ul\">");
                if (role.Equals(RoleType.OrgDeptAdmin))
                {
                    sb.Append("<li onclick=\"javascript:addTab('岗位信息管理','frmPositionManage.aspx');\">岗位信息管理</li>");
                }
                sb.Append("<li onclick=\"javascript:addTab('岗位申请管理','frmPosApplicantList.aspx');\">岗位申请列表</li>");
                sb.Append("</ul></div>");
            }

            sb.Append("<div title=\"个人信息维护\">");
            sb.Append("<ul class=\"panel-ul\">");
            sb.Append("<li onclick=\"javascript:addTab('个人信息','frmUserDetail.aspx?type=self');\">个人信息</li>");
            sb.Append("<li onclick=\"javascript:addTab('密码修改','frmModifyPWD.aspx');\">密码修改</li>");
            sb.Append("</ul></div>");

            return sb.ToString();

        }

        private string GetStatus()
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            String userID = Session[SessionMgm.UserID].ToString();
            String role = Session[SessionMgm.Role].ToString();
            User user = dc.User.SingleOrDefault(_user => _user.F_ID.Equals(userID));
            // 用于跟踪LINQ 冲突错误
            dc.Log = new System.IO.StringWriter();
            if (role.Equals(RoleType.OrgDeptAdmin) || role.Equals(RoleType.Applicant))
            {
                DeptMent dept = dc.DeptMent.SingleOrDefault(_dm => _dm.F_ID.Equals(user.F_belongDeptID));
                string temp = dc.Log.ToString();
                if (dept == null || dept.F_status != RoleType.Authoried || user.F_status != RoleType.Authoried)
                    return RoleType.Draft;
            }
            
            return RoleType.Authoried;
        }
    }

}
