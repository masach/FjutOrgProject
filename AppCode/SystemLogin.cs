using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationV2.App_Code;

public class SystemLogin : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        // mod by cy [20140923]
        //>解决视图模式下报错：Error Creating Control – Session state can only be used when enableSessionState is set to true, 
        //>either in a configuration file or in the Page directive. Please also make sure that System. Web.SessionStateModule or 
        //>a custom session state module is included in the <configuration>\<system.web>\<httpModules> section in the application configuration.
        //>调试模式下把下述注释代码放开即可；发布模式需注释掉。TODO:原因未知！
        //if (Context != null && Context.Session != null)
        //{
            // 验证登录
            if (Session[SessionMgm.RealName] == null || Session[SessionMgm.Role] == null)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Default.aspx");
            }
        //}

        base.OnInit(e);
    }

    protected virtual void OnPreInit(EventArgs e)
    {
        // 这个函数断点无效
        base.OnPreInit(e);
    }
   
}
