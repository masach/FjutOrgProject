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
                ddlType.DataSource = SearchType.AllTypes;
                ddlType.DataBind();
                GridView1.DataSourceID = null;
                IEnumerable<ViewPosApl> datasource = getDatasource(false);
                GridView1.DataSource = UtilHelper.ToDataTable(datasource);
                Session.Add(SessionMgm.DataSource, GridView1.DataSource);
                GridView1.DataBind();
                if (Session[SessionMgm.Role].ToString().Equals(RoleType.Applicant) == false)
                {
                    //btnAllRecord.Text = "查看审核通过";
                //    btnGenNotice.Visible = true;
                    btnGenList.Visible = true;
                    btnGenCmprList.Visible = true;
                }
                else
                {
                  //  btnFilter.Visible = false;
                  //  btnAllRecord.Visible = false;
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
                    else
                    {
                        DateTime selectedDate, inputDate;
                        if (DateTime.TryParse(dt.Rows[i][ddlField.SelectedValue].ToString(), out selectedDate))
                        {
                            if (DateTime.TryParse(txtKeyword.Text, out inputDate) == false)
                            {
                                UtilHelper.AlertMsg("请输入合法的日期");
                            }
                            else
                            {
                                if (ddlType.SelectedValue.Equals(SearchType.Bigger))
                                {
                                    if (DateTime.Compare(selectedDate, inputDate) < 0)
                                    {
                                        dt.Rows.RemoveAt(i--);
                                        
                                    }
                                        
                                }
                                else if (ddlType.SelectedValue.Equals(SearchType.Smaller))
                                    if (DateTime.Compare(selectedDate, inputDate) > 0)
                                        dt.Rows.RemoveAt(i--);
                            }
                        }
                        else
                            UtilHelper.AlertMsg("大小只能用于比较日期");

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

        

        /// <summary>
        /// 获取在第N个志愿填报某岗位的人
        /// </summary>
        /// <param name="seq">第seq个志愿</param>
        /// <param name="pos">岗位</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string getPersonWithWish(int seq, string pos, DataTable dt)
        {
            String filter = "F_pos" + seq + " = '" + pos +  "'";
            DataRow[] drs = dt.Select(filter);
            String result = "";
            foreach (DataRow dr in drs)
            {
                result = result + dr["F_realName"].ToString() + " ";
            }
            return result;

        }

        private List<string> getAllPos(DataTable dt)
        {
            List<String> allPos = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 1; i <= 3; i++)
                {
                    String pos = dr["F_pos" + i].ToString().Trim();
                    if (pos.Length > 0 && !allPos.Contains(pos))
                        allPos.Add(pos);
                }
            }
            return allPos;
        }
        private void FillSummary(String fileName)
        {
            String templeteFile = Server.MapPath("/resource/summary.xls");
            ExcelHelper helper = new ExcelHelper(templeteFile, fileName);
            DataTable dt = Session[SessionMgm.DataSource] as DataTable;
            List<String> allPos = getAllPos(dt);
            if (allPos.Count > 1)
                helper.CopyRows(3, allPos.Count - 1);
            int row = 3;
            for (int i = 0; i < allPos.Count; i++)
            {
                String first = getPersonWithWish(1, allPos[i], dt);
                String second = getPersonWithWish(2, allPos[i], dt);
                String third = getPersonWithWish(3, allPos[i], dt);
                helper.SetCells(row + i, 1, (i + 1).ToString());
                helper.SetCells(row + i, 2, allPos[i]);
                helper.SetCells(row + i, 3, first);
                helper.SetCells(row + i, 4, second);
                helper.SetCells(row + i, 5, third);

            }
            helper.SaveAsFile(fileName);
        }

        private void FillDetail(String fileName)
        {
            String templeteFile = Server.MapPath("/resource/detail.xls");
            ExcelHelper helper = new ExcelHelper(templeteFile, fileName);
            DataTable dt = Session[SessionMgm.DataSource] as DataTable;
            DataView dv = dt.DefaultView;
            dv.Sort = "F_pos1 asc, F_pos2 asc, F_pos3 asc";
            DataTable dtSorted = dv.ToTable();
            int row = 3;
            if (dtSorted.Rows.Count > 1)
            {
                helper.CopyRows(3, dtSorted.Rows.Count -1 );
            }
            foreach (DataRow dr in dtSorted.Rows)
            {
                helper.SetCells(row,1 , (row - 2).ToString());
                helper.SetCells(row, 2, dr["F_pos1"].ToString());
                helper.SetCells(row, 3, dr["F_realName"].ToString());
                helper.SetCells(row, 4, dr["F_sexual"].ToString());
                helper.SetCells(row, 5, dr["F_nationality"].ToString());
                helper.SetCells(row, 6, dr["F_nativeplace"].ToString());
                helper.SetCells(row, 7, dr["F_party"].ToString());
                helper.SetCells(row, 8, dr["F_birthday"].ToString());
                helper.SetCells(row, 9, dr["F_position"].ToString());
                helper.SetCells(row, 10, dr["F_posBeginDate"].ToString());
                helper.SetCells(row, 11, dr["F_adminRkBeginDate"].ToString());
                helper.SetCells(row, 12, dr["F_title"].ToString());
                helper.SetCells(row, 13, dr["F_highestEducation"].ToString());
                helper.SetCells(row, 14, dr["F_highestDegree"].ToString());
                helper.SetCells(row, 15, dr["F_highestGrduateSch"].ToString());
                helper.SetCells(row, 16, dr["F_pos1"].ToString());
                helper.SetCells(row, 17, dr["F_pos2"].ToString());
                helper.SetCells(row, 18, dr["F_pos3"].ToString());    
                row++;
            }
           
            helper.SaveAsFile(fileName);
            
        }

        private void FillExcel(String fileName, ViewPosApl userInfo)
        {            
            String templeteFile = Server.MapPath("/resource/applicationForm.xls");
            ExcelHelper helper = new ExcelHelper(templeteFile, fileName);
            helper.SetCells(2, 2, userInfo.F_realName);
            helper.SetCells(2, 7, userInfo.F_workDept + " " + userInfo.F_position );
            helper.SetCells(3, 2, userInfo.F_party);

            helper.SetCells(3, 6, userInfo.F_title);
            helper.SetCells(3, 10, userInfo.F_highestEducation);
           
            helper.SetCells(4, 2, userInfo.F_highestDegree);
            helper.SetCells(4, 6, userInfo.F_adminRkBeginDate.Value.ToShortDateString());
            helper.SetCells(5, 2, "岗位1:" + userInfo.F_pos1);
            helper.SetCells(6, 2, "岗位2:" + userInfo.F_pos2);
            helper.SetCells(7, 2, "岗位3:" + userInfo.F_pos3);
            helper.SetCells(8, 2, userInfo.F_resume);
            helper.SetCells(9, 2, userInfo.F_rwdandpunishmt);
            helper.SetCells(10, 2, userInfo.F_reason);

            helper.SaveAsFile(fileName);
            /*
            helper.CreateNewWordDocument(file);
            DataClassesDataContext dc = new DataClassesDataContext();
            String F_ID = Session[SessionMgm.SciProjectID].ToString();
            ScienceProject project = dc.ScienceProject.SingleOrDefault(sp => sp.F_ID.Equals(F_ID));
            if (project != null)
            {
                fillContent(helper, project);
            }
            project.F_name = UtilHelper.getValidatePath(project.F_name);
            String fileName = Server.MapPath("/resource/" + project.F_name + ".doc");
            bool result = helper.SaveAs(fileName);
            helper.Close();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "Application/msword";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(project.F_name) + ".doc");
            Response.TransmitFile(fileName);
            Response.Flush();
            Response.Close();
            Response.End();
             */
        }

        #region TODO:报表生成，待实现
        /// <summary>
        /// 需要根据模板修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Label1_Click(object sender, EventArgs e)
        {

            String F_ID = (sender as LinkButton).CommandArgument;
            String fileName = Server.MapPath("/resource/" + F_ID + ".xls");
            DataClassesDataContext dc = new DataClassesDataContext();
            ViewPosApl userInfo = dc.ViewPosApl.SingleOrDefault(_uid => _uid.F_ID.Equals(F_ID));
            FillExcel(fileName, userInfo);
            FileInfo fileInfo = new FileInfo(fileName);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.WriteFile(fileInfo.FullName);
            Response.Flush();
            File.Delete(fileName);//删除已下载文件
            Response.End();
        }

  

        protected void btnGenList_Click(object sender, EventArgs e)
        {
            String F_ID = "xxx";
            String fileName = Server.MapPath("/resource/" + F_ID + ".xls");
            FillDetail(fileName);
            FileInfo fileInfo = new FileInfo(fileName);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.WriteFile(fileInfo.FullName);
            Response.Flush();
            File.Delete(fileName);//删除已下载文件
            Response.End();
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



        protected void btnGenSummary_Click(object sender, EventArgs e)
        {
            String F_ID = "yyy";
            String fileName = Server.MapPath("/resource/" + F_ID + ".xls");
            FillSummary(fileName);
            FileInfo fileInfo = new FileInfo(fileName);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.WriteFile(fileInfo.FullName);
            Response.Flush();
            File.Delete(fileName);//删除已下载文件
            Response.End();

        }  
    }
}