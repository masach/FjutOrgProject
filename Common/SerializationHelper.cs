using System;
using System.IO;
using System.Xml.Serialization;

namespace Common
{
	public class SerializationHelper
	{
		/// <summary>
		/// 反序列化指定的类
		/// </summary>
		/// <param name="type">对象类型</param>
		/// <param name="filename">文件路径</param>
		/// <returns></returns>
		public static object Load(Type type, string filename)
		{
			object obj;
			FileStream fs = null;
			try
			{
				// open the stream...
				fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				XmlSerializer serializer = new XmlSerializer(type);
				obj = serializer.Deserialize(fs);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (fs != null)
					fs.Close();
			}
			return obj;
		}

		/// <summary>
		/// 保存(序列化)指定路径下的配置文件
		/// </summary>
		/// <param name="obj">对象</param>
		/// <param name="filename">文件路径</param>
		public static bool Save(object obj, string filename)
		{
			bool succeed = false;
			FileStream fs = null;
			// serialize it...
			try
			{
				fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
				XmlSerializer serializer = new XmlSerializer(obj.GetType());
				serializer.Serialize(fs, obj);

				succeed = true;//成功则将会返回true
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (fs != null)
					fs.Close();
			}
			return succeed;
		}
	}
}
