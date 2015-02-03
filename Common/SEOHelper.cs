using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Common
{
	public class SEOHelper
	{

		/// <summary>
		/// Renders page meta tag
		/// </summary>
		/// <param name="page">Page instance</param>
		/// <param name="name">Meta name</param>
		/// <param name="content">Content</param>
		/// <param name="OverwriteExisting">Overwrite existing content if exists</param>
		public static void RenderMetaTag(Page page, string name, string content, bool OverwriteExisting)
		{
			if (page == null || page.Header == null)
			{
				return;
			}

			if (content == null)
			{
				content = string.Empty;
			}

			foreach (Control control in page.Header.Controls)
			{
				if (control is HtmlMeta)
				{
					HtmlMeta meta = (HtmlMeta)control;
					if (meta.Name.ToLower().Equals(name.ToLower()) && !string.IsNullOrEmpty(content))
					{
						if (OverwriteExisting)
						{
							meta.Content = content;
						}
						else
						{
							if (string.IsNullOrEmpty(meta.Content))
							{
								meta.Content = content;
							}
						}
						break;
					}
				}
			}
		}


		/// <summary>
		/// Renders page title
		/// </summary>
		/// <param name="page">Page instance</param>
		/// <param name="title">Page title</param>
		/// <param name="OverwriteExisting">Overwrite existing content if exists</param>
		public static void RenderTitle(Page page, string title, bool OverwriteExisting)
		{
			RenderTitle(page, title, OverwriteExisting, true);
		}

		/// <summary>
		/// Renders page title
		/// </summary>
		/// <param name="page">Page instance</param>
		/// <param name="title">Page title</param>
		/// <param name="OverwriteExisting">Overwrite existing content if exists</param>
		public static void RenderTitle(Page page, string title, bool OverwriteExisting, bool IncludeStoreNmae)
		{
			if (page == null || page.Header == null)
				return;

			if (string.IsNullOrEmpty(title))
			{
				return;
			}

			if (IncludeStoreNmae)
			{
				title = title + " - " + GeneralConfigs.GetConfig().Sitetitle;
			}

			if (OverwriteExisting)
			{
				page.Title = HttpUtility.HtmlEncode(title);
			}
			else
			{
				if (string.IsNullOrEmpty(page.Title))
				{
					page.Title = HttpUtility.HtmlEncode(title);
				}
			}
		}

	}
}
