<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="frmStaffDetail1.aspx.cs"
    Inherits="EducationV2.frmStaffDetail1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html; charset=gb2312" />
    <title>用户基本信息</title>
</head>
<body>
    <div class="systeminfo">
        <div class="systitleline">
            <input id="Button1" type="button" value="保 存" onclick="savePage(1)" class="btn" />
        </div>
        <div class="maintable">
            <form id="frmPage1" runat="server">
            <table style="width: 100%">
                <tr class="headline">
                    <td colspan="8">
                        帐号信息
                    </td>
                </tr>
                <tr>
                    <td class="field">
                        关联用户
                    </td>
                    <td width="13%">
                        <input name="F_userName" id="F_userName" type="text" readonly="readonly" onclick="findUser()" />
                    </td>
                    <td class="field">
                        用户编号:
                    </td>
                    <td width="13%" name="F_UserID" id="F_UserID">
                    <!--
                        <input name="F_UserID" id="F_UserID" type="text" readonly="readonly" />
                        -->
                    </td>
                    <td class="field">
                        档案编号:
                    </td>
                    <td width="13%" name="F_StaffID" id="F_StaffID">
                    <!--
                        <input name="F_StaffID" id="F_StaffID" type="text" readonly="readonly" />-->
                    </td>
                    <td class="field">
                        归档时间:
                    </td>
                    <td width="13%" id="F_lastModifyTime" name="F_lastModifyTime">
                    </td>
                </tr>
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
                            class="t_tabletxt validate[required]" />
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
                        出生地
                    </td>
                    <td width="13%">
                        <input id="F_bornplace" name="F_bornplace" type="text" style="width: 85%" class="t_tabletxt" />
                    </td>
                    <td class="field">
                        出生日期
                    </td>
                    <td>
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
                    <td>
                        入党/盟时间
                    </td>
                    <td>
                        <input class="datebox" id="F_partyEntryDate" name="F_partyEntryDate" style="width: 85%;" />
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
                    <td>
                        参加工作时间
                    </td>
                    <td>
                        <input class="datebox" id="F_workBeginDate" name="F_workBeginDate" style="width: 85%;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        现工作部门
                    </td>
                    <td>
                       <%--  <input id="F_belongDeptID" name="F_belongDeptID" type="hidden" />--%>
                        <asp:DropDownList ID="F_workDept" name="F_workDept" runat="server" DataSourceID="DeptDataSource"
                            DataTextField="F_name" DataValueField="F_ID" ClientIDMode="Static" CssClass="t_tabletxt validate[required]"
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
                    <td>
                        职称
                    </td>
                    <td>
                        <asp:DropDownList ID="F_title" runat="server" DataSourceID="TitleDataSource" DataTextField="value"
                            DataValueField="value" ClientIDMode="Static" CssClass="t_tabletxt" Style="width: 85%">
                        </asp:DropDownList>
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
                        证件类型
                    </td>
                    <td>
                        <select name="F_idType" id="F_idType" class="t_tabletxt" style="width: 85%;">
                            <option value="身份证">身份证</option>
                            <option value="军人证">军人证</option>
                            <option value="护照">护照</option>
                            <option value="其他">其他</option>
                        </select>
                    </td>
                    <td>
                        证件号码
                    </td>
                    <td>
                        <input type="text" name="F_idNumber" id="F_idNumber" value="" style="width: 85%;"
                            class="t_tabletxt" />
                    </td>
                   
                </tr>
                <tr>
                 <td>
                        手机
                    </td>
                    <td>
                        <input type="text" name="F_mobile" id="F_mobile" value="" style="width: 85%;" class="t_tabletxt" />
                    </td>
                    <td>
                        联系电话(O)
                    </td>
                    <td>
                        <input type="text" name="F_phone" id="F_phone" value="" style="width: 85%;" class="t_tabletxt" />
                    </td>
                    <td>
                        联系电话(H)
                    </td>
                    <td>
                        <input type="text" name="F_phone2" id="F_phone2" value="" style="width: 85%;" class="t_tabletxt" />
                    </td>
                    <td>
                        电子邮箱
                    </td>
                    <td>
                        <input type="text" name="F_email" id="F_email" value="" style="width: 85%;" class="t_tabletxt" />
                    </td>
                  
                </tr>
                <tr>
                  <td>
                        联系地址
                    </td>
                    <td>
                        <input type="text" name="F_freeAddress" id="F_freeAddress" value="" style="width: 85%;"
                            class="t_tabletxt" />
                    </td>
                    <td>
                        传真号码
                    </td>
                    <td>
                        <input type="text" name="F_fax" id="F_fax" value="" style="width: 85%;" class="t_tabletxt" />
                    </td>
                       <td>
                           教工号
                    </td>
                    <td>
                        <input type="text" name="F_empno" id="F_empno" value="" style="width: 85%;" class="t_tabletxt validate[required]" />
                    </td>
                    <td>
                        审核状态
                    </td>
                    <td>
                        <select name="F_status" id="F_status" style="width: 85%;" class="t_tabletxt">
                            <option selected="selected" value="审核通过">审核通过</option>
                            <option value="待审核">待审核</option>
                        </select>
                    </td>
                 
                </tr>
            </table>
            </form>
        </div>
        <asp:XmlDataSource ID="TitleDataSource" runat="server" DataFile="~/App_Data/title.xml">
        </asp:XmlDataSource>
        
        <asp:XmlDataSource ID="AdminRkDataSource" runat="server" DataFile="~/App_Data/adminranking.xml">
    </asp:XmlDataSource>

        <asp:XmlDataSource ID="NationalityDataSource" runat="server" DataFile="~/App_Data/nationality.xml">
        </asp:XmlDataSource>
        <asp:LinqDataSource ID="DeptDataSource" runat="server" ContextTypeName="EducationV2.DataClassesDataContext"
            EnableDelete="false" EntityTypeName="" TableName="DeptMent" Where='F_status="审核通过"'
            OrderBy="F_type">
        </asp:LinqDataSource>
    </div>
    <!--
    <link rel="stylesheet" href="css/validationEngine.jquery.css" />
    <script type="text/javascript" src="Scripts/JQuery-1.10.js" charset="UTF-8"></script>
    <script src="js/jquery.validationEngine-zh_CN.js" type="text/javascript"></script>
    <script src="js/jquery.validationEngine.js" type="text/javascript"></script>
  -->
    <script type="text/javascript">
        $(function () {
            initialPage(1);
        });

        function chooseCity() {
            openWinEx("frmUserChooseCity.aspx?controlID=F_nativeplace", "选择籍贯", 250, 500, 1);
        }

        function findUser() {
            // 参数为socialpage1，让其不显示新增用户tab页面    
            openWinEx("frmUserFind.aspx?requestPage=staff", "查找用户", 800, 350, 1);
            //openWin("frmUserFind.aspx?requestPage=staff");
        }

        function addUser(strUser) {
            var user = JSON.parse(strUser);
            //alert(user.F_ID);
            $("#F_UserID", '#frmPage1').text(user.F_ID);
            $("#F_userName", '#frmPage1').val(user.F_userName);
        }

    </script>
</body>
</html>
