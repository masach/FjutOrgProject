using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EducationV2.AppCode;
namespace EducationV2
{
    public partial class frmPosApplicant2 : PosApplicantPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litPanel.Text = litContent;// add by cy [20140926]
            }
        }
    }
}