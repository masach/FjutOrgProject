var modified = false;

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

function bindInputChange() {
    $("input").change(function () {
        modified = true;
    });

}

function initialPage(pageNum)
{
    $.ajax({
        type: 'POST',
        url: 'Services/Expert.ashx?method=initialPage',
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

function getStatus() {
    $.ajax({
        type: 'POST',
        url: 'Services/Expert.ashx?method=getModiable',
        success: function (social) {
            if (social == "可修改" ) {
                $("input[type='button']").css("visibility", "visible");
            }
            else {
                $("input[type='button']").css("visibility", "hidden");
            }        

        }
    });
}

function fillBaseInfo(succ) {
    $("#F_userName").text(succ.F_userName);
    $("#F_Role").text(succ.F_Role);
    $("#F_lastModifyTime").text(ChangeDateFormat(succ.F_lastModifyTime));    
    $("#F_ID").text(succ.F_ID);

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

    //$("#F_workDept").val(succ.F_workDept);
    $("#F_workDept").val(succ.F_belongDeptID);

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
}


function fillPage2(succ) {
    $.ajax({
        type: 'POST',
        url: 'Services/Expert.ashx?method=getFamilyMember',
        dataType: 'json',
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
    //    if ($("#frmPage" + page).validationEngine("validate") == false)
    //        return false;

    var serRst = $('#frmPage' + page).serialize();
    var wkdpt = $("#F_workDept").find("option:selected").text();
    if (wkdpt != "" && wkdpt != undefined) {      
        serRst = serRst + '&F_workDeptText=' + wkdpt;
    }    

    $.ajax({
        type: 'POST',
        async: false,
        url: 'Services/Expert.ashx?method=savePage' + page,
        data: serRst, // 这里Asp.net控件居然可以被序列化       
        //data: $('table *').serialize(),        
        dataType: 'text', // 此处用text,可以获取服务端返回的string;如果用json,服务端也必须返回json,太麻烦.
        success: function (succ) {
            modified = false;
            alert(succ);
        },
        error: function (err) {
            modified = false;
            alert(err);
        }
    });
}

function checkUserName() {
    $.ajax({
        type: 'POST',
        url: 'Services/Expert.ashx?method=checkUserName',
        data: "F_userName=" + $("#F_userName").val(),
        success: function (succ) {
            alert(succ);
        }
    });
}

