window.onload = function () {
    //CommonLoad();
    var LoginShow = 'none', LoginNoShow ='inline-block';
    if (localStorage.getItem('token') == 'ImLogin') {
        LoginShow = 'block'; LoginNoShow = 'none';
        $('.mr-auto').html(localStorage.getItem('UID_NAME') +' 您好')
        $('.toast').toast('show')
    }
    $('.LoginShow').css('display', LoginShow)
    $('.LoginNoShow').css('display', LoginNoShow)
    /*
    $('.LoginShow').css('display', 'block')
    $('.LoginNoShow').css('display', 'none')
    $('.toast').toast('show')
    $('.mr-auto').html('帥哥 您好')*/
}

function LoginOut() {
    $('.LoginShow').css('display', 'none')
    $('.LoginNoShow').css('display', 'inline-block')
    localStorage.removeItem('token')
}

new Vue({
    el: "#Home",
    data: {
        TEST:'GGGGG'
    },
    methods: {
        CssHref(Apiurl,cssWH) {
            var url = Apiurl+'Api/GetHtmlCss/Indexy$xz/' + cssWH
            if (localStorage.getItem('token') == 'ImLogin')
                url = Apiurl+'Api/GetHtmlCss/Indexy$x' + localStorage.getItem('UID') +'/'+ cssWH
            return url;
        }
    }
})