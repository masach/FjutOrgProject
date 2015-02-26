<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPositionEdit.aspx.cs"
    Inherits="EducationV2.frmAddProject" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>岗位信息编辑</title>
    <link rel="stylesheet" type="text/css" href="css/Style.css" />
    <link rel="stylesheet" type="text/css" href="css/page.css" />
    <link href="css/ui-lightness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/JQuery-1.10.js" charset="UTF-8"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.10.3.custom.js"></script>
    <script src="Scripts/common.js" type="text/javascript"> </script>
    <link rel="stylesheet" href="css/validationEngine.jquery.css" />
    <script src="js/jquery.validationEngine-zh_CN.js" type="text/javascript"></script>
    <script src="js/jquery.validationEngine.js" type="text/javascript"></script>

    <script type="text/javascript">
        function windowclose() {
            window.opener = null;
            window.close();
        }
        function validate() {
        }

        function formSuccess() {
            return true;
        }

        $(function () {
            datepicker();
            $("#form1").validationEngine({
                onSuccess: formSuccess
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" style="text-align: center">
    <div class="systeminfo">
        <div class="systitleline add">
            岗位信息编辑</div>
        <div class="maintable">
            <table id="table1">
             
                <tr>
                    <td width="120">
                        岗位名称
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" CssClass="validate[required]"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        申报开始日期
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="datebox validate[required]"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        申报结束日期
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="datebox validate[required]"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        状态
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem>待审核</asp:ListItem>
                            <asp:ListItem Selected="True">审核通过</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="120">
                        岗位说明
                    </td>
                    <td>
                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox>
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
    </div>
    </form>
</body>
</html>
