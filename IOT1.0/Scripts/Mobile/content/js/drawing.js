function downLoad() {
    var originSrc = "";
    $(".swiper-container").on("click", ".download", function () {
        $(this).hide();
        originSrc = $(this).closest(".swiper-slide").data("origin");
        $(this).siblings(".imgContent").css({
            background: "#000 url(" + originSrc + ") center center no-repeat",
            backgroundSize: "contain"
        });
        $(this).siblings(".imgContent").find("img").attr("src", originSrc);
    });
}
function boxAdd() {
    var box = $(".drawing"), lis = box.find("li"), len = lis.length, html = "", imgSrc = null;
    var originSrc = "", fileSize = "";

    for (var i = 0 ; i < len ; i++) {
        imgSrc = box.find("li").eq(i).find("img").attr("src");
        originSrc = box.find("li").eq(i).data("origin");
        fileSize = box.find("li").eq(i).data("size");

        html += "<div class='swiper-slide' data-origin='" + originSrc + "'>" +
					"<div class='imgContent' style='background: #000 url(" + imgSrc + ") center center no-repeat;background-size:contain;'><img src='" + imgSrc + "'></div>" +
	                "<div class='download'>" +
	                   "<a href='javascript:;' class='downImg'>查看原图(<span>" + fileSize + "</span>M)</a>" +
	                "</div>" +
	              "</div>";
    }
    return html;
}
$(function () {
    var flag = true;
    var imgBox = $(".imgMsg");
    var lis = $(".drawing ul li");
    var swiper;
    lis.on("click", function () {
        if (lis.length > $(".swiper-wrapper .swiper-slide").length) {
            $(".swiper-wrapper").append(boxAdd());
        }
        var enterIndex = $(this).index();
        $(".seeImg").show();
        if (flag) {
            swiper = new Swiper('.swiper-container', {
                initialSlide: enterIndex,
                autoplay: false,
                loop: false,
                preventClicks: false,
                onInit: function (swiper) {
                    imgBox.find(".allIndex").html(swiper.slides.length);
                },
                onSlideChangeEnd: function (swiper) {
                    imgBox.find(".curIndex").html(swiper.activeIndex + 1);
                }
            });
            flag = false;
        }
        swiper.updateSlidesSize();
        imgBox.find(".allIndex").html(swiper.slides.length);
        swiper.slideTo(enterIndex, 10);
    });
    $("body").on("click", ".swiper-slide .imgContent", function () {
        $(this).closest(".seeImg").hide();
    });
    downLoad();
})