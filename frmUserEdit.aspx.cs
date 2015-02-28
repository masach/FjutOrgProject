using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EducationV2.App_Code;

namespace EducationV2
{
    public partial class frmUserEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                DataClassesDataContext dc = new DataClassesDataContext();

                if (Request.Params["id"] != null)
                {
                    btnAdd.Visible = false;
                    string id = Request.Params["id"].ToString();
                    if (String.IsNullOrWhiteSpace(id))
                    {
                        // 返回上一页
                        Response.Write("<script type='text/javascript'>alert(\"找不到不用户信息\");window.close()</script>");
                        
                        //Response.Write("<script type='text/javascript'>window.opener.location.reload(); window.close()</script>");
                        //Response.Write("<script type='text/javascript'>self.location.href = 'frmPositionManage.aspx';</script>");
                        return;
                    }
                    User user = dc.User.SingleOrDefault(_user => _user.F_ID.Equals(id));
                    txtName.Text = user.F_userName;
                    txtRealName.Text = user.F_realName;
                    txtPwd.Text = user.F_pwd;
                    ddlRole.SelectedValue = user.F_Role;
                    ddlStatus.SelectedValue = user.F_status;
                    //在这里ddlBelongDept.Items.Count居然为0 TODO:求解释
                    //if (ddlBelongDept.Items.FindByValue(user.F_belongDeptID) != null)
                        ddlBelongDept.SelectedValue = user.F_belongDeptID;
                    //else
                    //    ddlBelongDept.SelectedIndex = -1;
                    txtName.ReadOnly = true;
                }
                else
                {
                    btnModify.Visible = false;
                }

                if (Session[SessionMgm.Role].ToString().Equals(RoleType.OrgDeptAdmin))
                {
                    ddlRole.Items.Remove(RoleType.OrgDeptDirector);
                    //foreach (ListItem item in ddlBelongDept.Items)
                    //{
                    //    if (item.Value.Equals(Session[SessionMgm.BelongDept].ToString()))
                    //    {
                    //        item.Selected = true;
                    //        ddlRole.Enabled = false;
                    //    }
                    //}
                }
            }

           
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
            user.F_workDept = ddlBelongDept.SelectedItem.Text;

            user.F_Role = ddlRole.SelectedValue;
            user.F_realName = txtRealName.Text;
            user.F_pwd = UtilHelper.MD5Encrypt(txtPwd.Text);
            user.F_userName = txtName.Text;
            user.F_lastModifyTime = DateTime.Now;
            //user.F_belongUnitID = Session[SessionMgm.UnitID].ToString();
            dc.User.InsertOnSubmit(user);
            dc.SubmitChanges();
            
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

        protected void btnModify_Click(object sender, EventArgs e)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            User user = dc.User.SingleOrDefault(dm => dm.F_ID.Equals(Request.Params["id"].ToString()));
            if(txtPwd.Text.Length >0)
            user.F_pwd = UtilHelper.MD5Encrypt( txtPwd.Text);
            user.F_realName = txtRealName.Text;
            user.F_Role = ddlRole.SelectedValue;
            user.F_belongDeptID = ddlRole.SelectedValue;
            user.F_status = ddlStatus.SelectedValue;
            user.F_belongDeptID = ddlBelongDept.SelectedValue;
            user.F_lastModifyTime = DateTime.Now;
            dc.SubmitChanges();
            Response.Redirect("frmUserManage.aspx");
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            User user = dc.User.SingleOrDefault(dm => dm.F_ID.Equals(Request.Params["id"].ToString()));
            dc.User.DeleteOnSubmit(user);
            dc.SubmitChanges();
            Response.Redirect("frmUserManage.aspx");
        }
    }
}

