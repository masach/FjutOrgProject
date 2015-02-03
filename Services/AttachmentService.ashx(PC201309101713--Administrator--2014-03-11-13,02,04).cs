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
    /// <summary>
    /// AttachmentService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AttachmentService : IHttpHandler
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
                case "initialPage": result = InitialPage(); break;

            }
            context.Response.Write(result);
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        private string InitialPage()
        {
            context.Response.ContentType = "application/json";
            String F_ID = context.Session["applicationID"].ToString();
            int pageNum = int.Parse(paras["pageNum"]);
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