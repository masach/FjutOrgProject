var modified = false;
function initialPage()
{
    $.ajax({
        type: 'POST',
        url: 'Services/UnitService.ashx?method=initial',
        success: function (unit) {
            $("#F_name").val(unit.F_name);
            $("#F_organizationCode").val(unit.F_organizationCode);
            $("#F_majorBusiness").val(unit.F_majorBusiness);
            $("#F_address").val(unit.F_address);
            $("#F_principalName").val(unit.F_principalName);
            $("#F_contactName").val(unit.F_contactName);
            $("#F_principalIDNum").val(unit.F_principalIDNum);
            $("#F_principalMobile").val(unit.F_principalMobile);
            $("#F_contactDept").val(unit.F_contactDept);
            $("#F_principalPhone").val(unit.F_principalPhone);
            $("#F_contactDuty").val(unit.F_contactDuty);
            $("#F_majorBusiness").val(unit.F_majorBusiness);
            $("#F_zipCode").val(unit.F_zipCode);
            $("#F_contactPhone1").val(unit.F_contactPhone1);
            $("#F_location").val(unit.F_location);
            $("#F_contactPhone2").val(unit.F_contactPhone2);
            $("#F_type").val(unit.F_type);
            $("#F_contactMobile").val(unit.F_contactMobile);
            $("#F_registeredAssets").val(unit.F_registeredAssets);
            $("#F_contactFax").val(unit.F_contactFax);
            $("#F_bank").val(unit.F_bank);
            $("#F_contactEmail").val(unit.F_contactEmail);
            $("#F_bankAccount").val(unit.F_bankAccount);
            $("#F_bankAccountName").val(unit.F_bankAccountName);
            $("#F_description").val(unit.F_description);
            $("#F_awards").val(unit.F_awards);
            $("#F_auditComment").text(unit.F_auditComment);
            $("#F_auditDate").text(ChangeDateFormat(unit.F_auditDate));
            $("#F_status").text(unit.F_status);


            if (unit.F_status == "未提交") {
                $("#btnSave").css("visibility", "visible");
                $("#btnSubmit").css("visibility", "visible");
            }
            else {
                $("#btnSave").css("visibility", "hidden");
            }



        }
    });
}

$(function () {

  datepicker();
  bindInputChange();
  modified = false;
    

});

function bindInputChange() {
    $("input").change(function () {
        modified = true;
    });

}

function CheckUnitName() {
    $.ajax({
        type: 'POST',
        url: 'Services/UnitService.ashx?method=checkName',
        data: "F_name=" + $("#F_name").val(),
        success: function (succ) {
            alert(succ);
        }
    });
}
function addUnit() {
    $.ajax({
        type: 'POST',
        url: 'Services/UnitService.ashx?method=addUnit',
        data: $('#myfrm').serialize(),
        dataType: 'text',
        error: function (result) {
            modified = false;
            alert(result);
           
        },
        success: function (result) {
            modified = false;
            alert(result);
        }
    });
}


function savePage() {
    $.ajax({
        type: 'POST',
        url: 'Services/UnitService.ashx?method=save',
        data: $('#frmPage1').serialize(),
        dataType: 'text',
        error: function (result) {
            modified = false;
            alert(result);
        },
        success: function (result) {
            modified = false;
            alert(result);
        }
    });
}

function commit() {
    savePage();
    $.ajax({
        type: 'POST',
        url: 'Services/UnitService.ashx?method=commit',
        success: function (social) {
            $("input[type='button']").css("visibility", "hidden");
            $("#btnExport").css("visibility", "visible");
        }
    });
}