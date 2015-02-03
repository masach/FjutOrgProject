using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
namespace EducationV2
{
    public partial class frmUserChooseCity : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                XDocument xd = XDocument.Load(MapPath("~/App_Data/city.xml"));
                IEnumerable<XElement>  xes = xd.Element("cities").Elements();
                BindTree(SmartTreeView1.Nodes, xes);
                SmartTreeView1.CollapseAll();
            }
        }

        void BindTree(TreeNodeCollection nds, IEnumerable<XElement> parent)
        {
            TreeNode tn = null;
            IEnumerable<XElement> eles = from e in parent                       
                       select e;

            foreach (XElement ele in eles)
            {
                if (ele.HasElements)
                {
                    tn = new TreeNode(ele.Attribute("name").Value); // city
                }
                else
                {
                    tn = new TreeNode(ele.Value); // item
                }
                
                nds.Add(tn);
                BindTree(tn.ChildNodes, ele.Elements());
            }
        }


        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            String str = "";
            if (SmartTreeView1.CheckedNodes.Count > 0)
                str = "<script>window.opener.document.getElementById('" + Request["controlID"] + "').value = '" + SmartTreeView1.CheckedNodes[0].Text + "'; window.close();</script>";
            
            Literal1.Text = str;
        }

        public String GetValue()
        {
            if (SmartTreeView1.CheckedNodes == null || SmartTreeView1.CheckedNodes.Count == 0)
                return "";
            return SmartTreeView1.CheckedNodes[0].Value;
        }

        protected void SmartTreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            //String str = SmartTreeView1.SelectedNode.Text;
            //Literal1.Text = str;
        }
    }
}