<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmModifyPWD.aspx.cs" Inherits="EducationV2.frmModifyPWD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="title text-info" style="color: rgb(125, 125, 125); font-family: verdana, Arial, Helvetica, sans-serif;
            list-style-type: none; margin: 0px; padding: 9px 0px 0px; z-index: inherit; clear: both;
            font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal;
            letter-spacing: normal; line-height: normal; orphans: auto; text-align: left;
            text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px;
            -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">
            <h5 style="font-size: 16px; margin: 0px 15px 0px 0px; padding: 0px; border: 0px;
                clear: both; font-weight: bold; display: inline;">
                &nbsp;</h5>
        </div>
        <div class="line-du" style="color: rgb(0, 0, 0); font-family: verdana, Arial, Helvetica, sans-serif;
            list-style-type: none; margin: 0px; padding: 0px; z-index: inherit; border-top-width: 2px;
            border-top-style: double; border-top-color: rgb(220, 220, 220); font-size: 12px;
            font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal;
            line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none;
            white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px;
            background-color: rgb(255, 255, 255);">
        </div>
        <div  style="color: rgb(0, 0, 0); ">
            <table class="pass-wrap" style="font-size: 12px; margin: 50px 0px 0px; padding: 0px;
                border: 0px; border-collapse: collapse; border-spacing: 0px; width: 992px;">
                <tr>
                    <th style="margin: 0px; padding: 5px 6px 4px; color: rgb(125, 125, 125); text-align: right;">
                        <font color="#D90000">*</font>您的旧密码：
                    </th>
                    <td >
                        <asp:TextBox ID="txtOrgiPWD" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入原始密码"
                            ControlToValidate="txtOrgiPWD"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th style="margin: 0px; padding: 5px 6px 4px; color: rgb(125, 125, 125); text-align: right;">
                        <font color="#D90000">*</font>您的新密码：
                    </th>
                    <td  >
                        <div style="color: inherit; ">
                            <asp:TextBox ID="txtNewPWD1" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入新密码"
                                ControlToValidate="txtNewPWD1"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th style="margin: 0px; padding: 5px 6px 4px; border-width: 0px 0px 1px; border-style: solid;
                        border-color: rgb(220, 220, 220); line-height: 19.200000762939453px; overflow: hidden;
                        vertical-align: top; color: rgb(125, 125, 125); text-align: right;">
                        <font color="#D90000">*</font>新密码确认：
                    </th>
                    <td >
                        <asp:TextBox ID="txtNewPWD2" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="两次输入的密码不匹配"
                            ControlToCompare="txtNewPWD2" ControlToValidate="txtNewPWD1"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td style="margin: 0px; padding: 0px; border: 0px;">
                    </td>
                    <td style="margin: 0px; padding: 0px; border: 0px;">
                        <div id="ext-gen147" class="buts" style="color: inherit; font-family: verdana, Arial, Helvetica, sans-serif;
                            list-style-type: none; margin: 20px 0px 0px; padding: 0px; z-index: inherit;">
                            <asp:Button ID="btnCommit" runat="server" Text="确 认" OnClick="btnCommit_Click" CssClass="btn" />
                            &nbsp;</div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
