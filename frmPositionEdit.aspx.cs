using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EducationV2.App_Code;
using System.Data;

using System.Configuration;
using System.ComponentModel;
using System.Collections.Specialized;

namespace EducationV2
{
    public partial class frmAddProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataClassesDataContext dc = new DataClassesDataContext();
                Position pos = dc.Position.SingleOrDefault(_pd => _pd.F_ID.Equals(Request["id"]));
                if (pos != null)
                {
                    btnAdd.Visible = false;
                    txtName.Text = pos.F_posname;
                    if (pos.F_endDate != null)
                    {
                        txtEndDate.Text = pos.F_endDate.Value.ToShortDateString();         
                    }
                    else
                    {
                        txtEndDate.Text = DateTime.Now.ToShortDateString();
                    }
           
                    if (pos.F_startDate != null)
                    {
                        txtStartDate.Text = pos.F_startDate.Value.ToShortDateString();
                    }
                    else
                    {
                        txtStartDate.Text = DateTime.Now.ToShortDateString();
                    }

                    ddlType.SelectedValue = pos.F_status;
                    txtComment.Text = pos.F_comment;                  
                }
                else
                {
                    txtStartDate.Text = DateTime.Now.ToShortDateString();
                    txtEndDate.Text = DateTime.Now.ToShortDateString();
                    btnModify.Visible = false;
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            Position pos = new EducationV2.Position();
            pos.F_ID = Guid.NewGuid().ToString();
            pos.F_posname = txtName.Text.Trim();
            pos.F_startDate = DateTime.Parse(txtStartDate.Text);
            pos.F_endDate = DateTime.Parse(txtEndDate.Text);
            pos.F_status = ddlType.SelectedValue;
            pos.F_comment = txtComment.Text.Trim();
            dc.Position.InsertOnSubmit(pos);
            dc.SubmitChanges();
            //Response.Write("<script type='text/javascript'>window.opener.location.reload(); window.close()</script>");
            Response.Write("<script type='text/javascript'>self.location.href = 'frmPositionManage.aspx';</script>");
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            Position pos = dc.Position.SingleOrDefault(dm => dm.F_ID.Equals(Request.Params["id"].ToString()));
            pos.F_posname = txtName.Text.Trim();
            pos.F_startDate = DateTime.Parse(txtStartDate.Text);
            pos.F_endDate = DateTime.Parse(txtEndDate.Text);
            pos.F_status = ddlType.SelectedValue;
            pos.F_comment = txtComment.Text.Trim();
            dc.SubmitChanges();
            //Response.Write("<script>window.opener.location.reload(); window.close()</script>");
            Response.Write("<script type='text/javascript'>self.location.href = 'frmPositionManage.aspx';</script>");
        }
    }
}