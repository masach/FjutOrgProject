using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Web.SessionState;
using System.Web.Services;
using EducationV2.App_Code;
using System.Runtime.Serialization.Json;
using System.Text;

namespace EducationV2.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SocialProject : IHttpHandler,IRequiresSessionState
    {
        HttpContext context;
        NameValueCollection paras;
       
        public void ProcessRequest(HttpContext context)
        {            
            this.context = context;
            paras = context.Request.Params;
            String result = "";
            switch (paras["method"])
            {
                case "savePage1": result = SavePage1(); break;               
                case "savePage3": result = SavePage3(); break;
                case "savePage4": result = SavePage4(); break;
                case "savePage5": result = SavePage5(); break;
                case "savePage6": result = SavePage6(); break;
                case "savePage7": result = SavePage7(); break;
                case "initialPage1": result = InitialPage(1); break;
                case "initialPage3": result = InitialPage(3); break;
                case "initialPage4": result = InitialPage(4); break;
                case "initialPage5": result = InitialPage(5); break;
                case "initialPage6": result = InitialPage(6); break;
                case "initialPage7": result = InitialPage(7); break;
                case "getMembers": result = GetMembers(); break;
                case "commit": Commit(); break;
                case "getStatus": result = GetStatus(); break;
            }
            context.Response.Write(result);
        }

        private string GetStatus()
        {
            context.Response.ContentType = "text/plain";
            DataClassesDataContext dc = new DataClassesDataContext();
            String F_ID = context.Session["applicationID"].ToString();
            EducationV2.SocialProject socialProject = dc.SocialProject.SingleOrDefault(c => c.F_id.Equals(F_ID));
            if (socialProject == null)
                return ProjectStatus.Draft;
            return socialProject.F_status;
        }

        private void Commit()
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            String F_ID = context.Session["applicationID"].ToString();
            EducationV2.SocialProject socialProject = dc.SocialProject.Single(c => c.F_id.Equals(F_ID));
            socialProject.F_status = ProjectStatus.UnderDeptAudit;
            dc.SubmitChanges();
        }

        private string GetMembers()
        {
            context.Response.ContentType = "application/json";
            DataClassesDataContext dc = new DataClassesDataContext();
            String F_ID = context.Session["applicationID"].ToString();
            var members = dc.ApplicantMember.Where(m => m.F_applicantID.Equals(F_ID));
            List<User> users = new List<User>();
            foreach(var member in members )
            {
                User user = dc.User.Single(u => u.F_ID.Equals(member.F_userID));
                users.Add(user);
            }
            return UtilHelper.GetJSON(users);
        }

        private string SavePage5()
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            String F_ID = context.Session["applicationID"].ToString();
            EducationV2.SocialProject socialProject = dc.SocialProject.Single(c => c.F_id.Equals(F_ID));
            socialProject.F_relatedWord = paras["F_relatedWord"];
            socialProject.F_condition = paras["F_condition"];
            dc.SubmitChanges();
            return "保存成功";
        }

        private string SavePage6()
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            String F_ID = context.Session["applicationID"].ToString();
            EducationV2.SocialProject socialProject = dc.SocialProject.Single(c => c.F_id.Equals(F_ID));
            Decimal num = 0;
            Decimal.TryParse(paras["F_bookCost"], out num);
            socialProject.F_bookCost =num;
            socialProject.F_bookBasis = paras["F_bookBasis"];
            num = 0;
            Decimal.TryParse(paras["F_researchCost"], out num);
            socialProject.F_researchCost = num;
            socialProject.F_researchBasis = paras["F_researchBasis"];
            num = 0;
            Decimal.TryParse(paras["F_communicateCost"], out num);
            socialProject.F_communicateCost = num;
            socialProject.F_communicateBasis = paras["F_communicateBasis"];
            num = 0;
            Decimal.TryParse(paras["F_consultCost"], out num);
            socialProject.F_consultCost = num;
            socialProject.F_consultBasis = paras["F_consultBasis"];
            num = 0;
            Decimal.TryParse(paras["F_equirementCost"], out num);
            socialProject.F_equirementCost = num;
            socialProject.F_equirementBasis = paras["F_equirementBasis"];
            num = 0;
            Decimal.TryParse(paras["F_laborCost"], out num);
            socialProject.F_laborCost = num;
            socialProject.F_laborBasis = paras["F_laborBasis"];
            num = 0;
            Decimal.TryParse(paras["F_managerCost"], out num);
            socialProject.F_managerCost = num;
            socialProject.F_mangerBasis = paras["F_mangerBasis"];
            num = 0;
            Decimal.TryParse(paras["F_totalCost"], out num);
            socialProject.F_totalCost = num;
            socialProject.F_totalBasis = paras["F_totalBasis"];
            num = 0;
            Decimal.TryParse(paras["F_Fund"], out num);
            socialProject.F_Fund = num;
            num = 0;
            Decimal.TryParse(paras["F_educationFund"], out num);
            socialProject.F_educationFund = num;
            num = 0;
            Decimal.TryParse(paras["F_schoolFund"], out num);
            socialProject.F_schoolFund = num;
            num = 0;
            Decimal.TryParse(paras["F_firstYearFund"], out num);
            socialProject.F_firstYearFund = num;
            num = 0;
            Decimal.TryParse(paras["F_secondYearFund"], out num);
            socialProject.F_secondYearFund = num;
            num = 0;
            Decimal.TryParse(paras["F_firstYear"], out num);
            socialProject.F_firstYear = num;
            num = 0;
            Decimal.TryParse(paras["F_secondYear"], out num);
            socialProject.F_secondYear = num;
            socialProject.F_projectRecommendComment = paras["F_projectRecommendComment"];
            socialProject.F_projectRecommendName = paras["F_projectRecommendName"];
            socialProject.F_projectRecommendTitle = paras["F_projectRecommendTitle"];
            socialProject.F_projectRecommendExpertise = paras["F_projectRecommendExpertise"];
            socialProject.F_projectRecommendWordspace = paras["F_projectRecommendWordspace"];
            dc.SubmitChanges();
            return "保存成功";
        }

        private string SavePage7()
        {
            throw new NotImplementedException();
        }

        private string InitialPage(int pageNum)
        {
            String F_ID = context.Session["applicationID"].ToString();
            DataClassesDataContext dc = new DataClassesDataContext();
            EducationV2.SocialProject socialProject = dc.SocialProject.SingleOrDefault(c => c.F_id.Equals(F_ID));
            if (socialProject == null)
                return "";
            if(pageNum ==1)
            {
                context.Session["leader"] = null;
            }
            if(pageNum ==3)
            {
                if(!String.IsNullOrWhiteSpace(socialProject.F_leaderID))
                {
                    User user = dc.User.Single(u => u.F_ID.Equals(socialProject.F_leaderID));
                    socialProject.F_leader = user.F_realName;
                    socialProject.F_sexual = user.F_sexual;
                    socialProject.F_nationality = user.F_nationality;
                    socialProject.F_IDNumber = user.F_idNumber;
                    socialProject.F_duty = user.F_position;
                    socialProject.F_degree = user.F_highestDegree;
                    socialProject.F_education = user.F_highestEducation;
                    socialProject.F_phone = user.F_phone;
                    socialProject.F_workspace = user.F_workspace;
                }
            }
            if (pageNum != 4) socialProject.F_background = "";
            if (pageNum != 5)
            {
                socialProject.F_relatedWord = "";
                socialProject.F_condition = "";
            }
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            DataContractJsonSerializer djson = new DataContractJsonSerializer(socialProject.GetType());
            djson.WriteObject(ms, socialProject);
            byte[] json = ms.ToArray();
            ms.Close();
            String result = Encoding.UTF8.GetString(json, 0, json.Length);
            context.Response.ContentType = "application/json";
            return result;
        }

        private string SavePage4()
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            String F_ID = context.Session["applicationID"].ToString();
            EducationV2.SocialProject socialProject = dc.SocialProject.Single(c => c.F_id.Equals(F_ID));
            socialProject.F_background = paras["F_background"];
            dc.SubmitChanges();            
            return "保存成功";
        }

        private string SavePage3()
        {
            DataClassesDataContext dc = new DataClassesDataContext();            
            String F_ID = context.Session["applicationID"].ToString();
            EducationV2.SocialProject socialProject = dc.SocialProject.Single(c => c.F_id.Equals(F_ID));
            socialProject.F_keywords = paras["F_keywords"];
            socialProject.F_researchType = paras["F_researchType"];
            socialProject.F_sexual = paras["F_sexual"];
            socialProject.F_nationality = paras["F_nationality"];
            socialProject.F_IDNumber = paras["F_IDNumber"];
            socialProject.F_duty = paras["F_duty"];
            socialProject.F_subjectDuty = paras["F_subjectDuty"];
            socialProject.F_expertise = paras["F_expertise"];
            socialProject.F_education = paras["F_education"];
            socialProject.F_degree = paras["F_degree"];
            socialProject.F_mentor = paras["F_mentor"];
            socialProject.F_workspace = paras["F_workspace"];
            socialProject.F_phone = paras["F_phone"];
            socialProject.F_teamName = paras["F_teamName"];
            socialProject.F_recommendName = paras["F_recommendName"];
            socialProject.F_recommendTitle = paras["F_recommendTitle"];
            //socialProject.F_recommendWorkspace = paras["F_recommendWorkspace"];
            //socialProject.F_achievement = paras["F_achievement"];
            //decimal number = 0;
            //Decimal.TryParse(paras["F_wordCount"], out number);
            //socialProject.F_wordCount = number;
            //number=0;
            //Decimal.TryParse(paras["F_Fund"], out number);
            //socialProject.F_Fund = number;            
            //if(String.IsNullOrWhiteSpace( paras["F_completeDate"] ) ==false)
            //    socialProject.F_completeDate = DateTime.Parse(paras["F_completeDate"]);              
            //var members = dc.ApplicantMember.Where(st => st.F_applicantID.Equals(F_ID));
            //dc.ApplicantMember.DeleteAllOnSubmit(members);
            //if (!String.IsNullOrWhiteSpace(paras["F_memberID"]))
            //{
            //    String[] F_memberIDs = paras["F_memberID"].Split(new char[] { ',' });
            //    for (int i = 0; i < F_memberIDs.Length; i++)
            //    {
            //        ApplicantMember member = new ApplicantMember();
            //        member.F_applicantID = F_ID;
            //        member.F_ID = Guid.NewGuid().ToString();
            //        member.F_userID = F_memberIDs[i];
            //        member.F_memberOrder = i;
            //        dc.ApplicantMember.InsertOnSubmit(member);
            //    }
            //}
         
            dc.SubmitChanges();
            return "保存成功";
        }
        /// <summary>
        /// 增加一个新申请，需生成一个新的申请书F_ID，为了方便后面处理，
        /// 将F_ID存在Session中
        /// </summary>
        /// <returns></returns>
        private string SavePage1()
        { 
          //  EducationV2.SocialProject socialProject = new EducationV2.SocialProject();
          //  socialProject.F_id = Guid.NewGuid().ToString();
            bool create = false;
            DataClassesDataContext dc = new DataClassesDataContext();
            String F_ID = context.Session["applicationID"].ToString();
            EducationV2.SocialProject socialProject = dc.SocialProject.SingleOrDefault(c => c.F_id.Equals(F_ID));
            if (socialProject == null)
            {
                create = true;
                socialProject = new EducationV2.SocialProject();
                socialProject.F_id = F_ID;
                socialProject.F_status = ProjectStatus.Draft;
            }
            context.Session.Add("applicationID", socialProject.F_id);
            socialProject.F_status = ProjectStatus.Draft;
            socialProject.F_leaderID = paras["F_leaderID"];
            socialProject.F_type = paras["F_type"];
            socialProject.F_catogary = paras["F_catogary"];
            socialProject.F_name = paras["F_name"];
            socialProject.F_leader = paras["F_leader"];
            socialProject.F_department = paras["F_department"];
            if (String.IsNullOrEmpty(paras["F_applicantDate"]) == false)
                socialProject.F_applicantDate = DateTime.Parse( paras["F_applicantDate"]);
            if(create) 
                dc.SocialProject.InsertOnSubmit(socialProject);
            dc.SubmitChanges();
            return "保存成功";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}