<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPositionManage.aspx.cs"
    Inherits="EducationV2.frmPositionManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/ui-lightness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="css/Style.css" />
    <link rel="stylesheet" type="text/css" href="css/page.css" />  
    <style type="text/css">
        #leftDiv
        {
            float: left;
            width: 70%;
        }
        #rightDiv
        {
            float: right;
            width: 30%;     
            text-align:right;       
        }
        .style1
        {
            font-weight: normal;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div class="systeminfo">
        <div class="systitleline">
            <div id="leftDiv">
                <input type="button" value="刷 新" class="btn" onclick="refresh()" />&nbsp;
                <input type="button" value="新 增" class="btn" onclick="btnAdd_onclick()" />&nbsp;
                &nbsp;<asp:Button ID="btnDel" runat="server" Text="删 除" OnClick="btnDel_Click" ClientIDMode="Static"
                    CssClass="btn" />
                <ajaxToolkit:ConfirmButtonExtender ID="btnDel_ConfirmButtonExtender" runat="server"
                    ConfirmText="是否删除所选中的记录？" Enabled="True" TargetControlID="btnDel">
                </ajaxToolkit:ConfirmButtonExtender>       
            &nbsp;<asp:Button ID="btnAuthor" runat="server" Text="通 过" class="btn" OnClick="btnAuthor_Click" />
            &nbsp;<asp:Button ID="btnUnAuthor" runat="server" Text="不通过" OnClick="btnUnAuthor_Click"
                CssClass="btn" />
              
                &nbsp; &nbsp;
                <asp:DropDownList ID="ddlField" runat="server">
                    <asp:ListItem Value="F_posname">岗位名称</asp:ListItem>
                    <asp:ListItem Value="F_comment">岗位说明</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlType" runat="server">
                </asp:DropDownList>
                &nbsp;<asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>&nbsp;<asp:Button
                    ID="btnSearch" runat="server" Text="检 索" OnClick="btnSearch_Click" CssClass="btn" />
            </div>
            <div id="rightDiv">
                <span class="style1">申请截止时间：</span><asp:TextBox ID="txtEndDate" runat="server" CssClass="datebox"></asp:TextBox>
                &nbsp;<asp:Button ID="btnSetting" runat="server" Text="批量设置" 
                    CssClass="btn" onclick="btnSetting_Click"/>
            </div>
        </div>
        <div class="maintable">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <yyc:SmartGridView ID="SmartGridView1" runat='server' Width="100%" AllowPaging="True"
                        AutoGenerateColumns="False" DataKeyNames="F_ID" DataSourceID="LinqDataSource1"
                        CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
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
                            <asp:BoundField DataField="F_ID" HeaderText="F_ID" SortExpression="F_ID" ReadOnly="True"
                                Visible="False" />
                            <asp:HyperLinkField DataNavigateUrlFields="F_ID" DataNavigateUrlFormatString="frmPositionEdit.aspx?id={0}"
                                DataTextField="F_posname" HeaderText="岗位名称" Target="_self" />
                            <asp:BoundField DataField="F_startDate" HeaderText="申请开始时间" DataFormatString="{0:d}"
                                SortExpression="F_startDate"></asp:BoundField>
                            <asp:BoundField DataField="F_endDate" HeaderText="申请截止时间" DataFormatString="{0:d}"
                                SortExpression="F_endDate"></asp:BoundField>
                            <asp:BoundField DataField="F_comment" HeaderText="说明" SortExpression="F_comment">
                            </asp:BoundField>
                            <asp:BoundField DataField="F_status" HeaderText="状态" SortExpression="F_status" />
                        </Columns>
                        <CascadeCheckboxes>
                            <yyc:CascadeCheckbox ChildCheckboxID="chkSelect" ParentCheckboxID="allCheck" />
                        </CascadeCheckboxes>
                        <FooterStyle BackColor="#FEEBC9" Font-Bold="True" ForeColor="black" />
                        <HeaderStyle BackColor="#FEEBC9" Font-Bold="True" ForeColor="black" />
                        <PagerStyle CssClass="pager" />
                    </yyc:SmartGridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnDel" />
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    <asp:PostBackTrigger ControlID="SmartGridView1" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="EducationV2.DataClassesDataContext"
            EntityTypeName="" OrderBy="F_endDate desc" TableName="Position">
        </asp:LinqDataSource>
    </div>
     
    <script src="Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts/JQuery-1.10.js" charset="UTF-8"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.10.3.custom.js"></script>
    <script type="text/javascript">
        function btnAdd_onclick() {
            //window.open('frmPositionEdit.aspx', 'newwindow', 'height=450,width=400,top=200,left=400,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no');          
            self.location.href = 'frmPositionEdit.aspx'; // input如果是submit类型，则该语句无效         
        }
        datepicker();   

        $(".AlternatingRow").removeClass();
        $(".Row").removeClass();
    </script>
    </form>
</body>
</html>
