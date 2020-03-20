

function GetCard() {
    //AjaxReturnValue("GAME1", "GetCard", null)
    $.ajax(
        {
            type: 'POST', //傳送方式
            url: '/' + "GAME1" + '/' + "GetCard", //傳送目的地
            dataType: 'json', //資料格式
            data: null,
            success: function (data) {
                var Shtml="";
                $("#NEXT").css("display", "block");
                var List = $.parseJSON(data)
                $.each(List.GetCardRList, function (index, value) {
                    var i = index + 1;
                    //Shtml += '<button name = Button' + i + ' onclick="SetCard(' + i + ')">' + value.CardName + '</button> ';
                    Shtml += '<a id="P'+i+' href="Pick(' + i + ')"><img src="data: image / jpg; base64,' + value.PATH+'"></a> ';
                });
                Shtml += '     <button name =OK onclick="SetCard()"> 確定選擇</button> '
                $("#NEUTRAL").html(Shtml);
            },
            error: function (key,value) {
                alert("發生錯誤");
            }
        }
    )
}

function Pick(i) {
    $.ajax(
        {
            type: 'POST', //傳送方式
            url: '/' + "GAME1" + '/' + "Pick", //傳送目的地
            dataType: 'json', //資料格式
            data: { Index: i },
            success: function (data) {
                var CardData = $.parseJSON(data)
                if (CardData.IS_Error === "Y")
                {
                    alert("操作指令有誤，請重新選擇");
                    window.location.reload()
                }
                else if (CardData.IS_Error === "0") {
                }
                else
                {
                    var name = "P" + i;
                    if (CardData.IS_PICK==="Y")
                        document.getElementById(name).src = 'data: image / jpg; base64,' + CardData.PICK_PATH 
                    else
                        document.getElementById("P" + i).src = 'data: image / jpg; base64,' + CardData.PATH
                }
            },
            error: function (key, value) {
                alert("發生錯誤");
            }
        }
    )
}

function SetCard(Btn, i) {
    $.ajax(
        {
            type: 'POST', //傳送方式
            url: '/' + "GAME1" + '/' + "SetCard", //傳送目的地
            dataType: 'json', //資料格式
            data: null,
            success: function (data) {
                var List = $.parseJSON(data)
                if (List.IS_Error === "Y") {
                    alert("操作指令有誤，請重新選擇");
                    window.location.reload()
                }
                $.each(List.GetCardRList, function (index, value) {
                    var i = index + 1;
                    //Shtml += '<button name = Button' + i + ' onclick="SetCard(' + i + ')">' + value.CardName + '</button> ';
                    Shtml += '<a id="P' + i + ' href="Pick(' + i + ')"><img src="data: image / jpg; base64,' + value.PATH + '"></a> ';
                });
                Shtml += '     <button name =OK onclick="SetCard()"> 確定選擇</button> '
                $("#NEUTRAL").html(Shtml);
            },
            error: function (key, value) {
                alert("發生錯誤");
            }
        }

}

function AjaxReturnValue(controllerName, actionName, DataR) {
    $.ajax(
        {
            type: 'POST', //傳送方式
            url: '/' + controllerName + '/' + actionName, //傳送目的地
            dataType: 'json', //資料格式
            data: DataR,
            success: function () {

            },
            error: function () {
                alert("發生錯誤");
            }
        }
    )
}