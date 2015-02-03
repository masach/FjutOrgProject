<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPosApplicantP2.aspx.cs"
    Inherits="EducationV2.frmPosApplicant2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>岗位申请表-申请信息</title>
    <style type="text/css">
        .style1
        {
            width: 115px;
        }
    </style>
</head>
<body>
    <div class="systeminfo">
        <div class="systitleline">
            <asp:Literal ID="litPanel" runat="server"></asp:Literal>
        </div>
        <div class="maintable">
            <form id="frmPage2" runat="Server">
            <table style="width: 100%">
                <tr class="headline">
                    <td colspan="2">
                        竞聘岗位
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        岗位1
                    </td>
                    <td colspan="5">
                        <input type="hidden" name="F_pos1" id="F_pos1" value="" style="width: 200px;" class="t_tabletxt" />
                        <asp:DropDownList ID="F_posID1" name="F_posID1" runat="server" DataSourceID="PosDataSource"
                            DataTextField="F_posname" DataValueField="F_ID" ClientIDMode="Static" CssClass="t_tabletxt validate[required]"
                            Style="width: 200px" onchange="change(1)">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        岗位2
                    </td>
                    <td colspan="5">
                        <input type="hidden" name="F_pos2" id="F_pos2" value="" style="width: 200px;" class="t_tabletxt" />
                        <asp:DropDownList ID="F_posID2" name="F_posID2" runat="server" DataSourceID="PosDataSource"
                            DataTextField="F_posname" DataValueField="F_ID" ClientIDMode="Static" CssClass="t_tabletxt"
                            Style="width: 200px" onchange="change(2)">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        岗位3
                    </td>
                    <td colspan="5">
                        <input type="hidden" name="F_pos3" id="F_pos3" value="" style="width: 200px;" class="t_tabletxt" />
                        <asp:DropDownList ID="F_posID3" name="F_posID3" runat="server" DataSourceID="PosDataSource"
                            DataTextField="F_posname" DataValueField="F_ID" ClientIDMode="Static" CssClass="t_tabletxt"
                            Style="width: 200px" onchange="change(3)">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="headline">
                    <td colspan="2">
                        竞聘理由或有关情况说明
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea cols="110" name="F_reason" rows="50" id="F_reason" style="width: 95%; height: 200px"></textarea>
                    </td>
                </tr>
            </table>
            </form>
            <asp:LinqDataSource ID="PosDataSource" runat="server" ContextTypeName="EducationV2.DataClassesDataContext"
                EnableDelete="false" EntityTypeName="" TableName="Position" Where='F_status="审核通过"'
                OrderBy="F_endDate">
            </asp:LinqDataSource>
        </div>
    </div>
    <link rel="stylesheet" href="css/validationEngine.jquery.css" />
    <script src="js/jquery.validationEngine-zh_CN.js" type="text/javascript"></script>
    <script src="js/jquery.validationEngine.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            initialPage(2);
        });

        function change(idnum) {
            var ctrlsel;
            switch (idnum) {
                case 1:
                    ctrlsel = $("#F_posID1");
                    //var index = ctrlsel.get(0).selectedIndex;          
                    var val = ctrlsel.children('option:selected').text();
                    $("#F_pos1").val(val);

                    break;
                case 2:
                    ctrlsel = $("#F_posID2");
                    //var index = ctrlsel.get(0).selectedIndex;          
                    var val = ctrlsel.children('option:selected').text();
                    $("#F_pos2").val(val);
                   
                    break;
                case 3:
                    ctrlsel = $("#F_posID3");
                    //var index = ctrlsel.get(0).selectedIndex;          
                    var val = ctrlsel.children('option:selected').text();
                    $("#F_pos3").val(val);
                   
                    break;
                default:
                    break;
            }
        }
    </script>
</body>
</html>
