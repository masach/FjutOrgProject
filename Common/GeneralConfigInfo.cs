using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	/// <summary>
	/// 基本设置描述类, 加[Serializable]标记为可序列化
	/// </summary>
	[Serializable]
	public class GeneralConfigInfo : IConfigInfo
	{
		#region 私有字段
		private string m_appurl = "";//系统根目录的URL地址

		private string m_skinname = "Default"; //默认风格
		private string m_sitetitle = "LQL"; //站点名称
		private string m_sitetitle_en = "LQL"; //站点名称
		private string m_siteurl = "default.aspx"; //url地址
		private string m_weburl = ""; //网站url地址

		private int m_enbsubdomain = 1; //是否开启二级域名，开启时m_rwpath请设置为空，URL重写，比如 http://abc.域名.com/
		private string m_domainfix = "192|168|www|localhost|127|10"; //域前缀，如是这些域前缀的，不进行二级域名的重写，直接访问
		private string m_denyreg = "App_Browsers|common|images|css|img_msg|javascript|lql_manager"; //禁止注册的名称

		private int m_closed = 0; //关闭
		private string m_closedreason = ""; //论坛关闭提示信息
		private string m_copyright = "";//网站版权说明
		private string m_copyright_en = "";//网站版权说明
		private string m_keywords = "";//关键字
		private string m_descrip = "";//网站描述
		private string m_filetype = "";//文件上传类型
		private int m_filesize = 500;//文件上传限制大小
		private string m_stmpserver = "";//发送邮件Smtp服务器
		private string m_mailserverusername = "";//发送邮件的用户名
		private string m_mailserverpassword = "";//发送邮件的密码
		private string m_mailfrom = "";//具体发送邮件

		private int m_watermarkstatus = 3; //图片附件添加水印 0=不使用 1=左上 2=中上 3=右上 4=左中 ... 9=右下
		private int m_watermarktype = 0; //图片附件添加何种水印 0=文字 1=图片
		private int m_watermarktransparency = 5; //图片水印透明度 取值范围1--10 (10为不透明)
		private string m_watermarktext = "LQL";  //图片附件添加文字水印的内容
		private string m_watermarkpic = "watermark.gif";   //使用的水印图片的名称
		private string m_watermarkfontname = "Tahoma"; //图片附件添加文字水印的字体
		private int m_watermarkfontsize = 12; //图片附件添加文字水印的大小(像素)
		private int m_attachimgquality = 80; //是否是高质量图片 取值范围0--100

		private string m_IndexDirectory = "~/SearchData/Index/"; //搜索的索引目录
		private string m_StoreDirectory = "~/SearchData/Store/"; //搜索的存储目录
		private string m_DictsDirectory = "~/SearchData/Dicts/"; //搜索的字典目录
		private string m_IndexAnalyzer = "ThesaurusAnalyzer"; //索引的分词器，比如：KTDicSeg，ThesaurusAnalyzer等

		#endregion

		#region UrlScheme
		/// <summary>
		/// Url格式
		/// </summary>
		public string UrlScheme
		{
			get
			{
				return System.Web.HttpContext.Current.Request.Url.Scheme + "://";
			}
		}
		/// <summary>
		/// 系统根目录的URL地址
		/// </summary>
		public string AppUrl
		{
			get
			{
				if (!String.IsNullOrEmpty(m_appurl))
				{
					return m_appurl;
				}
				else
				{
					string Url = UrlScheme + System.Web.HttpContext.Current.Request.Url.Authority + System.Web.HttpContext.Current.Request.ApplicationPath;
					Url = Url.TrimEnd('/');
					return Url;
				}
			}
			set { m_appurl = value; }
		}

		#endregion

		#region 属性
		/// <summary>
		/// 站点名称
		/// </summary>
		public string Sitetitle
		{
			get { return m_sitetitle; }
			set { m_sitetitle = value; }
		}

		/// <summary>
		/// 英文站点名称
		/// </summary>
		public string Sitetitle_En
		{
			get { return m_sitetitle_en; }
			set { m_sitetitle_en = value; }
		}


		/// <summary>
		/// url地址
		/// </summary>
		public string Siteurl
		{
			get { return m_siteurl; }
			set { m_siteurl = value; }
		}

		/// <summary>
		/// 网站url地址
		/// </summary>
		public string Weburl
		{
			get { return m_weburl; }
			set { m_weburl = value; }
		}


		/// <summary>
		/// 是否开启二级域名(1表示开启二级域名重写，0表示未启用二级域名功能)，URL重写，比如 http://abc.域名.com/
		/// </summary>
		public int EnbleSubDomain
		{
			get { return m_enbsubdomain; }
			set { m_enbsubdomain = value; }
		}


		/// <summary>
		/// 域前缀，如是这些域前缀的，不进行二级域名的重写，直接访问
		/// </summary>
		public string DomainFix
		{
			get { return m_domainfix; }
			set { m_domainfix = value; }
		}

		/// <summary>
		/// 禁止注册的名称
		/// </summary>
		public string Denyreg
		{
			get { return m_denyreg; }
			set { m_denyreg = value; }
		}


		/// <summary>
		/// 关闭
		/// </summary>
		public int Closed
		{
			get { return m_closed; }
			set { m_closed = value; }
		}

		/// <summary>
		/// 关闭提示信息
		/// </summary>
		public string Closedreason
		{
			get { return m_closedreason; }
			set { m_closedreason = value; }
		}
		/// <summary>
		/// 默认风格
		/// </summary>
		public string SkinName
		{
			get { return m_skinname; }
			set { m_skinname = value; }
		}

		/// <summary>
		/// 网站版权说明
		/// </summary>
		public string CopyRight
		{
			get { return m_copyright; }
			set { m_copyright = value; }
		}

		/// <summary>
		/// 英文网站版权说明
		/// </summary>
		public string CopyRight_En
		{
			get { return m_copyright_en; }
			set { m_copyright_en = value; }
		}

		/// <summary>
		/// 网站关键字
		/// </summary>
		public string SiteKeyWorks
		{
			get { return m_keywords; }
			set { m_keywords = value; }
		}

		/// <summary>
		/// 网站描述
		/// </summary>
		public string SiteDescrip
		{
			get { return m_descrip; }
			set { m_descrip = value; }
		}

		/// <summary>
		/// 文件上传类型
		/// </summary>
		public string FileType
		{
			get { return m_filetype; }
			set { m_filetype = value; }
		}

		/// <summary>
		/// 文件上传限制大小（Kb）
		/// </summary>
		public int FileSize
		{
			get { return m_filesize; }
			set { m_filesize = value; }
		}

		/// <summary>
		/// 发送邮件Smtp服务器
		/// </summary>
		public string StmpServer
		{
			get { return m_stmpserver; }
			set { m_stmpserver = value; }
		}

		/// <summary>
		/// 发送邮件的用户名
		/// </summary>
		public string MailServerUsername
		{
			get { return m_mailserverusername; }
			set { m_mailserverusername = value; }
		}

		/// <summary>
		/// 发送邮件的密码
		/// </summary>
		public string MailServerPassword
		{
			get { return m_mailserverpassword; }
			set { m_mailserverpassword = value; }
		}

		/// <summary>
		/// 具体发送邮件
		/// </summary>
		public string MailFrom
		{
			get { return m_mailfrom; }
			set { m_mailfrom = value; }
		}

		/// <summary>
		/// 图片附件添加何种水印 0=文字 1=图片
		/// </summary>
		public int Watermarktype
		{
			get { return m_watermarktype; }
			set { m_watermarktype = value; }
		}

		/// <summary>
		/// 图片附件添加文字水印的内容
		/// </summary>
		public string Watermarktext
		{
			get { return m_watermarktext; }
			set { m_watermarktext = value; }
		}

		/// <summary>
		/// 使用的水印图片的名称
		/// </summary>
		public string Watermarkpic
		{
			get { return m_watermarkpic; }
			set { m_watermarkpic = value; }
		}

		/// <summary>
		/// 图片附件添加文字水印的字体
		/// </summary>
		public string Watermarkfontname
		{
			get { return m_watermarkfontname; }
			set { m_watermarkfontname = value; }
		}

		/// <summary>
		/// 图片附件添加文字水印的大小(像素)
		/// </summary>
		public int Watermarkfontsize
		{
			get { return m_watermarkfontsize; }
			set { m_watermarkfontsize = value; }
		}

		/// <summary>
		/// 附件图片质量　取值范围 1是　0不是
		/// </summary>
		public int Attachimgquality
		{
			get { return m_attachimgquality; }
			set { m_attachimgquality = value; }
		}

		/// <summary>
		/// 图片水印透明度 取值范围1--10 (10为不透明)
		/// </summary>
		public int Watermarktransparency
		{
			get { return m_watermarktransparency; }
			set { m_watermarktransparency = value; }
		}

		/// <summary>
		/// 图片附件添加水印 0=不使用 1=左上 2=中上 3=右上 4=左中 ... 9=右下
		/// </summary>
		public int Watermarkstatus
		{
			get { return m_watermarkstatus; }
			set { m_watermarkstatus = value; }
		}

		/// <summary>
		/// 搜索的索引目录
		/// </summary>
		public string IndexDirectory
		{
			get { return m_IndexDirectory; }
			set { m_IndexDirectory = value; }
		}
		/// <summary>
		/// 搜索的存储目录
		/// </summary>
		public string StoreDirectory
		{
			get { return m_StoreDirectory; }
			set { m_StoreDirectory = value; }
		}
		/// <summary>
		/// 搜索的字典目录
		/// </summary>
		public string DictsDirectory
		{
			get { return m_DictsDirectory; }
			set { m_DictsDirectory = value; }
		}
		/// <summary>
		/// 索引的分词器，比如：KTDicSeg，ThesaurusAnalyzer等
		/// </summary>
		public string IndexAnalyzer
		{
			get { return m_IndexAnalyzer; }
			set { m_IndexAnalyzer = value; }
		}
		#endregion
	}
}
