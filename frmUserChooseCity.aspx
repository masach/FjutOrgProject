<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUserChooseCity.aspx.cs" Inherits="EducationV2.frmUserChooseCity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>选择籍贯</title>   
    <link rel="stylesheet" type="text/css" href="css/Style.css" />  
</head>
<body id="Body1">
    <form id="form1" runat="server">
    <div style="padding-left: 30px">
        <div class="systitleline">
            <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="btnConfirm_Click" CssClass="btn" />
        </div>
        <div class="maintable">
            &nbsp;<yyc:SmartTreeView ID="SmartTreeView1" 
                runat="server"   ShowCheckBoxes="Leaf"
                ShowLines="False" ExpandDepth="0" 
                onselectednodechanged="SmartTreeView1_SelectedNodeChanged">
            </yyc:SmartTreeView>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>        
        </div>
        
    </div>
    </form>
     <script type="text/javascript" src="Scripts/JQuery-1.10.js" charset="UTF-8"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("input[type='checkbox']").unbind().bind('click', function () {
                selectSingle(this);
            });
        });
        function selectSingle(checkbox) {
            $("input[type='checkbox']").attr("checked", false);
            checkbox.checked = true;
        }
    </script>
</body>
</html>
