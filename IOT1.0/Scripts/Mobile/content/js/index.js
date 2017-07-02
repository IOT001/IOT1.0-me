function search() {
    var openBtn = $(".search_input"),
		search_pop = $(".searchBox"),
		main_box = $(".main_box"),
		shadow = $(".shadow");
    openBtn.on("click", function () {
        main_box.hide();
        search_pop.show();
        search_pop.find("input").focus();
    });
    search_pop.find(".cancle").on("click", function () {
        search_pop.find("input").val("");
        search_pop.hide();
        main_box.show();
    });
    search_pop.find(".search_btn").on("click", function () {
        var keyword = $(this).siblings("input").val();
        if (keyword == "") {
            shadow.show();
            myComFn.myAlertShow("安装单号/手机号/姓名");
            return false;
        } else {
            window.location.href = host + "/customize/search.html?keyword=" + keyword;
        }
    });
}
$(document).ready(function () {
    search();
});