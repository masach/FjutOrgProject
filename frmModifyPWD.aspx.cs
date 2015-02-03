using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EducationV2.App_Code;


namespace EducationV2
{
    public partial class frmModifyPWD : SystemLogin
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCommit_Click(object sender, EventArgs e)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            User user = dc.User.SingleOrDefault(_user => _user.F_ID.Equals(Session[SessionMgm.UserID].ToString()) && 
                _user.F_pwd.Equals( UtilHelper.MD5Encrypt( txtOrgiPWD.Text)));
            if (user != null)
            {
                user.F_pwd = UtilHelper.MD5Encrypt(txtNewPWD1.Text);
                dc.SubmitChanges();
                UtilHelper.AlertMsg("修改成功");
            }
            else
                UtilHelper.AlertMsg("密码错误");    
            
        }
    }
}