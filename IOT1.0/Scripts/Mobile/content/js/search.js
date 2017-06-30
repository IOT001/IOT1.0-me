//搜索
function search() {
    var searchBox = $(".search"),
        searchInput = $(".search .search_input input"),
        history = $(".history"),
        searchResult = $(".search_result");
    searchInput.on("click", function () {
        history.show();
        searchResult.hide();
        //searchInput.focus();
    });
    searchBox.find(".cancle").on("click", function () {
        searchInput.val("");
        if ($(".history:visible").length>0) {
            history.hide();
            searchResult.show();
        }
        else {
            window.location.href = workUrl;
        }
    });
    //搜索按钮
    searchBox.find(".search_btn").on("click", function () {
        $(".search_result").hide();
        var keyword = $(this).siblings("input").val();
        if (keyword == "") {
            $(".shadow").show();
            myComFn.myAlertShow("安装单号/手机号/姓名");
            return false;
        } else {
            window.location.href = host + "/customize/search.html?keyword=" + keyword;
        }
    });
}

/*联系方式点击*/
function contact() {
    $(".search_result").on("click", ".c_bottom .left", function (e) {
        var ev = e || window.event,
            parent = $(this).closest(".box");
        ev.stopPropagation();
        parent.siblings(".box").each(function () {
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
        $(".search_result").find(".c_bottom .left span").show();
        $(".search_result").find(".c_bottom .left .contact").hide();
    });
}

function handleChar(obj) {
    obj.each(function () {
        var textCon = $(this).find(".right > span"), num = 70, openNum = 65,
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
    });

}

function openOrClose() {
    var textSpan = null, text = null;
    $(document).on("click", ".remark .right a", function () {
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
        parent = $(".mainCon"),
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
var keyword = parseInt($("#txtKeyword").val());
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
    $.getJSON(ajaxUrl, { datatype: "search", keyword: keyword, pageindex: pageindex, pagesize: pagesize }, function (json) {
        var _html = "";
        var pagedata = json.pagedata;
        var totalpages = json.totalpages;
        _html = createSearchResultHtml(pagedata);
        if (_html.length > 0)
            $(".search_result .loadmore").before(_html);
        loadcompleted = true;
    });
    if (totalpages <= pageindex)
        $(".search_result .loadmore").empty().text("已全部加载完毕");
}

function createSearchResultHtml(pagedata) {
    var html = "";
    if (pagedata != null && pagedata.length > 0) {
        $(pagedata).each(function (idx, m) {
            var position = escape(m.projectaddress);
            var cityname = escape(m.cityname);

            var mapurl = host + "/customize/location.open?position=" + position + "&cityname=" + cityname;
            var drawingurl = host + "/customize/drawing.html?id=" + m.installcode;
            var uploadurl = host + "/customize/upload.html?id=" + m.installcode;
            var reuploadurl = host + "/customize/upload.html?id=" + m.installcode + "&actiontype=reupload";

            var isovertime = "";
            var statustext = " - ";

            html += "<div class='myBox'>";
            html += "	<div class='title'>";
            html += "		<div class='w600 clearfloat'>";
            html += "			<p>安装单号:<em>" + m.installcode + "</em></p>";
            if (m.status == '1' || m.status == '2') {
                statustext = '待安装';
                if (m.status == '2' && m.isovertime == '超时') {
                    html += "<b>" + m.isovertime + "</b>";
                }
            } else if (m.status == '3') {
                statustext = (m.auditingstate == 0) ? '待审核' : '待整改';
            } else if (m.status == '4') {
                statustext = '未结算';
            } else if (m.status == '5') {
                statustext = '已结算';
            }
            html += "			<span>" + statustext + "</span>";
            html += "		</div>";
            html += "	</div>";

            if (m.status == '1' || m.status == '2') {

                html += "		<div class='work'>";
                html += "			<div class='box box_r'>";
                html += "				<div class='line line03'>安装类型:<span>" + m.typename + "</span></div>";
                html += "				<div class='line line04'>预约时间:<span>" + m.installtimestr + "</span></div>";
                html += "				<div class='line line05'>业主:<span>" + m.customername + "</span> 设计师:<span>" + m.dzdesigner + "</span></div>";
                html += "				<div class='line line06'>工长:<span>" + m.corpname + "</span></div>";
                html += "				<div class='line line07'>";
                html += "					<div class='left'>地址:</div>";
                html += "					<div class='right'>";

                html += "						" + m.projectaddress;
                if (m.projectaddress != undefined && m.projectaddress.length > 0) {
                    html += "					<a href='" + mapurl + "' class='map'>";
                    html += "						<img src='" + host + "/areas/customize/content/images/comimg/map.png' alt='' />";
                    html += "					</a>";
                }

                html += "					</div>";
                html += "				</div>";
                html += "			</div>";
                html += "		</div>";

                html += "		<div class='c_bottom'>";
                html += "			<div class='w600 clearfloat'>";
                html += "				<div class='left'>";
                html += "					联系方式<span><img src='" + host + "/areas/customize/content/images/comimg/arrowBottom.png' alt='' /></span>";
                html += "					<div class='contact'>";
                html += "						<i>&nbsp;</i>";
                if (m.corptel) { html += "						<a href='tel:" + m.corptel + "' class='first'>联系工长</a>"; }
                if (m.customerphone) { html += "						<a href='tel:" + m.customerphone + "' class='first'>联系业主</a>"; }
                if (m.dzdesignerphone) { html += "						<a href='tel:" + m.dzdesignerphone + "'>联系设计师</a>"; }
                html += "					</div>";
                html += "				</div>";
                html += "				<div class='right'>";
                html += "					<a href='" + drawingurl + "'>查看图纸</a>";
                html += "					<a href='" + uploadurl + "'>确认安装</a>";
                html += "				</div>";
                html += "			</div>";
                html += "		</div>";

            } else if (m.status == '3') {

                if (m.auditingstate == 0) {

                    html += "			<div class='work'>";
                    html += "				<div class='box box_r'>";
                    html += "					<div class='line line03'>安装类型:<span>" + m.typename + "</span></div>";
                    html += "					<div class='line line04'>安装时间:<span>" + m.submittimestr + "</span></div>";
                    html += "					<div class='line line05'>业主:<span>" + m.customername + "</span> 设计师:<span>" + m.dzdesigner + "</span></div>";
                    html += "					<div class='line line06'>工长:<span>" + m.corpname + "</span></div>";
                    html += "					<div class='line line07'>";
                    html += "						<div class='left'>地址:</div>";
                    html += "						<div class='right'>" + m.projectaddress + "</div>";
                    html += "					</div>";

                    if (m.reformcontents != undefined && m.reformcontents.length > 0) {
                        html += "						<div class='line line02 remark'>";
                        html += "							<div class='left'>备注:</div>";
                        html += "							<div class='right'>";
                        html += "								<span>" + m.reformcontents + "</span>";
                        html += "								<a href='javascript:;' class='open'><span>...</span><em>展开&gt;</em></a>";
                        html += "							</div>";
                        html += "						</div>";
                    }

                    html += "				</div>";
                    html += "			</div>";

                } else if (m.auditingstate == 1) {

                    html += "			<div class='work'>";
                    html += "				<div class='box'>";
                    html += "					<div class='tTopcon'>";
                    html += "						<div class='line line01'>审核时间:<span>" + m.auditingtimestr + "</span></div>";
                    html += "						<div class='line line02 remark'>";
                    html += "							<div class='left'>备注:</div>";
                    html += "							<div class='right'>";
                    html += "								<span>" + m.reformcontents + "</span>";
                    html += "								<a href='javascript:;' class='open'><span>...</span><em>展开&gt;</em></a>";
                    html += "							</div>";
                    html += "						</div>";
                    html += "					</div>";
                    html += "					<div class='line line03'>安装类型:<span>" + m.typename + "</span></div>";
                    html += "					<div class='line line04'>安装时间:<span>" + m.submittimestr + "</span></div>";
                    html += "					<div class='line line05'>业主:<span>" + m.customername + "</span> 设计师:<span>" + m.dzdesigner + "</span></div>";
                    html += "					<div class='line line06'>工长:<span>" + m.corpname + "</span></div>";
                    html += "					<div class='line line07'>";
                    html += "						<div class='left'>地址:</div>";
                    html += "						<div class='right'>";

                    html += "							" + m.projectaddress;
                    if (m.projectaddress != undefined && m.projectaddress.length > 0) {
                        html += "					<a href='" + mapurl + "' class='map'>";
                        html += "						<img src='" + host + "/areas/customize/content/images/comimg/map.png' alt='' />";
                        html += "					</a>";
                    }

                    html += "						</div>";
                    html += "					</div>";
                    html += "				</div>";
                    html += "			</div>";
                    html += "";

                    html += "		<div class='c_bottom'>";
                    html += "			<div class='w600 clearfloat'>";
                    html += "				<div class='left'>";
                    html += "					联系方式<span><img src='" + host + "/areas/customize/content/images/comimg/arrowBottom.png' alt='' /></span>";
                    html += "					<div class='contact'>";
                    html += "						<i>&nbsp;</i>";
                    if (m.corptel) { html += "						<a href='tel:" + m.corptel + "' class='first'>联系工长</a>"; }
                    if (m.customerphone) { html += "				<a href='tel:" + m.customerphone + "' class='first'>联系业主</a>"; }
                    if (m.dzdesignerphone) { html += "				<a href='tel:" + m.dzdesignerphone + "'>联系设计师</a>"; }
                     
                    html += "					</div>";
                    html += "				</div>";
                    html += "				<div class='right'>";
                    html += "					<a href='" + drawingurl + "'>查看图纸</a>";
                    html += "					<a href='" + reuploadurl + "'>重新上传图片</a>";
                    html += "				</div>";
                    html += "			</div>";
                    html += "		</div>";

                } else if (m.auditingstate == 2) {

                    html += "			<div class='work'>";
                    html += "				<div class='box'>";
                    html += "					<div class='tTopcon'>";
                    html += "						<div class='line line01'>审核时间:<span>" + m.auditingtimestr + "</span></div>";
                    html += "						<div class='line line02 remark'>";
                    html += "							<div class='left'>备注:</div>";
                    html += "							<div class='right'><span>" + m.reformcontents + "</span><a href='javascript:;' class='open'><span>...</span><em>展开&gt;</em></a></div>";
                    html += "						</div>";
                    html += "					</div>";
                    html += "					<div class='line line03'>安装类型:<span>" + m.typename + "</span></div>";
                    html += "					<div class='line line04'>安装时间:<span>" + m.submittimestr + "</span></div>";
                    html += "					<div class='line line05'>业主:<span>" + m.customername + "</span> 设计师:<span>" + m.dzdesigner + "</span></div>";
                    html += "					<div class='line line06'>工长:<span>" + m.corpname + "</span></div>";
                    html += "					<div class='line line07'>";
                    html += "						<div class='left'>地址:</div>";
                    html += "						<div class='right'>";

                    html += "							" + m.projectaddress;
                    if (m.projectaddress != undefined && m.projectaddress.length > 0) {
                        html += "					<a href='" + mapurl + "' class='map'>";
                        html += "						<img src='" + host + "/areas/customize/content/images/comimg/map.png' alt='' />";
                        html += "					</a>";
                    }

                    html += "						</div>";
                    html += "					</div>";
                    html += "				</div>";
                    html += "			</div>";
                    html += "";
                    html += "			<div class='c_bottom'>";
                    html += "				<div class='w600 clearfloat'>";
                    html += "					<div class='right'>";
                    html += "						<a href='javascript:resolveReform(" + m.reformid + ",\'" + m.id + "\');' class='solve'>确认解决</a>";
                    html += "					</div>";
                    html += "				</div>";
                    html += "			</div>";

                }

            } else if (m.status == '4' || m.status == '5') {

                var boxcss = (m.status == '5') ? 'box' : 'box box_r';
                html += "		<div class='work'>";
                html += "			<div class='" + boxcss + "'>";

                if (m.status == '5') {
                    html += "					<div class='tTopcon'>";
                    html += "						<div class='line line09'>结算金额:<span>" + parseFloat(m.settlefee).toFixed(2) + "</span>元</div>";
                    html += "						<div class='line line02'>结算时间:<span>" + m.settletimestr + "</span></div>";
                    html += "					</div>";
                }
               

                html += "				<div class='line line03'>安装类型:<span>" + m.typename + "</span></div>";
                html += "				<div class='line line04'>完工时间:<span>" + m.auditingtimestr + "</span></div>";
                html += "				<div class='line line05'>业主:<span>" + m.customername + "</span> 设计师:<span>" + m.dzdesigner + "</span></div>";
                html += "				<div class='line line06'>工长:<span>" + m.corpname + "</span></div>";
                html += "				<div class='line line07'>";
                html += "					<div class='left'>地址:</div>";
                html += "					<div class='right'>" + m.projectaddress + "</div>";
                html += "				</div>";
                html += "			</div>";
                html += "		</div>";

            }
            html += "</div>";

        });
    }
    return html;
}

$(function () {
    search();
    contact();
    openOrClose();
    handleChar($(".remark"));
    loadmore();
});
