using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Web.SessionState;
using System.Web.Services;
using System.Runtime.Serialization;
using EducationV2.App_Code;

namespace EducationV2.Services
{
    /// <summary>
    /// ExtjsService 的摘要说明
    /// </summary>
    public class ExtjsService : IHttpHandler
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
                case "getPagingData": result = GetPagingData(); break;


            }
            context.Response.Write(result);
        }

        private string GetPagingData()
        {
            String result = "";
            returnPersons persons = new returnPersons();
            persons.results = 10;
            persons.rows = new List<person>();
            for (int i = 0; i < 3; i++)
            {
                person p = new person();
                p.age = i * 10;
                p.id = "00" + i.ToString();
                p.name = "name" + i.ToString();
                persons.rows.Add(p);
            }
            return UtilHelper.GetJSON(persons);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
    
    [System.Runtime.Serialization.DataContract]
    class person
    {
        [DataMember]
        public String id;
        [DataMember]
        public String name;
        [DataMember]
        public int age;
    }

    [DataContract]
    class returnPersons
    {
        [DataMember]
        public int results;
        [DataMember]
        public List<person> rows;
    }
}