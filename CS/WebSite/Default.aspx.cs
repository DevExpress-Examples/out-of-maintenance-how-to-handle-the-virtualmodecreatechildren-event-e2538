using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
#region #CreateChildren
using System.Collections.Generic;
using DevExpress.Web.ASPxTreeView;
using System.IO;

public partial class _Default : System.Web.UI.Page {
    const string FileImageUrl = "~/Images/file.png";
    const string DirImageUrl = "~/Images/directory.png";

    protected void Page_Load(object sender, EventArgs e) {
    }

    protected void treeView_VirtualModeCreateChildren(object source, TreeViewVirtualModeCreateChildrenEventArgs e) {
        string parentNodePath = string.IsNullOrEmpty(e.NodeName) ? Page.MapPath("~/") : e.NodeName;
        List<TreeViewVirtualNode> children = new List<TreeViewVirtualNode>();
        if (Directory.Exists(parentNodePath)) {
            foreach (string childPath in Directory.GetDirectories(parentNodePath)) {
                string childDirName = Path.GetFileName(childPath);
                if (IsSystemName(childDirName))
                    continue;
                TreeViewVirtualNode childNode = new TreeViewVirtualNode(childPath, childDirName);
                childNode.Image.Url = DirImageUrl;
                children.Add(childNode);
            }
            foreach (string childPath in Directory.GetFiles(parentNodePath)) {
                string childFileName = Path.GetFileName(childPath);
                if (IsSystemName(childFileName))
                    continue;
                TreeViewVirtualNode childNode = new TreeViewVirtualNode(childPath, childFileName);
                childNode.IsLeaf = true;
                childNode.Image.Url = FileImageUrl;
                children.Add(childNode);
            }
        }
        e.Children = children;
    }

    protected bool IsSystemName(string name) {
        name = name.ToLower();
        return name.StartsWith("app_") || name == "bin"
            || name.EndsWith(".aspx.cs") || name.EndsWith(".aspx.vb");
    }
}
#endregion #CreateChildren