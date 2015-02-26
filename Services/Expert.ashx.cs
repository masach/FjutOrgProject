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

namespace EducationV2
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Handler : IHttpHandler, IRequiresSessionState
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
                case "initialPage": result = GetBaseInfo(); break;
                case "checkUserName": result = CheckUserName(); break;
                case "getFamilyMember": result = GetFamilyMember(); break;

                case "savePage1": result = saveBaseInfo(); break;
                case "savePage2": result = savePage2(); break;
                case "savePage3": result = savePage3(); break;
               
                case "findUser": result = FindUser(); break;
                case "getCurrentRole": result = GetCurrentRole(); break;
                case "getStatus": result = GetStatus(); break;
                case "getModiable": result = GetModiable(); break;
                
                //case "getEducationInfo": result = GetEducationInfo(); break;
                //case "getCertInfo": result = GetCertInfo(); break;
                //case "getWorkExperience": result = GetWorkExperience(); break;
                //case "getPatent": result = GetPatent(); break;
                //case "getPaper": result = GetPaper(); break;
                //case "getAward": result = GetAward(); break;                
                  
                case "loadMenu": result = LoadMenu(); break;                
                case "isOrgAdmin": result = IsOrgAdmin(); break;

            }
            context.Response.Write(result);
        }

        private string IsOrgAdmin()
        {
            context.Response.ContentType = "text/plain";
            if (context.Session[SessionMgm.Role].ToString().Equals(RoleType.OrgDeptAdmin))
            {
                return "true";
            }
            else
                return "false";
        }

        private string GetModiable()
        {
            context.Response.ContentType = "text/plain";
            if (context.Session[SessionMgm.UserID].ToString().Equals(context.Session[SessionMgm.VisitUserID].ToString()))
                return "可修改";
            else
                return "不可修改";
        }

        private string LoadMenu()
        {
            String role = context.Session[SessionMgm.Role].ToString();
            String status = GetStatus();
            List<ExtjsComponent> components = new List<ExtjsComponent>();
            //单位管理,非专家都是管理员
            if (!role.Equals(RoleType.Applicant))
            {
                ExtjsComponent component = new ExtjsComponent();
                component.xtype = "panel";
                component.height = 200;
                component.width = 200;
                component.title = "单位管理";
                StringBuilder sb = new StringBuilder();

                if (role.Equals(RoleType.OrgDeptAdmin)) //单位管理员可以管部门，修改单位信息
                {
                    sb.Append("<a target='rightFrame'  href='frmModifyScho.aspx'>单位信息管理</a> <p />");
                    sb.Append("<a target='rightFrame'  href='frmDeptManage.aspx'>部门管理</a> <p />");
                }
                if (role.Equals(RoleType.EduAdmin)) //教育厅管理人员可以审核
                {
                    sb.Append("<a target='rightFrame'  href='frmSchoMgm.aspx'>单位管理</a> <p />");
                    sb.Append("<a target='rightFrame'  href='frmAdminManag.aspx'>人员管理</a> <p />");
                }
                else
                    sb.Append("<a target='rightFrame'  href='frmUserManage.aspx'>人员管理</a> <p />");
                component.html = sb.ToString();
                components.Add(component);
            }
            //申报管理，只要经过审核就可以申报
            if (status.Equals(RoleType.Authoried))
            {
                ExtjsComponent component = new ExtjsComponent();
                component.xtype = "panel";
                component.height = 200;
                component.width = 200;
                component.title = "申报管理";
                StringBuilder sb = new StringBuilder();
                if (role.Equals(RoleType.EduAdmin) || role.Equals(RoleType.OrgDeptAdmin))
                {
                    sb.Append("<a target='rightFrame'  href='frmProjectManage.aspx'>计划管理</a> <p />");
                }

                sb.Append("<a target='rightFrame'  href='frmSciProjectList.aspx'>科技项目申报管理</a> <p />");
                sb.Append("<a target='rightFrame'  href='frmSocialProjectList.aspx'>社科项目申报管理</a> <p />");
                component.html = sb.ToString();
                components.Add(component);
            }
            ExtjsComponent comp = new ExtjsComponent();
            comp.xtype = "panel";
            comp.height = 200;
            comp.width = 200;
            comp.title = "立项管理";
            comp.html = " <a target='rightFrame' href='frmApprovalList.aspx'>立项项目管理</a>";
            components.Add(comp);


            comp = new ExtjsComponent();
            comp.xtype = "panel";
            comp.height = 200;
            comp.width = 200;
            comp.title = "综合查询";
            comp.html = " <a target='rightFrame' href='frmAdvanceSearch.aspx'>项目查询</a>";
            components.Add(comp);


            comp = new ExtjsComponent();
            comp.xtype = "panel";
            comp.height = 200;
            comp.width = 200;
            comp.title = "执行和验收";
            comp.html = "<a target='rightFrame' href='frmExecuteList.aspx'> 项目执行情况表</a> <p />" +
               " <a target='rightFrame'    href='frmAcceptApplicantList.aspx'>  验收申请书 </a>";
            components.Add(comp);

            comp = new ExtjsComponent();
            comp.xtype = "panel";
            comp.height = 200;
            comp.width = 200;
            comp.title = "个人信息维护";
            comp.html = " <a target='rightFrame' href='frmUserDetail.aspx?type=self'>  个人信息 </a> <p />" +
               " <a target='rightFrame'    href='frmModifyPWD.aspx'>  密码修改 </a>";
            components.Add(comp);

            return UtilHelper.GetJSON(components);

        }


        #region Load Methods
        private string GetCurrentRole()
        {
            context.Response.ContentType = "text/plain";
            string role = context.Session[SessionMgm.Role].ToString();
            return role;
        }

        private string GetStatus()
        {
            context.Response.ContentType = "text/plain";
            DataClassesDataContext dc = new DataClassesDataContext();
            String userID = context.Session[SessionMgm.UserID].ToString();
            String role = context.Session[SessionMgm.Role].ToString();
            User user = dc.User.SingleOrDefault(_user => _user.F_ID.Equals(userID));
            if (String.IsNullOrEmpty(user.F_belongDeptID) == false)
            {           
                if (role.Equals(RoleType.Applicant) || role.Equals(RoleType.OrgDeptAdmin))
                {
                    DeptMent dept = dc.DeptMent.SingleOrDefault(_dm => _dm.F_ID.Equals(user.F_belongDeptID));
                    if (dept == null || dept.F_status != RoleType.Authoried || user.F_status != RoleType.Authoried)
                        return RoleType.Draft;
                }
            }
            return RoleType.Authoried;
        }       

        private string GetBaseInfo()
        {
            String result;
            context.Response.ContentType = "application/json";
            DataClassesDataContext dc = new DataClassesDataContext();
            String F_ID = context.Session[SessionMgm.VisitUserID].ToString();
            User newUser = dc.User.SingleOrDefault(user => user.F_ID.Equals(F_ID));
            if (newUser == null)
            {
                newUser = new User();
                newUser.F_Role = RoleType.Applicant;
                newUser.F_ID = F_ID;
            }

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            DataContractJsonSerializer djson = new DataContractJsonSerializer(newUser.GetType());
            djson.WriteObject(ms, newUser);
            byte[] json = ms.ToArray();
            ms.Close();
            result = Encoding.UTF8.GetString(json, 0, json.Length);

            return result;
        }

        private string GetFamilyMember()
        {
            context.Response.ContentType = "application/json";
            DataClassesDataContext dc = new DataClassesDataContext();
            String F_ID = context.Session[SessionMgm.VisitUserID].ToString();
            var members = dc.FamilyMember.Where(pp => pp.F_userID.Equals(F_ID));
            List<FamilyMember> pps = new List<FamilyMember>();
            foreach (var member in members)
            {
                pps.Add(member);
            }
            return UtilHelper.GetJSON(pps);
        }
        #endregion

        #region Load Methods -- no use presently
    
        private string GetPatent()
        {
            context.Response.ContentType = "application/json";
            DataClassesDataContext dc = new DataClassesDataContext();
            String F_ID = context.Session[SessionMgm.VisitUserID].ToString();
            var patents = dc.Patent.Where(pt => pt.F_userID.Equals(F_ID));
            List<Patent> pts = new List<Patent>();
            foreach (var patent in patents)
            {
                pts.Add(patent);
            }
            return UtilHelper.GetJSON(pts);
        }

   
 #endregion

        private string FindDutyUser()
        {
            String result = "";
            context.Response.ContentType = "application/json";
            DataClassesDataContext dc = new DataClassesDataContext();
            String F_idType = paras["F_idType"];
            String F_idNo = paras["F_idNo"];
            User user = dc.User.Single(u => u.F_idType.Equals(F_idType) && u.F_idNumber.Equals(F_idNo));
            if (user != null)
            {

                result = UtilHelper.GetJSON(user);
            }
            return result;
        }

        private string FindUser()
        {
            String result = "";
            context.Response.ContentType = "application/json";
            DataClassesDataContext dc = new DataClassesDataContext();
            String F_idType = paras["F_idType"];
            String F_idNo = paras["F_idNo"];
            User user = dc.User.SingleOrDefault(u => u.F_idType.Equals(F_idType) && u.F_idNumber.Equals(F_idNo));
            if (user != null)
            {
                result = UtilHelper.GetJSON(user);
            }
            return result;
        }

        private string CheckUserName()
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            User user = dc.User.SingleOrDefault(u => u.F_userName.Equals(paras["F_userName"]));

            if (user == null)
                return "用户名 " + paras["F_userName"] + " 可用";
            else
                return "用户名 " + paras["F_userName"] + " 已被占用";
        }

        private string Initial()
        {
            context.Response.ContentType = "text/plain";
            return Guid.NewGuid().ToString();
        }

        #region Save Methods
        private String saveBaseInfo()
        {
            String F_ID = context.Session[SessionMgm.VisitUserID].ToString();
            context.Response.ContentType = "text/plain";
            DataClassesDataContext dc = new DataClassesDataContext();
            bool insert = false;
            User newUser = dc.User.SingleOrDefault(user => user.F_ID.Equals(F_ID/*paras["F_ID"]*/));
            if (newUser == null)
            {
                newUser = new User();
                newUser.F_Role = RoleType.Applicant;
                newUser.F_ID = paras["F_ID"];
                insert = true;
            }
           
            // newUser.F_userName = paras["F_userName"];
           
            newUser.F_realName = paras["F_realName"];
            newUser.F_sexual = paras["F_sexual"];
            if (String.IsNullOrEmpty(paras["F_birthday"]) == false)
                newUser.F_birthday = DateTime.Parse(paras["F_birthday"]);
            newUser.F_bornplace = paras["F_bornplace"];

            newUser.F_nativeplace = paras["F_nativeplace"];
            newUser.F_nationality = paras["F_nationality"];
            newUser.F_party = paras["F_party"];
            if (String.IsNullOrEmpty(paras["F_partyEntryDate"]) == false)
                newUser.F_partyEntryDate = DateTime.Parse(paras["F_partyEntryDate"]);

            newUser.F_highestDegree = paras["F_highestDegree"];
            newUser.F_highestEducation = paras["F_highestEducation"];
            newUser.F_highestGrduateSch = paras["F_highestGrduateSch"];
            if (String.IsNullOrEmpty(paras["F_workBeginDate"]) == false)
                newUser.F_workBeginDate = DateTime.Parse(paras["F_workBeginDate"]);

            newUser.F_workDept = paras["F_workDept"];
            newUser.F_position = paras["F_position"];
            newUser.F_title = paras["F_title"];

            if (String.IsNullOrEmpty(paras["F_posBeginDate"]) == false)
                newUser.F_posBeginDate = DateTime.Parse(paras["F_posBeginDate"]);
            newUser.F_adminRanking = paras["F_adminRanking"];
            if (String.IsNullOrEmpty(paras["F_adminRkBeginDate"]) == false)
                newUser.F_adminRkBeginDate = DateTime.Parse(paras["F_adminRkBeginDate"]);

            newUser.F_idType = paras["F_idType"];          
            newUser.F_idNumber = paras["F_idNumber"];
            if (String.IsNullOrWhiteSpace(newUser.F_idNumber) == false)
            {
                User t = dc.User.SingleOrDefault(_user => _user.F_idType.Equals(newUser.F_idType) && _user.F_idNumber.Equals(newUser.F_idNumber));
                if (t != null && t.F_ID.Equals(newUser.F_ID) == false)
                {
                    return "系统中已存在相同的证件号和证件名";
                }
            }
            newUser.F_mobile = paras["F_mobile"];
            newUser.F_phone = paras["F_phone"];

            newUser.F_phone2 = paras["F_phone2"];
            newUser.F_email = paras["F_email"];
            newUser.F_freeAddress = paras["F_freeAddress"];
            newUser.F_fax = paras["F_fax"];
            
            newUser.F_lastModifyTime = DateTime.Now;            

            if (insert)
                dc.User.InsertOnSubmit(newUser);

            dc.SubmitChanges();
            return "保存成功";
        }

        private String savePage3()
        {
            String F_ID = context.Session[SessionMgm.VisitUserID].ToString();
            context.Response.ContentType = "text/plain";
            DataClassesDataContext dc = new DataClassesDataContext();
            User newUser = dc.User.SingleOrDefault(user => user.F_ID.Equals(F_ID));
            if (newUser == null)
            {
                return "系统中不存在该用户信息，请先保存用户基本资料!";
            }

            newUser.F_resume = paras["F_resume"];
            newUser.F_rwdandpunishmt = paras["F_rwdandpunishmt"];           
            newUser.F_lastModifyTime = DateTime.Now;
            dc.SubmitChanges();
            return "保存成功";
        }

        private String savePage2()
        {
            String F_ID = context.Session[SessionMgm.VisitUserID].ToString();
            context.Response.ContentType = "text/plain";
            DataClassesDataContext dc = new DataClassesDataContext();            
            User newUser = dc.User.SingleOrDefault(user => user.F_ID.Equals(F_ID));
            if (newUser == null)
            {
                return "系统中不存在该用户信息";
            }

            var members = dc.FamilyMember.Where(we => we.F_userID.Equals(F_ID));
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
                    eb.F_userID = newUser.F_ID;
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
            dc.SubmitChanges();
            return "保存成功";
        }

        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }


}