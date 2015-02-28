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
    public class StaffHandler : IHttpHandler, IRequiresSessionState
    {
        static readonly string JSON_SUCC = "{\"success\":true}";
        static readonly string JSON_NULL = "{\"success\":false}";
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
                case "getFamilyMember": result = GetFamilyMember(); break;

                case "savePage1": result = saveBaseInfo(); break;
                case "savePage2": result = savePage2(); break;
                case "savePage3": result = savePage3(); break;
               
                case "findStaff": result = FindStaff(); break;             
            }
            context.Response.Write(result);
        }

        #region Private Methods
        /// <summary>
        /// 如果有id则返回为真，V.V
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool getStaffID(out String id)
        {
            id = String.Empty;
            if (context.Request.Params["stfID"] != null)
            {
                id = context.Request.Params["stfID"];
                return true;
            }

            return false;
        }

        #endregion

        #region Load Methods
        private string GetBaseInfo()
        {
            context.Response.ContentType = "application/json";
            String id;
            if (!getStaffID(out id))
            {
                return JSON_NULL; // todo
            }
            String result = String.Empty;             
            DataClassesDataContext dc = new DataClassesDataContext();
            Staff newStaff = dc.Staff.SingleOrDefault(staff => staff.F_StaffID.Equals(id));
            if (newStaff != null)
            {
                result = UtilHelper.GetJSON(newStaff);
            }
            return result;
        }

        private string GetFamilyMember()
        {
            context.Response.ContentType = "application/json";
            DataClassesDataContext dc = new DataClassesDataContext();
            String id;
            if (!getStaffID(out id))
            {
                return JSON_NULL; // todo
            }

            var members = dc.StaffFamilyMember.Where(pp => pp.F_StaffID.Equals(id));
            List<StaffFamilyMember> pps = new List<StaffFamilyMember>();
            foreach (var member in members)
            {
                pps.Add(member);
            }
            return UtilHelper.GetJSON(pps);
        }
        #endregion

        #region Load Methods -- no use presently
        #endregion

       
        private string FindStaff()
        {
            String result = "";
            context.Response.ContentType = "application/json";
            DataClassesDataContext dc = new DataClassesDataContext();
            String F_idType = paras["F_idType"];
            String F_idNo = paras["F_idNo"];
            Staff staff = dc.Staff.SingleOrDefault(u => u.F_idType.Equals(F_idType) && u.F_idNumber.Equals(F_idNo));
            if (staff != null)
            {
                result = UtilHelper.GetJSON(staff);
            }
            return result;
        }

        private string Initial()
        {
            context.Response.ContentType = "text/plain";
            return Guid.NewGuid().ToString();
        }

        #region Save Methods
        private String saveBaseInfo()
        {
            context.Response.ContentType = "text/plain";
            DataClassesDataContext dc = new DataClassesDataContext();

            bool insert = false;
            string id;
            Staff newStaff;
            if (getStaffID(out id))
            {
                newStaff = dc.Staff.SingleOrDefault(staff => staff.F_StaffID.Equals(id/*paras["F_ID"]*/));
            }
            else
            {
                newStaff = new Staff();
                newStaff.F_StaffID = Guid.NewGuid().ToString();
                insert = true;
            }
            
            newStaff.F_empno = paras["F_empno"];
            if (String.IsNullOrWhiteSpace(newStaff.F_empno) == false)
            {
                Staff t = dc.Staff.SingleOrDefault(_staff => _staff.F_empno.Equals(newStaff.F_empno));
                if (t != null && t.F_StaffID.Equals(newStaff.F_StaffID) == false)
                {
                    return "档案库中已存在相同的教工号";
                }
            }

            if (String.IsNullOrEmpty(paras["F_UserID"]) == false)
            {
                newStaff.F_UserID = paras["F_UserID"].ToString().Trim();
                newStaff.F_userName = paras["F_userName"];
            }
            else
            {
                newStaff.F_UserID = createNewUser(dc);
                newStaff.F_userName = paras["F_empno"];
            }

            newStaff.F_realName = paras["F_realName"];
            newStaff.F_sexual = paras["F_sexual"];
            if (String.IsNullOrEmpty(paras["F_birthday"]) == false)
                newStaff.F_birthday = DateTime.Parse(paras["F_birthday"]);
            newStaff.F_bornplace = paras["F_bornplace"];

            newStaff.F_nativeplace = paras["F_nativeplace"];
            newStaff.F_nationality = paras["F_nationality"];
            newStaff.F_party = paras["F_party"];
            if (String.IsNullOrEmpty(paras["F_partyEntryDate"]) == false)
                newStaff.F_partyEntryDate = DateTime.Parse(paras["F_partyEntryDate"]);

            newStaff.F_highestDegree = paras["F_highestDegree"];
            newStaff.F_highestEducation = paras["F_highestEducation"];
            newStaff.F_highestGrduateSch = paras["F_highestGrduateSch"];
            if (String.IsNullOrEmpty(paras["F_workBeginDate"]) == false)
                newStaff.F_workBeginDate = DateTime.Parse(paras["F_workBeginDate"]);

            newStaff.F_workDept = paras["F_workDeptText"];
            newStaff.F_belongDeptID = paras["F_workDept"];
           
            newStaff.F_position = paras["F_position"];
            newStaff.F_title = paras["F_title"];

            if (String.IsNullOrEmpty(paras["F_posBeginDate"]) == false)
                newStaff.F_posBeginDate = DateTime.Parse(paras["F_posBeginDate"]);
            newStaff.F_adminRanking = paras["F_adminRanking"];
            if (String.IsNullOrEmpty(paras["F_adminRkBeginDate"]) == false)
                newStaff.F_adminRkBeginDate = DateTime.Parse(paras["F_adminRkBeginDate"]);

            newStaff.F_idType = paras["F_idType"];          
            newStaff.F_idNumber = paras["F_idNumber"];
            if (String.IsNullOrWhiteSpace(newStaff.F_idNumber) == false)
            {
                Staff t = dc.Staff.SingleOrDefault(_staff => _staff.F_idType.Equals(newStaff.F_idType) && _staff.F_idNumber.Equals(newStaff.F_idNumber));
                if (t != null && t.F_StaffID.Equals(newStaff.F_StaffID) == false)
                {
                    return "档案库中已存在相同的证件号和证件名";
                }
            }
            newStaff.F_mobile = paras["F_mobile"];
            newStaff.F_phone = paras["F_phone"];

            newStaff.F_phone2 = paras["F_phone2"];
            newStaff.F_email = paras["F_email"];
            newStaff.F_freeAddress = paras["F_freeAddress"];
            newStaff.F_fax = paras["F_fax"];
     
            newStaff.F_status = paras["F_status"];
            newStaff.F_lastModifyTime = DateTime.Now;            

            if (insert)
                dc.Staff.InsertOnSubmit(newStaff);

            dc.SubmitChanges();
            return UtilHelper.GetJSON(newStaff);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dc"></param>
        /// <returns>新用户的id</returns>
        private string createNewUser(DataClassesDataContext dc)
        {
            // 创建新用户
            User newUser = new User();
            newUser.F_Role = RoleType.Applicant;
            newUser.F_ID = Guid.NewGuid().ToString();

            newUser.F_userName = paras["F_empno"];// 教工号即username，密码同教工号
            newUser.F_pwd = UtilHelper.MD5Encrypt(newUser.F_userName);

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

            newUser.F_workDept = paras["F_workDeptText"];
            newUser.F_belongDeptID = paras["F_workDept"];
           
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
                User t = dc.User.SingleOrDefault(_staff => _staff.F_idType.Equals(newUser.F_idType) && _staff.F_idNumber.Equals(newUser.F_idNumber));
                if (t != null && t.F_ID.Equals(newUser.F_ID) == false)
                {
                    return "该证件号和证件名已被其他用户使用";
                }
            }
            newUser.F_mobile = paras["F_mobile"];
            newUser.F_phone = paras["F_phone"];

            newUser.F_phone2 = paras["F_phone2"];
            newUser.F_email = paras["F_email"];
            newUser.F_freeAddress = paras["F_freeAddress"];
            newUser.F_fax = paras["F_fax"];

            // 在此处自动创建的用户默认为“审核通过”
            newUser.F_status = InfoStatus.Authoried;
            newUser.F_lastModifyTime = DateTime.Now;

            dc.User.InsertOnSubmit(newUser);
            return newUser.F_ID;
        }
        private String savePage3()
        {            
            context.Response.ContentType = "text/plain";
            DataClassesDataContext dc = new DataClassesDataContext();

            string id;
            Staff newStaff = null;
            if (getStaffID(out id))
            {
                newStaff = dc.Staff.SingleOrDefault(staff => staff.F_StaffID.Equals(id/*paras["F_ID"]*/));
            }

            if (newStaff == null)
            {
                return "系统中不存在该职工信息，请先保存职工基本资料!";
            }

            newStaff.F_resume = paras["F_resume"];
            newStaff.F_rwdandpunishmt = paras["F_rwdandpunishmt"];           
            newStaff.F_lastModifyTime = DateTime.Now;
            dc.SubmitChanges();
            return UtilHelper.GetJSON(newStaff);
        }

        private String savePage2()
        {
            context.Response.ContentType = "text/plain";
            DataClassesDataContext dc = new DataClassesDataContext();   

            string id;
            Staff newStaff = null;
            if (getStaffID(out id))
            {
                newStaff = dc.Staff.SingleOrDefault(staff => staff.F_StaffID.Equals(id/*paras["F_ID"]*/));
            }

            if (newStaff == null)
            {
                return "系统中不存在该职工信息，请先保存职工基本资料!";
            }

            var members = dc.StaffFamilyMember.Where(we => we.F_StaffID.Equals(id));
            dc.StaffFamilyMember.DeleteAllOnSubmit(members);

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
                    StaffFamilyMember eb = new StaffFamilyMember();
                    eb.F_StaffID = newStaff.F_StaffID;
                    eb.F_MemID = Guid.NewGuid().ToString();

                    eb.F_familyMemAppelation = F_familyMemAppelations[i];
                    eb.F_familyMemName = F_familyMemNames[i];
                    if (F_familyMemBirthdays[i].Trim().Length > 0)
                    {                        
                        DateTime.TryParse(F_familyMemBirthdays[i], out dt);
                        eb.F_familyMemBirthday = dt;
                    }
                    eb.F_familyMemParty = F_familyMemPartys[i];
                    eb.F_familyMemWorkInfo = F_familyMemWorkInfos[i];                   
                   
                    dc.StaffFamilyMember.InsertOnSubmit(eb);
                }
            }
            dc.SubmitChanges();
            return UtilHelper.GetJSON(newStaff);
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