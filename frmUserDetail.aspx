<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUserDetail.aspx.cs" Inherits="EducationV2.frmUser" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    
    <script type="text/javascript" src="Scripts/JQuery-1.10.js" charset="UTF-8"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.10.3.custom.js"></script>
    <script src="Scripts/common.js" type="text/javascript"> </script>
    <script src="Scripts/expert.js" type="text/javascript"> </script>

    <link href="css/ui-lightness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/pub.css" type="text/css" />
    <link href="css/model.css" rel="stylesheet" type="text/css" />
    <link href="css/sortabletable.css" rel="stylesheet" type="text/css" />
    <link href="css/zjps_button.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/Style.css" />
    <style type="text/css">
        #tabs
        {
            background: #fff;
        }
        #tabs ul li a
        {
            padding: 6px 7px;
            color: #000;
            font-weight: normal;
        }
        .ui-state-active
        {
            color: #eb8f00;
        }

        .hidetd
        {
            visibility: hidden;
        }
            
        
         .field td
        {
            padding-left: 8px;
            width: 11%;
        }             
     
        .headline td         
        {
            font-weight: bold;
        }
    </style>


</head>
<body>
    <div id="tabs">
        <ul>
            <li id="page1"><a href="frmUserDetail1.aspx">基本信息</a></li>
            <li id="page2"><a href="frmUserDetail2.aspx">家庭信息</a></li>
            <li id="page3"><a href="frmUserDetail3.aspx">个人简历</a></li>
        </ul>
    </div>
</body>
</html>
