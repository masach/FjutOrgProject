var modified = false;
var applicantid = 0;
var curpagenum = 0;

function bindInputChange() {
    $("input").change(function () {
        modified = true;
    });  
    $("textarea").change(function () {
        modified = true;
    });
}

$(function () {
    $("#tabs").tabs({
        beforeActivate: function (event, ui) {
            if (modified)
                return confirm('是否放弃变更');
        },
        load: function (event, ui) {
            datepicker();            
            bindInputChange();
            modified = false;

        }
    });
});


function saveResult() {
    modified = false;
    // 刷新父界面
    window.opener.location.reload();
    alert('保存成功');    
}

function saveResultErr() {
    alert('保存失败');   
}

function commitResultErr() {
    alert('提交失败');
}

function savePage(page) {
    //alert($('#frmPage' + page).serialize());
    var flag = false;
    if ($("#frmPage" + page).validationEngine("validate") == false)
        return false;

    var serRst = $('#frmPage' + page).serialize();
    if (applicantid != 0) {
        serRst = serRst + '&aplID=' + applicantid;
    }

    $.ajax({
        type: 'POST',
        async: false,
        url: 'Services/PosApplicantService.ashx?method=savePage' + page,
        data: serRst,
        dataType: 'json', // 此处用text,可以获取服务端返回的string;如果用json,服务端也必须返回json,太麻烦.
        success: function (succ) {
            // 做茧自缚啊
            if (applicantid == 0) {
                if (succ.F_ID != "" && succ.F_ID != undefined) {
                    applicantid = succ.F_ID;
                }
            }
            saveResult();
            flag = true;
        },
        error: function (err) {
            saveResultErr();
            flag = false;
        }
    });
    return flag;
}

function commit(pageNum) {
    var blsave = savePage(pageNum);
    if (!blsave) {              
        return;
    }
    var serRst;
    if (applicantid != 0) {
        serRst = 'aplID=' + applicantid;
    }
    $.ajax({
        type: 'POST',
        url: 'Services/PosApplicantService.ashx?method=commit',
        data: serRst,
        dataType: 'json',
        success: function (social) {
            //$("input[type='button']").css("visibility", "hidden");

            // 为何隐藏有时候不生效啊 TODO
            $("#btnSave").css("visibility", "hidden");
            var test = $("#btnSave");
            //$("#btnSave").css("display", "none");

            // 刷新父界面
            window.opener.location.reload();
        },
        error: function (err) {
            alert(err.responseText);
        }
    });
}

function initialPage(page) {
    var par = "pageNum=" + page;
    if (applicantid != 0) {
        par = par + "&aplID=" + applicantid;
    }
    else {
        var urlid = getUrlParameter("aplID");
        if (urlid != "" && urlid != undefined) {
            applicantid = urlid;
            par = par + "&aplID=" + applicantid;
        }
    }

    $.ajax({
        type: 'POST',
        url: 'Services/PosApplicantService.ashx?method=initialPage',
        dataType: 'json',
        data: par,
        success: function (es) {
            if (page == 1) {
                fillBaseInfo(es);
                fillFamily(es);
            }
            else if (page == 2) {
                // 项目成果        
                $("#F_reason").val(es.F_reason);
                $("#F_pos1").val(es.F_pos1);
                $("#F_pos2").val(es.F_pos2);
                $("#F_pos3").val(es.F_pos3);
                $("#F_posID1").val(es.F_posID1);
                $("#F_posID2").val(es.F_posID2);
                $("#F_posID3").val(es.F_posID3);
            }        
        }
    });
}

function fillBaseInfo(succ) {
    $("#F_realName").val(succ.F_realName);
    $("#F_sexual").val(succ.F_sexual);    
    $("#F_birthday").val(ChangeDateFormat(succ.F_birthday));

    $("#F_nativeplace").val(succ.F_nativeplace);
    $("#F_nationality").val(succ.F_nationality);
    $("#F_party").val(succ.F_party);
  
    $("#F_highestDegree").val(succ.F_highestDegree);
    $("#F_highestEducation").val(succ.F_highestEducation);
    $("#F_highestGrduateSch").val(succ.F_highestGrduateSch);
   
    $("#F_workDept").val(succ.F_workDept);
    //$("#F_workDept").val(succ.F_belongDeptID);
    $("#F_position").val(succ.F_position);

    $("#F_posBeginDate").val(ChangeDateFormat(succ.F_posBeginDate));
    $("#F_adminRanking").val(succ.F_adminRanking);
    $("#F_adminRkBeginDate").val(ChangeDateFormat(succ.F_adminRkBeginDate));

    $("#F_title").val(succ.F_title);

    $("#F_resume").val(succ.F_resume);
    $("#F_rwdandpunishmt").val(succ.F_rwdandpunishmt);
}

function fillFamily(succ) {
    $.ajax({
        type: 'POST',
        url: 'Services/PosApplicantService.ashx?method=initialFamily',
        dataType: 'json',
        //data: 'visituserid=' + succ.F_UserID,
        success: function (stands) {
            for (var i = 0; i < stands.length; i++) {
                if (i > 0)
                    add_row('table_jtcy', 'table_jtcy_row');
                //$("#F_familyMemID" + i).val(stands[i].F_familyMemID);
                $("#F_familyMemAppelation" + i).val(stands[i].F_familyMemAppelation);
                $("#F_familyMemName" + i).val(stands[i].F_familyMemName);
                $("#F_familyMemBirthday" + i).val(ChangeDateFormat(stands[i].F_familyMemBirthday));
                $("#F_familyMemParty" + i).val(stands[i].F_familyMemParty);
                $("#F_familyMemWorkInfo" + i).val(stands[i].F_familyMemWorkInfo);

            }
        }
    });
}

//////////////////////////////////////////////////////////////////////////
// P6~P8 各级管理员验收界面 BEGIN
//////////////////////////////////////////////////////////////////////////

// 验收意见相关的界面，判断角色和验收状态，灰/亮化界面控件
function checkRole(role) {
    $.ajax({
        type: 'POST',
        url: 'Services/Expert.ashx?method=getCurrentRole',
        success: function (patents) {
            if (patents == role) {
                $('input').removeAttr("disabled");                     
                $('textarea').removeAttr("disabled");
                //$("#btnSave").css("visibility", "visable");
                $("input[type='button']").css("display", "inline");
            }
            else {
                $('input').attr("disabled", "disabled");
                $('textarea').attr("disabled", "disabled");               
                $("input[type='button']").css("display", "none");
            }
        }
    });
}

function disableCtrls() {
    $('input').attr("disabled", "disabled");
    $('textarea').attr("disabled", "disabled");
    //$("input[type='button']").css("visibility", "hidden");
    $("input[type='button']").css("display", "none");
}

function admin_accept_commit(pageNum) {
    curpagenum = pageNum;
    var config = {
        title: '确认提交',
        msg: '是否确定提交，提交后将不能修改',
        modal: true,
        value: pageNum,
        fn: commitCallBack,
        buttons: Ext.Msg.OKCANCEL,
        icon: Ext.Msg.WARNING
    }
    Ext.MessageBox.show(config);
}

function commitCallBack(id, msg) {
    if (id == "ok") {
        // savePage中改为同步才可以
        var res = savePage(curpagenum);
        if (res) {
            disableCtrls();
            curpagenum = 0;
        }   
    }
    else if (id == Ext.Msg.CANCEL) {
    }
}

//////////////////////////////////////////////////////////////////////////
// P6~P8 各级管理员验收界面 END
//////////////////////////////////////////////////////////////////////////