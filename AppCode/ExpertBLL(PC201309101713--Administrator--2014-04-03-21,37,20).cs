using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace EducationV2.App_Code
{
    public class ExpertBLL
    {
        public static void DelUsers(List<String> userIDs)
        {
            DataClassesDataContext dc = new DataClassesDataContext();
            var users = dc.User.Where( _user => userIDs.Contains( _user.F_ID));

        }
    }
}