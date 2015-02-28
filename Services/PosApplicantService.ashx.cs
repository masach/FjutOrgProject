using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Web.SessionState;
using System.Web.Services;
using EducationV2.App_Code;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace EducationV2.Services
{
    /// <summary>
    /// PosApplicantService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class PosApplicantService : IHttpHandler, IRequiresSessionState
    {
        HttpContext context;
        NameValueCollection paras;
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            paras = context.Request.Params;
            String result = "{\"success\":true}";
            switch (paras["method"])
            {
                case "initialPage": result = GetBaseInfo(); break;
                case "initialFamily": result = GetFamilyMember(); break;

                case "savePage1": result = savePage1(); break;
                case "savePage2": result = savePage2(); break;

                case "commit": result = Commit(); break;
                case "initialRole": InitialRole(); break;
                case "getStatus": result = GetStatus(); break;
              
            }
            context.Response.Write(result);
        }



        private string GetStatus()
        {
            context.Response.ContentType = "text/plain";
            DataClassesDataContext dc = new DataClassesDataContext();
            string id;
            if (context.Request.Params["aplID"] != null)
            {
                id = context.Request.Params["aplID"];

            }
            else
            {
                return string.Empty;
            }
            PosApplicant exs = dc.PosApplicant.SingleOrDefault(es => es.F_ID.Equals(id));
            return exs.F_status;
        }

        /// <summary>
        /// 申请提交
        /// </summary>
        private string Commit()
        {
            context.Response.ContentType = "text/plain";
            DataClassesDataContext dc = new DataClassesDataContext();           
            string id;
            if (context.Request.Params["aplID"] != null)
            {
                id = context.Request.Params["aplID"];
            }
            else
            {
                return "提交失败，可能新业务没保存成功！";
            }

            ViewPosApl vapl = dc.ViewPosApl.SingleOrDefault(x => x.F_ID.Equals(id) 
                 && ((x.F_endDate1 == null ? true : (x.F_endDate1.Value.Date >= DateTime.Now.Date))
                      && (x.F_endDate2 == null ? true : (x.F_endDate2.Value.Date >= DateTime.Now.Date))
                      && (x.F_endDate3 == null ? true : (x.F_endDate3.Value.Date >= DateTime.Now.Date))
                     )
                );
            if (null == vapl)
            {
                // 说明本次提交某岗位已经过期了（可能是先前只保存未提交的业务）
                return "提交失败，可能岗位已过期！";

            }
         
            PosApplicant exs = dc.PosApplicant.SingleOrDefault(es => es.F_ID.Equals(id));


            if (exs.F_appliedDate == null)
            {
                exs.F_appliedDate = DateTime.Now; // 如果用户未提交过则设置时间
            }

            // 状态赋值代码需要成对出现
            exs.F_status = ProjectStatus.ApplicantCommit;
            exs.F_statusEnum = (short)ProjectStatus.EStatus.ApplicantCommit;

            dc.SubmitChanges();
            return "{\"success\":true}";
        }

        private void InitialRole()
        {
            throw new NotImplementedException();
        }

         #region Save Methods
        private String savePage1()
        {
            String F_ID = context.Session[SessionMgm.UserID].ToString();
            context.Response.ContentType = "text/plain";
            DataClassesDataContext dc = new DataClassesDataContext();
            PosApplicant apl = null;
            if (saveBaseInfo(F_ID, dc)
                && saveFamilyInfo(F_ID, dc)
                && saveApplicantPartInfo(out apl, dc))
            {
                dc.SubmitChanges();
                return "{\"success\":true}";
            }
            else
            {
                return "保存失败";
            }
        }

        private bool saveBaseInfo(string userid, DataClassesDataContext dc)
        {                        
            bool insert = false;
            User newUser = dc.User.SingleOrDefault(user => user.F_ID.Equals(userid/*paras["F_ID"]*/));
            if (newUser == null)
            {
                //newUser = new User();
                //newUser.F_Role = RoleType.Applicant;
                //newUser.F_ID = Guid.NewGuid().ToString();
                //insert = true;
                return false;
            }

            // newUser.F_userName = paras["F_userName"];

            newUser.F_realName = paras["F_realName"];
            newUser.F_sexual = paras["F_sexual"];
            if (String.IsNullOrEmpty(paras["F_birthday"]) == false)
                newUser.F_birthday = DateTime.Parse(paras["F_birthday"]);          

            newUser.F_nativeplace = paras["F_nativeplace"];
            newUser.F_nationality = paras["F_nationality"];
            newUser.F_party = paras["F_party"];         

            newUser.F_highestDegree = paras["F_highestDegree"];
            newUser.F_highestEducation = paras["F_highestEducation"];
            newUser.F_highestGrduateSch = paras["F_highestGrduateSch"];           

            newUser.F_workDept = paras["F_workDept"];
            newUser.F_position = paras["F_position"];
            newUser.F_title = paras["F_title"];

            if (String.IsNullOrEmpty(paras["F_posBeginDate"]) == false)
                newUser.F_posBeginDate = DateTime.Parse(paras["F_posBeginDate"]);
            newUser.F_adminRanking = paras["F_adminRanking"];
            if (String.IsNullOrEmpty(paras["F_adminRkBeginDate"]) == false)
                newUser.F_adminRkBeginDate = DateTime.Parse(paras["F_adminRkBeginDate"]);

            newUser.F_resume = paras["F_resume"];
            newUser.F_rwdandpunishmt = paras["F_rwdandpunishmt"];
            newUser.F_lastModifyTime = DateTime.Now;

            if (insert)
                dc.User.InsertOnSubmit(newUser);

            return true;            
        }

        private bool saveFamilyInfo(string userid, DataClassesDataContext dc)
        {
            var members = dc.FamilyMember.Where(we => we.F_userID.Equals(userid));
            dc.FamilyMember.DeleteAllOnSubmit(members);

            if (!String.IsNullOrWhiteSpace(paras["F_familyMemAppelation"]))
            {
                //String[] F_familyMemIDs = paras["F_familyMemID"].Split(new char[] { ',' });
                String[] F_familyMemAppelations = paras["F_familyMemAppelation"].Split(new char[] { ',' });
                String[] F_familyMemNames = paras["F_familyMemName"].Split(new char[] { ',' });
                String[] F_familyMemBirthdays = paras["F_familyMemBirthday"].Split(new char[] { ',' });
                String[] F_familyMemPartys = paras["F_familyMemParty"].Split(new char[] { ',' });
                String[] F_familyMemWorkInfos = paras["F_familyMemWorkInfo"].Split(new char[] { ',' });

                DateTime dt = DateTime.Now;
                for (int i = 0; i < F_familyMemAppelations.Length; i++)
                {
                    FamilyMember eb = new FamilyMember();
                    eb.F_userID = userid;
                    eb.F_ID = Guid.NewGuid().ToString();

                    eb.F_familyMemAppelation = F_familyMemAppelations[i];
                    eb.F_familyMemName = F_familyMemNames[i];
                    if (F_familyMemBirthdays[i].Trim().Length > 0)
                    {
                        DateTime.TryParse(F_familyMemBirthdays[i], out dt);
                        eb.F_familyMemBirthday = dt;
                    }
                    eb.F_familyMemParty = F_familyMemPartys[i];
                    eb.F_familyMemWorkInfo = F_familyMemWorkInfos[i];

                    dc.FamilyMember.InsertOnSubmit(eb);
                }
            }
            return true;
        }


        private String savePage2()
        {
            context.Response.ContentType = "text/plain";
            DataClassesDataContext dc = new DataClassesDataContext();
            PosApplicant apl = null;
            if (saveApplicantPartInfo(out apl, dc))
            {
                apl.F_pos1 = paras["F_pos1"];
                apl.F_pos2 = paras["F_pos2"];
                apl.F_pos3 = paras["F_pos3"];
                apl.F_posID1 = paras["F_posID1"];
                apl.F_posID2 = paras["F_posID2"];
                apl.F_posID3 = paras["F_posID3"];
                apl.F_reason = paras["F_reason"];
            }
            
            dc.SubmitChanges();
            
            // return "保存成功";
            return UtilHelper.GetJSON(apl);// 为了把业务id返回给页面已支持用户切换tab页面的刷新
        }

        private bool saveApplicantPartInfo(out PosApplicant apl, DataClassesDataContext dc)
        {
            bool insert = false;
            string id;            
            if (context.Request.Params["aplID"] != null)
            {
                id = context.Request.Params["aplID"];
                apl = dc.PosApplicant.SingleOrDefault(_apl => _apl.F_ID.Equals(id));
            }
            else
            {
                apl = new PosApplicant();
                apl.F_ID = Guid.NewGuid().ToString();
                apl.F_UserID = context.Session[SessionMgm.UserID].ToString();
                insert = true;
            }
           
            //apl.F_appliedDate = DateTime.Now;
            apl.F_status = ProjectStatus.ApplicantDraft;
            apl.F_statusEnum = (short)ProjectStatus.EStatus.ApplicantDraft;

            if (insert)
                dc.PosApplicant.InsertOnSubmit(apl);
            
            return true;
        }
        #endregion


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region Load Methods
        private string GetBaseInfo()
        {
            String result = "";
            context.Response.ContentType = "application/json";
            DataClassesDataContext dc = new DataClassesDataContext();

            string id;
            if (context.Request.Params["aplID"] != null)
            {
                id = context.Request.Params["aplID"];
                ViewPosApl exs = dc.ViewPosApl.SingleOrDefault(es => es.F_ID.Equals(id));
                result = UtilHelper.GetJSON(exs);
            } 
            else
            {
                id = context.Session[SessionMgm.UserID].ToString(); // 当前登录用户即该业务的申请者
                User usr = dc.User.SingleOrDefault(es => es.F_ID.Equals(id));
                result = UtilHelper.GetJSON(usr);
                result = result.Replace("F_ID", "F_UserID");
            }           
            return result;
        }

        private string GetFamilyMember()
        {
            context.Response.ContentType = "application/json";
            DataClassesDataContext dc = new DataClassesDataContext();
            IQueryable<FamilyMember> members = null;

            string id;
            if (context.Request.Params["aplID"] != null)
            {
                id = context.Request.Params["aplID"];
                ViewPosApl exs = dc.ViewPosApl.SingleOrDefault(es => es.F_ID.Equals(id));
                if (null != exs)
                {
                    members = dc.FamilyMember.Where(pp => pp.F_userID.Equals(exs.F_UserID));                    
                }
            }
            else
            {
                id = context.Session[SessionMgm.UserID].ToString();
                members = dc.FamilyMember.Where(pp => pp.F_userID.Equals(id));
            }           

            List<FamilyMember> pps = new List<FamilyMember>();
            foreach (var member in members)
            {
                pps.Add(member);
            }
            return UtilHelper.GetJSON(pps);
        }
        #endregion

        #region Needn't Methods
        /*
        private string GetPapers()
        {
            String result = "";
            context.Response.ContentType = "application/json";
            DataClassesDataContext dc = new DataClassesDataContext();
            String projectID = context.Session[SessionMgm.PosApplicantID].ToString();
            var _stands = dc.PublishedPaper.Where(_st => _st.F_projectID.Equals(projectID));
            List<PublishedPaper> papers = new List<PublishedPaper>();
            foreach (var pap in _stands)
            {
                papers.Add(pap);
            }
            result = UtilHelper.GetJSON(papers);
            return result;
        }

        private string GetStands()
        {
            String result = "";
            context.Response.ContentType = "application/json";
            DataClassesDataContext dc = new DataClassesDataContext();
            String projectID = context.Session[SessionMgm.PosApplicantID].ToString();
            var _stands = dc.Standard.Where(_st => _st.F_projectID.Equals(projectID));
            List<Standard> stands = new List<Standard>();
            foreach (var patent in _stands)
            {
                stands.Add(patent);
            }
            result = UtilHelper.GetJSON(stands);
            return result;
        }


        private String GetPatents()
        {
            String result = "";
            context.Response.ContentType = "application/json";
            DataClassesDataContext dc = new DataClassesDataContext();
            String projectID = context.Session[SessionMgm.PosApplicantID].ToString();
            var _patents = dc.Patent.Where(pt => pt.F_projectID.Equals(projectID));
            List<Patent> patents = new List<Patent>();
            foreach (var patent in _patents)
            {
                patents.Add(patent);
            }
            result = UtilHelper.GetJSON(patents);
            return result;
        }


        private string GetAwards()
        {
            context.Response.ContentType = "application/json";
            DataClassesDataContext dc = new DataClassesDataContext();
            String projectID = context.Session[SessionMgm.PosApplicantID].ToString();
            var awards = dc.Award.Where(pp => pp.F_projectID.Equals(projectID));
            List<Award> pps = new List<Award>();
            foreach (var award in awards)
            {
                pps.Add(award);
            }
            return UtilHelper.GetJSON(pps);
        }
         */ 
        #endregion
    }
}