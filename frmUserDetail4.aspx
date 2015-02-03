<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUserDetail4.aspx.cs" Inherits="EducationV2.frmUserDetail3v" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript">

        $(function () {
            initialPage(2);
            getStatus();
        });
    </script>
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/Style.css" />

    <style type="text/css">
        .hidetd         
        {
            visibility:hidden;
            }    
    </style>

</head>
<body>
    <form id="myfrm2" method="post" action="/#">
    <div class="systeminfo">
        <div class="systitleline">
         <input id="Button1" type="button" value="保 存" onclick="savePage2()" style="visibility: hidden"
                            class="btn" />
        </div>
        <div class="maintable">
            
            <!--=======页面主题，列表网格========-->
            <div>注：该页面无法修改已申请验收项目的项目成果（专利、论文、奖项）信息。</div>
            <table id="table_" class="sort-table" width="100%">
          
                <tr>
                    <td width="4%">
                        主要技术工作经历
                    </td>
                    <td>
                        <table id="table_gzjl" class="hold-table" width="100%">
                            <tr>
                                <td  align="center" class="hidetd">
                                    PID
                                </td>
                                <td width="31%" align="center">
                                    工作单位
                                </td>
                                <td width="12%" align="center">
                                    起始日期
                                </td>
                                <td width="12%" align="center">
                                    结束日期
                                </td>
                                <td width="35%" align="center">
                                    工作内容
                                </td>
                                <td width="10%" style="padding-left: 8px;">
                                    <a href="javascript:add_row('table_gzjl','table_gzjl_row')">增加</a>
                                </td>
                            </tr>
                            <tr id="table_gzjl_row">
                                 <td  align="center" class="hidetd">                                   
                                </td>
                                <td>
                                    <input type="text" name="F_workspace" id="F_workspace0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" name="F_beginDate" id="F_beginDate0" value="" style="width: 85%;"
                                        class="datebox" />
                                </td>
                                <td>
                                    <input type="text" name="F_endDate" id="F_endDate0" value="" style="width: 85%;"
                                        class="datebox" />
                                </td>
                                <td>
                                    <input type="text" name="F_content" id="F_content0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td rowspan="4" width="4%">
                        代表性成果/论著
                    </td>
                    <td>
                        <table id="table_ysqzl" class="hold-table" width="100%">
                            <tr>
                                 <td  align="center" class="hidetd">
                                    PID
                                </td>
                                <td width="31%" align="center">
                                    己授权专利名称
                                </td>
                                <td width="24%" align="center">
                                    专利授权号
                                </td>
                                <td width="15%" align="center">
                                    专利类型
                                </td>
                                <td width="20%" align="center">
                                    专利授权公告日
                                </td>
                                <td width="10%" style="padding-left: 8px;">
                                    <a href="javascript:add_row('table_ysqzl','table_patent_row')">增加</a>
                                </td>
                            </tr>
                            <tr id="table_patent_row">
                            <td  align="center" class="hidetd">
                                    <input type="text" name="F_patentPrjID" id="F_patentPrjID0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" name="F_patentName" id="F_patentName0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" name="F_patentCode" id="F_patentCode0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" name="F_patentType" id="F_patentType0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" name="F_patentDeclareDate" id="F_patentDeclareDate0" value=""
                                        style="width: 85%;" class="datebox" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="hold-table" width="100%">
                            <tr>
                               <td  align="center" class="hidetd">
                                    ph
                                </td>
                                <td width="11%" style="padding-left: 8px;">
                                    发明专利
                                </td>
                                <td width="10%" style="padding-left: 8px;">
                                    国内申请数
                                </td>
                                <td width="10%">
                                    <input type="text" name="F_appliedCivInvPatent" id="F_appliedCivInvPatent" value=""
                                        onchange="hj_fmzls()" style="width: 85%;" class="t_tabletxt" />
                                </td>
                                <td width="14%" style="padding-left: 8px;">
                                    授权数
                                </td>
                                <td width="10%">
                                    <input type="text" name="F_authedCivInvPatent" id="F_authedCivInvPatent" value=""
                                        onchange="hj_fmzls()" style="width: 85%;" class="t_tabletxt" />
                                </td>
                                <td width="15%" style="padding-left: 8px;">
                                    国外申请数
                                </td>
                                <td width="10%">
                                    <input type="text" name="F_appliedForeInvPatent" id="F_appliedForeInvPatent" value=""
                                        onchange="hj_fmzls()" style="width: 85%;" class="t_tabletxt" />
                                </td>
                                <td width="10%" style="padding-left: 8px;">
                                    授权数
                                </td>
                                <td width="10%">
                                    <input type="text" name="F_authedForeInvPatent" id="F_authedForeInvPatent" value=""
                                        onchange="hj_fmzls()" style="width: 85%;" class="t_tabletxt" />
                                </td>
                            </tr>
                            <tr>
                                <td  align="center" class="hidetd">
                                    ph
                                </td>
                                <td style="padding-left: 8px;">
                                    实用新型专利
                                </td>
                                <td style="padding-left: 8px;">
                                    国内申请数
                                </td>
                                <td>
                                    <input type="text" name="F_appliedCivUtiPatent" id="F_appliedCivUtiPatent" value=""
                                        onchange="hj_fmzls()" style="width: 85%;" class="t_tabletxt" />
                                </td>
                                <td style="padding-left: 8px;">
                                    授权数
                                </td>
                                <td>
                                    <input type="text" name="F_authedCivUtilPatent" id="F_authedCivUtilPatent" value=""
                                        onchange="hj_fmzls()" style="width: 85%;" class="t_tabletxt" />
                                </td>
                                <td style="padding-left: 8px;">
                                    国外申请数
                                </td>
                                <td>
                                    <input type="text" name="F_appliedForUtiPatent" id="F_appliedForUtiPatent" value=""
                                        onchange="hj_fmzls()" style="width: 85%;" class="t_tabletxt" />
                                </td>
                                <td style="padding-left: 8px;">
                                    授权数
                                </td>
                                <td>
                                    <input type="text" name="F_authedForUtiPatent" id="F_authedForUtiPatent" value=""
                                        onchange="hj_fmzls()" style="width: 85%;" class="t_tabletxt" />
                                </td>
                            </tr>
                            <tr>
                              <td  align="center" class="hidetd">
                                    ph
                                </td>
                                <td colspan="2" style="padding-left: 8px;">
                                    合计
                                </td>
                                <td colspan="7">
                                    <input type="text" name="hj_fmzl" id="F_totalPatent" value="" style="width: 74 px;"
                                        class="t_tabletxt" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="table_kjlwzz" class="hold-table" width="100%">
                            <tr>
                                <td  align="center" class="hidetd">
                                    PID
                                </td>
                                <td width="31%" align="center">
                                    论文/科技专蓍名称
                                </td>
                                <td width="24%" align="center">
                                    所发表刊物名称
                                </td>
                                <td width="15%" align="center">
                                    刊号/书号
                                </td>
                                <td width="8%" align="center">
                                    卷期号
                                </td>
                                <td width="12%" align="center">
                                    合(独)著/排名
                                </td>
                                <td width="10%" style="padding-left: 8px;">
                                    <a href="javascript:add_row('table_kjlwzz','table_kjlwzz_row')">增加</a>
                                </td>
                            </tr>
                            <tr id="table_kjlwzz_row">
                                <td  align="center" class="hidetd">
                                    <input type="text" name="F_paperPrjID" id="F_paperPrjID0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" name="F_paperName" id="F_paperName0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" name="F_journalName" id="F_journalName0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" name="F_journalCode" id="F_journalCode0" value="" style="width: 75%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" name="F_volume" id="F_volume0" value="" style="width: 85%;" class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" name="F_paperRank" id="F_paperRank0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="hold-table" width="100%">
                            <tr>
                             <td  align="center" class="hidetd">
                                    ph
                                </td>
                                <td width="21%" style="padding-left: 8px;">
                                    国内刊物发表论文数(篇)
                                </td>
                                <td width="10%">
                                    <input type="text" name="F_publishCivPaper" id="F_publishCivPaper" value="" onchange="hj_fblws()"
                                        style="width: 85%;" class="t_tabletxt" />
                                </td>
                                <td width="14%" style="padding-left: 8px;">
                                    国外发表数(篇)
                                </td>
                                <td width="10%">
                                    <input type="text" name="F_publishForPaper" id="F_publishForPaper" value="" onchange="hj_fblws()"
                                        style="width: 85%;" class="t_tabletxt" />
                                </td>
                                <td width="15%" style="padding-left: 8px;">
                                    出版科技专著(部)
                                </td>
                                <td width="10%">
                                    <input type="text" name="F_publishMonograph" id="F_publishMonograph" value="" onchange="hj_fblws()"
                                        style="width: 85%;" class="t_tabletxt" />
                                </td>
                                <td width="10%" style="padding-left: 8px;">
                                    合计
                                </td>
                                <td width="10%">
                                    <input type="text" name="hj_fblw" id="F_totalPaper" value="" style="width: 90%;"
                                        class="t_tabletxt" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="4%">
                        获奖情况
                    </td>
                    <td>
                        <table id="table_hjqk" class="hold-table" width="100%">
                            <tr>
                                <td  align="center" class="hidetd">
                                    PID
                                </td>
                                <td width="10%" align="center">
                                    获奖时间
                                </td>
                                <td width="21%" align="center">
                                    奖项名称
                                </td>
                                <td width="24%" align="center">
                                    获奖项目名称
                                </td>
                                <td width="10%" align="center">
                                    获奖等级
                                </td>
                                <td width="5%" align="center">
                                    排名
                                </td>
                                <td width="20%" align="center">
                                    颁发单位
                                </td>
                                <td width="10%" style="padding-left: 8px;">
                                    <a href="javascript:add_row('table_hjqk','table_hjqk_row')">增加</a>
                                </td>
                            </tr>
                            <tr id="table_hjqk_row">
                             <td  align="center" class="hidetd">
                                    <input type="text" name="F_awardPrjID" id="F_awardPrjID0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" name="F_awardDate" id="F_awardDate0" value="" style="width: 85%;"
                                        class="datebox" />
                                </td>
                                <td>
                                    <input type="text" name="F_awardName" id="F_awardName0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" name="F_awardProjectName" id="F_awardProjectName0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" name="F_awardGrade" id="F_awardGrade0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" name="F_awardRank" id="F_awardRank0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" name="F_issuedUnit" id="F_issuedUnit0" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                  <tr>      <td width="4%" class="tdtitle">
                        资格证书
                    </td>
                    <td>
                        <table id="table_zgzs" class="hold-table" width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="45%" align="center">
                                    资格证书名称
                                </td>
                                <td width="21%" align="center">
                                    证书编号
                                </td>
                                <td width="9%" align="center">
                                    颁证时间
                                </td>
                                <td width="20%" align="center">
                                    颁证机构
                                </td>
                                <td width="5%" align="center">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr id="table_zgzs_row">
                                <td>
                                    <input type="text" id="F_name0" name="F_name" value="" style="width: 85%;" class="t_tabletxt"
                                        id='zgzs_certname' />
                                </td>
                                <td>
                                    <input type="text" id="F_code0" name="F_code" value="" style="width: 85%;" class="t_tabletxt" />
                                </td>
                                <td>
                                    <input type="text" id="F_grantDate0" name="F_grantDate" value="" style="width: 85%;"
                                        class="datebox" />
                                </td>
                                <td>
                                    <input type="text" id="F_grantOrg0" name="F_grantOrg" value="" style="width: 85%;"
                                        class="t_tabletxt" />
                                </td>
                                <td class="tdtitle">
                                    <a href="javascript:add_row('table_zgzs','table_zgzs_row')">增加</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            <tr>
                    <td width="4%" class="tdtitle">
                        研究方向
                    </td>
                    <td>
                        <table id="table1" class="hold-table" width="100%">
                            <tr>
                                <td width="24%" class="tdtitle">
                                    最熟悉的专业学科
                                </td>
                                <td>
                                    <input type="hidden" name="COMM_EXPERTSUBJECT/I_STUDYDIRECTIONID" value="" class="t_tabletxt" />
                                    <input type="hidden" name="COMM_EXPERTSUBJECT/I_SUBJECTID" value="" class="t_tabletxt" />
                                    <input type="text" name="F_mostFamiliarSubject" id="F_mostFamiliarSubject0" value=""
                                        style="width: 387px;" class="t_tabletxt" />
                                    <%--<a href="javascript:doSelect_with_parameter('temp/I_SUBJECTID', 'COMM_EXPERTSUBJECT/I_SUBJECTID', '学科领域分类', '学科领域', 'p_applybaseinfo.pr.prGetSUBJECTTree.do?pid=0&eid=40', '0');" title="请选择涉及主要学科"><img border="0" src="images/arrowright.gif" width="16" height="16" title="请选择涉及主要学科" /></a>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtitle">
                                    比较熟悉的专业学科
                                </td>
                                <td>
                                    <table id="table_zykx" class="hold-table" width="100%" border="1" cellspacing="0"
                                        cellpadding="0">
                                        <tr>
                                        </tr>
                                        <tr id="table_zykx_row">
                                            <td width="87%">
                                                <input type="text" id="F_familiarSubject0" name="F_familiarSubject" value="" style="width: 387px;"
                                                    class="t_tabletxt" />
                                                <%-- <a href="javascript:doSelect_with_parameter('tempselectid0', 'selectid0', '学科领域分类', '学科领域', 'p_applybaseinfo.pr.prGetSUBJECTTree.do?pid=0&eid=40', '0');" title="请选择涉及主要学科">
                                                    <img alt="" border="0" src="images/arrowright.gif" width="16" height="16" title="请选择涉及主要学科" /></a>--%>
                                            </td>
                                            <td colspan="2">
                                                <a href="javascript:add_row('table_zykx','table_zykx_row')">增加</a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
