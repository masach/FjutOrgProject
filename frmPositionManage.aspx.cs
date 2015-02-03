using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EducationV2.App_Code;
using EducationV2.Services;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using EducationV2.AppCode;


namespace EducationV2
{
    public partial class frmPositionManage : SystemLogin
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlType.DataSource = SearchType.Types;
                ddlType.DataBind();
                // 能进入该页面的是管理员
                //if (Session[SessionMgm.Role].ToString().Equals(RoleType.OrgDeptAdmin)
                //    || Session[SessionMgm.Role].ToString().Equals(RoleType.OrgDeptDirector))
                //{
                //    btnAdd.Visible = true;
                //    btnDel.Visible = true;
                //}
            }
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            List<String> ids = GetSelectedIDs();
            if (ids.Count == 0)
            {
                // 客户端已经弹出窗口，这个语句无效
                MessageBox.Show(this, "请选择相应的条目！");
                return;
            }
            ExpertBll.DeletePositions(ids);
            SmartGridView1.DataBind();
        }

        private List<String> GetSelectedIDs()
        {
            List<String> result = new List<string>();
            foreach (GridViewRow gvr in SmartGridView1.Rows)
            {
                CheckBox cbSel = (CheckBox)gvr.Cells[1].FindControl("chkSelect");
                if (cbSel.Checked == true)
                {
                    result.Add(SmartGridView1.DataKeys[gvr.RowIndex].Value.ToString());
                }
            }
            return result;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtKeyword.Text))
            {
                LinqDataSource1.Where = null;
                SmartGridView1.DataBind();
                return;
            }
                
            DataClassesDataContext dc = new DataClassesDataContext();
            if (ddlType.SelectedValue.Equals(SearchType.Bigger))
            {
                this.LinqDataSource1.Where = ddlField.SelectedValue + " > \"" + txtKeyword.Text + "\"";
            }
            else if (ddlType.SelectedValue.Equals(SearchType.ExactMatch))
            {
                this.LinqDataSource1.Where = ddlField.SelectedValue + " = \"" + txtKeyword.Text + "\"";
            }
            else if (ddlType.SelectedValue.Equals(SearchType.FuzzMatch))
            {
                this.LinqDataSource1.Where = ddlField.SelectedValue + ".Contains(\"" + txtKeyword.Text + "\")";
            }
            else if (ddlType.SelectedValue.Equals(SearchType.Smaller))
            {
                this.LinqDataSource1.Where = ddlField.SelectedValue + " < \"" + txtKeyword.Text + "\"";
            }
            SmartGridView1.PageIndex = 0;
            SmartGridView1.DataBind();
        }

        protected void btnAuthor_Click(object sender, EventArgs e)
        {
            List<String> posIDs = GetSelectedIDs();
            if (posIDs.Count == 0)
            {
                MessageBox.Show(this, "请选择相应的条目！");
                return;
            }
            ExpertBll.AuthPositions(posIDs);
            SmartGridView1.DataBind();
        }

        protected void btnUnAuthor_Click(object sender, EventArgs e)
        {
            List<String> posIDs = GetSelectedIDs();
            if (posIDs.Count == 0)
            {
                MessageBox.Show(this, "请选择相应的条目！");
                return;
            }
            ExpertBll.UnAuthPositions(posIDs);
            SmartGridView1.DataBind();
        }

        protected void btnSetting_Click(object sender, EventArgs e)
        {
            List<String> posIDs = GetSelectedIDs();
            if (posIDs.Count == 0)
            {
                MessageBox.Show(this, "请选择相应的条目！");
                return;
            }
            DateTime dt = DateTime.MinValue;
            if (String.IsNullOrEmpty(txtEndDate.Text.Trim()) == false)
            {
                DateTime.TryParse(txtEndDate.Text.Trim(), out dt);
            }
            else if(null == DateTime.MinValue)
            {
                //
                return;
            }
            ExpertBll.SetPositionsEndDate(posIDs, dt);
            SmartGridView1.DataBind();                
        }


    }
}