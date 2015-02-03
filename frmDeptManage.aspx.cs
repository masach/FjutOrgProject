using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EducationV2.App_Code;
using System.Data;
namespace EducationV2
{
    public partial class frmDeptManag : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                gvDept.DataSource = GetDataSource();
                gvDept.DataBind();
                
            }
        }

        DataTable GetDataSource()
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            var depts = dc.DeptMent.Where(dm => dm.F_unitID.Equals(Session[SessionMgm.UnitID].ToString()));
            DataTable dt = new DataTable();
            dt.Columns.Add("F_ID");
            dt.Columns.Add("部门名");
            dt.Columns.Add("机构类别");

            dt.Columns.Add("部门用户数");
            dt.Columns.Add("状态");

            foreach (DeptMent dm in depts)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dm.F_ID;
                dr[1] = dm.F_name;
                dr[2] = dm.F_type;
                dr[3] = dc.User.Count(_user => _user.F_belongDeptID.Equals(dm.F_ID));
                dr[4] = dm.F_status;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private List<String> GetSelectedIDs()
        {
            List<String> result = new List<string>();
            foreach (GridViewRow gvr in this.gvDept.Rows)
            {
                CheckBox cbSel = (CheckBox)gvr.Cells[2].FindControl("chkSelect");
                if (cbSel.Checked == true)
                {
                    result.Add(gvDept.DataKeys[gvr.RowIndex].Value.ToString());
                }
            }
            return result;
        }

        protected void btnDel_Click(object sender, EventArgs e)
        { 
            List<String> deptIDs = GetSelectedIDs();
            ExpertBll.DelDepts(deptIDs);
            gvDept.DataSource = GetDataSource();
            gvDept.DataBind();           
        }
           

        protected void gvDept_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDept.PageIndex = e.NewPageIndex;
            gvDept.DataSource = GetDataSource();
            gvDept.DataBind();
        }

        protected void btnAuthor_Click(object sender, EventArgs e)
        {
            List<String> posIDs = GetSelectedIDs();
            ExpertBll.AuthDepts(posIDs);
            gvDept.DataBind();
        }

        protected void btnUnAuthor_Click(object sender, EventArgs e)
        {
            List<String> posIDs = GetSelectedIDs();
            ExpertBll.UnAuthDepts(posIDs);
            gvDept.DataBind();
        }
    }
}