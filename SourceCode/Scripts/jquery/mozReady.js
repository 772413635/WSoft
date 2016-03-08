function setCookie(name,value,days){
    var exp = new Date();
    exp.setTime(exp.getTime() + days*24*60*60*1000);
    document.cookie = name + "="+ escape (value) + ";expires=" + exp.toGMTString();
}

function getCookie(name,value){
	var arr = document.cookie.match(new RegExp("(^| )"+name+"=([^;]*)(;|$)"));
	if(arr != null) return unescape(arr[2]); return value;
}

$(function(){

	$("#picture").load(function(){
		$(this).parent().height($(this).height());
		$(this).fadeIn();
	});
/*
	$(".thumb_picture").click(function(){
		$("#picture").fadeOut().attr("src",$(this).attr("data-imgurl"));
		$("#note_box").html($(this).attr("data-note"));
		$(this).addClass("current").siblings().removeClass("current");
	});
	$("#toleft").click(function(){
		var index=$(".thumb .current").index();
		if(index>0){
			$(".thumb_picture:eq("+(index-1)+")").trigger("click");
			var w=parseInt($("#thumb ul li:eq(0)").width());
			var mr=parseInt($("#thumb ul li:eq(0)").css("margin-right"));
			var li_w=w+mr;
			var ml=-parseInt($("#thumb ul").css("margin-left"));
			if(index*li_w<ml){
				$("#toleft1").trigger("click");
			}
		}
	});
	$("#toright").click(function(){
		var index=$(".thumb .current").index();
		if(index<$(".thumb_picture").length){
			$(".thumb_picture:eq("+(index+1)+")").trigger("click");
			var w=parseInt($("#thumb ul li:eq(0)").width());
			var mr=parseInt($("#thumb ul li:eq(0)").css("margin-right"));
			var li_w=w+mr;
			var ml=-parseInt($("#thumb ul").css("margin-left"));
			if((index-3)*li_w>ml){
				$("#toright1").trigger("click");
			}
		}
	});

	$("#toleft1").click(function(){
		var width=parseInt($("#thumb").width());
		var w=parseInt($("#thumb ul li:eq(0)").width());
		var mr=parseInt($("#thumb ul li:eq(0)").css("margin-right"));
		var li_w=w+mr;
		var n=$("#thumb ul li").length;
		var pages=Math.ceil(n*li_w/width);
		var ml=-parseInt($("#thumb ul").css("margin-left"));
		if(ml>width){
			$("#thumb ul").animate({"margin-left":-(ml-width-mr)+"px"});
		}
	});
	$("#toright1").click(function(){
		var width=parseInt($("#thumb").width());
		var w=parseInt($("#thumb ul li:eq(0)").width());
		var mr=parseInt($("#thumb ul li:eq(0)").css("margin-right"));
		var li_w=w+mr;
		var n=$("#thumb ul li").length;
		var pages=Math.ceil(n*li_w/width);
		var ml=-parseInt($("#thumb ul").css("margin-left"));
		if(ml<(pages-1)*width){
			$("#thumb ul").animate({"margin-left":-(ml+width+mr)+"px"});
		}
	});
*/	
	
/*start added by kelong @2014-02-11*/	
	$(".thumb-picture").click(function(){
		$("#picture").fadeOut().attr("src",$(this).attr("data-imgurl"));
		$("#note_box").html($(this).attr("data-note"));
		$(this).addClass("current").siblings().removeClass("current");
	});
	$("#to-left").click(function(){
		var width=parseInt($("#thumb").width());
		var w=parseInt($("#thumb ul li:eq(0)").width());
		var mr=parseInt($("#thumb ul li:eq(0)").css("margin-right"));
		var li_w=w+mr;
		var n=$("#thumb ul li").length;
		var pages=Math.ceil(n*li_w/width);
		var ml=-parseInt($("#thumb ul").css("margin-left"));
		if(ml>width){
			$("#thumb ul").animate({"margin-left":-(ml-width-mr)+"px"});
		}else{
			alert("没有了");
		}
	});
	$("#to-right").click(function(){
		var width=parseInt($("#thumb").width());
		var w=parseInt($("#thumb ul li:eq(0)").width());
		var mr=parseInt($("#thumb ul li:eq(0)").css("margin-right"));
		var li_w=w+mr+2;//2 is border width
		var n=$("#thumb ul li").length;
		var pages=Math.ceil(n*li_w/(width+mr));
		var ml=-parseInt($("#thumb ul").css("margin-left"));
		if(ml<(pages-1)*width){
			$("#thumb ul").animate({"margin-left":-(ml+width+mr)+"px"});
		}else{
			alert("没有了");
		}
	});
	$("#to-left-big").click(function(){
		var index=$("#thumb .current").index();
		if(index>0){
			$(".thumb-picture:eq("+(index-1)+")").trigger("click");
			var w=parseInt($("#thumb ul li:eq(0)").width());
			var mr=parseInt($("#thumb ul li:eq(0)").css("margin-right"));
			var li_w=w+mr;
			var ml=-parseInt($("#thumb ul").css("margin-left"));
			if(index*li_w<ml){
				$("#to-left").trigger("click");
			}
		}else{
			alert("没有了");
		}
	});
	$("#to-right-big").click(function(){
		var index=$("#thumb .current").index();
		if(index<$(".thumb-picture").length){
			$(".thumb-picture:eq("+(index+1)+")").trigger("click");
			var w=parseInt($("#thumb ul li:eq(0)").width());
			var mr=parseInt($("#thumb ul li:eq(0)").css("margin-right"));
			var li_w=w+mr;
			var ml=-parseInt($("#thumb ul").css("margin-left"));
			if((index-3)*li_w>ml){
				$("#to-right").trigger("click");
			}
		}else{
			alert("没有了");
		}
	});
/*end added by kelong @2014-02-11*/
	$(".big_size").click(function(){
		$(".article_content").addClass("big_font");
		$(this).css("background-position","0px -68px");
		$(".normal_size").css("background-position","0px -32px");
		setCookie("fontsize","big",30);
	});
	$(".normal_size").click(function(){
		$(".article_content").removeClass("big_font");
		$(this).css("background-position","0px 0px");
		$(".big_size").css("background-position","0px -102px");
		setCookie("fontsize","small",30);
	});
	var fontsize=getCookie("fontsize","small");
	if(fontsize=="big"){
		$(".big_size").trigger("click");
	}
})