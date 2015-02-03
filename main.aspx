<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="EducationV2.main" %>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>福建工程学院党委组织部管理系统</title>
    <link rel="stylesheet" type="text/css" href="easyui/themes/gray/easyui.css" /> 
    <link rel="stylesheet" type="text/css" href="easyui/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="css/main.css" />
        <link rel="Stylesheet" href="resource/css/ext-all.css" />
    <link rel="stylesheet" type="text/css" href="css/Style.css" />
    <script type="text/javascript" src="Scripts/bootstrap.js"></script>
    <script type="text/javascript" src="Scripts/ext-lang-zh_CN.js"></script>
     <script type="text/javascript" src="Scripts/JQuery-1.10.js" charset="UTF-8"></script>
    <script type="text/javascript" src="easyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="easyui/locale/easyui-lang-zh_CN.js"></script>
    <style type="text/css">
        body
        {
            margin-top: 0px;
            margin-left: 0px;
            margin-bottom: 0px;
            margin-right: 0px;
        }
        
        ul li
        {
            margin-top: 15;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
        /*
        * view(url) 在layout中打开页面
        */
        function view(url) {
            $('#iframe').attr('src', url);
        }

        /*
        *添加选项卡方法
        */
        function addTab(title, url) {
            //先判断是否存在标题为title的选项卡
            var tab = $('#tt').tabs('exists', title);
            if (tab) {
                //若存在，则直接打开
                $('#tt').tabs('select', title);
            } else {
                //否则创建
                $('#tt').tabs('add', {
                    title: title,
                    content: "<iframe width='100%' height='100%'  id='iframe' frameborder='0' scrolling='auto'  src='" + url + "'></iframe>",
                    closable: true
                });
            }

        }

        /*
        *根据title,选中Accordion对应的面板
        */
        function selectAccordion(title) {
            $('#accordionPanel').accordion('select', title);
        }

        Ext.onReady(function () {
            var task = { run: function () { Ext.get("timeInfo").dom.innerHTML = (zjd = new Date()).getFullYear() + "年" + (zjd.getMonth() + 1) + "月" + zjd.getDate() + "日 星期" + "日一二三四五六".charAt(zjd.getDay()) + " " + zjd.getHours() + ":" + zjd.getMinutes() + ":" + zjd.getSeconds(); },
                interval: 1000
            };
            Ext.TaskManager.start(task);

        });
        /*
        *刷新时间
        */


        /*
        *检测浏览器窗口大小改变,来改变页面layout大小
        */
        $(function () {

            $(window).resize(function () {
                $('#cc').layout('resize');
            });

            //加载菜单Services/Expert.ashx?method=loadMenu


        });
	
	
	
    </script>
</head>
<body style="border: none; visibility: visible; width: 100%; height: 100%;" >
    <form id="form1" runat="server">

    <div id="cc" class="easyui-layout" style="width: 100%; height: 100%;">
        <!-- 页面顶部top及菜单栏 -->
        <div region="north" style="height: 89%; width: 100%; overflow: hidden;">
            <div class="header" style="height: 88px">
                <div style="text-align: right; padding-right: 20px; padding-top: 38px; color: #fff">
                    &nbsp;&nbsp;<img height="12" src="images/person.png" width="12" style="vertical-align: middle;" />&nbsp;<asp:Label
                        ID="labRole" runat="server"></asp:Label>
                    :<asp:Label ID="labUserName" runat="server"></asp:Label>
                    ，欢迎您使用本系统 <span style="color: #fff" id="timeInfo" name="timeInfo"></span><a href="/loginout.aspx"
                        style="color: #fff; text-decoration: none;">注销</a>
                </div>
            </div>
            
        </div>
        <!-- 页面底部信息 -->
        <!-- 左侧导航菜单 -->
        <div region="west" title="导航菜单栏" style="width: 180px;">
            <div class="easyui-accordion" fit="true" style="text-align: center;" id="accordionPanel">
                <asp:Literal runat="server" ID="litMenu"></asp:Literal>
            </div>
        </div>
       
        <!-- 主显示区域选项卡界面 title="主显示区域"-->
        <div region="center">
            <div class="easyui-tabs" fit="true" id="tt">
                <div title="主页">
                    <iframe width='100%' height='100%' id='iframe' frameborder='0' scrolling='auto' src='default.html'>
                    </iframe>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
