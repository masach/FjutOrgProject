using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace EducationV2.App_Code
{
    [DataContract]
    public class ExtjsComponent
    {
        //{xtype:'panel',height:100,width:200,html:"原有的组件"}
        [DataMember]
        public String xtype;
        [DataMember]
        public int height;
        [DataMember]
        public int width;
        [DataMember]
        public String html;
        [DataMember]
        public String title;
    }
}