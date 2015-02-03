var modified = false;
$(function () {
    $("#tabs").tabs({
        beforeActivate: function (event, ui) {
            if (modified)
                return confirm('是否放弃变更');
        },
        load: function (event, ui) {
            datepicker();
            fillYear();
            bindInputChange();
            getStatus();
            modified = false;

        }
    });
});



function bindInputChange() {
    $("input").change(function () {
        modified = true;
    });

}


function getStatus() {
    $.ajax({
        type: 'POST',
        url: 'Services/ExecuteStatusService.ashx?method=getStatus',
        success: function (social) {
            if (social == "未提交" || social == "审批没有通过") {
                $("input[type='button']").css("visibility", "visible");              

            }
            else {
                $("input[type='button']").css("visibility", "hidden");
            }
            $("input[type='button']", "#frmAttachment").css("visibility", "visible");
            $("#btnUnitConfirm").css("visibility", "visible");
            $("#btnEduConfirm").css("visibility", "visible");

        }
    });
}

function btn_unitConfirm() {
    $.ajax({
        type: 'POST',
        url: 'Services/ExecuteStatusService.ashx?method=saveUnitConfirm' + page,
        data: $('#frmPage5' ).serialize(),
        dataType: 'json',
        error: function (err) { saveResult(); },
        success: function (succ) {
            saveResult();
        }
    });
}

function commit(pageNum) {
    savePage(pageNum);
    $.ajax({
        type: 'POST',
        url: 'Services/ExecuteStatusService.ashx?method=commit',
        success: function (social) {
            $("input[type='button']").css("visibility", "hidden");
        }
    });
}

function savePage(page) {
    $.ajax({
        type: 'POST',
        url: 'Services/ExecuteStatusService.ashx?method=savePage' + page,
        data: $('#frmPage' + page).serialize(),
        dataType: 'json',
        error: function (err) { saveResult(); },
        success: function (succ) {
            saveResult();
        }
    });
}

function saveResult() {
    alert('保存成功');
    modified = false;
}




function initialPage(page) {
    $.ajax({
        type: 'POST',
        url: 'Services/ExecuteStatusService.ashx?method=initialPage',
        data: "pageNum=" + page,
        success: function (es) {

            if (page == 1) {
                $("#F_applicantUnit").text(es.F_applicantUnit);
                $("#F_applicantName").text(es.F_applicantName);
                $("#F_acceptNo").text(es.F_acceptNo);
                $("#F_applicantType").text(es.F_applicantType);
                $("#F_startAndStopDate").text(es.F_startAndStopDate);
                $("#F_domain").text(es.F_domain);
                $("#F_leaderName").text(es.F_leaderName);
                $("#F_leaderNo").text(es.F_leaderNo);
                $("#F_planMoney").text(es.F_planMoney);
                $("#F_ContactPhone").text(es.F_ContactPhone);
                $("#F_mobile").text(es.F_mobile);
                $("#F_fillingDate").val(ChangeDateFormat(es.F_fillingDate));

            }
            else if (page == 3) {
                $("#F_newOutput").val(es.F_newOutput);
                $("#F_newProfit").val(es.F_newProfit);
                $("#F_newTax").val(es.F_newTax);
                $("#F_transTech").val(es.F_transTech);
                $("#F_transFee").val(es.F_transFee);
                $("#F_totalSaving").val(es.F_totalSaving);
                $("#F_demoArea").val(es.F_demoArea);
                $("#F_promtArea").val(es.F_promtArea);
                $("#F_newProduction").val(es.F_newProduction);
                $("#F_awardPerson").val(es.F_awardPerson);
                $("#F_awardDesp").val(es.F_awardDesp);
                $("#F_postPHD").val(es.F_postPHD);
                $("#F_phd").val(es.F_phd);
                $("#F_returnee").val(es.F_returnee);
                $("#F_promotPerson").val(es.F_promotPerson);
                $("#F_train").val(es.F_train);
            }
            else if (page == 4) {
                $("#F_lastRemain").val(es.F_lastRemain);
                $("#F_currentFee").val(es.F_currentFee);
                $("#F_xmzjf").val(es.F_xmzjf);
                $("#F_equiFee").val(es.F_equiFee);
                $("#F_equiTry").val(es.F_equiTry);
                $("#F_materialFee").val(es.F_materialFee);
                $("#F_assayFee").val(es.F_assayFee);
                $("#F_powerFee").val(es.F_powerFee);
                $("#F_trivalFee").val(es.F_trivalFee);
                $("#F_confeFee").val(es.F_confeFee);
                $("#F_cooperFee").val(es.F_cooperFee);
                $("#F_knowledgeFee").val(es.F_knowledgeFee);
                $("#F_employFee").val(es.F_employFee);
                $("#F_adviseFee").val(es.F_adviseFee);
                $("#F_manageFee").val(es.F_manageFee);
                $("#F_otherFee").val(es.F_otherFee);

                $("#F_feeMemo").val(es.F_feeMemo);
                $("#F_expendSum").val(es.F_expendSum);
                $("#F_endYearSum").val(es.F_endYearSum);

            }
            else if (page == 5) {
                $("#F_selfEvaluation").val(es.F_selfEvaluation);
                $("#F_plan").val(es.F_plan);
    

            }
            else if (page == 6) {
                $("#F_selfEvaluation").val(es.F_selfEvaluation);
                $("#F_plan").val(es.F_plan);
            }
            else if (page == 7) {
                $("#F_condition").val(social.F_condition);
            }
            else if (page == 8) {
                $("#F_totalInvent").val(social.F_totalInvent);
                $("#F_firstYear").val(social.F_firstYear);
                $("#F_secondYear").val(social.F_secondYear);
                $("#F_thirdYear").val(social.F_thirdYear);
                $("#F_fourthYear").val(social.F_fourthYear);
                $("#F_totalFund").val(social.F_totalFund);
                $("#F_totalFund2").val(social.F_totalFund);

                $("#F_firstYearFund").val(social.F_firstYearFund);
                $("#F_secondYearFund").val(social.F_secondYearFund);
                $("#F_thirdYearFund").val(social.F_thirdYearFund);
                $("#F_fouthYearFund").val(social.F_fouthYearFund);

                $("#F_totalOtherFund").val(social.F_totalOtherFund);
                $("#F_otherDepartment").val(social.F_otherDepartment);
                $("#F_schoolFund").val(social.F_schoolFund);
                $("#F_load").val(social.F_load);
                $("#F_otherFund").val(social.F_otherFund);

                $("#F_directCost").val(social.F_directCost);
                $("#F_directFund").val(social.F_directFund);
                $("#F_directBasis").val(social.F_directBasis);
                $("#F_laborCost").val(social.F_laborCost);
                $("#F_laborFund").val(social.F_laborFund);
                $("#F_laborBasis").val(social.F_laborBasis);
                $("#F_facilityCost").val(social.F_facilityCost);
                $("#F_facilityFund").val(social.F_facilityFund);
                $("#F_facilityBasis").val(social.F_facilityBasis);

                $("#F_buyFacilityCost").val(social.F_buyFacilityCost);
                $("#F_buyFacilityFund").val(social.F_buyFacilityFund);
                $("#F_buyFacilityBasis").val(social.F_buyFacilityBasis);
                $("#F_trialFacilityCost").val(social.F_trialFacilityCost);
                $("#F_trialFacilityFund").val(social.F_trialFacilityFund);
                $("#F_trialFacilityBasis").val(social.F_trialFacilityBasis);
                $("#F_repairCost").val(social.F_repairCost);
                $("#F_repairFund").val(social.F_repairFund);
                $("#F_repairBasis").val(social.F_repairBasis);

                $("#F_materialCost").val(social.F_materialCost);
                $("#F_materialFund").val(social.F_materialFund);
                $("#F_materialBasis").val(social.F_materialBasis);
                $("#F_assistCost").val(social.F_assistCost);
                $("#F_assistFund").val(social.F_assistFund);
                $("#F_assistBasis").val(social.F_assistBasis);
                $("#F_conferenceCost").val(social.F_conferenceCost);
                $("#F_conferenceFund").val(social.F_conferenceFund);
                $("#F_conferenceBasis").val(social.F_conferenceBasis);

                $("#F_tripCost").val(social.F_tripCost);
                $("#F_tripFund").val(social.F_tripFund);
                $("#F_tripBasis").val(social.F_tripBasis);
                $("#F_intellectualCost").val(social.F_intellectualCost);
                $("#F_intellectualFund").val(social.F_intellectualFund);
                $("#F_intellectualBasis").val(social.F_intellectualBasis);
                $("#F_internationCost").val(social.F_internationCost);
                $("#F_internationFund").val(social.F_internationFund);
                $("#F_internationBasis").val(social.F_internationBasis);
                $("#F_otherDirectCost").val(social.F_otherDirectCost);
                $("#F_otherDirectFund").val(social.F_otherDirectFund);
                $("#F_otherDirectBasis").val(social.F_otherDirectBasis);

                $("#F_indirectCost").val(social.F_indirectCost);
                $("#F_indirectFund").val(social.F_indirectFund);
                $("#F_indirectBasis").val(social.F_indirectBasis);
                $("#F_managerCost").val(social.F_managerCost);
                $("#F_managerFund").val(social.F_managerFund);
                $("#F_managerBasis").val(social.F_managerBasis);


                $("#F_depreciationCost").val(social.F_depreciationCost);
                $("#F_depreciationFund").val(social.F_depreciationFund);
                $("#F_depreciationBasis").val(social.F_depreciationBasis);
                $("#F_developCost").val(social.F_developCost);
                $("#F_developFund").val(social.F_developFund);
                $("#F_developBasis").val(social.F_developBasis);
                $("#F_developDirectCost").val(social.F_developDirectCost);
                $("#F_developDirectFund").val(social.F_developDirectFund);
                $("#F_developDirectBasis").val(social.F_developDirectBasis);
                $("#F_developIndirectCost").val(social.F_developIndirectCost);
                $("#F_developIndirectFund").val(social.F_developIndirectFund);
                $("#F_developIndirectBasis").val(social.F_developIndirectBasis);

                $("#F_totalCost").val(social.F_totalCost);
                $("#F_totalFundBasis").val(social.F_totalFundBasis);

            }
            else if (page == 9) {
                $("#F_recommendContent").val(social.F_recommendContent);
                $("#F_recommendName").val(social.F_recommendName);
                $("#F_recommendTitle").val(social.F_recommendTitle);
                $("#F_recommendExpert").val(social.F_recommendExpert);
                $("#F_recommendWorkspace").val(social.F_recommendWorkspace);
            }
            else if (page == 10) {
                $("#F_cooperator1Comment").val(social.F_cooperator1Comment);
                $("#F_cooperator1Date").val(ChangeDateFormat(social.F_cooperator1Date));
                $("#F_cooperator2Comment").val(social.F_cooperator2Comment);
                $("#F_cooperator2Date").val(ChangeDateFormat(social.F_cooperator2Date));
                $("#F_cooperator3Comment").val(social.F_cooperator3Comment);
                $("#F_cooperator3Date").val(ChangeDateFormat(social.F_cooperator3Date));
            }



        }
    });
}