<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmExpert12.aspx.cs" EnableViewState="false" Inherits="EducationV2.frmExpert1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="css/Style.css" />
    <script type="text/javascript">
        $(function () {
            initialPage(1);
            getStatus();
        });
        function savePage() {
            $.ajax({
                type: 'POST',
                url: 'Services/Expert.ashx?method=addExpert',
                data: $('#myfrm').serialize(),
                dataType: 'json',
                error: function (err) {
                    modified = false;
                    alert('保存成功');
                },
                success: function (succ) {
                    modified = false;
                    alert('保存成功');
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="maintable">
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td style="width: 15%; text-align: right;">
                    <span style="color: Red">*</span> 名称：
                </td>
                <td style="text-align: left; padding-left: 5px;">
                    <asp:TextBox ID="Title" runat="server" Width="250px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator" runat="server" ErrorMessage=" *标题不能为空" ControlToValidate="Title"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="display: <%=Lang%>">
                <td style="text-align: right;">
                    英文名称：
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TitleEn" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <span style="color: Red">*</span> 分类：
                </td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="Category" runat="server">
                    </asp:DropDownList>
                    请选择末级分类
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    内容：
                </td>
                <td style="text-align: left;">
                    <fckeditorv2:fckeditor id="Content" runat="server" height="400px" width="100%" basepath="/editorT/">
                        </fckeditorv2:fckeditor>
                </td>
            </tr>
            <tr style="display: <%=Lang%>">
                <td style="text-align: right;">
                    英文内容：
                </td>
                <td style="text-align: left;">
                    <fckeditorv2:fckeditor id="ContentEn" runat="server" height="400px" width="100%"
                        basepath="/editorT/">
                        </fckeditorv2:fckeditor>
                </td>
            </tr>
            <tr style="display: none;">
                <td style="text-align: right;">
                    图片：
                </td>
                <td style="text-align: left;">
                    <asp:Image ID="imgPicture" runat="server" onerror="this.src='/JX_Admin/images/nopic.png'" /><br />
                    <asp:FileUpload ID="fuPicture" runat="server" />
                    注： 图片格式为.jpg、.bmp、.gif、.jpeg和.png格式,尺寸为146*95px 大小不超过10MB
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    排序：
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="Orders" runat="server" Text="0" Width="50"></asp:TextBox>
                    注： 数字越大排名越靠前
                </td>
            </tr>
            <tr style="display: none;">
                <td style="text-align: right;">
                    是否置顶：
                </td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="IsTop" runat="server" />
                </td>
            </tr>
            <tr style="display: none;">
                <td style="text-align: right;">
                    是否推荐：
                </td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="IsRecommend" runat="server" />
                    注：推荐到首页轮播图
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    创建时间：
                </td>
                <td style="text-align: left;">
                    <asp:Literal ID="CreateTime" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                </td>
                <td style="text-align: left;">
                    <asp:Button ID="btnEdit" runat="server" Text="保存" OnClick="btnEdit_Click" Width="70px" />
                    <input id="btnBack" type="button" value="取消" onclick="javascript:window.history.back();"
                        style="width: 70px;" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
