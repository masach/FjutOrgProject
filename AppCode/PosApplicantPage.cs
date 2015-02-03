using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationV2.App_Code;

namespace EducationV2.AppCode
{
    public class PosApplicantPage : System.Web.UI.Page
    {
        protected String litContent = "";
        String Tag = "PosApplicantP"; // mod by cy [20140926]
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (HttpContext.Current == null)
            {
                return;
            }
            if (!IsPostBack)
            {
                bool showSave = false, showCommit = false;
                String fileName = Request.Path;
                fileName = fileName.Substring(fileName.IndexOf(Tag) + Tag.Length);
                String strPageIndex = fileName.Substring(0, fileName.IndexOf('.'));
                int pageIndex = 0;
                int.TryParse(strPageIndex, out pageIndex);

                PosApplicant projappl = null;
                string id;                
                //if (Request.QueryString["aplID"] != null)// 子页面后台是无法获得get参数的
                string url = Request.UrlReferrer.ToString();
                if(!string.IsNullOrEmpty(getParam(url, "aplID")))
                {
                    id = getParam(url, "aplID");
                    DataClassesDataContext dc = new DataClassesDataContext();                   
                    projappl = dc.PosApplicant.SingleOrDefault(_apl => _apl.F_ID.Equals(id));
                }

                if (projappl == null)
                {
                    showSave = true;
                    showCommit = true;
                }
                else
                {
                    if (Session[SessionMgm.UserID].Equals(projappl.F_UserID) && projappl.F_statusEnum.Equals((short)ProjectStatus.EStatus.ApplicantDraft))
                    {
                        showSave = true;
                        showCommit = true;
                    }
                    else if (Session[SessionMgm.UserID].Equals(projappl.F_UserID) && projappl.F_statusEnum.Equals((short)ProjectStatus.EStatus.ApplicantCommit))
                    {
                        showCommit = true;
                    }
                }
                if (showSave)
                {
                    litContent = "<input id='btnSave' type='button' value='保 存' onclick='savePage(" + pageIndex + ")' class='btn' /> ";
                }
                if (showCommit)
                    litContent += "  <input id='btnSubmit' type='button' value='提 交' onclick='commit(" + pageIndex + ")' class='btn' />";
                if (litContent.Length > 0)
                {
                    litContent = " <div class='systitleline'>" + litContent + "</div>";
                }
            }
        }

        /// <summary>
        /// 获取url中的parameter
        /// </summary>
        /// <param name="strHref"></param>
        /// <param name="strName"></param>
        /// <returns></returns>
        private string getParam(string strHref, string strName)
        {
            int intPos = strHref.IndexOf("?");
            if (intPos < 1)
                return string.Empty;

            string strRight = strHref.Substring(intPos + 1);

            string[] arrPram = strRight.Split('&');//SplitString方法：将某字符串按特定字符或字符串分割为字符串数组
            for (int i = 0; i < arrPram.Length; i++)
            {
                string[] arrPramName = arrPram[i].Split('=');
                if (arrPramName[0].ToLower() == strName.ToLower()) return arrPramName[1];
            }
            return string.Empty;
        }
    }
}