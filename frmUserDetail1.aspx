<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="frmUserDetail1.aspx.cs"
    Inherits="EducationV2.frmUserDetail1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html; charset=gb2312" />
    <title>�û�������Ϣ</title>
</head>
<body>
    <div class="systeminfo">
        <div class="systitleline">
            <input id="Button1" type="button" value="�� ��" onclick="savePage(1)" style="visibility: hidden"
                class="btn" />
        </div>
        <div class="maintable">
            <form id="frmPage1" runat="server">
            <table style="width: 100%">
                <tr class="headline">
                    <td colspan="6">
                        �ʺ���Ϣ
                    </td>
                </tr>
                <tr>
                    <td class="field">
                        ��¼��
                    </td>
                    <td width="13%" id="F_userName" name="F_userName">
                    </td>
                    <td class="field">
                        �ʻ�����:
                    </td>
                    <td width="13%" id="F_Role" name="F_Role">
                    </td>
                    <td class="field">
                        ���ʱ��:
                    </td>
                    <td width="13%" id="F_lastModifyTime" name="F_lastModifyTime">
                    </td>
                    <td class="hidetd">
                        ר�ұ��:
                    </td>
                    <td width="13%" name="F_ID" id="F_ID" class="hidetd">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="headline">
                    <td colspan="6">
                        ������Ϣ
                    </td>
                </tr>
                <tr>
                    <td class="field">
                        ����
                    </td>
                    <td width="13%">
                        <input type="text" name="F_realName" id="F_realName" value="" style="width: 85%;"
                            class="t_tabletxt" />
                    </td>
                    <td class="field">
                        �Ա�
                    </td>
                    <td width="13%">
                        <select name="F_sexual" id="F_sexual" style="width: 85%;" class="t_tabletxt">
                            <option value="��">��</option>
                            <option value="Ů">Ů</option>
                        </select>
                    </td>
                    <td class="field">
                        ������
                    </td>
                    <td width="13%">
                        <input id="F_bornplace" name="F_bornplace" type="text" style="width: 85%" class="t_tabletxt" />
                    </td>
                    <td class="field">
                        ��������
                    </td>
                    <td>
                        <input class="datebox" type="text" name="F_birthday" id="F_birthday" style="width: 85%;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        ����
                    </td>
                    <td>
                        <input id="F_nativeplace" name="F_nativeplace" type="text" style="width: 85%" readonly="readonly"
                            onclick="chooseCity()" class="t_tabletxt" />
                    </td>
                    <td>
                        ����
                    </td>
                    <td>
                        <asp:DropDownList ID="F_nationality" runat="server" ClientIDMode="Static" DataSourceID="NationalityDataSource"
                            DataTextField="value" DataValueField="value" CssClass="t_tabletxt" Style="width: 85%">
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                    <td>
                        ������ò
                    </td>
                    <td>
                        <input id="F_party" name="F_party" type="text" style="width: 85%" class="t_tabletxt" />
                    </td>
                    <td>
                        �뵳/��ʱ��
                    </td>
                    <td>
                        <input class="datebox" id="F_partyEntryDate" name="F_partyEntryDate" style="width: 85%;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        ���ѧλ
                    </td>
                    <td>
                        <select name="F_highestDegree" id="F_highestDegree" style="width: 85%;" class="t_tabletxt">
                            <option value="��ʿ">��ʿ</option>
                            <option value="˶ʿ">˶ʿ</option>
                            <option value="ѧʿ">ѧʿ</option>
                            <option value="����" selected="selected">����</option>
                        </select>
                    </td>
                    <td>
                        ���ѧ��
                    </td>
                    <td>
                        <select name="F_highestEducation" id="F_highestEducation" style="width: 85%;" class="t_tabletxt">
                            <option value="����">����</option>
                            <option value="��ר/ְУ">��ר/ְУ</option>
                            <option value="��ר">��ר</option>
                            <option value="����">����</option>
                            <option value="˶ʿ">˶ʿ</option>
                            <option value="��ʿ">��ʿ</option>
                        </select>
                    </td>
                    <td>
                        ��ҵʱ�䡢ԺУϵ��רҵ
                    </td>
                    <td>
                        <input id="F_highestGrduateSch" name="F_highestGrduateSch" type="text" style="width: 85%"
                            class="t_tabletxt" />
                    </td>
                    <td>
                        �μӹ���ʱ��
                    </td>
                    <td>
                        <input class="datebox" id="F_workBeginDate" name="F_workBeginDate" style="width: 85%;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        �ֹ�������
                    </td>
                    <td>
                    <%-- 
                        <input id="F_belongDeptID" name="F_belongDeptID" type="hidden" />
                        --%>
                        <asp:DropDownList ID="F_workDept" name="F_workDept" runat="server" DataSourceID="DeptDataSource"
                            DataTextField="F_name" DataValueField="F_ID" ClientIDMode="Static" CssClass="t_tabletxt validate[required]"
                            Style="width: 85%">
                        </asp:DropDownList>
                    </td>
                    <td>
                        �ֹ���ְ��
                    </td>
                    <td>
                        <input type="text" name="F_position" id="F_position" value="" style="width: 85%;"
                            class="t_tabletxt" />
                    </td>
                    <td>
                        ����ְʱ��
                    </td>
                    <td>
                        <input class="datebox" id="F_posBeginDate" name="F_posBeginDate" style="width: 85%;" />
                    </td>
                    <td>
                        ְ��
                    </td>
                    <td>
                        <asp:DropDownList ID="F_title" runat="server" DataSourceID="TitleDataSource" DataTextField="value"
                            DataValueField="value" ClientIDMode="Static" CssClass="t_tabletxt" Style="width: 85%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                                    
                    <td>
                        ��������
                    </td>
                    <td>
                        <asp:DropDownList ID="F_adminRanking" runat="server" DataSourceID="AdminRkDataSource" DataTextField="value"
                            DataValueField="value" ClientIDMode="Static" CssClass="t_tabletxt" Style="width: 85%">
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                    <td>
                        ����ְ��ʱ��
                    </td>
                    <td>
                        <input class="datebox" id="F_adminRkBeginDate" name="F_adminRkBeginDate" style="width: 85%;" />
                    </td>

                    <td>
                        ֤������
                    </td>
                    <td>
                        <select name="F_idType" id="F_idType" class="t_tabletxt" style="width: 85%;">
                            <option value="���֤">���֤</option>
                            <option value="����֤">����֤</option>
                            <option value="����">����</option>
                            <option value="����">����</option>
                        </select>
                    </td>
                    <td>
                        ֤������
                    </td>
                    <td>
                        <input type="text" name="F_idNumber" id="F_idNumber" value="" style="width: 85%;"
                            class="t_tabletxt" />
                    </td>
                   
                </tr>
                <tr>
                 <td>
                        �ֻ�
                    </td>
                    <td>
                        <input type="text" name="F_mobile" id="F_mobile" value="" style="width: 85%;" class="t_tabletxt" />
                    </td>
                    <td>
                        ��ϵ�绰(O)
                    </td>
                    <td>
                        <input type="text" name="F_phone" id="F_phone" value="" style="width: 85%;" class="t_tabletxt" />
                    </td>
                    <td>
                        ��ϵ�绰(H)
                    </td>
                    <td>
                        <input type="text" name="F_phone2" id="F_phone2" value="" style="width: 85%;" class="t_tabletxt" />
                    </td>
                    <td>
                        ��������
                    </td>
                    <td>
                        <input type="text" name="F_email" id="F_email" value="" style="width: 85%;" class="t_tabletxt" />
                    </td>
                  
                </tr>
                <tr>
                    <td>
                        ��ϵ��ַ
                    </td>
                    <td>
                        <input type="text" name="F_freeAddress" id="F_freeAddress" value="" style="width: 85%;"
                            class="t_tabletxt" />
                    </td>
                    <td>
                        �������
                    </td>
                    <td>
                        <input type="text" name="F_fax" id="F_fax" value="" style="width: 85%;" class="t_tabletxt" />
                    </td>
                    <td colspan="6">
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
            EnableDelete="false" EntityTypeName="" TableName="DeptMent" Where='F_status="���ͨ��"'
            OrderBy="F_type">
        </asp:LinqDataSource>
    </div>
    <script type="text/javascript">
        $(function () {
            initialPage(1);
            getStatus();
        });

        function chooseCity() {
            openWinEx("frmUserChooseCity.aspx?controlID=F_nativeplace", "ѡ�񼮹�", 250, 500, 1);
        }
    </script>
</body>
</html>
