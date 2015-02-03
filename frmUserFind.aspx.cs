using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EducationV2.App_Code;

namespace EducationV2
{
    public partial class frmUserFind : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if("sciPage1".Equals(Request.Params["requestPage"]) ||  "socialPage1".Equals(Request.Params["requestPage"]))
                {
                    tabAddUser.Visible = false;
                }                
            }
        }

        protected void btnKeySearch_Click(object sender, EventArgs e)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            // linq动态组合可参见http://www.cnblogs.com/whitewolf/archive/2010/08/02/1790166.html
            //>这里就简单做一下咯
            User user = null;
            if (ddlField.SelectedValue == "F_userName")
            {
                user = dc.User.SingleOrDefault(_user => _user.F_userName.Equals(txtKeyword.Text.Trim()));
            }
            else if (ddlField.SelectedValue == "F_ID")
            {
                user = dc.User.SingleOrDefault(_user => _user.F_ID.Equals(txtKeyword.Text.Trim()));
            }
                
            if (user != null)
            {
                // 这样获取的result字符串有很多\/之类的玩意，会传不到前端哦
                //> TODO
                String result = UtilHelper.GetJSON(user);

                Response.Write("<script> window.opener.addUser('" + result + "'); window.close();</script>");
            }
            else
            {
                UtilHelper.AlertMsg("找不到指定用户");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            
            // 如果user是RoleType.ProjectMember类型的，也会查找到 by cy [20140919]
            User user = dc.User.SingleOrDefault(_user => _user.F_idType.Equals(ddlType.SelectedValue) && _user.F_idNumber.Equals(txtNo.Text));
            if (user != null)
            {
                String result = UtilHelper.GetJSON(user);
                Response.Write("<script> window.opener.addUser('" + result + "'); window.close();</script>");
            }
            else
            {
                UtilHelper.AlertMsg("找不到指定用户");
            }            
        }

        protected void btnReturn1_Click(object sender, EventArgs e)
        {
            Response.Write("<script> window.close();</script>");

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            User user = dc.User.SingleOrDefault(_user => _user.F_userName.Equals(txtName.Text));
            if (user != null)
            {
                Response.Write("<script>alert('该用户名已被使用');</script>");
                return;
            }
            user = new EducationV2.User();
            user.F_ID = Guid.NewGuid().ToString();
            user.F_status = ddlStatus.SelectedValue;

            user.F_belongDeptID = ddlBelongDept.SelectedValue;

            user.F_Role = ddlRole.SelectedValue;
            user.F_realName = txtRealName.Text;
            user.F_pwd = UtilHelper.MD5Encrypt(txtPwd.Text);
            user.F_userName = txtName.Text;
            user.F_lastModifyTime = DateTime.Now;
            //user.F_belongUnitID = Session[SessionMgm.UnitID].ToString();
            dc.User.InsertOnSubmit(user);
            dc.SubmitChanges();
            String result = UtilHelper.GetJSON(user);
            Response.Write("<script> window.opener.addUser('" + result + "'); window.close();</script>");

        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRole.SelectedValue.Equals(RoleType.OrgDeptAdmin))
            {
                ddlBelongDept.Enabled = false;
            }
            else
                ddlBelongDept.Enabled = true;

        }
    }
}