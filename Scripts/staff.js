var modified = false;
var applicantid = 0;

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


function initialPage(pageNum) {

    var par = "pageNum=" + pageNum;
    if (applicantid != 0) {
        par = par + "&stfID=" + applicantid;
    }
    else {
        var urlid = getUrlParameter("stfID");
        if (urlid != "" && urlid != undefined) {
            applicantid = urlid;
            par = par + "&stfID=" + applicantid;
        }
    }

    $.ajax({
        type: 'POST',
        url: 'Services/Staff.ashx?method=initialPage',
        dataType: 'json',
        data: par,
        success: function (data) {
            if (pageNum == 1) {
                fillBaseInfo(data);              
            }
            else if (pageNum == 2) {
                fillPage2(data);              
            }
            else if (pageNum == 3) {
                fillPage3(data);
            }
        }
    });
}


function fillBaseInfo(succ) {
    $("#F_userName").val(succ.F_userName);
    $("#F_UserID").val(succ.F_UserID);
    $("#F_lastModifyTime").text(ChangeDateFormat(succ.F_lastModifyTime));    
    $("#F_StaffID").val(succ.StaffID);

    $("#F_realName").val(succ.F_realName);
    $("#F_sexual").val(succ.F_sexual);
    $("#F_bornplace").val(succ.F_bornplace);
    $("#F_birthday").val(ChangeDateFormat(succ.F_birthday));

    $("#F_nativeplace").val(succ.F_nativeplace);
    $("#F_nationality").val(succ.F_nationality);
    $("#F_party").val(succ.F_party);
    $("#F_partyEntryDate").val(ChangeDateFormat(succ.F_partyEntryDate));

    $("#F_highestDegree").val(succ.F_highestDegree);
    $("#F_highestEducation").val(succ.F_highestEducation);
    $("#F_highestGrduateSch").val(succ.F_highestGrduateSch);
    $("#F_workBeginDate").val(ChangeDateFormat(succ.F_workBeginDate));

    $("#F_workDept").val(succ.F_workDept);
    //$("#F_workDept").val(succ.F_belongDeptID);
    $("#F_position").val(succ.F_position);
    $("#F_title").val(succ.F_title);


    $("#F_posBeginDate").val(ChangeDateFormat(succ.F_posBeginDate));
    $("#F_adminRanking").val(succ.F_adminRanking);
    $("#F_adminRkBeginDate").val(ChangeDateFormat(succ.F_adminRkBeginDate));

    $("#F_idType").val(succ.F_idType);
    $("#F_idNumber").val(succ.F_idNumber);
    $("#F_mobile").val(succ.F_mobile);
    $("#F_phone").val(succ.F_phone);

    $("#F_phone2").val(succ.F_phone2);
    $("#F_email").val(succ.F_email);
    $("#F_freeAddress").val(succ.F_freeAddress);
    $("#F_fax").val(succ.F_fax);

    $("#F_status").val(succ.F_status);
}


function fillPage2(succ) {
    var par = "pageNum=" + 2;
    if (applicantid != 0) {
        par = par + "&stfID=" + applicantid;
    }
    else {
        var urlid = getUrlParameter("stfID");
        if (urlid != "" && urlid != undefined) {
            applicantid = urlid;
            par = par + "&stfID=" + applicantid;
        }
    }

    $.ajax({
        type: 'POST',
        url: 'Services/Staff.ashx?method=getFamilyMember',
        dataType: 'json',
        data: par,
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


function fillPage3(succ) {
    $("#F_resume").val(succ.F_resume);
    $("#F_rwdandpunishmt").val(succ.F_rwdandpunishmt);
}

function savePage(page) {
    var flag = false;
    if ($("#frmPage" + page).validationEngine("validate") == false)
        return false;

    var serRst = $('#frmPage' + page).serialize();
    if (applicantid != 0) {
        serRst = serRst + '&stfID=' + applicantid;
    }

    $.ajax({
        type: 'POST',
        async: false,
        url: 'Services/Staff.ashx?method=savePage' + page,
        data: serRst,
        dataType: 'json',
        success: function (succ) {
            // 做茧自缚啊
            if (applicantid == 0) {
                if (succ.F_StaffID != "" && succ.F_StaffID != undefined) {
                    applicantid = succ.F_StaffID;
                }
            }
            saveResult();
            flag = true;
            modified = false;        
        },
        error: function (err) {
            alert(err.responseText);
            flag = false;
        }
    });
}

function saveResult() {
    modified = false;
    // 刷新父界面
    window.opener.location.reload();
    alert('保存成功');
}

