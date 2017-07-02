function handleChar(obj) {
    var textCon = obj.find(".right > span"), num = 74, openNum = 69,
		otext = textCon.text(),
		bytesCount = 0,
		str = "";
    textCon.data("text", otext);
    for (var i = 0; i < otext.length; i++) {
        if (otext.charCodeAt(i) > 255) {
            bytesCount += 2;
        } else {
            bytesCount += 1;
        }
        if (bytesCount < openNum) {
            str += otext.charAt(i);
        }
    }
    if (bytesCount > num) {
        textCon.text(str);
        textCon.siblings("a").addClass("open");
    } else {
        textCon.siblings("a").hide();
    }
}

function openOrClose() {
    var textSpan = null, text = null;
    $(document).on("click", ".line08 .right a", function () {
        textSpan = $(this).siblings("span");
        text = textSpan.text();
        textSpan.text(textSpan.data("text"));
        textSpan.data("text", text);
        if ($(this).hasClass("open")) {
            $(this).removeClass("open").addClass("upfload");
            $(this).find("em").text("收起>")
        } else if ($(this).hasClass("upfload")) {
            $(this).removeClass("upfload").addClass("open");
            $(this).find("em").text("展开>");
        }
    });
}

//显示当前是第几条
function showNowIndex() {
    var doc = $(document),
        win = $(window),
        elements = null,
        len = 0,
        docTop = 0,
        i = 0,
        parent = $(".progressCom"),
        backTopCon = $(".backTop"),
        marginBotom = Math.ceil(parseFloat(parent.find(".myBox").eq(0).css("margin-bottom")));
    $(window).on("scroll", throttle(function () {
        len = parent.find(".myBox").length;
        backTop = win.height() + doc.scrollTop();
        for (i = 0; i < len; i++) {
            var target = parent.find(".myBox").eq(i);
            if (target.offset().top <= backTop && (target.offset().top + target.height() + marginBotom) >= backTop) {
                backTopCon.find("p").eq(0).find("span").text(i + 1);
                if ((i + 1) >= 3) {
                    backTopCon.show();
                } else {
                    backTopCon.hide();
                }
            }
        }
        if (len >= 1 && (parent.find(".myBox").eq(i - 1).offset().top + parent.find(".myBox").eq(i - 1).height() + marginBotom) < backTop) {
            backTopCon.find("p").eq(0).find("span").text(len);
        }
        if (len == 0) {
            backTopCon.find("p").eq(0).find("span").text(len);
        }
        backTopCon.find("p").eq(1).find("span").text(len);
    }, 50, 100)).trigger("scroll");
}

//数据加载是否完成
var loadcompleted = true;

//分页数据信息
var totalrows = parseInt($("#totalrows").val());
var totalpages = parseInt($("#totalpages").val());
var pageindex = parseInt($("#pageindex").val());
var pagesize = parseInt($("#pagesize").val());

//下拉加载
function loadmore() {
    //alert("totalrows:" + totalrows + ", totalpages:" + totalpages + ", pageindex:" + pageindex + ", totalpages > pageindex:" + (totalpages > pageindex));
    if (totalpages > pageindex) {
        myComFn.isBottom(function () {
            if (loadcompleted) {
                pageindex = pageindex + 1;
                getMoreData(pageindex);
                showNowIndex();
            }
        });
    }
}

function getMoreData(pageindex) {
    loadcompleted = false;
    $.getJSON(ajaxUrl, { datatype: "settlement", pageindex: pageindex, pagesize: pagesize }, function (json) {
        var _html = "";
        var pagedata = json.pagedata;
        var totalpages = json.totalpages;
        _html = createSettleHtml(pagedata);
        if (_html.length > 0)
            $(".progressCom .loadmore").before(_html);
        loadcompleted = true;
    });
    if (totalpages <= pageindex)
        $(".progressCom .loadmore").empty().text("已全部加载完毕");
}

function createSettleHtml(pagedata) {
    var html = "";
    if (pagedata != null && pagedata.length > 0) {
        $(pagedata).each(function (idx, m) {
            html += "<div class='myBox'>";
            html += "	<div class='title'>";
            html += "		<div class='w600 clearfloat'>";
            html += "			<p>安装单号:<em>" + m.installcode + "</em></p>";
            html += "			<span>" + (m.settlestate == 1 ? "已结算" : "未结算") + "</span>";
            html += "		</div>";
            html += "	</div>";
            html += "	<div class='work'>";
            html += "		<div class='box'>";

            if (m.settlestate == 1) {
                html += "			<div class='tTopcon'>";
                html += "				<div class='line line01'>结算金额:<span>" + parseFloat(m.settlefee).toFixed(2) + "</span>元</div>";
                html += "				<div class='line line02'>结算时间:<span>" + (m.settletimestr.indexOf('1900') != -1 ? "-" : m.settletimestr) + "</span></div>";
                html += "			</div>";
            }
           

            html += "			<div class='line line03'>安装类型:<span>" + m.installtype + "</span></div>";
            html += "			<div class='line line04'>完工时间:<span>" + m.auditingtimestr + "</span></div>";
            //if (m.settlestate == 1) {
            //    html += "			<div class='line line04'>完工时间:<span>" + m.auditingtimestr + "</span></div>";
            //} else {
            //    html += "			<div class='line line04'>安装时间:<span>" + m.installtimestr + "</span></div>";
            //}

            html += "			<div class='line line05'>业主:<span>" + m.customername + "</span>设计师:<span>" + m.dzdesigner + "</span></div>";
            html += "			<div class='line line06'>工长:<span>" + m.corpname + "</span></div>";
            html += "			<div class='line line07'>";
            html += "				<div class='left'>地址:</div>";
            html += "				<div class='right'>" + m.address + "</div>";
            html += "			</div>";

            if (m.settlestate == 1 && m.settleremark.length > 0) {
                html += "				<div class='line line08'>";
                html += "					<div class='left'>备注:</div>";
                html += "					<div class='right'><span>" + m.settleremark + "</span><a href='javascript:;'><span>...</span><em>展开></em></a></div>";
                html += "				</div>";
            }

            html += "		</div>";
            html += "	</div>";
            html += "</div>";
        });
    }
    return html;
}

$(document).ready(function () {
    handleChar($(".line08"));
    openOrClose();
    showNowIndex();
    loadmore();

    $(".backTop").on("click", function () {
        $("html,body").animate({
            scrollTop: 0
        }, 600);
    })
});