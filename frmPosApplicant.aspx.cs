using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EducationV2.App_Code;
namespace EducationV2
{
    public partial class frmAcceptApplicant : SystemLogin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.Params["id"] != null)
            //{
            //    Session.Add(SessionMgm.PosApplicantID, Request.Params["id"]);
            //}
            //else
            //{
            //    Session.Add(SessionMgm.VisitUserID, Session[SessionMgm.UserID]);
            //}
            
            Session.Add(SessionMgm.PageJumtType, PageJumpType.ACCEPTAPPLICANT);
        }
    }
}