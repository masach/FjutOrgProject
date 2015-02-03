﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationV2.Services
{
    /// <summary>
    /// GetExpertID 的摘要说明
    /// </summary>
    public class GetExpertID : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(Guid.NewGuid().ToString());

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