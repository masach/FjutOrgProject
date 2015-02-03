<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUserDetail3.aspx.cs"
    Inherits="EducationV2.frmUserDetail3" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Style.css" />

</head>
<body>
    <div class="systeminfo">
        <div class="systitleline">
            <input id="Button1" type="button" value="保 存" onclick="savePage(3)" style="visibility: hidden"
                class="btn" />
        </div>
        <div class="maintable">
            <form id="frmPage3" method="post" action="/#">
            <table id="table5" align="center" border="0" cellpadding="0" cellspacing="0" width="100%"
                style="background-color: White">
               <tr class="headline">
                    <td>
                        个人简历
                    </td>
                </tr>               
                <tr>
                    <td>
                        <textarea cols="110" name="F_resume" rows="30" id="F_resume" style="width: 95%; height: 150px"></textarea>
                    </td>
                </tr>
                <tr class="headline">
                    <td>
                        奖惩情况
                    </td>
                </tr>        
                <tr>
                    <td>
                        <textarea cols="110" name="F_rwdandpunishmt" rows="30" id="F_rwdandpunishmt" style="width: 95%; height: 150px"></textarea>
                    </td>
                </tr>
          
            </table>
            </form>
        </div>
    </div>
    <script type="text/javascript">

            $(function () {
                initialPage(3);
                getStatus();
            });
    </script>
</body>
</html>
