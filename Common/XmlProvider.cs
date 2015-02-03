using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Xml;
using System.Web.Caching;
using System.Collections;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using Common;

namespace Common
{
    /// <summary>
    /// 配置文件如xml等操作类
    /// </summary>
    public class XmlProvider : XmlDocument
    {
        private XmlDocument doc = new XmlDocument();
        private string cacheKey = "";
        private string xmlFilePath="";
       

        /// <summary>
        /// 缓存关键字
        /// </summary>
        public string CacheKey
        {
            get{
                return cacheKey;
            }
            set{
                cacheKey = value;
            }
        }

        /// <summary>
        /// 文件虚拟路径
        /// </summary>
        /// <remarks>文件路径</remarks>
        public string XmlFilePath
        {
            get{
                return "/" + xmlFilePath;
            }
            set{
                xmlFilePath = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public XmlProvider(): base()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public XmlProvider(XmlNameTable nt) : base(new XmlImplementation(nt))
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tempXmlFilePath">文件路径相对路径</param>
        public XmlProvider(string tempXmlFilePath)
        {
            this.xmlFilePath = tempXmlFilePath;
            this.cacheKey = tempXmlFilePath;
            GetXmlDocument();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tempXmlFilePath">文件路径根目录的相对路径</param>
        /// <param name="tempCacheKey">要缓存的关键字</param>
        public XmlProvider(string tempXmlFilePath, string tempCacheKey)
        {
            this.xmlFilePath = tempXmlFilePath;
            this.cacheKey = tempCacheKey;
            if (cacheKey == ""){
                cacheKey = tempXmlFilePath;
            }
            GetXmlDocument();
        }

        ///<summary>
        ///获取XmlDocument实体类
        ///</summary>    
        /// <returns>指定的XML描述文件的一个xmldocument实例</returns>
        private XmlDocument GetXmlDocument()
        {
            doc = LqlCache.Get(CacheKey) as XmlDocument;
            if (doc == null)
            {
                XmlDocument xmldoc = new XmlDocument();
                
                string file = HttpContext.Current.Server.MapPath(XmlFilePath);
                CacheDependency dp = new CacheDependency(file);
                xmldoc.Load(file);

                doc = xmldoc;
                LqlCache.Max(CacheKey, doc, dp);
            }
            return doc;
        }

        /// <summary>
        /// 加载的文件名(含路径)
        /// </summary>
        /// <param name="filename"></param>
        public override void Load(string file)
        {
            if (System.IO.File.Exists(file))
            {
                base.Load(file);
            }
            else
            {
                throw new Exception("文件: " + file + " 不存在!");
            }
        }

        /// <summary>
        /// 获取所有指定名称的节点(XmlNodeList)
        /// </summary>
        /// <param >节点名称</param>
        public XmlNodeList getXmlNodes(string strNode)
        {
            //根据指定路径获取节点
            XmlNodeList strReturn = null;
            try
            {
                strReturn=doc.SelectNodes(strNode);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return strReturn;
        }

        /// <summary>
        /// 在指定的Xml元素下,添加子Xml元素，同时带属性
        /// </summary>
        /// <param name="xmlElement"></param>
        /// <param name="xValue"></param>
        /// <param name="attrName"></param>
        /// <param name="attrValue"></param>
        /// <param name="IsCDataSection"></param>
        /// <returns></returns>
        public bool AddChildWhitAttributes(ref XmlElement xmlElement, string xValue, string attrName, string attrValue)
        {
            return  AddChildWhitAttributes(ref xmlElement,  xValue,  attrName,  attrValue,  false);
        }

        /// <summary>
        /// 在指定的Xml元素下,添加子Xml元素，同时带属性
        /// </summary>
        /// <param name="xmlElement"></param>
        /// <param name="xValue"></param>
        /// <param name="attrName"></param>
        /// <param name="attrValue"></param>
        /// <param name="IsCDataSection"></param>
        /// <returns></returns>
        public bool AddChildWhitAttributes(ref XmlElement xmlElement, string xValue, string attrName, string attrValue, bool IsCDataSection)
        {
            if ((xmlElement != null) && (xmlElement.OwnerDocument != null))
            {
                if (IsCDataSection)
                {
                    XmlCDataSection tempdata = xmlElement.OwnerDocument.CreateCDataSection(attrName);
                    tempdata.InnerText = xValue;
                    xmlElement.AppendChild(tempdata);

                    XmlAttribute xa = xmlElement.OwnerDocument.CreateAttribute(attrName);
                    xa.Value = attrValue;
                    xmlElement.Attributes.Append(xa);
                }
                else
                {
                    XmlAttribute xa = xmlElement.OwnerDocument.CreateAttribute(attrName);
                    xa.Value = attrValue;
                    xmlElement.Attributes.Append(xa);
                    xmlElement.InnerText = xValue;
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 在指定的Xml元素下,添加子Xml元素
        /// </summary>
        /// <param name="xmlElement">被追加子元素的Xml元素</param>
        /// <param name="childElementName">要添加的Xml元素名称</param>
        /// <param name="childElementValue">要添加的Xml元素值</param>
        /// <returns></returns>
        public bool AppendChildElementByNameValue(ref XmlElement xmlElement, string childElementName, object childElementValue)
        {
            return AppendChildElementByNameValue(ref xmlElement, childElementName, childElementValue, false);
        }


        /// <summary>
        /// 在指定的Xml元素下,添加子Xml元素
        /// </summary>
        /// <param name="xmlElement">被追加子元素的Xml元素</param>
        /// <param name="childElementName">要添加的Xml元素名称</param>
        /// <param name="childElementValue">要添加的Xml元素值</param>
        /// <param name="IsCDataSection">是否是CDataSection类型的子元素</param>
        /// <returns></returns>
        public bool AppendChildElementByNameValue(ref XmlElement xmlElement, string childElementName, object childElementValue, bool IsCDataSection)
        {
            if ((xmlElement != null) && (xmlElement.OwnerDocument != null))
            {
                //是否是CData类型Xml元素
                if (IsCDataSection)
                {
                    XmlCDataSection tempdata = xmlElement.OwnerDocument.CreateCDataSection(childElementName);
                    tempdata.InnerText = FiltrateControlCharacter(childElementValue.ToString());
                    XmlElement childXmlElement = xmlElement.OwnerDocument.CreateElement(childElementName);
                    childXmlElement.AppendChild(tempdata);
                    xmlElement.AppendChild(childXmlElement);
                }
                else
                {
                    XmlElement childXmlElement = xmlElement.OwnerDocument.CreateElement(childElementName);
                    childXmlElement.InnerText = FiltrateControlCharacter(childElementValue.ToString());
                    xmlElement.AppendChild(childXmlElement);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 在指定的Xml结点下,添加子Xml元素
        /// </summary>
        /// <param name="xmlElement">被追加子元素的Xml节点</param>
        /// <param name="childElementName">要添加的Xml元素名称</param>
        /// <param name="childElementValue">要添加的Xml元素值</param>
        /// <returns></returns>
        public bool AppendChildElementByNameValue(ref XmlNode xmlNode, string childElementName, object childElementValue)
        {
            return AppendChildElementByNameValue(ref xmlNode, childElementName, childElementValue, false);
        }


        /// <summary>
        /// 在指定的Xml结点下,添加子Xml元素
        /// </summary>
        /// <param name="xmlElement">被追加子元素的Xml节点</param>
        /// <param name="childElementName">要添加的Xml元素名称</param>
        /// <param name="childElementValue">要添加的Xml元素值</param>
        /// <param name="IsCDataSection">是否是CDataSection类型的子元素</param>
        /// <returns></returns>
        public bool AppendChildElementByNameValue(ref XmlNode xmlNode, string childElementName, object childElementValue, bool IsCDataSection)
        {
            if ((xmlNode != null) && (xmlNode.OwnerDocument != null))
            {
                //是否是CData类型Xml结点
                if (IsCDataSection)
                {
                    XmlCDataSection tempdata = xmlNode.OwnerDocument.CreateCDataSection(childElementName);
                    tempdata.InnerText = FiltrateControlCharacter(childElementValue.ToString());
                    XmlElement childXmlElement = xmlNode.OwnerDocument.CreateElement(childElementName);
                    childXmlElement.AppendChild(tempdata);
                    xmlNode.AppendChild(childXmlElement);
                }
                else
                {
                    XmlElement childXmlElement = xmlNode.OwnerDocument.CreateElement(childElementName);
                    childXmlElement.InnerText = FiltrateControlCharacter(childElementValue.ToString());
                    xmlNode.AppendChild(childXmlElement);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 通过数据行向当前XML元素下追加子元素
        /// </summary>
        /// <param name="xmlElement">被追加子元素的Xml元素</param>
        /// <param name="dcc">当前数据表中的列集合</param>
        /// <param name="dr">当前行数据</param>
        /// <returns></returns>
        public bool AppendChildElementByDataRow(ref XmlElement xmlElement, DataColumnCollection dcc, DataRow dr)
        {
            return AppendChildElementByDataRow(ref xmlElement, dcc, dr, null);
        }

        /// <summary>
        /// 通过数据行向当前XML元素下追加子元素
        /// </summary>
        /// <param name="xmlElement">被追加子元素的Xml元素</param>
        /// <param name="dcc">当前数据表中的列集合</param>
        /// <param name="dr">当前行数据</param>
        /// <param name="removecols">不会被追加的列名</param>
        /// <returns></returns>
        public bool AppendChildElementByDataRow(ref XmlElement xmlElement, DataColumnCollection dcc, DataRow dr, string removecols)
        {
            if ((xmlElement != null) && (xmlElement.OwnerDocument != null))
            {
                foreach (DataColumn dc in dcc)
                {
                    if ((removecols == null) ||
                        (removecols == "") ||
                        (("," + removecols + ",").ToLower().IndexOf("," + dc.Caption.ToLower() + ",") < 0))
                    {
                        XmlElement tempElement = xmlElement.OwnerDocument.CreateElement(dc.Caption);
                        tempElement.InnerText = FiltrateControlCharacter(dr[dc.Caption].ToString().Trim());
                        xmlElement.AppendChild(tempElement);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 实始化节点, 当节点存在则清除当前路径下的所有子结点, 如不存在则直接创建该结点
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <returns></returns>
        public XmlNode InitializeNode(string xmlpath)
        {
            XmlNode xmlNode = this.SelectSingleNode(xmlpath);
            if (xmlNode != null)
            {
                xmlNode.RemoveAll();
            }
            else
            {
                xmlNode = CreateNode(xmlpath);
            }
            return xmlNode;
        }


        /// <summary>
        /// 删除指定路径下面的所有子结点和自身
        /// </summary>
        /// <param name="xmlpath">指定路径</param>
        public void RemoveNodeAndChildNode(string xmlpath)
        {
            XmlNodeList xmlNodeList = this.SelectNodes(xmlpath);
            if (xmlNodeList.Count > 0)
            {
                foreach (XmlNode xn in xmlNodeList)
                {
                    xn.RemoveAll();
                    xn.ParentNode.RemoveChild(xn);
                }
            }
        }

        /// <summary>
        /// 创建指定路径下的节点
        /// </summary>
        /// <param name="xmlpath">节点路径</param>
        /// <returns></returns>
        public XmlNode CreateNode(string xmlpath)
        {

            string[] xpathArray = xmlpath.Split('/');
            string root = "";
            XmlNode parentNode = this;
            //建立相关节点
            for (int i = 1; i < xpathArray.Length; i++)
            {
                XmlNode node = this.SelectSingleNode(root + "/" + xpathArray[i]);
                // 如果当前路径不存在则建立,否则设置当前路径到它的子路径上
                if (node == null)
                {
                    XmlElement newElement = this.CreateElement(xpathArray[i]);
                    parentNode.AppendChild(newElement);
                }
                //设置低一级的路径
                root = root + "/" + xpathArray[i];
                parentNode = this.SelectSingleNode(root);
            }
            return parentNode;
        }

        /// <summary>
        /// 得到指定路径的节点值
        /// </summary>
        /// <param name="xmlnode">要查找节点</param>
        /// <param name="path">指定路径</param>
        /// <returns></returns>
        public string GetSingleNodeValue(XmlNode xmlnode, string path)
        {
            if (xmlnode == null)
            {
                return null;
            }

            if (xmlnode.SelectSingleNode(path) != null)
            {
                if (xmlnode.SelectSingleNode(path).LastChild != null)
                {
                    return xmlnode.SelectSingleNode(path).LastChild.Value;
                }
                else
                {
                    return "";
                }
            }
            return null;
        }

        /// <summary>
        /// 查找指定路径是否存在
        /// </summary>
        /// <param name="xmlnode">要查找节点</param>
        /// <param name="path">指定路径</param>
        /// <returns></returns>
        public bool IsNodeExit(XmlNode xmlnode, string path)
        {
            bool isfind = true;
            if (xmlnode == null) {
                isfind = false;
            } else {
                if (xmlnode.SelectSingleNode(path) != null) {
                    isfind = true;
                } else {
                    isfind = false;
                }
            }
            return isfind;
        }

        /// <summary>
        /// 过滤控制字符,包括0x00 - 0x08,0x0b - 0x0c,0x0e - 0x1f
        /// </summary>
        /// <param name="content">要过滤的内容</param>
        /// <returns>过滤后的内容</returns>
        private string FiltrateControlCharacter(string content)
        {
            return Regex.Replace(content, "[\x00-\x08|\x0b-\x0c|\x0e-\x1f]", "");
        }

    }
}
