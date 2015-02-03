<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUserEdit.aspx.cs" Inherits="EducationV2.frmUserEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户信息编辑</title>
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
    <style type="text/css">
        .style1
        {
            width: 240px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="systeminfo maintable">      
            <table id="table1" border="1" cellpadding="0" cellspacing="0" class="sort-table"
                width="300">
                <tr>
                    <td class="systitleline add" colspan="2" align="center">
                        用户信息编辑
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        用户名
                    </td>
                    <td class="style1">
                        <asp:TextBox ID="txtName" runat="server" CssClass="validate[required]"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        真实姓名
                    </td>
                    <td class="style1">
                        <asp:TextBox ID="txtRealName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        用户类型
                    </td>
                    <td class="style1">
                        <asp:DropDownList ID="ddlRole" runat="server" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                           <asp:ListItem>申请人员</asp:ListItem>
                           <asp:ListItem>系统管理员</asp:ListItem>
                           <asp:ListItem>组织部部长</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                
                <tr>
                    <td width="120">
                        <asp:Label ID="labBelongType" runat="server" Text="所在部门 "></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:DropDownList ID="ddlBelongDept" runat="server" DataSourceID="SqlDataSource1"
                            DataTextField="F_name" DataValueField="F_ID">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        密码
                    </td>
                    <td class="style1">
                        <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        状态
                    </td>
                    <td class="style1">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem>审核通过</asp:ListItem>
                            <asp:ListItem>待审核</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="2" align="center">
                     <input type="button" value="返 回" class="btn" onclick="returnBack()" />&nbsp;
                        <asp:Button ID="btnAdd" runat="server" Text="增 加" OnClick="btnAdd_Click" CssClass="btn" />
                        &nbsp;<asp:Button ID="btnModify" runat="server" Text="修 改" OnClick="btnModify_Click"
                            CssClass="btn" />
                        &nbsp;
                    </td>
                </tr>
            </table>
 
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FjutOrgDeptConnectionString %>"
        SelectCommand="SELECT [F_ID], [F_name] FROM [DeptMent] WHERE ([F_unitID] = @F_unitID)">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="F_unitID" SessionField="UnitID" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
