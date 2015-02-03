var modified = false;

function initialAttachment(pageNum)
{
    $.ajax({
        type: 'POST',
        url: 'Services/AttachmentService.ashx?method=initialPage',
        data: "pageNum=" + pageNum ,
        success: function (data) {
            if(pageNum ==1)
            {
                for(var i =0; i< 13; i++)
                    $("#attachment" + i).text(data[i]);
            }
            else if(pageNum ==2)
            {

            }
        }
    });
}

// 用于初始化验收资料页面 add by cy [20140921]
//>F_attachmenttype值为3x
function initialApplicantAttachment(pageNum) {
    $.ajax({
        type: 'POST',
        url: 'Services/AttachmentService.ashx?method=initialPageEx',
        data: {        
             pageNum : pageNum,
             attachmentTypes : '30, 31, 32, 33, 34'
        },
        dataType : 'json',
        success: function (data) {
            $(data).each(function (i) {
                $("#attachment" + data[i].type).text(data[i].count);
            });
        }
    });
}