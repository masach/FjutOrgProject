<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmDeptEdit.aspx.cs"
    Inherits="EducationV2.frmDeptEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>部门信息编辑</title>
    <link rel="stylesheet" type="text/css" href="css/Style.css" />
    <link rel="stylesheet" href="css/validationEngine.jquery.css" />
    <script type="text/javascript" src="Scripts/JQuery-1.10.js" charset="UTF-8"></script>
    <script src="js/jquery.validationEngine-zh_CN.js" type="text/javascript"></script>
    <script src="js/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#form1").validationEngine();
        });
        $(window).unload(function () {
            refreshParent();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="systitleline add">
            &nbsp;部门信息编辑
        </div>
        <div class="systeminfo maintable">
            <table id="table1" cellpadding="0" cellspacing="0" width="80%">
                <tr>
                    <td width="120">
                        部门名
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" CssClass="validate[required]"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        机构类别
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOrgType" runat="server">
                            <asp:ListItem>管理机构</asp:ListItem>
                            <asp:ListItem>教学机构</asp:ListItem>
                            <asp:ListItem>其他机构</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        是否启用
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlState" runat="server">
                            <asp:ListItem>审核通过</asp:ListItem>
                            <asp:ListItem>待审核</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="200" colspan="2" align="center">
                      <input type="button" value="返 回" class="btn" onclick="returnBack()" />&nbsp;
                        <asp:Button ID="btnAdd" runat="server" Text="增 加" OnClick="btnAdd_Click" CssClass="btn" />
                        &nbsp;<asp:Button ID="btnModify" runat="server" Text="修 改" OnClick="btnModify_Click"
                            CssClass="btn" />
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
