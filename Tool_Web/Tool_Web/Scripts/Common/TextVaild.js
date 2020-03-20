function BlurEmailVaild(Id, vaildId) {
    if ($(Id)[0].value != "") {
        var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!regex.test($(Id)[0].value))
            $(vaildId).html("請填寫正確的E-MAIL格式");
        else {
            $(Id).addClass('is-valid')
            $(Id).removeClass('is-invalid')
            $(vaildId).css('display', 'none')
            return
        }
    }
    else
        $(vaildId).html("欄位不可空白");

    $(vaildId).css('display', 'block')
    $(Id).addClass('is-invalid')
    $(Id).removeClass('is-valid')
}

function ChkSubmit(e) {
    var Ischeck = true;
    e.forEach(function (val, index) {
        if ($(val).hasClass('is-invalid')) {
            $(val).focus();
            Ischeck = false;
            return false;//= break
        }
    })
    return Ischeck;
}
function onFocus(e) {
    $(e).focus();
}