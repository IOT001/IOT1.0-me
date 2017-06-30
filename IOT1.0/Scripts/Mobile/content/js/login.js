function login(){
	var parent = $(".loginCon"),
		nameCon = parent.find(".name"),
		passwordCon = parent.find(".password"),
		subBtn = parent.find(".loginBtn"),
		param = {},
		loading = $(".loading"),
		url = "";//请求地址
	subBtn.on("click",function(){
		param.name = nameCon.val();
		if( param.name=="" ){
			myComFn.myAlertShow("请输入用户名或手机");
			return false;
		}
		param.password = passwordCon.val();
		if( param.password == "" ){
			myComFn.myAlertShow("请输入密码");
			return false;
		}
		loading.show();
		/*$.getJSON(url,param,function(data){ 登录请求
			loading.hide();
		});*/
	});
}
$(document).ready(function(){
	login();
});