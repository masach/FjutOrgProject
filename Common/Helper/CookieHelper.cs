using System;  
 using System.Web;  
   
 namespace Cmj_Common  
 {  
     /// <summary>  
     /// Cookie������  
     /// </summary>  
     public class CookiesHelper  
     {  
   
         #region ��ȡCookie  
         /// <summary>  
         /// ���Cookie��ֵ  
         /// </summary>  
         /// <param name="cookieName"></param>  
         /// <returns></returns>  
         public static string GetCookieValue(string cookieName)  
         {  
             return GetCookieValue(cookieName, null);  
         }  
   
         /// <summary>  
         /// ���Cookie��ֵ  
         /// </summary>  
         /// <param name="cookieName"></param>  
         /// <param name="key"></param>  
         /// <returns></returns>  
         public static string GetCookieValue(string cookieName, string key)  
         {  
             HttpRequest request = HttpContext.Current.Request;  
             if (request != null)  
                 return GetCookieValue(request.Cookies[cookieName], key);  
             return "";  
         }  
   
         /// <summary>  
         /// ���Cookie���Ӽ�ֵ  
         /// </summary>  
         /// <param name="cookie"></param>  
         /// <param name="key"></param>  
         /// <returns></returns>  
         public static string GetCookieValue(HttpCookie cookie, string key)  
         {  
             if (cookie != null)  
             {  
                 if (!string.IsNullOrEmpty(key) && cookie.HasKeys)  
                     return cookie.Values[key];  
                 else  
                     return cookie.Value;  
             }  
             return "";  
         }  
   
         /// <summary>  
         /// ���Cookie  
         /// </summary>  
         /// <param name="cookieName"></param>  
         /// <returns></returns>  
         public static HttpCookie GetCookie(string cookieName)  
         {  
             HttpRequest request = HttpContext.Current.Request;  
             if (request != null)  
                 return request.Cookies[cookieName];  
             return null;  
         }  
   
         #endregion  
   
         #region ɾ��Cookie  
   
         /// <summary>  
         /// ɾ��Cookie  
         /// </summary>  
         /// <param name="cookieName"></param>  
         public static void RemoveCookie(string cookieName)  
         {  
             RemoveCookie(cookieName, null);  
         }  
   
         /// <summary>  
         /// ɾ��Cookie���Ӽ�  
         /// </summary>  
         /// <param name="cookieName"></param>  
         /// <param name="key"></param>  
         public static void RemoveCookie(string cookieName, string key)  
         {  
             HttpResponse response = HttpContext.Current.Response;  
             if (response != null)  
             {  
                 HttpCookie cookie = response.Cookies[cookieName];  
                 if (cookie != null)  
                 {  
                     if (!string.IsNullOrEmpty(key) && cookie.HasKeys)  
                         cookie.Values.Remove(key);  
                     else  
                         response.Cookies.Remove(cookieName);  
                 }  
             }  
         }  
   
         #endregion  
   
         #region ����/�޸�Cookie  
   
         /// <summary>  
         /// ����Cookie�Ӽ���ֵ  
         /// </summary>  
         /// <param name="cookieName"></param>  
         /// <param name="key"></param>  
         /// <param name="value"></param>  
         public static void SetCookie(string cookieName, string key, string value)  
         {  
             SetCookie(cookieName, key, value, null);  
         }  
   
         /// <summary>  
         /// ����Cookieֵ  
         /// </summary>  
         /// <param name="key"></param>  
         /// <param name="value"></param>  
         public static void SetCookie(string key, string value)  
         {  
             SetCookie(key, null, value, null);  
         }  
   
         /// <summary>  
         /// ����Cookieֵ�͹���ʱ��  
         /// </summary>  
         /// <param name="key"></param>  
         /// <param name="value"></param>  
         /// <param name="expires"></param>  
         public static void SetCookie(string key, string value, DateTime expires)  
         {  
             SetCookie(key, null, value, expires);  
         }  
   
         /// <summary>  
         /// ����Cookie����ʱ��  
         /// </summary>  
         /// <param name="cookieName"></param>  
         /// <param name="expires"></param>  
         public static void SetCookie(string cookieName, DateTime expires)  
         {  
             SetCookie(cookieName, null, null, expires);  
         }  
   
         /// <summary>  
         /// ����Cookie  
         /// </summary>  
         /// <param name="cookieName"></param>  
         /// <param name="key"></param>  
         /// <param name="value"></param>  
         /// <param name="expires"></param>  
         public static void SetCookie(string cookieName, string key, string value, DateTime? expires)  
         {  
             HttpResponse response = HttpContext.Current.Response;  
             if (response != null)  
             {  
                 HttpCookie cookie = response.Cookies[cookieName];  
                 if (cookie != null)  
                 {  
                     if (!string.IsNullOrEmpty(key) && cookie.HasKeys)  
                         cookie.Values.Set(key, value);  
                     else  
                         if (!string.IsNullOrEmpty(value))  
                             cookie.Value = value;  
                     if (expires != null)  
                         cookie.Expires = expires.Value;  
                     response.SetCookie(cookie);  
                 }  
             }  
   
         }  
   
         #endregion  
   
         #region ���Cookie  
   
         /// <summary>  
         /// ���Cookie  
         /// </summary>  
         /// <param name="key"></param>  
         /// <param name="value"></param>  
         public static void AddCookie(string key, string value)  
         {  
             AddCookie(new HttpCookie(key, value));  
         }  
   
         /// <summary>  
         /// ���Cookie  
         /// </summary>  
         /// <param name="key"></param>  
         /// <param name="value"></param>  
         /// <param name="expires"></param>  
         public static void AddCookie(string key, string value, DateTime expires)  
         {  
             HttpCookie cookie = new HttpCookie(key, value);  
             cookie.Expires = expires;  
             AddCookie(cookie);  
         }  
   
         /// <summary>  
         /// ���ΪCookie.Values����  
         /// </summary>  
         /// <param name="cookieName"></param>  
         /// <param name="key"></param>  
         /// <param name="value"></param>  
         public static void AddCookie(string cookieName, string key, string value)  
         {  
             HttpCookie cookie = new HttpCookie(cookieName);  
             cookie.Values.Add(key, value);  
             AddCookie(cookie);  
         }  
   
         /// <summary>  
         /// ���ΪCookie����  
         /// </summary>  
         /// <param name="cookieName">Cookie����</param>  
         /// <param name="expires">����ʱ��</param>  
         public static void AddCookie(string cookieName, DateTime expires)  
         {  
             HttpCookie cookie = new HttpCookie(cookieName);  
            cookie.Expires = expires;  
             AddCookie(cookie);  
         }  
   
         /// <summary>  
         /// ���ΪCookie.Values����  
         /// </summary>  
         /// <param name="cookieName"></param>  
         /// <param name="key"></param>  
         /// <param name="value"></param>  
         /// <param name="expires"></param>  
         public static void AddCookie(string cookieName, string key, string value, DateTime expires)  
         {  
             HttpCookie cookie = new HttpCookie(cookieName);  
             cookie.Expires = expires;  
             cookie.Values.Add(key, value);  
             AddCookie(cookie);  
         }  
   
         /// <summary>  
         /// ���Cookie  
         /// </summary>  
         /// <param name="cookie"></param>  
         public static void AddCookie(HttpCookie cookie)  
         {  
             HttpResponse response = HttpContext.Current.Response;  
             if (response != null)  
             {  
                 //ָ���ͻ��˽ű��Ƿ���Է���[Ĭ��Ϊfalse]  
                 cookie.HttpOnly = true;  
                 //ָ��ͳһ��Path���ȱ���ͨ��ͨȡ  
                 cookie.Path = "/";  
                 //���ÿ���,�������������������¾Ͷ����Է��ʵ���  
                 //cookie.Domain = "chinesecoo.com";  
                 response.AppendCookie(cookie);  
             }  
         }  
   
         #endregion  
     }  
 }  
