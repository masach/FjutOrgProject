<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmDeptManage.aspx.cs"
    Inherits="EducationV2.frmDeptManag" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
  <link rel="stylesheet" type="text/css" href="css/Style.css" />
    <script src="Scripts/common.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="css/page.css" />
    <script type="text/javascript">
        function btnAdd_onclick() {
            //            window.open('frmDeptEdit.aspx', 'newwindow',
            //            'height=150,width=400,top=200,left=400,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no');
            self.location.href = 'frmDeptEdit.aspx'; // input如果是submit类型，则该语句无效         
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="systeminfo">

     <div class="systitleline">
                    <input type="button" value="刷 新" class="btn" onclick="refresh()" />&nbsp;
                    <input type="button" id="btnAdd" value="添 加" onclick="btnAdd_onclick()" class="btn" />&nbsp;
                    <asp:Button ID="btnDel" runat="server" Text="删 除" CssClass="btn" OnClick="btnDel_Click" />
                    <ajaxToolkit:ConfirmButtonExtender ID="btnDel_ConfirmButtonExtender" runat="server"
                        ConfirmText="将会删除属于选中部门的所有用户，确认删除？" Enabled="True" TargetControlID="btnDel">
                    </ajaxToolkit:ConfirmButtonExtender>
                    &nbsp;
                    <asp:Button ID="btnAuthor" runat="server" Text="通 过" class="btn" OnClick="btnAuthor_Click" />
                    &nbsp;
                    <asp:Button ID="btnUnAuthor" runat="server" Text="不通过" OnClick="btnUnAuthor_Click"
                        CssClass="btn" />
                </div>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="maintable">
                    <yyc:SmartGridView ID="gvDept" runat='server' ShowHeaderWhenEmpty="True" Width="100%"
                        DataKeyNames="F_ID" AllowPaging="True" AutoGenerateColumns="False"
                        OnPageIndexChanging="gvDept_PageIndexChanging">                        
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
                            <asp:BoundField DataField="F_ID" HeaderText="F_ID" ReadOnly="True" SortExpression="F_ID"
                                Visible="False" />
                            <asp:HyperLinkField DataNavigateUrlFields="F_ID" DataNavigateUrlFormatString="frmDeptEdit.aspx?id={0}"
                                DataTextField="部门名" HeaderText="部门名" Target="_self" />
                            <asp:BoundField DataField="机构类别" HeaderText="机构类别" SortExpression="机构类别" />
                            <asp:BoundField DataField="部门用户数" HeaderText="部门用户数" SortExpression="部门用户数" />
                            <asp:BoundField DataField="状态" HeaderText="状态" ReadOnly="True" SortExpression="状态" />
                        </Columns>
                        <CascadeCheckboxes>
                            <yyc:CascadeCheckbox ChildCheckboxID="chkSelect" ParentCheckboxID="allCheck" />
                        </CascadeCheckboxes>
                        <PagerStyle CssClass="pager" />
                    </yyc:SmartGridView>
                </div>
            </ContentTemplate>
            <Triggers>
                
                <asp:PostBackTrigger ControlID="gvDept" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
