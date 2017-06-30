var wxjsapi = {
    get_current_timestamp: function () { return Math.round(new Date().getTime() / 1000); },
    get_random_str: function (len, strlist) {
        len = len || 32;
        if (null == strlist || strlist.length <= 0)
            strlist = 'ABCDEFGHJKMNPQRSTWXYZabcdefhijkmnprstwxyz2345678';
        var maxPos = strlist.length;
        var ret = '';
        for (i = 0; i < len; i++) {
            ret += strlist.charAt(Math.floor(Math.random() * maxPos));
        }
        return ret;
    },
    get_signature: function (timestamp, noncestr) {
        var sign = "";
        var host = "http://extend.525j.com.cn/authentication/jssdk/getsign";
        var params = { noncestr: noncestr, timestamp: timestamp, url: document.location.href, type: "customize" };
        $.ajax({
            type: "get", async: false, url: host, data: params, dataType: "jsonp", jsonp: "jsoncallback",
            success: function (json) { sign = json.signature; },
            error: function () { alert("signature error."); }
        });
        return sign;
    },
    config: function () {
        var _appid = "wxbe43958da555fb62";
        var _timestamp = wxjsapi.get_current_timestamp();
        var _noncestr = wxjsapi.get_random_str(16, null);
        var _signature = wxjsapi.get_signature(_timestamp, _noncestr);
        wx.config({
            debug: false,
            appId: _appid,
            timestamp: _timestamp,
            nonceStr: _noncestr,
            signature: _signature,
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
            ]
        });
    },
}