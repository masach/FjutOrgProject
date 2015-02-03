using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	/// <summary>
	/// 基本设置类
	/// </summary>
	public class GeneralConfigs : IConfigInfo
	{

		private static object lockHelper = new object();

		private static System.Timers.Timer generalConfigTimer = new System.Timers.Timer(15000);

		private static GeneralConfigInfo m_configinfo;

		/// <summary>
		/// 静态构造函数初始化相应实例和定时器
		/// </summary>
		static GeneralConfigs()
		{
			m_configinfo = GeneralConfigFileManager.LoadConfig();
			generalConfigTimer.AutoReset = true;
			generalConfigTimer.Enabled = true;
			generalConfigTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
			generalConfigTimer.Start();
		}

		private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			ResetConfig();
		}


		/// <summary>
		/// 重设配置类实例
		/// </summary>
		public static void ResetConfig()
		{
			m_configinfo = GeneralConfigFileManager.LoadConfig();
		}

		public static GeneralConfigInfo GetConfig()
		{
			return m_configinfo;
		}


		/// <summary>
		/// 获取默认模板
		/// </summary>
		/// <returns></returns>
		public static string GetDefaultSkinName()
		{
			return GetConfig().SkinName;
		}


		#region Helper
		/// <summary>
		/// 序列化配置信息为XML
		/// </summary>
		/// <param name="configinfo">配置信息</param>
		/// <param name="configFilePath">配置文件完整路径</param>
		public static GeneralConfigInfo Serialiaze(GeneralConfigInfo configinfo, string configFilePath)
		{
			lock (lockHelper)
			{
				SerializationHelper.Save(configinfo, configFilePath);
			}
			return configinfo;
		}

		/// <summary>
		/// 反序列化配置信息为对象
		/// </summary>
		/// <param name="configFilePath">配置文件完整路径</param>
		public static GeneralConfigInfo Deserialize(string configFilePath)
		{
			return (GeneralConfigInfo)SerializationHelper.Load(typeof(GeneralConfigInfo), configFilePath);
		}
		#endregion

	}
}
