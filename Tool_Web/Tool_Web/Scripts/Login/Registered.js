new Vue({
    el: "#Registered",
    data: {
        LoginM: {
            ID: "",
            PASSWORD: "",
            ChkPASSWORD: "",
            NAME: "",
            EMAIL: "",
            SEX: "M",
        },
        NameVaild: "",
        EmailVaild: "",
        ChkUserDataURL: ""
    },
    methods: {
        IdVaildBlur(id, value, eVaild,Url) {
            let vm = this;
            let chk = true;
            if (Url != undefined)
               vm.ChkUserDataURL = Url;
            $('#' + eVaild).html('')
            $('#' + id).addClass('is-valid')
            $('#' + id).removeClass('is-invalid')
            if (id === 'ChkPASSWORD') {
                if (vm.LoginM.PASSWORD != vm.LoginM.ChkPASSWORD)
                    $('#' + eVaild).html('與密碼不相同')
                else
                    chk=false;
            }
            else if (value != "") {
                if (value.length < 5)
                    $('#' + eVaild).html('長度不可小於5碼')
                else if (/[\W]/g.test(value))
                    $('#' + eVaild).html('不可包含中文或特殊字元')
                else if (id === "ID") {
                    $.ajax(
                        {
                            type: 'POST', //傳送方式
                            url: vm.ChkUserDataURL, //傳送目的地
                            async: false,//同步傳遞
                            dataType: 'json', //資料格式
                            data: { model: vm.LoginM },
                            success: function (data) {
                                var Data = $.parseJSON(data)
                                if (Data.IS_Error === "Y")
                                    $('#' + eVaild).html(Data.MSG)
                                else
                                    chk = false;
                            },
                            error: function (key, value) {
                                $('#' + eVaild).html("使用者帳號無法驗證，請重新輸入")
                            }
                        }
                    )
                }
                else
                    chk = false;
            }
            else
                $('#' + eVaild).html('欄位不可空白');

            if (chk) {
                $('#' + id).addClass('is-invalid')
                $('#' + id).removeClass('is-valid')
            }
        },
        TextVaildBlur() {
            let vm = this;
            if (vm.LoginM.NAME != "") {
                $('#Name').addClass('is-valid')
                $('#Name').removeClass('is-invalid')
                vm.NameVaild = "";
                let IllegalString = "[`~!#$^&*()=|{}':;',\[\].<>/?~！#￥……&*（）——|{}【】‘；：”“'。，、？]‘'";
                for (i = 0; i < this.LoginM.NAME.length; i++) {
                    let s = this.LoginM.NAME.charAt(i);
                    if (IllegalString.indexOf(s) >= 0) {
                        vm.NameVaild = "不可包含特殊字元";
                        break;
                    }
                }
                if (vm.NameVaild === "")
                    return
            }
            if (vm.NameVaild === "")
                vm.NameVaild = "欄位不可空白";
            $('#Name').addClass('is-invalid')
            $('#Name').removeClass('is-valid')

        },
        EmailVaildBlur() {
            let vm = this;
            if (vm.LoginM.EMAIL != "") {
                $('#Email').addClass('is-valid')
                $('#Email').removeClass('is-invalid')
                vm.EmailVaild = "";
                var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (!regex.test(vm.LoginM.EMAIL))
                    vm.EmailVaild = "請填寫正確的E-MAIL格式";
                else
                    return
            }
            if (vm.EmailVaild === "")
                vm.EmailVaild = "欄位不可空白";
            $('#Email').addClass('is-invalid')
            $('#Email').removeClass('is-valid')
        },
        SetLoginData(Url) {
            let vm = this;
            vm.IdVaildBlur('ID', vm.LoginM.ID, 'IdVaild');
            vm.IdVaildBlur('PASSWORD', vm.LoginM.PASSWORD, 'PasswordVaild');
            vm.IdVaildBlur('ChkPASSWORD', vm.LoginM.ChkPASSWORD, 'ChkPasswordVaild');
            vm.TextVaildBlur();
            vm.EmailVaildBlur();
            var ar = ["ID", "PASSWORD", "ChkPASSWORD", "Name", "Email"];
            for (i = 0; i < ar.length; i++) {
                if ($('#' + ar[i]).hasClass('is-invalid')) {
                    alert("尚有資料有誤，請檢查")
                    return
                }
            }
            $.ajax(
                {
                    type: 'POST', //傳送方式
                    url: Url + "/SetLogin", //傳送目的地
                    dataType: 'json', //資料格式
                    data: { model: vm.LoginM },
                    success: function (data) {
                        var CardData = $.parseJSON(data)
                        if (CardData.IS_Error === "Y") {
                            if (CardData.MSG != "")
                                alert(CardData.MSG);
                            else
                                alert("註冊失敗，請重新註冊");
                        }
                        else {
                            location.href = Url+'/RegisteredVaild';
                        }

                    },
                    error: function (key, value) {
                        alert("發生錯誤，請重新註冊");
                    }
                }
            )
        }

    }
})
function Onresize() {
    var w = window.innerWidth;
    var h = window.innerHeight;
    var backgroundSizeW = "100% ";
    var backgroundSizeH = "100% ";
    let marginLeftW = "36%";
    this.EmptyDivheight = "35%";
    if (w < 800) {
        backgroundSizeW = "800px ";
    }
    $("body").css("background-size", backgroundSizeW + backgroundSizeH);
}