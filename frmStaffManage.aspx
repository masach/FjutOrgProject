<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmStaffManage.aspx.cs" Inherits="EducationV2.frmStaffManage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>职工档案管理</title>
    <!--<link href="css/ui-lightness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/JQuery-1.10.js" charset="UTF-8"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.10.3.custom.js"></script>-->
    <link rel="stylesheet" type="text/css" href="css/Style.css" />
    <script src="Scripts/common.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="css/page.css" />
    <script type="text/javascript">
        function btnAdd_onclick() {
            window.open('frmStaffDetail.aspx', 'newwindow', 'height=550,width=600,top=200,left=400,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no');
            // self.location.href = 'frmStaffDetail.aspx'; // input如果是submit类型，则该语句无效     
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="systeminfo">
        <div class="systitleline">
            <input type="button" value="刷 新" class="btn" onclick="refresh()" />&nbsp;
            <input type="button" id="btnAdd" value="添 加" onclick="btnAdd_onclick()" class="btn" />&nbsp;
            
            <asp:Button ID="btnDel" runat="server" Text="删 除" OnClick="btnDel_Click" class="btn" />
            <ajaxToolkit:ConfirmButtonExtender ID="btnDel_ConfirmButtonExtender" runat="server"
                ConfirmText="是否删除所选中的记录？" Enabled="True" TargetControlID="btnDel">
            </ajaxToolkit:ConfirmButtonExtender>
            &nbsp;
            <asp:Button ID="btnAuthor" runat="server" Text="通 过" class="btn" OnClick="btnAuthor_Click" />
            &nbsp;
            <asp:Button ID="btnUnAuthor" runat="server" Text="不通过" OnClick="btnUnAuthor_Click"
                CssClass="btn" />
            &nbsp;<asp:DropDownList ID="ddlField" runat="server">
                <asp:ListItem Value="用户名">用户名</asp:ListItem>
                <asp:ListItem Value="真实姓名">真实姓名</asp:ListItem>
                <asp:ListItem Value="所属部门">所属部门</asp:ListItem>
                <asp:ListItem Value="姓别">性别</asp:ListItem>
                <asp:ListItem Value="学历">学历</asp:ListItem>
                <asp:ListItem Value="学位">学位</asp:ListItem>
                <asp:ListItem Value="状态">状态</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
            <asp:DropDownList ID="ddlType" runat="server">
            </asp:DropDownList>
            &nbsp;&nbsp;<asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:Button
                ID="btnSearch" runat="server" Text="检 索" OnClick="btnSearch_Click" CssClass="btn" />
            <p />
        </div>
        &nbsp;<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="maintable">
                    <yyc:SmartGridView ID="gvStaffsInDept" runat='server' AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CellPadding="4" Width="100%" DataKeyNames="F_StaffID"
                        OnPageIndexChanged="gvStaffsInDept_PageIndexChanged" DataSourceID="odsStaffInDept"
                        GridLines="None">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="50px">
                                <HeaderTemplate>
                                    序号
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="50px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="30px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="allCheck" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="30px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="F_StaffID" HeaderText="F_StaffID" SortExpression="F_StaffID" Visible="false" />                            
                            <asp:BoundField DataField="真实姓名" HeaderText="真实姓名" SortExpression="真实姓名" />
                            <asp:BoundField DataField="所属部门" HeaderText="所属部门" SortExpression="所属部门" />
                            <asp:BoundField DataField="姓别" HeaderText="性别" SortExpression="姓别" />
                            <asp:BoundField DataField="学历" HeaderText="学历" SortExpression="学历" />
                            <asp:BoundField DataField="学位" HeaderText="学位" SortExpression="学位" />
                            <asp:BoundField DataField="状态" HeaderText="状态" SortExpression="状态" />

                            <asp:HyperLinkField DataNavigateUrlFields="F_UserID" SortExpression="用户名" HeaderText="用户名"
                                Target="_blank" DataTextField="用户名" DataNavigateUrlFormatString="frmUserEdit.aspx?id={0}" />

                            <asp:HyperLinkField DataNavigateUrlFields="F_StaffID" DataNavigateUrlFormatString="frmStaffDetail.aspx?stfID={0}"
                                HeaderText="档案详情" Target="_blank" Text="编辑" />
                        </Columns>
                        <CascadeCheckboxes>
                            <yyc:CascadeCheckbox ChildCheckboxID="chkSelect" ParentCheckboxID="allCheck" />
                        </CascadeCheckboxes>
                        <PagerStyle CssClass="pager" />
                    </yyc:SmartGridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAuthor" />
                <asp:AsyncPostBackTrigger ControlID="btnUnAuthor" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                <asp:AsyncPostBackTrigger ControlID="gvStaffsInDept" EventName="PageIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:ObjectDataSource ID="odsStaffInDept" runat="server" SelectMethod="GetStaffsInDept"
        TypeName="EducationV2.GridDataSource"></asp:ObjectDataSource>
    </form>
</body>
</html>
