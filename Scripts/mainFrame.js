var firstVisibleTabIndex;
var role;
$(function () {
    $("#accordion").accordion({
        heightStyle: "content"
    });
    $.ajax({
        type: 'POST',
        url: 'Services/Expert.ashx?method=getCurrentRole',
        success: function (data) {
            role = data;
            if (data == "申请人员") {
                $("#deptMgr").hide();
            }
            else if (data == "部门主管") {
                $("#dept0").hide();
                $("#dept1").hide();
                $("#dept3").hide();
            }
            else if (data == "系统管理员") {
                $("#dept3").hide();
            }
            else if (data == "教育厅管理人员") {
                $("#dept0").hide();
                $("#dept1").hide();
                $("#dept2").hide();
            }
        }
    });

    $.ajax({
        type: 'POST',
        url: 'Services/Expert.ashx?method=getStatus',
        success: function (data) {

            if (data != "审核通过") {
                if (role != "教育厅管理人员") {
                    $("#applyMgr").hide();
  
                }

            }

        }
    });
});