var rootUrl = "/";
var pathname = window.location.pathname;


//查询对象
function QueryObject() {
    this.Page = 0;
    this.Rp = 0;
    this.SortName = "Id";
    this.SortOrder = "asc";
    this.WhereStr = "";
}


//表示全局唯一标识符 (GUID)
$.Guid = function (g) {
    /// <summary>表示全局唯一标识符 (GUID)</summary>
    /// <param name="g" type="String">guid字符串</param>
    var arr = new Array(); //存放32位数值的数组


    if (typeof (g) == "string") { //如果构造函数的参数为字符串

        InitByString(arr, g);

    } else {

        InitByOther(arr);

    }

    //返回一个值，该值指示 Guid 的两个实例是否表示同一个值。

    this.Equals = function (o) {

        if (o && o.IsGuid) {

            return this.ToString() == o.ToString();

        } else {

            return false;

        }

    };

    //Guid对象的标记

    this.IsGuid = function () {
    };

    //返回 Guid 类的此实例值的 String 表示形式。

    this.ToString = function (format) {

        if (typeof (format) == "string") {

            if (format == "N" || format == "D" || format == "B" || format == "P") {

                return ToStringWithFormat(arr, format);

            } else {

                return ToStringWithFormat(arr, "D");

            }

        } else {

            return ToStringWithFormat(arr, "D");

        }

    };

    //由字符串加载

    function InitByString(arr, g) {

        g = g.replace(/\{|\(|\)|\}|-/g, "");

        g = g.toLowerCase();

        if (g.length != 32 || g.search(/[^0-9,a-f]/i) != -1) {

            InitByOther(arr);

        } else {

            for (var i = 0; i < g.length; i++) {

                arr.push(g[i]);

            }

        }

    }

    //由其他类型加载

    function InitByOther(arr) {

        var i = 32;

        while (i--) {

            arr.push("0");

        }

    }

    /*

    根据所提供的格式说明符，返回此 Guid 实例值的 String 表示形式。

    N  32 位： xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

    D  由连字符分隔的 32 位数字 xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx

    B  括在大括号中、由连字符分隔的 32 位数字：{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}

    P  括在圆括号中、由连字符分隔的 32 位数字：(xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)

    */

    function ToStringWithFormat(arr, format) {

        switch (format) {
            case "N":
                return arr.toString().replace(/,/g, "");
            case "D":
                var str = arr.slice(0, 8) + "-" + arr.slice(8, 12) + "-" + arr.slice(12, 16) + "-" + arr.slice(16, 20) + "-" + arr.slice(20, 32);

                str = str.replace(/,/g, "");

                return str;
            case "B":
                var str = ToStringWithFormat(arr, "D");

                str = "{" + str + "}";

                return str;
            case "P":
                var str = ToStringWithFormat(arr, "D");

                str = "(" + str + ")";

                return str;
            default:
                return new Guid();
        }

    }

};

//Guid 类的默认实例，其值保证均为零。
$.Guid.Empty = $.Guid();

//初始化 Guid 类的一个新实例。
$.Guid.NewGuid = function () {
    /// <summary>初始化 Guid 类的一个新实例</summary>
    var g = "";

    var i = 32;

    while (i--) {

        g += Math.floor(Math.random() * 16.0).toString(16);

    }

    return new $.Guid(g);

};


//获取url中的参数
$.GetRequest = function () {
    /// <summary>获取url中的参数</summary>
    var url = location.search; //获取url中"?"符后的字串

    var theRequest = new Object();
    theRequest.count = 0;
    if (url.indexOf("?") != -1) {

        var str = url.substr(1);

        strs = str.split("&");

        for (var i = 0; i < strs.length; i++) {

            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
            theRequest.count++;
        }

    }

    return theRequest;

};

//Json日期转换
$.JsonToDate = function (val) {
    /// <summary>Json日期转换</summary>
    /// <param name="data" type="Object">Json格式的日期</param>
    var returnData = null;
    if (val != null) {
        var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
        //月份为0-11，所以+1，月份小于10时补个0
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        var fullDataString = date.getFullYear() + "-" + month + "-" + currentDate + " " + (date.getHours() < 10 ? "0" + date.getHours() : date.getHours()) + ":" + (date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes()) + ":" + (date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds());
        returnData = new $.JsonDate(date, fullDataString);
    }
    return returnData;
};

//Json
$.JsonDate = function (dateObject, fullDateString) {
    this.dateObject = dateObject;
    this.fullDateString = fullDateString;
};


/*======================================格式化=========================================*/

function FormatterNull(val) {
    if (val == null) {
        return "";
    } else {
        return val;
    }
}


/*=======================================导航===========================================*/

//首页
function goHome() {
    if (pathname == "/" || pathname.indexOf("/Home/Index") >= 0) {
        $.scrollTo("#header", 500);

    } else {
        window.location.href = rootUrl+"Home/Index";
    }

}
//成功案例
function goAl() {
    if (pathname == "/" || pathname.indexOf("/Home/Index")>=0) {
        $.scrollTo("#header1", 500);
    }
    else {
        window.location.href = rootUrl + "Case/Index";
    }
}
//关于我们
function goGw() {
    if (pathname == "/" || pathname.indexOf("/Home/Index") >= 0) {
        $.scrollTo("#header2", 500);
    } else {
        window.location.href = rootUrl + "Home/Index?id=aboutUs";
    }
}



//图片显示错误

function showError(obj) {
    obj.src = "/Files/noimg.jpg";
    obj.onerror = null;
}


//二级菜单
$(function () {
    $(".ermenu li:last").attr("style", "border-bottom: none");
    $(".onemenu").mouseenter(function () {
        $(".ermenu").hide();
        $(this).find(".ermenu").slideDown(300);
    });
    $(".ermenu").mouseleave(function () {
        $(this).slideUp(300);
    });
  
});