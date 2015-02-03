<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPosApplicantP1.aspx.cs"
    Inherits="EducationV2.frmPosApplicant1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>岗位申请表-基本信息</title>
</head>
<body>
    <div class="systeminfo">
        <div class="systitleline">
            <asp:Literal ID="litPanel" runat="server"></asp:Literal>
        </div>
        <div class="maintable">
            <form id="frmPage1" runat="server">
            <table style="width: 100%">
                <tr class="headline">
                    <td colspan="6">
                        基本信息
                    </td>
                </tr>
                <tr>
                    <td class="field">
                        姓名
                    </td>
                    <td width="13%">
                        <input type="text" name="F_realName" id="F_realName" value="" style="width: 85%;"
                            class="t_tabletxt" />
                    </td>
                    <td class="field">
                        性别
                    </td>
                    <td width="13%">
                        <select name="F_sexual" id="F_sexual" style="width: 85%;" class="t_tabletxt">
                            <option value="男">男</option>
                            <option value="女">女</option>
                        </select>
                    </td>
                    <td class="field">
                        出生日期&nbsp;
                    </td>
                    <td width="13%">
                        <input class="datebox" type="text" name="F_birthday" id="F_birthday" style="width: 85%;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        籍贯
                    </td>
                    <td>
                        <input id="F_nativeplace" name="F_nativeplace" type="text" style="width: 85%" readonly="readonly"
                            onclick="chooseCity()" class="t_tabletxt" />
                    </td>
                    <td>
                        民族
                    </td>
                    <td>
                        <asp:DropDownList ID="F_nationality" runat="server" ClientIDMode="Static" DataSourceID="NationalityDataSource"
                            DataTextField="value" DataValueField="value" CssClass="t_tabletxt" Style="width: 85%">
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                    <td>
                        政治面貌
                    </td>
                    <td>
                        <input id="F_party" name="F_party" type="text" style="width: 85%" class="t_tabletxt" />
                    </td>
                </tr>
                <tr>
                    <td>
                        最高学位
                    </td>
                    <td>
                        <select name="F_highestDegree" id="F_highestDegree" style="width: 85%;" class="t_tabletxt">
                            <option value="博士">博士</option>
                            <option value="硕士">硕士</option>
                            <option value="学士">学士</option>
                            <option value="其他" selected="selected">其他</option>
                        </select>
                    </td>
                    <td>
                        最高学历
                    </td>
                    <td>
                        <select name="F_highestEducation" id="F_highestEducation" style="width: 85%;" class="t_tabletxt">
                            <option value="其它">其它</option>
                            <option value="中专/职校">中专/职校</option>
                            <option value="大专">大专</option>
                            <option value="本科">本科</option>
                            <option value="硕士">硕士</option>
                            <option value="博士">博士</option>
                        </select>
                    </td>
                    <td>
                        毕业时间、院校及专业
                    </td>
                    <td>
                        <input id="F_highestGrduateSch" name="F_highestGrduateSch" type="text" style="width: 85%"
                            class="t_tabletxt" />
                    </td>
                </tr>
                <tr>
                    <td>
                        现工作部门
                    </td>
                    <td>
                        <input id="F_belongDeptID" name="F_belongDeptID" type="hidden" />
                        <asp:DropDownList ID="F_workDept" name="F_workDept" runat="server" DataSourceID="DeptDataSource"
                            DataTextField="F_name" DataValueField="F_name" ClientIDMode="Static" CssClass="t_tabletxt"
                            Style="width: 85%">
                        </asp:DropDownList>
                    </td>
                    <td>
                        现工作职务
                    </td>
                    <td>
                        <input type="text" name="F_position" id="F_position" value="" style="width: 85%;"
                            class="t_tabletxt" />
                    </td>
                    <td>
                        任现职时间
                    </td>
                    <td>
                        <input class="datebox" id="F_posBeginDate" name="F_posBeginDate" style="width: 85%;" />
                    </td>
                </tr>
                <tr>
                   

                        <td>
                        行政级别
                    </td>
                    <td>
                        <asp:DropDownList ID="F_adminRanking" runat="server" DataSourceID="AdminRkDataSource" DataTextField="value"
                            DataValueField="value" ClientIDMode="Static" CssClass="t_tabletxt" Style="width: 85%">
                        </asp:DropDownList>
                        &nbsp;
                    </td>

                    <td>
                        现任职级时间
                    </td>
                    <td>
                        <input class="datebox" id="F_adminRkBeginDate" name="F_adminRkBeginDate" style="width: 85%;" />
                    </td>
                     <td>
                        职称
                    </td>

                    <td>
                        <asp:DropDownList ID="F_title" runat="server" DataSourceID="TitleDataSource" DataTextField="value"
                            DataValueField="value" ClientIDMode="Static" CssClass="t_tabletxt" Style="width: 85%">
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr class="headline">
                    <td colspan="6">
                        家庭主要成员及重要社会关系
                    </td>
                </tr>
            </table>
            <table style="width: 100%" id="table_jtcy">
                <tr>
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
                    <td width="10%" style="padding-left: 8px;">
                        <a href="javascript:add_row('table_jtcy','table_jtcy_row')">增加</a>
                    </td>
                </tr>
                <tr id="table_jtcy_row">
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
            <table style="width: 100%">
                <tr class="headline">
                    <td>
                        个人简历
                    </td>
                </tr>
                <tr>
                    <td>
                        <textarea cols="110" name="F_resume" rows="20" id="F_resume" style="width: 70%; height: 100px"></textarea>
                    </td>
                </tr>
                <tr class="headline">
                    <td>
                        奖惩情况
                    </td>
                </tr>
                <tr>
                    <td>
                        <textarea cols="110" name="F_rwdandpunishmt" rows="20" id="F_rwdandpunishmt" style="width: 70%;
                            height: 100px"></textarea>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <asp:XmlDataSource ID="TitleDataSource" runat="server" DataFile="~/App_Data/title.xml">
    </asp:XmlDataSource>
        <asp:XmlDataSource ID="AdminRkDataSource" runat="server" DataFile="~/App_Data/adminranking.xml">
    </asp:XmlDataSource>
    <asp:XmlDataSource ID="NationalityDataSource" runat="server" DataFile="~/App_Data/nationality.xml">
    </asp:XmlDataSource>
    <asp:LinqDataSource ID="DeptDataSource" runat="server" ContextTypeName="EducationV2.DataClassesDataContext"
        EnableDelete="false" EntityTypeName="" OrderBy="F_type" TableName="DeptMent">
    </asp:LinqDataSource>
    <link rel="stylesheet" href="css/validationEngine.jquery.css" />
    <script src="js/jquery.validationEngine-zh_CN.js" type="text/javascript"></script>
    <script src="js/jquery.validationEngine.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            initialPage(1);
        });
        function chooseCity() {
            openWinEx("frmUserChooseCity.aspx?controlID=F_nativeplace", "选择籍贯", 250, 500, 1);
        }
    </script>
</body>
</html>
