using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EducationV2.App_Code;
namespace EducationV2
{
    public partial class frmDeptEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["id"] != null)
                {
                    btnAdd.Visible = false;
                    DataClassesDataContext dc = new DataClassesDataContext();
                    DeptMent dept = dc.DeptMent.SingleOrDefault(dm => dm.F_ID.Equals(Request.Params["id"].ToString()));
                    txtName.Text = dept.F_name;
                    ddlOrgType.SelectedValue = dept.F_type;
                    ddlState.SelectedValue = dept.F_status;

                }
                else
                {
                    
                    btnModify.Visible = false;
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            DeptMent dept = new DeptMent();
            dept.F_ID = Guid.NewGuid().ToString();
            dept.F_unitID = Session[SessionMgm.UnitID].ToString();
            dept.F_name = txtName.Text;
            dept.F_type = ddlOrgType.SelectedValue;
            dept.F_status = ddlState.SelectedValue;

            dc.DeptMent.InsertOnSubmit(dept);
            dc.SubmitChanges();
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            DeptMent dept = dc.DeptMent.SingleOrDefault( dm=> dm.F_ID.Equals(Request.Params["id"].ToString()));
            dept.F_name = txtName.Text;
            dept.F_type = ddlOrgType.SelectedValue;
            dept.F_status = ddlState.SelectedValue;
            dc.SubmitChanges();
            Response.Redirect("frmDeptManage.aspx");
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            if (dc.User.Count(_user => _user.F_belongDeptID.Equals(Request.Params["id"])) > 0)
            {
                Response.Write("<script>alert('部门用户数大于0，不可删除');</script>");
                return;
            }
            DeptMent dept = dc.DeptMent.SingleOrDefault(dm => dm.F_ID.Equals(Request.Params["id"].ToString()));
            dc.DeptMent.DeleteOnSubmit(dept);
            dc.SubmitChanges();
            Response.Redirect("frmDeptManage.aspx");
        }
    }
}