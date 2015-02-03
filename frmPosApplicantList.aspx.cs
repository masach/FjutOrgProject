using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EducationV2.App_Code;
using System.Data;

namespace EducationV2
{
    public partial class frmPosApplicantList : SystemLogin
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlType.DataSource = SearchType.Types;
                ddlType.DataBind();
                GridView1.DataSourceID = null;
                IEnumerable<ViewPosApl> datasource = getDatasource(false);
                GridView1.DataSource = UtilHelper.ToDataTable(datasource);
                Session.Add(SessionMgm.DataSource, GridView1.DataSource);
                GridView1.DataBind();
                if (Session[SessionMgm.Role].ToString().Equals(RoleType.Applicant) == false)
                {
                    //btnAllRecord.Text = "查看审核通过";
                    btnGenNotice.Visible = true;
                    btnGenList.Visible = true;
                    btnGenCmprList.Visible = true;
                }
                else
                {
                    btnFilter.Visible = false;
                    btnAllRecord.Visible = false;
                }
            }
        }

        private IEnumerable<ViewPosApl> getDatasource(Boolean inSearch)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            IEnumerable<ViewPosApl> datasource;
            if (Session[SessionMgm.Role].ToString().Equals(RoleType.Applicant))
            {
                List<String> userIDs = Global.GetSuitUsers(Session);
                datasource = from x in dc.ViewPosApl.AsEnumerable() where userIDs.Contains(x.F_UserID)
                             orderby x.F_appliedDate descending 
                             select x;
            }             
            else
            {
                //if(inSearch == false)
                    datasource = from x in dc.ViewPosApl.AsEnumerable() where x.F_statusEnum.Equals((short)ProjectStatus.EStatus.ApplicantDraft) == false
                                 orderby x.F_appliedDate descending 
                                 select x;
                //else
                //    datasource = from x in dc.ScienceProject.AsEnumerable() where x.F_status.Equals(ProjectStatus.UnderEducationAudit) || x.F_status.Equals(ProjectStatus.Pass) select x;
            }
            
            return datasource;
        }

      

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            List<String> userIDs = Global.GetSuitUsers(Session);
            IEnumerable<ViewPosApl> datasource = getDatasource(false);
           
            if (txtKeyword.Text.Trim().Length > 0)
            {
                DataTable dt = UtilHelper.ToDataTable(datasource);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (ddlType.SelectedValue.Equals(SearchType.ExactMatch))
                    {
                        if (dt.Rows[i][ddlField.SelectedValue].ToString().Equals(txtKeyword.Text) == false)
                        {
                            dt.Rows.RemoveAt(i);
                            i--;
                        }
                    }
                    else if (ddlType.SelectedValue.Equals(SearchType.FuzzMatch))
                    {
                        if (dt.Rows[i][ddlField.SelectedValue].ToString().Contains(txtKeyword.Text) == false)
                        {
                            dt.Rows.RemoveAt(i);
                            i--;
                        }
                    }
                }
                GridView1.DataSource = dt;
            }
            else
                GridView1.DataSource = UtilHelper.ToDataTable(datasource);
            Session.Add(SessionMgm.DataSource, GridView1.DataSource);
            GridView1.PageIndex = 0;
            GridView1.DataBind();
        }

        /*
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            String F_UserID = Session[SessionMgm.UserID].ToString();
            String str = "<script>window.open('frmPosApplicant.aspx?id=" + F_ID + "&requireSave=true','_blank')</script>";
            Response.Write(str);

        }
        */
        short condition = -1;
        protected void btnFilter_Click(object sender, EventArgs e)
        {

            DataClassesDataContext dc = new DataClassesDataContext();
            switch (Session[SessionMgm.Role].ToString())
            {
                case "申请人员": break;
                case "系统管理员": 
                case "组织部部长": 
                    // 仅显示已超过截止申报时间的申请
                condition = (short)ProjectStatus.EStatus.ApplicantComplete;
                break; 
            }
            if (-1 != condition)
            {
                List<String> userIDs = Global.GetSuitUsers(Session);
                var datasource = from x in dc.ViewPosApl.AsEnumerable() where userIDs.Contains(x.F_UserID) && 
                                     x.F_statusEnum.Equals(condition)
                                 orderby x.F_appliedDate descending 
                                 select x;
                GridView1.DataSource = UtilHelper.ToDataTable(datasource);
                Session.Add(SessionMgm.DataSource, GridView1.DataSource);
                GridView1.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = Session[SessionMgm.DataSource];
            GridView1.DataBind();
        }

        protected void btnAllRecord_Click(object sender, EventArgs e)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            List<String> userIDs = Global.GetSuitUsers(Session);
            IEnumerable<ViewPosApl> datasource = getDatasource(false);
            GridView1.DataSource = UtilHelper.ToDataTable(datasource);
            Session.Add(SessionMgm.DataSource, GridView1.DataSource);
            GridView1.DataBind();
        }

        #region TODO:报表生成，待实现
        /// <summary>
        /// 需要根据模板修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Label1_Click(object sender, EventArgs e)
        {
            //WordHelper helper = new WordHelper();
            //String file = Server.MapPath("/resource/sciProject.dot");
            //helper.CreateNewWordDocument(file);
            //DataClassesDataContext dc = new DataClassesDataContext();
            //String F_ID = (sender as LinkButton).CommandArgument;
            //ViewPosApl project = dc.ViewPosApl.SingleOrDefault(sp => sp.F_ID.Equals(F_ID));
            //fillContent(helper, project);
            //fillParticipants(helper, project);
            //fillAudit(helper, project);
            //project.F_name = UtilHelper.getValidatePath(project.F_name);
            //String fileName = Server.MapPath("/resource/" + project.F_name + ".doc");
            //bool result = helper.SaveAs(fileName);
            //helper.Close();
            //Response.Clear();
            //Response.ContentType = "Application/msword";
            //Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.HtmlEncode(Path.GetFileName(fileName)));
            //Response.TransmitFile(fileName);
            //Response.End();
        }

        /// <summary>
        /// 需要根据模板修改
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="project"></param>
        private void fillAudit(WordHelper helper, ViewPosApl project)
        {
            //DataClassesDataContext dc = new DataClassesDataContext();
            //var deptAudit = dc.AuditOpinion.SingleOrDefault(ao => ao.F_projectID.Equals(project.F_ID) && ao.F_type.Equals(RoleType.DeptAdmin));
            //if (deptAudit != null)
            //{
            //    helper.Replace("F_deptComment", deptAudit.F_content);
            //    if (deptAudit.F_date != null)
            //        helper.Replace("F_deptDate", deptAudit.F_date.Value.ToLongDateString());
            //}
            //var teamAudit = dc.AuditOpinion.SingleOrDefault(ao => ao.F_projectID.Equals(project.F_ID) && ao.F_type.Equals(RoleType.TeamAdmin));
            //if (teamAudit != null)
            //{
            //    helper.Replace("F_teamComment", teamAudit.F_content);
            //    if (teamAudit.F_date != null)
            //        helper.Replace("F_teamDate", teamAudit.F_date.Value.ToLongDateString());
            //}

            //var schoAudit = dc.AuditOpinion.SingleOrDefault(ao => ao.F_projectID.Equals(project.F_ID) && ao.F_type.Equals(RoleType.SchoolAdmin));
            //if (schoAudit != null)
            //{
            //    helper.Replace("F_scholComment", schoAudit.F_content);
            //    if (schoAudit.F_date != null)
            //        helper.Replace("F_scholDate", schoAudit.F_date.Value.ToLongDateString());
            //}

            //var eduAudit = dc.AuditOpinion.SingleOrDefault(ao => ao.F_projectID.Equals(project.F_ID) && ao.F_type.Equals(RoleType.EduAdmin));
            //if (eduAudit != null)
            //{
            //    helper.Replace("F_eduComment", eduAudit.F_content);
            //    if (eduAudit.F_date != null)
            //        helper.Replace("F_eduDate", eduAudit.F_date.Value.ToLongDateString());
            //}
        }

        /// <summary>
        /// 需要根据模板修改
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="project"></param>
        private void fillParticipants(WordHelper helper, ViewPosApl project)
        {
            //DataClassesDataContext dc = new DataClassesDataContext();
            //var participants = dc.ApplicantMember.Where(am => am.F_applicantID.Equals(project.F_ID)).OrderBy(am => am.F_seq);
            //if (helper.FindTable("F_participants"))
            //{

            //    foreach (ApplicantMember member in participants)
            //    {
            //        helper.MoveNextRow();
            //        User user = dc.User.SingleOrDefault(us => us.F_ID.Equals(member.F_userID));
            //        helper.SetCellValue(member.F_realName);
            //        helper.MoveNextCell();
            //        helper.SetCellValue(member.F_title);
            //        helper.MoveNextCell();
            //        helper.SetCellValue(member.F_expert);
            //        helper.MoveNextCell();
            //        helper.SetCellValue(member.F_duty);
            //        helper.MoveNextCell();
            //        helper.SetCellValue(member.F_workspace);
            //        helper.MoveNextCell();
            //        helper.SetCellValue("");

            //    }

            //}
        }

        /// <summary>
        /// 需要根据模板修改
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="project"></param>
        private void fillContent(WordHelper helper, ViewPosApl project)
        {
            //Type sciType = project.GetType();
            //System.Reflection.PropertyInfo[] properties = sciType.GetProperties();
            //String[] manualTypes = new String[] { "F_beginDate", "F_finishDate", "F_applicantDate" };
            //foreach (System.Reflection.PropertyInfo property in properties)
            //{
            //    if (property.GetValue(project, null) != null)
            //    {
            //        String value = property.GetValue(project, null).ToString();
            //        String name = property.Name;
            //        if (!manualTypes.Contains(name))
            //            helper.Replace(name, value);
            //    }
            //}
            //String F_type = "重点（  ）产学研（  ）一般（  ）；B类（  ）";
            //switch (project.F_type)
            //{
            //    case "重点": F_type = "重点（ √ ）产学研（  ）一般（  ）；B类（  ）"; break;
            //    case "产学研": F_type = "重点（  ）产学研（ √ ）一般（  ）；B类（  ）"; break;
            //    case "一般": F_type = "重点（  ）产学研（  ）一般（ √ ）；B类（  ）"; break;
            //    case "B类": F_type = "重点（  ）产学研（  ）一般（  ）；B类（ √ ）"; break;
            //}
            //if (project.F_beginDate != null)
            //    helper.Replace("F_beginDate", project.F_beginDate.Value.ToShortDateString());
            //if (project.F_finishDate != null)
            //    helper.Replace("F_finishDate", project.F_finishDate.Value.ToShortDateString());
            //if (project.F_applicantDate != null)
            //    helper.Replace("F_applicantDate", project.F_applicantDate.Value.ToShortDateString());
            //helper.Replace("F_type1", F_type);
            //helper.Replace("F_name1", project.F_name);
            //helper.Replace("F_belongeddomain1", project.F_belongeddomain);
            //helper.Replace("F_belongeddomain2", project.F_belongDomain2);
            //helper.Replace("F_belongedSubject1", project.F_belongedSubject);
            //helper.Replace("F_belongedSubject2", project.F_belongSubject2);
            //helper.Replace("F_leader1", project.F_leader);
            //helper.Replace("F_totalFund2", project.F_totalFund.ToString());
            //if (project.F_cooperator1Comment != null)
            //    helper.Replace("F_cooperator1Comment", project.F_cooperator1Comment);
            //if (project.F_cooperator1Date != null)
            //    helper.Replace("F_cooperator1Date", project.F_cooperator1Date.Value.ToShortDateString());

        }

        protected void btnGenNotice_Click(object sender, EventArgs e)
        {
            //DataClassesDataContext dc = new DataClassesDataContext();
            //List<String> ids = GetSelectedIDs();
            //String F_ID = Session[SessionMgm.UserID].ToString();
            //String passComment = dc.User.SingleOrDefault(_user => _user.F_ID.Equals(F_ID)).F_passComment;
            //var records = dc.ScienceProject.Where(_sci => ids.Contains(_sci.F_ID));
            //foreach (ScienceProject project in records)
            //{
            //    project.F_status = ProjectStatus.Pass;
            //    RemoveHistoricalOpinion(project.F_ID , RoleType.EduAdmin);
            //    AuditOpinion opinion = new AuditOpinion();
            //    opinion.F_projectID = project.F_ID;
            //    opinion.F_ID = Guid.NewGuid().ToString();
            //    opinion.F_content = passComment;
            //    opinion.F_date = DateTime.Now;
            //    opinion.F_type = RoleType.EduAdmin;
            //    opinion.F_result = "审核通过";
            //    dc.AuditOpinion.InsertOnSubmit(opinion);

            //}
            //dc.SubmitChanges();
            //GridView1.PageIndex = 0;
            //Response.Redirect("frmSciProjectList.aspx");

        }

        protected void btnGenList_Click(object sender, EventArgs e)
        {
            //DataClassesDataContext dc = new DataClassesDataContext();
            //List<String> ids = GetSelectedIDs();
            //String F_ID = Session[SessionMgm.UserID].ToString();
            //String denyComment = dc.User.SingleOrDefault(_user => _user.F_ID.Equals(F_ID)).F_denyComment;
            //var records = dc.ScienceProject.Where(_sci => ids.Contains(_sci.F_ID));
            //foreach (ScienceProject project in records)
            //{
            //    project.F_status = ProjectStatus.Deny;
            //    RemoveHistoricalOpinion(project.F_ID, RoleType.EduAdmin);
            //    AuditOpinion opinion = new AuditOpinion();
            //    opinion.F_projectID = project.F_ID;
            //    opinion.F_ID = Guid.NewGuid().ToString();
            //    opinion.F_content = denyComment;
            //    opinion.F_date = DateTime.Now;
            //    opinion.F_type = RoleType.EduAdmin;
            //    opinion.F_result = "审核不通过";
            //    dc.AuditOpinion.InsertOnSubmit(opinion);

            //}
            //dc.SubmitChanges();
            //GridView1.PageIndex = 0;
            //Response.Redirect("frmSciProjectList.aspx");
        }
        #endregion

        protected void btnDel_Click(object sender, EventArgs e)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            List<String> ids = GetSelectedIDs();
            IEnumerable<PosApplicant> records = null; 
            if (Session[SessionMgm.Role].ToString().Equals(RoleType.Applicant) == false)
            {
                records = dc.PosApplicant.Where(_apl => ids.Contains(_apl.F_ID));
            }
            else
            {
                records = dc.PosApplicant.Where(_apl => ids.Contains(_apl.F_ID)
                && !_apl.F_statusEnum.Equals((short)ProjectStatus.EStatus.ApplicantComplete));
            }
            
            
            dc.PosApplicant.DeleteAllOnSubmit(records);
            dc.SubmitChanges();
            GridView1.PageIndex = 0;
            Response.Redirect("frmPosApplicantList.aspx");
        }


        private List<String> GetSelectedIDs()
        {
            List<String> result = new List<string>();
            foreach (GridViewRow gvr in this.GridView1.Rows)
            {
                CheckBox cbSel = (CheckBox)gvr.Cells[2].FindControl("chkSelect");
                if (cbSel.Checked == true)
                {
                    result.Add(GridView1.DataKeys[gvr.RowIndex].Value.ToString());
                }
            }
            return result;
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            String role = Session[SessionMgm.Role].ToString();

            if (role.Equals(RoleType.EduAdmin))
            {
                GridView1.Columns[GridView1.Columns.Count-1].Visible = false;
            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool visible = true;
                DataRowView row = e.Row.DataItem as DataRowView;
                switch (Session[SessionMgm.Role].ToString())
                {
                    case "申报人员（专家）": visible = row["F_status"].Equals(ProjectStatus.UnderDeptAudit); break;
                    case "部门主管": visible = row["F_status"].Equals(ProjectStatus.UnderSchoolAudit); break;
                    case "单位主管": visible = row["F_status"].Equals(ProjectStatus.UnderEducationAudit); break;
                }
                if (visible == false)
                    ((LinkButton)e.Row.Cells[e.Row.Cells.Count-1].Controls[1]).Text = "";


            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ret")
            {
                //String role = Session[SessionMgm.Role].ToString();
                //String id = e.CommandArgument.ToString();
                //DataClassesDataContext dc = new DataClassesDataContext();
                //ScienceProject project = dc.ScienceProject.SingleOrDefault(_sp => _sp.F_ID.Equals(id));
                //if (project.F_status.Equals(ProjectStatus.UnderEducationAudit) && role.Equals(RoleType.SchoolAdmin))
                //    project.F_status = ProjectStatus.UnderSchoolAudit;
                //else if (project.F_status.Equals(ProjectStatus.UnderSchoolAudit) && role.Equals(RoleType.DeptAdmin))
                //    project.F_status = ProjectStatus.UnderDeptAudit;
                //else if (project.F_status.Equals(ProjectStatus.UnderDeptAudit) && role.Equals(RoleType.Expert))
                //    project.F_status = ProjectStatus.Draft;
                //dc.SubmitChanges();
                //Response.Redirect("frmSciProjectList.aspx");
            }
        }  
    }
}