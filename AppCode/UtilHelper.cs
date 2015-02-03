using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Web;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Configuration;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using System.Net;

namespace EducationV2
{
    public class UtilHelper
    {

        /// <summary>
        /// LINQ返回DataTable类型
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="varlist"> </param>
        /// <returns> </returns>
        public static DataTable ToDataTable<T>(IQueryable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names
            PropertyInfo[] oProps = null;

            if (varlist == null)
                return dtReturn;

            // 此 RPC 请求中提供了过多的参数。最多应为 2100。
            //> 要求varlist最多传输2100行数据?
            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                             == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public static string ConnectionString = ConfigurationManager.ConnectionStrings["FjutOrgDeptConnectionString"].ToString();

        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strText));
            return System.Text.Encoding.Default.GetString(result);
        }

        public static void ZipFileMain(List<String> filenames, string name)
        {
            ZipOutputStream s = new ZipOutputStream(File.Create(name));
            Crc32 crc = new Crc32();
            //压缩级别
            s.SetLevel(3); // 0 - store only to 9 - means best compression
            try
            {
                foreach (string file in filenames)
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(file);//文件地址
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    //建立压缩实体
                    String filename = Path.GetFileName(file);
                    ZipEntry entry = new ZipEntry(filename);//原文件名
                    //时间
                    entry.DateTime = DateTime.Now;
                    //空间大小
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                s.Finish();
                s.Close();
            }
        }
        /// <summary>
        /// LINQ返回DataTable类型
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="varlist"> </param>
        /// <returns> </returns>
        public static DataTable ToDataTableWithStrCol<T>(IQueryable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names
            PropertyInfo[] oProps = null;

            if (varlist == null)
                return dtReturn;

            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {

                            dtReturn.Columns.Add(new DataColumn(pi.Name, typeof(String)));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }



        public static float ParseFloat(String str)
        {
            float result = 0;
            if (String.IsNullOrEmpty(str) == false)
            {
                float.TryParse(str, out result);
            }
            return result;
        }

        public static decimal ParseDecimal(String str)
        {
            decimal result = 0;
            if (String.IsNullOrEmpty(str) == false)
            {
                decimal.TryParse(str, out result);
            }
            return result;
        }

        public static int ParseInt(String str)
        {
            int result = 0;
            if (string.IsNullOrEmpty(str) == false)
                Int32.TryParse(str, out result);
            return result;
        }

        public static int ParseInt(String str, int def)
        {
            if (String.IsNullOrEmpty(str) == false)
                Int32.TryParse(str, out def);
            return def;
        }

        public static String GetFileType(String contentType)
        {
            String result = "未知类型";
            if (contentType.StartsWith("application"))
                result = "二进制数据";
            else if (contentType.StartsWith(""))
                result = "";
            return result;
        }

        public static String GetJSON(object type)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            DataContractJsonSerializer djson = new DataContractJsonSerializer(type.GetType());
            djson.WriteObject(ms, type);
            byte[] json = ms.ToArray();
            ms.Close();
            String result = Encoding.UTF8.GetString(json, 0, json.Length);
            return result;
        }

        public static object ConvertFromJSON(Type type, string jsonstr)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(jsonstr));
            DataContractJsonSerializer djson = new DataContractJsonSerializer(type);
            return djson.ReadObject(ms);
        }

        /// <summary>

        /// 下载文件，支持大文件、续传、速度限制。支持续传的响应头Accept-Ranges、ETag，请求头Range 。 
        /// Accept-Ranges：响应头，向客户端指明，此进程支持可恢复下载.实现后台智能传输服务（BITS），值为：bytes； 
        /// ETag：响应头，用于对客户端的初始（200）响应，以及来自客户端的恢复请求， 
        /// 必须为每个文件提供一个唯一的ETag值（可由文件名和文件最后被修改的日期组成），这使客户端软件能够验证它们已经下载的字节块是否仍然是最新的。 
        /// Range：续传的起始位置，即已经下载到客户端的字节数，值如：bytes=1474560- 。 
        /// 另外：UrlEncode编码后会把文件名中的空格转换中+（+转换为%2b），但是浏览器是不能理解加号为空格的，所以在浏览器下载得到的文件，空格就变成了加号； 
        /// 解决办法：UrlEncode 之后, 将 "+" 替换成 "%20"，因为浏览器将%20转换为空格 
        /// </summary> 
        /// <param name="httpContext">当前请求的HttpContext</param> 
        /// <param name="filePath">下载文件的物理路径，含路径、文件名</param> 
        /// <param name="speed">下载速度：每秒允许下载的字节数</param> 
        /// <returns>true下载成功，false下载失败</returns> 
        public static bool DownloadFile(HttpContext httpContext, string filePath)
        {
            long speed =1;
            httpContext.Response.Clear();
            bool ret = true;
            try
            {
                #region --验证：HttpMethod，请求的文件是否存在#region
                switch (httpContext.Request.HttpMethod.ToUpper())
                { //目前只支持GET和HEAD方法 
                    case "GET":
                    case "HEAD":
                        break;
                    default:
                        httpContext.Response.StatusCode = 501;
                        return false;
                }
                if (!File.Exists(filePath))
                {
                    httpContext.Response.StatusCode = 404;
                    return false;
                }
                #endregion

                #region 定义局部变量#region 定义局部变量
                long startBytes = 0;
                long stopBytes = 0;
                int packSize = 1024 * 10; //分块读取，每块10K bytes 
                string fileName = Path.GetFileName(filePath);
                FileStream myFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                long fileLength = myFile.Length;

                int sleep = (int)Math.Ceiling(1000.0 * packSize / speed);//毫秒数：读取下一数据块的时间间隔 
                sleep = 0;
                string lastUpdateTiemStr = File.GetLastWriteTimeUtc(filePath).ToString("r");
                string eTag = HttpUtility.UrlEncode(fileName, Encoding.UTF8) + lastUpdateTiemStr;//便于恢复下载时提取请求头; 
                #endregion

                #region --验证：文件是否太大，是否是续传，且在上次被请求的日期之后是否被修改过
                if (myFile.Length > long.MaxValue)
                {//-------文件太大了------- 
                    httpContext.Response.StatusCode = 413;//请求实体太大 
                    return false;
                }

                if (httpContext.Request.Headers["If-Range"] != null)//对应响应头ETag：文件名+文件最后修改时间 
                {
                    //----------上次被请求的日期之后被修改过-------------- 
                    if (httpContext.Request.Headers["If-Range"].Replace("\"", "") != eTag)
                    {//文件修改过 
                        httpContext.Response.StatusCode = 412;//预处理失败 
                        return false;
                    }
                }
                #endregion

                try
                {
                    #region -------添加重要响应头、解析请求头、相关验证
                    httpContext.Response.Clear();

                    if (httpContext.Request.Headers["Range"] != null)
                    {//------如果是续传请求，则获取续传的起始位置，即已经下载到客户端的字节数------
                        httpContext.Response.StatusCode = 206;//重要：续传必须，表示局部范围响应。初始下载时默认为200 
                        string[] range = httpContext.Request.Headers["Range"].Split(new char[] { '=', '-' });//"bytes=1474560-" 
                        startBytes = Convert.ToInt64(range[1]);//已经下载的字节数，即本次下载的开始位置  
                        if (startBytes < 0 || startBytes >= fileLength)
                        {//无效的起始位置 
                            return false;
                        }
                        if (range.Length == 3)
                        {
                            stopBytes = Convert.ToInt64(range[2]);//结束下载的字节数，即本次下载的结束位置  
                            if (startBytes < 0 || startBytes >= fileLength)
                            {
                                return false;
                            }
                        }
                    }

                    httpContext.Response.Buffer = false;
                    
                    httpContext.Response.AddHeader("Accept-Ranges", "bytes");//重要：续传必须 
                    httpContext.Response.AppendHeader("ETag", "\"" + eTag + "\"");//重要：续传必须 
                    httpContext.Response.AppendHeader("Last-Modified", lastUpdateTiemStr);//把最后修改日期写入响应                
                    httpContext.Response.ContentType = "application/octet-stream";//MIME类型：匹配任意文件类型 
                    httpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).Replace("+", "%20"));
                    httpContext.Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    httpContext.Response.AddHeader("Connection", "Keep-Alive");
                    httpContext.Response.ContentEncoding = Encoding.UTF8;
                    if (startBytes > 0)
                    {//------如果是续传请求，告诉客户端本次的开始字节数，总长度，以便客户端将续传数据追加到startBytes位置后---------- 
                        httpContext.Response.AddHeader("Content-Range", string.Format("bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                    }
                    #endregion

                    #region -------向客户端发送数据块-------------------
                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Ceiling((fileLength - startBytes + 0.0) / packSize);//分块下载，剩余部分可分成的块数 
                    for (int i = 0; i < maxCount && httpContext.Response.IsClientConnected; i++)
                    {//客户端中断连接，则暂停 
                        httpContext.Response.BinaryWrite(br.ReadBytes(packSize));
                        httpContext.Response.Flush();
                        if (sleep > 1) Thread.Sleep(sleep);
                    }
                    #endregion
                }
                catch
                {
                    ret = false;
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        internal static void AlertMsg(HttpResponse Response, string msg)
        {
            Response.Write("<script > alert('" + msg + "'); </script>");
        }



        internal static void AlertMsg(string msg)
        {
            AlertMsg(HttpContext.Current.Response, msg);
        }

        internal static string getValidatePath(string rPath)
        {
            StringBuilder rBuilder = new StringBuilder(rPath);
            foreach (char rInvalidChar in Path.GetInvalidFileNameChars())
                rBuilder.Replace(rInvalidChar.ToString(), string.Empty);
            return rBuilder.ToString();
        }

        internal static int CompareDate(DateTime dt1, DateTime dt2)
        {
            if (dt1 == null || dt2 == null)
                return 1;
            if (dt1.Year != dt2.Year)
                return dt1.Year - dt2.Year;
            if (dt1.Month != dt2.Month)
                return dt1.Month - dt2.Month;
            return dt1.Day - dt2.Day;
        }

      

        internal static void AlertInUpdatePanel(System.Web.UI.UpdatePanel updatePanel, Type type, string msg)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(updatePanel, type, "updateScript", "alert('" + msg +"')", true);
        }

        internal static void InvokeScript(System.Web.UI.UpdatePanel updatePane, Type type, string script)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(updatePane, type, "updateScript", script, true);
        }

        internal static DataTable ToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names
            PropertyInfo[] oProps = null;

            if (varlist == null)
                return dtReturn;

            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                             == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public static string GetClientIPv4Address()
        {
            string ipv4 = String.Empty;

            foreach (IPAddress ip in Dns.GetHostAddresses(GetClientIP()))
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    ipv4 = ip.ToString();
                    break;
                }
            }

            if (ipv4 != String.Empty)
            {
                return ipv4;
            }
            // 利用 Dns.GetHostEntry 方法，由获取的 IPv6 位址反查 DNS 纪录，
            // 再逐一判断何者为 IPv4 协议，即可转为 IPv4 位址。
            foreach (IPAddress ip in Dns.GetHostEntry(GetClientIP()).AddressList)
            //foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    ipv4 = ip.ToString();
                    break;
                }
            }

            return ipv4;
        }
        public static string GetClientIP()
        {
            if (null == HttpContext.Current.Request.ServerVariables["HTTP_VIA"])
            {
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
        }
    }


}