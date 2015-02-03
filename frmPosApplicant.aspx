<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPosApplicant.aspx.cs" Inherits="EducationV2.frmAcceptApplicant" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
  
    <script type="text/javascript" src="Scripts/JQuery-1.10.js" charset="UTF-8"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-1.10.3.custom.js"></script>      
    <script src="js/jquery.validationEngine-zh_CN.js" type="text/javascript"></script>
    <script src="js/jquery.validationEngine.js" type="text/javascript"></script> 
    <script type="text/javascript" src="Scripts/bootstrap.js"></script>
    <script type="text/javascript" src="Scripts/ext-lang-zh_CN.js"></script>

    <script src="Scripts/common.js" type="text/javascript"> </script>
    <script src="Scripts/posApplicant.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            var Request = new QueryString();
            var Num = Request["tabNum"];

            if (Num != null) {
                var tabs = $("#tabs").tabs();
                tabs.tabs('option', 'active', Num);
            }
        });
    </script>

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
<body onload="fullscreen()">
 
  
    <form id="exec" action="">
    <div id="tabs">
        <ul>
            <li id="page1"><a href="frmPosApplicantP1.aspx" >个人资料</a></li>
            <li id="page2"><a href="frmPosApplicantP2.aspx" >申报信息</a> </li>
        </ul>

    </div>
    </form>


</body>
</html>
