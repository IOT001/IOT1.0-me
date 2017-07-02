wx.ready(function () {
    wx.checkJsApi({
        jsApiList: [
            'checkJsApi',
            'onMenuShareTimeline',
            'onMenuShareAppMessage',
            'onMenuShareQQ',
            'onMenuShareWeibo',
            'onMenuShareQZone',
            'hideMenuItems',
            'showMenuItems',
            'hideAllNonBaseMenuItem',
            'showAllNonBaseMenuItem',
            'chooseImage',
            'previewImage',
            'uploadImage',
            'downloadImage',
            'getNetworkType',
            'openLocation',
            'getLocation',
            'hideOptionMenu',
            'showOptionMenu',
            'closeWindow',
            'scanQRCode',
            'chooseWXPay'
        ],
        success: function (res) { }
    });

    wx.onMenuShareAppMessage({
        title: sharedata.title,
        desc: sharedata.desc,
        link: sharedata.link,
        imgUrl: sharedata.imgUrl,
        trigger: function (res) { },
        success: function (res) { },
        cancel: function (res) { },
        fail: function (res) { alert(JSON.stringify(res)); }
    });

    wx.onMenuShareTimeline({
        title: sharedata.title,
        link: sharedata.link,
        imgUrl: sharedata.imgUrl,
        trigger: function (res) { },
        success: function (res) { },
        cancel: function (res) { },
        fail: function (res) { alert(JSON.stringify(res)); }
    });

    wx.onMenuShareQQ({
        title: sharedata.title,
        desc: sharedata.desc,
        link: sharedata.link,
        imgUrl: sharedata.imgUrl,
        trigger: function (res) { },
        complete: function (res) { },
        success: function (res) { },
        cancel: function (res) { },
        fail: function (res) { alert(JSON.stringify(res)); }
    });

    wx.onMenuShareWeibo({
        title: sharedata.title,
        desc: sharedata.desc,
        link: sharedata.link,
        imgUrl: sharedata.imgUrl,
        trigger: function (res) { },
        complete: function (res) { },
        success: function (res) { },
        cancel: function (res) { },
        fail: function (res) { alert(JSON.stringify(res)); }
    });

    wx.hideMenuItems({
        menuList: [
            'menuItem:share:qq',
            'menuItem:share:appMessage',
            'menuItem:readMode',
            'menuItem:share:timeline',
            'menuItem:share:weiboApp',
            'menuItem:copyUrl',
            'menuItem:openWithQQBrowser',
            'menuItem:openWithSafari',
            'menuItem:share:email',
            'menuItem:share:QZone',
            'menuItem:favorite'
        ],
        success: function (res) {
        },
        fail: function (res) {
            alert(JSON.stringify(res));
        }
    });
});

wx.error(function (res) {

});