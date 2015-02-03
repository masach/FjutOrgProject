<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUserFind.aspx.cs" Inherits="EducationV2.frmUserFind" %>

<%@ Register Assembly="Hxj.Web.UI" Namespace="Hxj.Web.UI" TagPrefix="Hxj" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查找用户</title>
    <link rel="stylesheet" type="text/css" href="css/Style.css" />
    <link href="css/ui-lightness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
</head>
<body onload="return resizeWindow(800, 350);">
    <form id="form1" runat="server">
    <div class="maintable">
        <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
            <ajaxToolkit:TabPanel runat="server" HeaderText="查找用户" ID="tabFindUser"  OnClientClick="resizeWindow(800, 350);"  >
            <ContentTemplate>--%>
        <Hxj:TabControl ID="TabControl1" runat="server" TabSelectedIndex="0">
            <Hxj:TabItem ID="tabFindUserByName" runat="server" Text="查找方式一">
                <table style="width: 600px">
                  <tr>
                        <td colspan="2" class="auto-style1">
                            请输入找类型和数据，点击“查找”，系统将查找符合条件的人员信息。
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px">
                            查找类型
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlField" runat="server">
                                <asp:ListItem Value="F_userName">用户名</asp:ListItem>
                                <asp:ListItem Value="F_ID">用户编号</asp:ListItem>                             
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            关键字
                        </td>
                        <td>
                            <asp:TextBox ID="txtKeyword" runat="server" CssClass="t_tabletxt"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="关键字不能为空"
                                ControlToValidate="txtKeyword" ForeColor="Red" ValidationGroup="searchKeyGroup"></asp:RequiredFieldValidator>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnKeySearch" runat="server" Text="确 认" CssClass="btn" OnClick="btnKeySearch_Click"
                                ValidationGroup="searchKeyGroup" />
                            &nbsp;<asp:Button ID="Button2" runat="server" Text="返 回" CssClass="btn" OnClick="btnReturn1_Click" />
                        </td>
                    </tr>
                </table>
    
            </Hxj:TabItem>
            <Hxj:TabItem ID="tabFindUser" runat="server" Text="查找方式二">
                <table style="width: 600px">
                    <tr>
                        <td colspan="2" class="auto-style1">
                            请输入要查找人员的证件类型和证件号码，点击“查找”，系统将查找符合条件的人员信息。
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px">
                            证件类型
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="t_tabletxt">
                                <asp:ListItem Text="身份证" Value="身份证" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="军人证" Value="军人证"></asp:ListItem>
                                <asp:ListItem Text="护照" Value="护照"></asp:ListItem>
                                <asp:ListItem Text="其他" Value="其他"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            证件号码
                        </td>
                        <td>
                            <asp:TextBox ID="txtNo" runat="server" CssClass="t_tabletxt"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="证件号不能为空"
                                ControlToValidate="txtNo" ForeColor="Red" ValidationGroup="searchGroup"></asp:RequiredFieldValidator>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnSearch" runat="server" Text="确 认" CssClass="btn" OnClick="btnSearch_Click"
                                ValidationGroup="searchGroup" />
                            &nbsp;<asp:Button ID="btnReturn1" runat="server" Text="返 回" CssClass="btn" OnClick="btnReturn1_Click" />
                        </td>
                    </tr>
                </table>
                <%-- </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="tabAddUser" runat="server" HeaderText="增加用户" OnClientClick="resizeWindow(800, 500)"  >
            <ContentTemplate>--%>
            </Hxj:TabItem>
            <Hxj:TabItem ID="tabAddUser" runat="server" Text="增加用户">
                <table class="hold-table" width="700px" cellspacing="0" cellpadding="0" style="padding-left: 8px;">
                   <tr>
                        <td colspan="2" class="auto-style1">
                            请输入数据，点击“确定”，系统将新建一个用户。
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
                          <asp:Button ID="btnAdd" runat="server" Text="确 定" CssClass="btn" OnClick="btnAdd_Click"
                                ValidationGroup="addGroup" />
                            &nbsp;
                            <asp:Button ID="btnReturn" runat="server" Text="返 回" CssClass="btn" OnClick="btnReturn1_Click" />
                        </td>
                    </tr>
                   
                </table>
            </Hxj:TabItem>
        </Hxj:TabControl>
        <%-- </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>--%>
        <asp:XmlDataSource ID="NationalityDataSource" runat="server" DataFile="~/App_Data/nationality.xml">
        </asp:XmlDataSource>
        <asp:XmlDataSource ID="TitleDataSource" runat="server" DataFile="~/App_Data/title.xml">
        </asp:XmlDataSource>
         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FjutOrgDeptConnectionString %>"
        SelectCommand="SELECT [F_ID], [F_name] FROM [DeptMent] WHERE ([F_unitID] = @F_unitID)">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="F_unitID" SessionField="UnitID" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    </div>
    </form>
    <script type="text/javascript" src="Scripts/JQuery-1.10.js" charset="UTF-8"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.10.3.custom.js"></script>
    <script src="Scripts/common.js" type="text/javascript"> </script>
    <script type="text/javascript">
        $(function () {
            datepicker();
        });
        
    </script>
</body>
</html>
