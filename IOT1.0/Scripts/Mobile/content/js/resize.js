function throttle(func,wait,mustRun){
    var timeout,sartTime = new Date();
    return function(){
        var args = arguments,context = this,curTime = new Date();
        clearTimeout(timeout);
        if( curTime - sartTime >= mustRun ){
            func.apply(context,args);
            startTime = curTime;
        }else{
            timeout = setTimeout(func,wait);
        }
    }
}
var myComFn = {
    myAlertShow: function(text,type){
        var shadow = $(".shadow"),
            myAlert = $(".alert");
        myAlert.find("p").html(text);
        if( arguments.length==1 || type==1 ){
            myAlert.find(".cancel").hide();
        }else{
            myAlert.find(".cancel").show();
        }
        shadow.show();
        myAlert.show();
    },
    myAlertClose: function(){
        var shadow = $(".shadow"),
            myAlert = $(".alert");
        myAlert.find("a").on("click",function(){
            myAlert.hide();
            shadow.hide();
        });
    },
    isBottom: function(callback){
        var doc = $(document),
            win = $(window),
            distance = 0,
            direction = 0;//距离多少开始加载
        win.on("scroll",function(){
            direction>doc.scrollTop()?void(0):function(){
                if( doc.scrollTop()!=0&&(win.height()+doc.scrollTop()>=doc.height()-distance) ){
                    callback&&throttle(callback,50,100)();
                }
            }();
            direction = doc.scrollTop();
        });
    }
};
$(function () {
    $(window).resize(function () {
        var isWidescreen = $(this).width();
        if (isWidescreen < 320) {
            isWidescreen = 320;
        } else if (isWidescreen > 640) {
            isWidescreen = 640;
        }
        var ratio = isWidescreen / 320;
        $("html").css({ 'font-size': ratio * 16 + "px" });
    }).trigger('resize');
    myComFn.myAlertClose();
});
