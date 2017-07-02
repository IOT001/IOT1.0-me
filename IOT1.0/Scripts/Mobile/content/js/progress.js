/*状态切换*/
function tabChange() {
    var tabs = $(".topFixed li"),
		progressCom = $(".mainCon .progressCom"),
		index = 0;
    tabs.on("click", function () {
        index = $(this).index();
        showNowIndex(progressCom.eq(index + 1));
        $(this).siblings("li").removeClass("selected");
        $(this).addClass("selected");
        $(".backTop").hide();
        progressCom.hide();
        progressCom.eq(index + 1).show();
    });
}

//显示当前是第几条
function showNowIndex(parent) {
    var doc = $(document),
        win = $(window),
        elements = null,
        len = 0,
        docTop = 0,
        i = 0,
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

/*联系方式点击*/
function contact() {
    $(".waitInstall").on("click", ".c_bottom .left", function (e) {
        var ev = e || window.event,
			parent = $(this).closest(".myBox");
        ev.stopPropagation();
        parent.siblings(".myBox").each(function () {
            $(this).find(".c_bottom .left span").show();
            $(this).find(".c_bottom .left .contact").hide();
        });
        if ($(this).find("span").is(":hidden")) {
            $(this).find("span").show();
            $(this).find(".contact").hide();
        } else {
            $(this).find("span").hide();
            $(this).find(".contact").show();
        }
    });
    $("body").on("click", function () {
        $(".waitInstall").find(".c_bottom .left span").show();
        $(".waitInstall").find(".c_bottom .left .contact").hide();
    });
}

//数据加载是否完成
var loadcompleted = true;

//分页数据信息
var status = parseInt($("#status").val());
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
                showNowIndex($(".progressCom"));
            }
        });
    }
}

function getMoreData(pageindex) {
    loadcompleted = false;
    $.getJSON(ajaxUrl, { datatype: "progress", status: status, pageindex: pageindex, pagesize: pagesize }, function (json) {
        var _html = "";
        var pagedata = json.pagedata;
        var totalpages = json.totalpages;
        if (status == 1) {//待安装
            _html = createWaitInstallHtml(pagedata);
        } else if (status == 2) {//待审核
            _html = createWaitAuditingHtml(pagedata);
        } else if (status == 3) {//已完工
            _html = createWorkCompletedHtml(pagedata);
        }
        if (_html.length > 0)
            $(".progressCom .loadmore").before(_html);
        loadcompleted = true;
    });
    if (totalpages <= pageindex)
        $(".progressCom .loadmore").empty().text("已全部加载完毕");
}

//创建待安装HTML片段
function createWaitInstallHtml(pagedata) {
    var html = "";
    if (pagedata != null && pagedata.length > 0) {
        $(pagedata).each(function (idx, m) {
            var position = escape(m.projectaddress);
            var cityname = escape(m.cityname);
            var mapurl = host + "/customize/location.open?position=" + position + "&cityname=" + cityname;
            var drawingurl = host + "/customize/drawing.html?id=" + m.installcode;
            var uploadurl = host + "/customize/upload.html?id=" + m.installcode;

            html += "<div class='myBox'>";
            html += "	<div class='title'>";
            html += "		<div class='w600 clearfloat'>";
            html += "			<p>安装单号:<em>" + m.installcode + "</em></p>";
            html += "			<span>" + m.isovertime + "</span>";
            html += "		</div>";
            html += "	</div>";
            html += "	<div class='work'>";
            html += "		<div class='line line01'>安装类型:<span>" + m.typename + "</span></div>";
            html += "		<div class='line line02'>预约时间:<span>" + m.installtimestr + "</span>";

            if (m.isdelayed == 1) {
                html += "				<em><img src='" + host + "/areas/customize/content/images/comimg/new.png' alt='' /></em>";
            }

            html += "		</div>";
            html += "		<div class='line line03'>业主:<span>" + m.customername + "</span>设计师:<span>" + m.dzdesigner + "</span></div>";
            html += "		<div class='line line04'>工长:<span>" + m.corpname + "</span></div>";
            html += "		<div class='line line05 clearfloat'>";
            html += "			<div class='left'>地址:</div>";
            html += "			<div class='right'>";

            html += "				" + m.projectaddress;
            if (m.projectaddress != undefined && m.projectaddress.length > 0) {
                html += "					<a href='" + mapurl + "' class='map'>";
                html += "						<img src='" + host + "/areas/customize/content/images/comimg/map.png' alt='' />";
                html += "					</a>";
            }

            html += "			</div>";
            html += "		</div>";
            html += "	</div>";

            html += "	<div class='c_bottom'>";
            html += "		<div class='w600 clearfloat'>";
            html += "			<div class='left'>";
            html += "				联系方式<span><img src='" + host + "/areas/customize/content/images/comimg/arrowBottom.png' alt='' /></span>";
            html += "				<div class='contact'>";
            html += "					<i>&nbsp;</i>";
            if (m.corptel) {
                html += "					<a href='tel:" + m.corptel + "' class='first'>联系工长</a>";
            }
            if (m.customerphone) {
                html += "					<a href='tel:" + m.customerphone + "' class='first'>联系业主</a>";
            }
            if (m.dzdesignerphone) {
                html += "					<a href='tel:" + m.dzdesignerphone + "'>联系设计师</a>";
            }
            html += "				</div>";
            html += "			</div>";
            html += "			<div class='right'>";
            html += "				<a href='" + drawingurl + "'>查看图纸</a>";
            html += "				<a href='" + uploadurl + "'>确认安装</a>";
            html += "			</div>";
            html += "		</div>";
            html += "	</div>";
            html += "</div>";
        });
    }
    return html;
}

//创建待审核HTML片段
function createWaitAuditingHtml(pagedata) {
    var html = "";
    if (pagedata != null && pagedata.length > 0) {
        $(pagedata).each(function (idx, m) {
            html += "<div class='myBox'>";
            html += "	<div class='title'>";
            html += "		<div class='w600 clearfloat'>";
            html += "			<p>安装单号:<em>" + m.installcode + "</em></p>";
            html += "		</div>";
            html += "	</div>";
            html += "	<div class='work'>";
            html += "		<div class='line line01'>安装类型:<span>" + m.typename + "</span></div>";
            html += "		<div class='line line02'>安装时间:<span>" + m.submittimestr + "</span></div>";
            html += "		<div class='line line03'>业主:<span>" + m.customername + "</span>设计师:<span>" + m.dzdesigner + "</span></div>";
            html += "		<div class='line line04'>工长:<span>" + m.corpname + "</span></div>";
            html += "		<div class='line line05'>";
            html += "			<div class='left'>地址:</div>";
            html += "			<div class='right'>";
            html += "				" + m.projectaddress;
            html += "			</div>";
            html += "		</div>";

            if (m.reformcontents != undefined && m.reformcontents.length > 0) {
                html += "		<div class='line line05 clearfloat'>";
                html += "			<div class='left'>备注:</div>";
                html += "			<div class='right'>";
                html += "				" + m.reformcontents;
                html += "			</div>";
                html += "		</div>";
            }

            html += "	</div>";
            html += "</div>";

        });
    }
    return html;
}

//创建已完工HTML片段
function createWorkCompletedHtml(pagedata) {
    var html = "";
    if (pagedata != null && pagedata.length > 0) {
        $(pagedata).each(function (idx, m) {
            html += "<div class='myBox'>";
            html += "	<div class='title'>";
            html += "		<div class='w600 clearfloat'>";
            html += "			<p>安装单号:<em>" + m.installcode + "</em></p>";
            html += "		</div>";
            html += "	</div>";
            html += "	<div class='work'>";
            html += "		<div class='line line01'>安装类型:<span>" + m.typename + "</span></div>";
            html += "		<div class='line line02'>安装时间:<span>" + m.submittimestr + "</span></div>";
            html += "		<div class='line line02'>审核时间:<span>" + m.auditingtimestr + "</span></div>";
            html += "		<div class='line line03'>业主:<span>" + m.customername + "</span>设计师:<span>" + m.dzdesigner + "</span></div>";
            html += "		<div class='line line04'>工长:<span>" + m.corpname + "</span></div>";
            html += "		<div class='line line05 clearfloat'>";
            html += "			<div class='left'>地址:</div>";
            html += "			<div class='right'>";
            html += "				" + m.projectaddress;
            html += "			</div>";
            html += "		</div>";
            html += "	</div>";
            html += "</div>";
        });
    }
    return html;
}

$(document).ready(function () {
    //tabChange();
    contact();
    loadmore();
    showNowIndex($(".progressCom"));

    $(".backTop").on("click", function () {
        $("html,body").animate({
            scrollTop: 0
        }, 600);
    })
});