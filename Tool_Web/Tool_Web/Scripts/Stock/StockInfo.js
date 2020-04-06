$(document).ready(function () {
    console.log(1);
    $(".datepicker").datepicker( "setStartDate", "2016/01/05");;
});     

function LoadApiData (Url) {
    $.ajax(
        {
            type: 'POST', //傳送方式
            url: Url + "Api/StockList", //傳送目的地
            dataType: 'json', //資料格式
            contentType: 'application/json',
            data: JSON.stringify({ SourceCode: null }),
            success: function (data) {
                var html = '<option value="Set">自選股</option> '
                if (data.IS_Error == "N") {
                    data.Data.forEach(function (item) {
                        html += '<option value="' + item.Value+'">' + item.Text+'</option> '
                    })
                    $('#CategorySelect').html(html)
                }
                else
                    alert("網頁顯示錯誤");
            },
            error: function (key, value) {
                alert("網頁格式出現未知錯誤");
            }
        }
    )
}