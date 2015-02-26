<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPosApplicantList.aspx.cs"
    Inherits="EducationV2.frmPosApplicantList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="css/Style.css" />
    <link rel="stylesheet" type="text/css" href="css/page.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="systeminfo">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="systitleline">
                    <input type="button" value="刷 新" class="btn" onclick="window.location.reload();" />&nbsp;
                    <input type="button" id="btnAdd" value="新 增" class="btn" onclick="btnAdd_onclick()" />&nbsp;
                    <asp:Button ID="btnDel" runat="server" Text="删 除" CssClass="btn" OnClientClick="return confirm('您确认删除该记录吗?');"
                        OnClick="btnDel_Click" />
                    &nbsp;<%--<asp:Button ID="btnFilter" runat="server" CssClass="btn80" Text="仅显示有效记录"
                        OnClick="btnFilter_Click" />
                    &nbsp;
                    <asp:Button ID="btnAllRecord" runat="server" OnClick="btnAllRecord_Click" Text="显示所有记录"
                        CssClass="btn80" />--%>&nbsp;
                    <asp:DropDownList ID="ddlField" runat="server">
                        <asp:ListItem Value="F_realName">姓名</asp:ListItem>
                        <asp:ListItem Value="F_workDept">部门</asp:ListItem>
                        <asp:ListItem Value="F_position">现任职务</asp:ListItem>
                        <asp:ListItem Value="F_title">职称</asp:ListItem>
                        <asp:ListItem Value="F_appliedDate">申请日期</asp:ListItem>
                        <asp:ListItem Value="F_status">状态</asp:ListItem>
                        <asp:ListItem Value="F_pos1">第一志愿</asp:ListItem>
                        <asp:ListItem Value="F_pos2">第二志愿</asp:ListItem>
                        <asp:ListItem Value="F_pos3">第三志愿</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    <asp:DropDownList ID="ddlType" runat="server">
                    </asp:DropDownList>
                    &nbsp;<asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>&nbsp;<asp:Button
                        ID="btnSearch" runat="server" Text="检 索" OnClick="btnSearch_Click" CssClass="btn" />
                   
                    &nbsp;<asp:Button ID="btnGenList" runat="server" Text="生成详细汇总表" Visible="false" CssClass="btn80"
                        OnClick="btnGenList_Click" />         
                    &nbsp;<asp:Button ID="btnGenCmprList" runat="server" Text="生成简要汇总表" Visible="false" CssClass="btn80"
                        OnClick="btnGenSummary_Click" />                    
                </div>
                <div class="maintable">
                    <yyc:SmartGridView Width="100%" ID="GridView1" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" DataKeyNames="F_ID" DataSourceID="LinqDataSource1"
                        OnRowDataBound="GridView1_RowDataBound" CellPadding="4" GridLines="None" OnPageIndexChanging="GridView1_PageIndexChanging"
                        OnDataBound="GridView1_DataBound" OnRowCommand="GridView1_RowCommand" ShowHeaderWhenEmpty="True">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="50px">
                                <HeaderTemplate>
                                    序号
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="35px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="20px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="allCheck" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="20px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="F_ID" HeaderText="F_ID" ReadOnly="True" SortExpression="F_ID"
                                Visible="False" />
                            <asp:BoundField DataField="F_UserID" HeaderText="F_UserID" ReadOnly="True" SortExpression="F_UserID"
                                Visible="False" />
                            <asp:TemplateField HeaderText="名称" SortExpression="F_realName">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("F_realName") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# "<a target=\"_blank\"  href=frmPosApplicant.aspx?aplID=" + Eval("F_ID") + ">"  + Eval("F_realName") + "</a>" %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="F_workDept" HeaderText="工作部门" ReadOnly="True" SortExpression="F_workDept">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_position" HeaderText="现任职务" ReadOnly="True" SortExpression="F_position">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_pos1" HeaderText="第一志愿" ReadOnly="True" SortExpression="F_Pos1">
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_pos2" HeaderText="第二志愿" ReadOnly="True" SortExpression="F_Pos2">
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_pos3" HeaderText="第三志愿" ReadOnly="True" SortExpression="F_Pos3">
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_appliedDate" HeaderText="申请时间" ReadOnly="True" SortExpression="F_appliedDate"
                                DataFormatString="{0:d}">
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="F_status" HeaderText="状态" ReadOnly="True" SortExpression="F_status">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:LinkButton ID="exportWord" runat="server" Text="导出Excel" OnClick="Label1_Click"
                                        CommandArgument='<%# Eval("F_ID") %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <CascadeCheckboxes>
                            <yyc:CascadeCheckbox ChildCheckboxID="chkSelect" ParentCheckboxID="allCheck" />
                        </CascadeCheckboxes>
                        <PagerStyle CssClass="pager" />
                    </yyc:SmartGridView>
                    <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="EducationV2.DataClassesDataContext"
                        EntityTypeName="" OrderBy="F_applicantDate desc" Select="new (F_ID, F_status, F_leaderID, F_type, F_belongeddomain, F_belongedSubject, F_name, F_leader, F_dept, F_applicantDate)"
                        TableName="ScienceProject">
                    </asp:LinqDataSource>
                </div>
            </ContentTemplate>
            <Triggers>
            
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                <asp:AsyncPostBackTrigger ControlID="btnDel" />
                <asp:PostBackTrigger ControlID="GridView1" />
                <asp:PostBackTrigger ControlID="btnGenList" />
                <asp:PostBackTrigger ControlID="btnGenCmprList" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
    <script type="text/javascript" src="Scripts/JQuery-1.10.js" charset="UTF-8"></script>
    <script src="Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function btnAdd_onclick() {
            window.open('frmPosApplicant.aspx', 'newwindow', 'height=550,width=600,top=200,left=400,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no');
            //window.location.href = 'frmPosApplicant.aspx'; // input如果是submit类型，则该语句无效 

        }
    </script>
</body>
</html>
