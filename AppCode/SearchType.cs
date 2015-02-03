using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationV2.App_Code
{
    public class SearchType
    {
        public static String FuzzMatch = "包含";
        public static String ExactMatch = "等于";
        public static String Bigger = "大于";
        public static String Smaller = "小于";
        public static String[] Types = new String[] { "包含", "等于" };
        public static String[] AllTypes = new String[] { "包含", "等于","大于", "小于" };
    }
}