function openWin(src) {
    window.open(src, 'newwindow',
    'toolbar=no,menubar=no,scrollbars=no, alwaysRaised=yes,location=no, status=no, z-look=yes');
}

/* 
* 打开新窗口 
* f:链接地址 
* n:窗口的名称 
* w:窗口的宽度 
* h:窗口的高度 
* s:窗口是否有滚动条，1：有滚动条；0：没有滚动条 
*/
function openWinEx(f, n, w, h, s) {
    sb = s == "1" ? "1" : "0";
    l = (screen.width - w) / 2;
    t = (screen.height - h) / 2;
    sFeatures = "left=" + l + ",top=" + t + ",height=" + h + ",width=" + w
            + ",center=1,scrollbars=" + sb + ",status=0,directories=0,channelmode=0";
    openwin = window.open(f, n, sFeatures);
    if (!openwin.opener)
        openwin.opener = self;
    openwin.focus();
    return openwin;
} 

function refresh() {
    window.location.reload();
}

function refreshParent() {
    window.opener.location.href = window.opener.location.href;
}

function datepicker() {
    $('.datebox').not('.hasDatePicker').datepicker({
        yearRange: "1930:2030",
        dateFormat: "yy-mm-dd"
    });

    $("input[type='number']").removeClass("validate[required,custom[number]]");
    $("input[type='number']").addClass("validate[required,custom[number]]");
    $("input[type='number']").attr("value", "0");

}

function resizeWindow(width, height) {
    var iWidth = window.screen.availWidth;
    var iHeight = window.screen.availHeight;
    window.moveTo((iWidth - width) / 2, (iHeight - height) / 2);
    window.resizeTo(width, height);
}

function winClose() {
    window.opener = null;
    window.close();
}

function fullscreen(){
    var iWidth = window.screen.availWidth;
    var iHeight = window.screen.availHeight;

    window.moveTo(0,0);

    window.resizeTo(iWidth,iHeight);

}

function returnBack() {
    history.go(-1);
    return false;
}

function winCloseAndRefreshParent() {
    refreshParent();
    winClose();
}


function round(num) {
    return Math.round(num * 1000) / 1000;
}

function fillYear() {
    var date = new Date();
    var year = date.getFullYear();
    for (var i = -2; i < 10; i++)
        $("<option value=" + (year + i) + ">" + (year + i) + "年</option>").appendTo(".selYear");
}

function getTodayFormatStr() {
    var nowdate = new Date();
    var year = nowdate.getFullYear();
    var month = nowdate.getMonth();
    var date = nowdate.getDate();
    var restr = year + '-' + month + '-' + date;
    return restr;
  }

function ChangeDateFormat(cellval) {
    if (cellval == null)
        return "";
    var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    return date.getFullYear() + "-" + month + "-" + currentDate;
}

//var rowCount_wang = 1;
//function add_row_wang(tableid, rowid) {
//    var isChrome = navigator.userAgent.toLowerCase().match(/chrome/);
//    if (isChrome != null) {
//        add_row(tableid, rowid);
//        return;
//    }

//    var table = document.getElementById(tableid);
//    var row = document.getElementById(rowid);
//    var cells = row.cells;
//    if (tableid == "table_zykx" && table.rows.length > 2) {
//        alert("比较熟悉的专业学科 最多允许填写两项");
//        return;
//    }
//    rowCount_wang = rowCount_wang + 1;
//    var newrow = table.insertRow(table.rows.length);
//    var newrowid = "row_" + rowCount_wang;
//    newrow.id = newrowid;
//    for (var i = 0; i < (cells.length); i++) {
//        var cell = newrow.insertCell(i);
//        var html = cells[i].innerHTML;
//        if (html.indexOf("id") < 0)
//            continue;


//        alert(html);
//        var oldID = html.substring(html.indexOf("id="), html.indexOf("_rowNum")) + "_rowNum0";    //字段要求命名为如：F_scope_rowNum0
////        alert(oldID);
//        var newID = html.substring(html.indexOf("id="), html.indexOf("_rowNum")) + "_rowNum" + (table.rows.length - 2);
////        alert(newID);
//        html = html.replace(oldID, newID);


//        cell.innerHTML = html;
//        cell.className = cells[i].className;
//        var $datecontrol;
//        if (newID.indexOf("\"") == -1) {  //非IE下id="F_scope_rowNum0"  IE下id=F_scope_rowNum0
//            $datecontrol = $("#" + newID.substring(3, newID.length));
////            alert(newID.substring(3, newID.length));
//        }
//        else {
//            $datecontrol = $("#" + newID.substring(4, newID.length));
////            alert(newID.substring(4, newID.length));
//        }
//        

//        if ($datecontrol.attr("class") != null &&
//            ($datecontrol.attr("class") == "datebox hasDatepicker" || $datecontrol.attr("class") == "datebox")) {
//            $datecontrol.on('focus', function () {
//                var $this = $(this);
//                if (!$this.data('datepicker')) {
//                    $this.removeClass("hasDatepicker");
//                    $this.datepicker();
//                    $this.datepicker("show");
//                }
//            });
//        };
//    }
//    var lastcell = newrow.insertCell(cells.length);

//    lastcell.innerHTML = "<a href='javascript:delete_row(\"" + tableid + "\",\"" + newrowid + "\")' >删除</a>";
//    lastcell.className = cells[cells.length - 1].className;

//    


//}


//modify by wangchenyang 20141110 (解决动态增加行的IE兼容性)
var rowCount_wang = 1;
function add_row(tableid, rowid) {
    //    var isChrome = navigator.userAgent.toLowerCase().match(/chrome/);
    //    if (isChrome != null) {
    //        add_row(tableid, rowid);
    //        return;
    //    }

    var table = document.getElementById(tableid);
    var row = document.getElementById(rowid);
    var cells = row.cells;
    if (tableid == "table_zykx" && table.rows.length > 2) {
        alert("比较熟悉的专业学科 最多允许填写两项");
        return;
    }
    rowCount_wang = rowCount_wang + 1;
    var newrow = table.insertRow(table.rows.length);
    var newrowid = "row_" + rowCount_wang;
    newrow.id = newrowid;
    for (var i = 0; i < (cells.length); i++) {
        var cell = newrow.insertCell(i);
        var html = cells[i].innerHTML;
        if (html.indexOf("id") < 0)
            continue;


        //        alert(html);
        var oldID = html.substring(html.indexOf("id="));
        oldID = oldID.substring(oldID.indexOf("F_"), oldID.indexOf("0") + 1);
        //        alert(oldID);
        var newID = oldID.substring(0, oldID.indexOf("0")) + (table.rows.length - 2);
        //        alert(newID);
        html = html.replace(oldID, newID);


        cell.innerHTML = html;
        cell.className = cells[i].className;
        var $datecontrol = $("#" + newID);



        if ($datecontrol.attr("class") != null &&
            ($datecontrol.attr("class") == "datebox hasDatepicker" || $datecontrol.attr("class") == "datebox")) {
            $datecontrol.on('focus', function () {
                var $this = $(this);
                if (!$this.data('datepicker')) {
                    $this.removeClass("hasDatepicker");
                    $this.datepicker();
                    $this.datepicker("show");
                }
            });
        };
    }
    var lastcell = newrow.insertCell(cells.length);

    lastcell.innerHTML = "<a href='javascript:delete_row(\"" + tableid + "\",\"" + newrowid + "\")' >删除</a>";
    lastcell.className = cells[cells.length - 1].className;




}


//删除行
function delete_row(tableID, rowid) {
    if (window.confirm("是否确认删除？")) {
        var row = document.getElementById(rowid);
        var table = document.getElementById(tableID);
        if (row == null || table == null) {
            return;
        }
        table.deleteRow(row.rowIndex);
    }
    return;
}


function QueryString() {
    var name, value, i;
    var str = location.href;
    var num = str.indexOf("?");
    str = str.substr(num + 1);
    var arrtmp = str.split("&");
    for (i = 0; i < arrtmp.length; i++) {
        num = arrtmp[i].indexOf("=");
        if (num > 0) {
            name = arrtmp[i].substring(0, num);
            value = arrtmp[i].substr(num + 1);
            this[name] = value;
        }
    }
}

function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}   