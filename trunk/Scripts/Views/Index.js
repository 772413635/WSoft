var circle;
var circle2;
var circle3;
var mr1, mr2, mr3, mr4;
var i = 1;
var c;

/************************************图片查看************************************/
hs.graphicsDir = '/Scripts/highslide/graphics/';
hs.align = 'center';


hs.lang.creditsText = "";
hs.lang.restoreTitle = "";
hs.lang.playTitle = "播放幻灯片 [Spacebar]";
hs.lang.nextTitle = "下一个 [→]";
hs.lang.previousTitle = "上一个 [←]";
hs.lang.pauseTitle = "暂停 [Spacebar]";
hs.lang.moveTitle = "移动";
hs.lang.closeTitle = "关闭 [Esc]";
hs.lang.fullExpandTitle = "原图 [F]";



hs.transitions = ['expand', 'crossfade'];
hs.wrapperClassName = 'dark borderless floating-caption';
hs.fadeInOut = true;
hs.dimmingOpacity = .75;
if (hs.addSlideshow) hs.addSlideshow({
    interval: 2500,
    repeat: true,
    useControls: true,
    fixedControls: 'fit',
    overlayOptions: {
        opacity: .6,
        position: 'bottom center',
        hideOnMouseOut: true
    }
});
/********************************************************************************/


$(function () {
    var width = 0.9;
    var paper = Raphael("circle");

    circle = paper.circle(153, 153, 149);
    circle2 = paper.circle(153, 153, 110);
    circle3 = paper.circle(153, 153, 75);

    circle.attr({
        "stroke-width": width,
        "stroke": "#bbc0c6",
        "stroke-dasharray": "--"
    });

    circle2.attr({
        "stroke-width": width,
        "stroke": "#375471",
        "stroke-dasharray": "--"
    });

    circle3.attr({
        "stroke-width": width,
        "stroke": "#15385e",
        "stroke-dasharray": "--"
    });

    mr1 = paper.circle(153, 245, 5).attr({//圆1
        fill: "#bcca75",
        stroke: "#061e38",
        "stroke-width": 0
    });
    mr2 = paper.circle(153, 273, 6.5).attr({//圆2
        fill: "#556678",
        stroke: "#061e38",
        "stroke-width": 0
    });;
    mr3 = paper.circle(153, 301, 9).attr({//圆3
        fill: "#47586c",
        stroke: "#061e38",
        "stroke-width": 0
    });
    mr4 = paper.circle(153, 335, 12.5).attr({//圆4
        fill: "#243850",
        stroke: "#061e38",
        "stroke-width": 0
    });

    //绘制文字
    paper.text(153, 153, "100分").attr({
        "fill": 'white',
        "font-size": "45px",
        "font-family": "微软雅黑"
    });
    //绘制箭头
    c = paper.path("M146 333L153 339L160 333");
    c.attr({
        "stroke": "#d3d3d5",
        "opacity": "0.5"
    });


    var t1, t2;
    t1 = paper.text(100, 338, "向下滚动").attr({
        "fill": '#d3d3d5',
        "font-size": "12px",
        "font-family": "Apercu Regular', Calibri, sans-serif",
        "opacity": "0.5"
    });
    t2 = paper.text(210, 338, "scroll down").attr({
        "fill": '#d3d3d5',
        "font-size": "12px",
        "font-family": "Apercu Regular', Calibri, sans-serif",
        "opacity": "0.5"
    });
    t1.hide();
    t2.hide();
    mr4.node.onmouseover = function () {
        t1.show();
        t2.show();
    };
    mr4.node.onmouseout = function () {
        t1.hide();
        t2.hide();
    };
    c.node.onmouseover = function () {
        t1.show();
        t2.show();
    };


    circle.animate({ "transform": "r60" }, 5000);
    circle2.animate({ "transform": "r-60" }, 5000);
    circle3.animate({ "transform": "r60" }, 5000);
    setInterval(ss, 5000);
    changeColor();
    Initial();
});
//圈圈滚动函数
function ss() {
    i++;
    circle.animate({ "transform": "r" + 60 * i }, 5000);
    circle2.animate({ "transform": "r-" + 60 * i }, 5000);
    circle3.animate({ "transform": "r" + 60 * i }, 5000);
}
//改变颜色函数
function changeColor() {
    mr1.animate({ fill: "#556678" }, 500);
    mr2.animate({ fill: "#bcca75" }, 500, function () {
        mr2.animate({ fill: "#556678" }, 500);
        mr3.animate({ fill: "#bcca75" }, 500, function () {
            mr3.animate({ fill: "#47586c" }, 500);
            c.animate({
                "stroke": "white",
                "opacity": "0.5"
            }, 500);
            mr4.animate({ fill: "#bcca75" }, 500, function () {
                mr4.animate({ fill: "#243850" }, 500);
                c.attr({
                    "stroke": "d3d3d5",
                    "opacity": "0.5"
                });
                mr1.animate({ fill: "#bcca75" }, 500, function () {
                    changeColor();
                });

            });
        });
    });



}


//初始化
function Initial() {
    var id = $.GetRequest()["id"];
    switch (id) {
        case "aboutUs":
            {
                $.scrollTo("#header2", 500);
            }
            break;
    }


}
