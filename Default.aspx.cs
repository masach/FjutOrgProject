using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EducationV2.App_Code;

namespace EducationV2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Add(SessionMgm.IPAddr, UtilHelper.GetClientIPv4Address());             
        }

  

        protected void btnCommit_Click(object sender, ImageClickEventArgs e)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            User user = dc.User.SingleOrDefault(u => u.F_userName.Equals(txtUserName.Text) && 
                u.F_pwd.Equals(UtilHelper.MD5Encrypt(txtPwd.Text)) && u.F_Role.Equals(ddlType.SelectedValue)  );
       
            if (user == null)
            {
                Response.Write("<script>alert('密码错误或用户不存在');</script>");
            }
            else
            {
                Session.Add(SessionMgm.UserID, user.F_ID);
                Session.Add(SessionMgm.Role, user.F_Role);
                Session.Add(SessionMgm.RealName, user.F_realName);
                Session.Add(SessionMgm.UnitID, SpecialUnitAndDept.UNIT_ID_FJUT);
               
                //if (user.F_Role.Equals(RoleType.DeptAdmin) && user.F_belongDeptID != null)
                //{
                //    Session.Add(SessionMgm.BelongDept, user.F_belongDeptID);
                //}

                Response.Redirect("main.aspx");
            }
        }

     

    
    }
}