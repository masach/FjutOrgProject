<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUserDetail2.aspx.cs"
    Inherits="EducationV2.frmUserDetail2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>家庭成员及社会关系</title>
</head>
<body>
    <form id="frmPage2" method="post" action="/#">
    <div class="systeminfo">
        <div class="systitleline">
            <input id="Button1" type="button" value="保 存" onclick="savePage(2)" style="visibility: hidden"
                class="btn" />
        </div>
        <div class="maintable">
            <div style="font-weight:bold;">
                家庭主要成员及社会关系
            </div>
            <table id="table_jtcy" class="hold-table" width="100%">
                <tr>
                    <!--
                    <td class="hidetd">
                        PID
                    </td>
                    -->
                    <td>
                        称谓
                    </td>
                    <td>
                        姓名
                    </td>
                    <td>
                        出生年月
                    </td>
                    <td>
                        政治面貌
                    </td>
                    <td style="width: 30%;">
                        工作单位及职务
                    </td>
                    <td width="15%" style="padding-left: 8px;">
                        <a href="javascript:add_row('table_jtcy','table_jtcy_row')">增加</a>
                    </td>
                </tr>
                <tr id="table_jtcy_row">
                    <!--
                    <td class="hidetd">
                        <input type="text" name="F_familyMemID" id="F_familyMemID0" value="" style="width: 85%;"
                            class="t_tabletxt" />
                    </td>
                    -->
                    <td>
                        <input type="text" name="F_familyMemAppelation" id="F_familyMemAppelation0" value=""
                            style="width: 85%;" class="t_tabletxt" />
                    </td>
                    <td>
                        <input type="text" name="F_familyMemName" id="F_familyMemName0" value="" style="width: 85%;"
                            class="t_tabletxt" />
                    </td>
                    <td>
                        <input type="text" name="F_familyMemBirthday" id="F_familyMemBirthday0" value=""
                            style="width: 85%;" class="datebox" />
                    </td>
                    <td>
                        <input type="text" name="F_familyMemParty" id="F_familyMemParty0" value="" style="width: 85%;"
                            class="t_tabletxt" />
                    </td>
                    <td>
                        <input type="text" name="F_familyMemWorkInfo" id="F_familyMemWorkInfo0" value=""
                            style="width: 85%;" class="t_tabletxt" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
    <script type="text/javascript">

        $(function () {
            initialPage(2);
            getStatus();
        });
    </script>
</body>
</html>
