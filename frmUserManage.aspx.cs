using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EducationV2.App_Code;
namespace EducationV2
{
    public partial class frmUserManag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ddlType.DataSource = SearchType.Types;
                ddlType.DataBind();
              
            }
        }

        [Obsolete]
        private void RegisterClientScript()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("function openAddFrame() {");
            if (Session[SessionMgm.Role].ToString().Equals(RoleType.EduAdmin))
                sb.Append(" window.open('frmUserAdd.aspx', 'newwindow','height=550,width=400,top=200,left=400,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no');");
            else
                sb.Append(" window.open('frmUserEdit.aspx', 'newwindow','height=550,width=400,top=200,left=400,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no');");
            sb.Append("window.location.href = 'frmUserManage.aspx';");
            sb.Append("}");

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "openAddFrame", sb.ToString(), true);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue.Equals(SearchType.ExactMatch))
            {
                odsUserInDept.FilterExpression = ddlField.SelectedValue + " = '" + txtKeyword.Text + "'";
                
            }
            else if (ddlType.SelectedValue.Equals(SearchType.FuzzMatch))
            {
                odsUserInDept.FilterExpression = ddlField.SelectedValue + " like '%" + txtKeyword.Text + "%'";
            }
            gvUsersInDept.DataBind();
            ViewState["express"] = odsUserInDept.FilterExpression;
        }

      



        protected void btnDel_Click(object sender, EventArgs e)
        {
            List<String> userIDs = GetSelectedIDs();
            ExpertBll.DelUsers(userIDs);
            gvUsersInDept.DataBind();
     
        }

     

        private List<String> GetSelectedIDs()
        {
            List<String> result = new List<string>();
            foreach (GridViewRow gvr in this.gvUsersInDept.Rows)
            {
                CheckBox cbSel = (CheckBox)gvr.Cells[2].FindControl("chkSelect");
                if (cbSel.Checked == true)
                {
                    result.Add(gvUsersInDept.DataKeys[gvr.RowIndex].Value.ToString());
                }
            }
            return result;
        }

        protected void btnAuthor_Click(object sender, EventArgs e)
        {
            List<String> userIDs = GetSelectedIDs();
            ExpertBll.AuthUsers(userIDs);
            gvUsersInDept.DataBind();
        }

        protected void btnUnAuthor_Click(object sender, EventArgs e)
        {
            List<String> userIDs = GetSelectedIDs();
            ExpertBll.UnAuthUsers(userIDs);
            gvUsersInDept.DataBind();
        }

        protected void gvUsersInDept_PageIndexChanged(object sender, EventArgs e)
        {
            
            odsUserInDept.FilterExpression = ViewState["express"] as String;
            gvUsersInDept.DataBind();
            
        }


    }
}