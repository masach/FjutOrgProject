<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EducationV2.Default" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<head id="Head1" runat="server">
		<title>欢迎使用福建工程学院党委组织部管理系统</title>
        <link rel="stylesheet" href="css/validationEngine.jquery.css" />
           <script type="text/javascript" src="Scripts/JQuery-1.10.js" charset="UTF-8"></script>
        <script src="js/jquery.validationEngine-zh_CN.js" type="text/javascript"></script> 
        <script src="js/jquery.validationEngine.js" type="text/javascript"></script> 
        <script type="text/javascript">
            $(document).ready(function () {
                $("#form1").validationEngine();
            });
        </script>

	
        
</head>
     <body >
         <form id="form1" runat="server" clientidmode="Static">
     <center>
     <table style="width:1000px; text-align:center">
     <tr>
     <td align="left">
      <img src="images/logo.jpg" alt="logo"
                        style="margin: 0px; padding: 0px; border-width: 0px;" />
      <img src="images/logo2.jpg" alt="log"
                        style="margin: 0px; padding: 0px; border-width: 0px;" />
     </td>
     </tr>
     <tr>
     <td>
       <div class="login_left" 
                style="margin: 0px; padding: 0px; float: left; height: 340px; width: 426px; background-image: url(images/login_left.jpg); background-color: rgb(255, 255, 255); color: rgb(17, 68, 158); font-family: Verdana, Tahoma, Arial, Helvetica, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: center; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-position: initial initial; background-repeat: no-repeat no-repeat;">
                <img class="login_pic" src="images/login_pic.png" alt=""
                    style="margin: 29px 0px 0px; padding: 0px; border-width: 0px;" /></div>
            <div class="login_right" 
                style="margin: 0px; text-align:left; padding: 0px; float: left; height: 340px; width: 462px; background-image: url(images/login_right.jpg); background-color: rgb(255, 255, 255); text-align: left; color: rgb(17, 68, 158); font-family: Verdana, Tahoma, Arial, Helvetica, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-position: initial initial; background-repeat: no-repeat no-repeat;">
                <dl style="margin: 92px 0px 0px 105px; padding: 0px; width: 285px; min-height: 20px;">
                    <dt class="uesr" 
                        style="margin: 7px 0px; padding: 0px; float: left; height: 20px; line-height: 20px;  ">
                        <label id="lbYhm" style="margin: 0px; padding: 0px;">
                        角　色：</label></dt>
                    <dd style="margin: 7px 0px; padding: 0px; float: left; height: 20px; line-height: 20px;">
                    <asp:DropDownList ID="ddlType" runat="server" Width="130px">
                     <asp:ListItem>申请人员</asp:ListItem>
                     <asp:ListItem>系统管理员</asp:ListItem>
                     <asp:ListItem>组织部部长</asp:ListItem>
                          </asp:DropDownList>
                    </dd>
                </dl>
                <div style="margin: 0px; padding: 0px; clear: both;">
                </div>
                <dl style="margin: 0px 0px 0px 105px; padding: 0px; width: 285px; min-height: 20px;">
                    <dt class="passw" 
                        style="margin: 7px 0px; padding: 0px; float: left; height: 20px; line-height: 20px; background-repeat: no-repeat no-repeat;">
                        <label id="lbYhm0" style="margin: 0px; padding: 0px;">
                        用户名</label><label id="lbMm" style="margin: 0px; padding: 0px;">：</label></dt>
                    <dd style="margin: 7px 0px; padding: 0px; float: left; height: 20px; line-height: 20px;">
                        <asp:TextBox ID="txtUserName" runat="server" ClientIDMode="Static" CssClass="validate[required]"></asp:TextBox>
                    </dd>
                </dl>
               <div style="margin: 0px; padding: 0px; clear: both;">
                </div>
                <dl style="margin: 0px 0px 0px 105px; padding: 0px; width: 285px; min-height: 20px;">
                    <dt class="passw" 
                        style="margin: 7px 0px; padding: 0px; float: left; height: 20px; line-height: 20px; background-repeat: no-repeat no-repeat;">
                        <label id="Label1" style="margin: 0px; padding: 0px;">
                        密　码：</label></dt>
                    <dd style="margin: 7px 0px; padding: 0px; float: left; height: 20px; line-height: 20px;">
                        <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" CssClass="validate[required]"
                            ClientIDMode="Static"></asp:TextBox>
                    </dd>
                </dl>
   
                <div style="margin: 0px; padding: 0px; clear: both;">
                </div>
              
                <div style="margin: 0px; padding: 0px; clear: both;">
                </div>
                <dl style="margin: 0px 0px 0px 105px; padding: 0px; width: 285px; min-height: 20px;">
                    <dd style="margin: 7px 0px; padding: 0px; float: left; height: 20px; line-height: 20px;">
                        
                        <asp:ImageButton ID="btnCommit" runat="server" ImageUrl="~/images/login_in.gif" 
                            onclick="btnCommit_Click" />
                        <span class="Apple-converted-space">&nbsp;</span> 
                        <asp:ImageButton ID="ImageButton1" runat="server" 
                            ImageUrl="~/images/login_res.gif" />
                            <br style="margin: 0px; padding: 0px;" />
                      
                    </dd>
                </dl>
            </div>
         </td>
     </tr>
     <tr>
     
     </tr>

          
     <tr>

     <td >
       &copy;2015-2015
                <a  href="http://zzb.fjut.edu.cn" target="_blank">福建工程学院党委组织部</a>版权所有&nbsp;&nbsp;
                    <br />联系电话：
     </td>
     </tr>



     </table>

     </center>
         </form>
      </body>          
</html>

