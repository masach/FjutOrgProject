using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EducationV2.App_Code;
namespace EducationV2
{
    public partial class frmUser : SystemLogin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String F_ID = "";
            if (Request.Params["type"] != null && Request.Params["type"].ToString().Equals("self"))
                F_ID = Session[SessionMgm.UserID].ToString();
            else if (Request.Params["id"] != null)
                F_ID = Request.Params["id"];
            else
                F_ID = Guid.NewGuid().ToString();
            Session.Add(SessionMgm.VisitUserID, F_ID);
        }
    }
}