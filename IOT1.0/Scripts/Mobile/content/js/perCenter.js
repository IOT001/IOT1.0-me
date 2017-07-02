function signOut(){		
	$(".signOut").on("click",function(){
		myComFn.myAlertShow("是否退出当前账号？",2);
	});
}
$(document).ready(function(){
	signOut();
});