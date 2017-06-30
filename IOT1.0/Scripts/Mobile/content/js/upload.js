function upLoad() {
    var ul = $(".upCon ul"), deleteIndex = 0;
    $(".upCon .default").on("click", function () {
        //调用微信上传图片API
        wx.chooseImage({
            count: 9, // 默认9
            sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
            sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
            success: function (res) {
                var localIds = res.localIds; // 返回选定照片的本地ID列表，localId可以作为img标签的src属性显示图片
                uploadCallBack(localIds)
            }
        });
    });
    //微信上传图片API回调
    function uploadCallBack(urlArray) {
        var li = "";
        for (var i = 0; i < urlArray.length; i++) {
            li += '<li>' +
            		'<a href="javascript:;" class="close">' +
            			'<img src="' + host + '/areas/customize/content/images/upload/close.png" alt="">' +
            		'</a>' +
            		'<div class="imgCon" data-localid="' + urlArray[i] + '" style="background: url(' + urlArray[i] + ') center center no-repeat;background-size: cover"></div>' +
            	  '</li>';
        }
        ul.append(li);
        $(".subBtn a").addClass("on");
    }
    $(".upCon").on("click", ".close", function () {
        var li = $(this).closest("li");
        deleteIndex = li.index();
        myComFn.myAlertShow("您是否要删除图片？", 2);
    });
    $(".alert .confirm").unbind("click").on("click", function () {
        if ($(".upCon li").length == 1) {
            $(".subBtn a").removeClass("on");
        }
        if ($(".upCon li").length > 1 && !$(".alert .cancel").is(":hidden")) {
            $(".upCon li").eq(deleteIndex).remove();
        }
        $(".alert").hide();
        $(".shadow").hide();
    });
}
var arrLocalId = new Array();
function uploadImgToWXServer() {
    arrLocalId = [];
    $(".imgCon").each(function () {
        var id = $(this).data("localid");
        if (id)  arrLocalId.push(id);
    });
    if (arrLocalId.length > 9) {
        $(".subBtn a").addClass("on");
        layer.open({ content: "最多添加9张图片", btn: '确定' });
        return false;
    }
    else
        if (arrLocalId.length > 0) {
            $("#serverid").val("");
            syncUpload(arrLocalId);
        }
        else {
            $(".subBtn a").addClass("on");
            layer.open({ content: "请先添加图片", btn: '确定' });
            return false;
        }

}

var syncUpload = function (localIds) {
    var localId = localIds.pop();
    wx.uploadImage({
        localId: localId,
        isShowProgressTips: 1,
        success: function (res) {
            var serverId = res.serverId; // 返回图片的服务器端ID
            $("#serverid").val($("#serverid").val() + serverId + ",");
            if (localIds.length > 0) {
                syncUpload(localIds);
            } else {
                if ($("#serverid").val()) {
                    ajaxSubmit();
                } else {
                    $(".subBtn a").addClass("on");
                    layer.open({ content: "图片上传失败，请稍后再试", btn: '确定' });
                }
            }
        }
    });
};

function imgSub() {
    var form = $("form"),
		subBtn = $(".subBtn"),
		imgCon = $(".upCon");

    subBtn.on("click", ".on", function () {
        if (form.find("input:last") == "") {
            form.find("input:last").remove();
        }
        if (imgCon.find("li").length <= 1) {
            myComFn.myAlertShow("请先上传图片！");
            return false;
        }
        //询问
        var allindex = layer.open({
            content: '请确认图片无误后提交，提交成功后讲进入待审核列表，并且图片不可编辑'
          , btn: ['确定', '取消']
          , yes: function (index) {
              layer.closeAll();
              //先上传图片到微信服务器
              $(".subBtn a").removeClass("on");
              uploadImgToWXServer();
          }
        });
    });
}

function ajaxSubmit() {
    //提交
    var form = $("form");
    $(".loading").show();
    $(".subBtn a").removeClass("on");
    $(form).ajaxSubmit({
        type: form.method,
        url: form.action,
        success: function (data) {
            $(".loading").hide();
            if (data.succeed) {
                layer.open({ content: data.message, btn: '确定', shadeClose: false, yes: function () { window.location.href = returnUrl; } });
            } else {
                $(".subBtn a").addClass("on");
                layer.open({ content: data.message, btn: '确定' });
            }
        },
        error: function (xhr, textstatus, ex) {
            $(".loading").hide();
            $(".subBtn a").addClass("on");
            layer.open({ content: "操作失败：" + ex.message, btn: '确定' });
        }
    });
}

$(document).ready(function () {
    upLoad();
    imgSub();
});