window.onload = CommonLoad();
function CommonLoad() {
    var ID = "";
    if (localStorage.getItem('token') == 'ImLogin') {
        ID = localStorage.getItem('ID')
    }
    var dev = "";
    if (window.location.host == 'localhost:8080')
        dev = "/DTW_Business"
    $.post(window.location.origin + dev + '/Home/SetSession', { 'ID': ID }, function (data) {
        if (data != "Y") {
            alert("網頁閒置過久，系統已自動登出");
            localStorage.removeItem('token');
        }
    });
}
function SetStyle(Url) {
    console.log(0);
    $.ajax(
        {
            type: 'POST', //傳送方式
            url: Url, //傳送目的地
            async: false,//同步傳遞
            dataType: 'json', //資料格式
            data: { UID: localStorage.getItem("UID") },
            success: function (data) {
                if (Data.IS_Error == "N")
                    return data
                else
                    alert("網頁顯示錯誤");
            },
            error: function (key, value) {
                alert("網頁格式有未知錯誤");
            }
        }
    )
}