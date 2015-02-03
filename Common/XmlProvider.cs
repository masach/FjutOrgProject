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
    /// �����ļ���xml�Ȳ�����
    /// </summary>
    public class XmlProvider : XmlDocument
    {
        private XmlDocument doc = new XmlDocument();
        private string cacheKey = "";
        private string xmlFilePath="";
       

        /// <summary>
        /// ����ؼ���
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
        /// �ļ�����·��
        /// </summary>
        /// <remarks>�ļ�·��</remarks>
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
        /// ���캯��
        /// </summary>
        public XmlProvider(): base()
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        public XmlProvider(XmlNameTable nt) : base(new XmlImplementation(nt))
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="tempXmlFilePath">�ļ�·�����·��</param>
        public XmlProvider(string tempXmlFilePath)
        {
            this.xmlFilePath = tempXmlFilePath;
            this.cacheKey = tempXmlFilePath;
            GetXmlDocument();
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="tempXmlFilePath">�ļ�·����Ŀ¼�����·��</param>
        /// <param name="tempCacheKey">Ҫ����Ĺؼ���</param>
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
        ///��ȡXmlDocumentʵ����
        ///</summary>    
        /// <returns>ָ����XML�����ļ���һ��xmldocumentʵ��</returns>
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
        /// ���ص��ļ���(��·��)
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
                throw new Exception("�ļ�: " + file + " ������!");
            }
        }

        /// <summary>
        /// ��ȡ����ָ�����ƵĽڵ�(XmlNodeList)
        /// </summary>
        /// <param >�ڵ�����</param>
        public XmlNodeList getXmlNodes(string strNode)
        {
            //����ָ��·����ȡ�ڵ�
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
        /// ��ָ����XmlԪ����,�����XmlԪ�أ�ͬʱ������
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
        /// ��ָ����XmlԪ����,�����XmlԪ�أ�ͬʱ������
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
        /// ��ָ����XmlԪ����,�����XmlԪ��
        /// </summary>
        /// <param name="xmlElement">��׷����Ԫ�ص�XmlԪ��</param>
        /// <param name="childElementName">Ҫ��ӵ�XmlԪ������</param>
        /// <param name="childElementValue">Ҫ��ӵ�XmlԪ��ֵ</param>
        /// <returns></returns>
        public bool AppendChildElementByNameValue(ref XmlElement xmlElement, string childElementName, object childElementValue)
        {
            return AppendChildElementByNameValue(ref xmlElement, childElementName, childElementValue, false);
        }


        /// <summary>
        /// ��ָ����XmlԪ����,�����XmlԪ��
        /// </summary>
        /// <param name="xmlElement">��׷����Ԫ�ص�XmlԪ��</param>
        /// <param name="childElementName">Ҫ��ӵ�XmlԪ������</param>
        /// <param name="childElementValue">Ҫ��ӵ�XmlԪ��ֵ</param>
        /// <param name="IsCDataSection">�Ƿ���CDataSection���͵���Ԫ��</param>
        /// <returns></returns>
        public bool AppendChildElementByNameValue(ref XmlElement xmlElement, string childElementName, object childElementValue, bool IsCDataSection)
        {
            if ((xmlElement != null) && (xmlElement.OwnerDocument != null))
            {
                //�Ƿ���CData����XmlԪ��
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
        /// ��ָ����Xml�����,�����XmlԪ��
        /// </summary>
        /// <param name="xmlElement">��׷����Ԫ�ص�Xml�ڵ�</param>
        /// <param name="childElementName">Ҫ��ӵ�XmlԪ������</param>
        /// <param name="childElementValue">Ҫ��ӵ�XmlԪ��ֵ</param>
        /// <returns></returns>
        public bool AppendChildElementByNameValue(ref XmlNode xmlNode, string childElementName, object childElementValue)
        {
            return AppendChildElementByNameValue(ref xmlNode, childElementName, childElementValue, false);
        }


        /// <summary>
        /// ��ָ����Xml�����,�����XmlԪ��
        /// </summary>
        /// <param name="xmlElement">��׷����Ԫ�ص�Xml�ڵ�</param>
        /// <param name="childElementName">Ҫ��ӵ�XmlԪ������</param>
        /// <param name="childElementValue">Ҫ��ӵ�XmlԪ��ֵ</param>
        /// <param name="IsCDataSection">�Ƿ���CDataSection���͵���Ԫ��</param>
        /// <returns></returns>
        public bool AppendChildElementByNameValue(ref XmlNode xmlNode, string childElementName, object childElementValue, bool IsCDataSection)
        {
            if ((xmlNode != null) && (xmlNode.OwnerDocument != null))
            {
                //�Ƿ���CData����Xml���
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
        /// ͨ����������ǰXMLԪ����׷����Ԫ��
        /// </summary>
        /// <param name="xmlElement">��׷����Ԫ�ص�XmlԪ��</param>
        /// <param name="dcc">��ǰ���ݱ��е��м���</param>
        /// <param name="dr">��ǰ������</param>
        /// <returns></returns>
        public bool AppendChildElementByDataRow(ref XmlElement xmlElement, DataColumnCollection dcc, DataRow dr)
        {
            return AppendChildElementByDataRow(ref xmlElement, dcc, dr, null);
        }

        /// <summary>
        /// ͨ����������ǰXMLԪ����׷����Ԫ��
        /// </summary>
        /// <param name="xmlElement">��׷����Ԫ�ص�XmlԪ��</param>
        /// <param name="dcc">��ǰ���ݱ��е��м���</param>
        /// <param name="dr">��ǰ������</param>
        /// <param name="removecols">���ᱻ׷�ӵ�����</param>
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
        /// ʵʼ���ڵ�, ���ڵ�����������ǰ·���µ������ӽ��, �粻������ֱ�Ӵ����ý��
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
        /// ɾ��ָ��·������������ӽ�������
        /// </summary>
        /// <param name="xmlpath">ָ��·��</param>
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
        /// ����ָ��·���µĽڵ�
        /// </summary>
        /// <param name="xmlpath">�ڵ�·��</param>
        /// <returns></returns>
        public XmlNode CreateNode(string xmlpath)
        {

            string[] xpathArray = xmlpath.Split('/');
            string root = "";
            XmlNode parentNode = this;
            //������ؽڵ�
            for (int i = 1; i < xpathArray.Length; i++)
            {
                XmlNode node = this.SelectSingleNode(root + "/" + xpathArray[i]);
                // �����ǰ·������������,�������õ�ǰ·����������·����
                if (node == null)
                {
                    XmlElement newElement = this.CreateElement(xpathArray[i]);
                    parentNode.AppendChild(newElement);
                }
                //���õ�һ����·��
                root = root + "/" + xpathArray[i];
                parentNode = this.SelectSingleNode(root);
            }
            return parentNode;
        }

        /// <summary>
        /// �õ�ָ��·���Ľڵ�ֵ
        /// </summary>
        /// <param name="xmlnode">Ҫ���ҽڵ�</param>
        /// <param name="path">ָ��·��</param>
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
        /// ����ָ��·���Ƿ����
        /// </summary>
        /// <param name="xmlnode">Ҫ���ҽڵ�</param>
        /// <param name="path">ָ��·��</param>
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
        /// ���˿����ַ�,����0x00 - 0x08,0x0b - 0x0c,0x0e - 0x1f
        /// </summary>
        /// <param name="content">Ҫ���˵�����</param>
        /// <returns>���˺������</returns>
        private string FiltrateControlCharacter(string content)
        {
            return Regex.Replace(content, "[\x00-\x08|\x0b-\x0c|\x0e-\x1f]", "");
        }

    }
}
