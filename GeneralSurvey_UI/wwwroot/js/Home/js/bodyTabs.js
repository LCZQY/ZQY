

function showNotice() {
    layui.use('layer', function () { //独立版的layer无需执行这一句
        var $ = layui.jquery, layer = layui.layer; //独立版的layer无需执行这一句

        //公告层
        layer.open({
            type: 1,
            title: "系统公告", //不显示标题栏
            closeBtn: false,
            area: '310px',
            shade: 0.8,
            id: 'LAY_layuipro', //设定一个id，防止重复弹出
            btn: ['我明白了'],
            moveType: 1, //拖拽模式，0或者1
            content: '<div style="padding:15px 20px; text-align:justify; line-height: 22px; text-indent:2em;border-bottom:1px solid #e2e2e2;"><p>' +
                '欢迎使用调研信息管理系统，使用说明如下：<br/>1.......'
                + '</p></div> ',
            success: function (layero) {
                var btn = layero.find('.layui-layer-btn');
                btn.css('text-align', 'center');
                btn.on("click", function () {
                    window.sessionStorage.setItem("showNotice", "true");
                })
                if ($(window).width() > 432) {  //如果页面宽度不足以显示顶部“系统公告”按钮，则不提示
                    btn.on("click", function () {
                        layer.tips('系统公告躲在了这里', '#showNotice', {
                            tips: 3
                        });
                    })
                }
            }
        });

   });
}
/// 组合左边导航标签
function navBar(data) {
    var ulHtml = '<ul class="layui-nav layui-nav-tree">';
    for (var i = 0; i < data.length; i++) {
        if (data[i].spread) {
            ulHtml += '<li class="layui-nav-item layui-nav-itemed">';
        } else {
            ulHtml += '<li class="layui-nav-item">';
        }
        if (data[i].children != undefined && data[i].children.length > 0) {
            ulHtml += '<a href="javascript:;">';
            if (data[i].icon != undefined && data[i].icon != '') {
                if (data[i].icon.indexOf("icon-") != -1) {
                    ulHtml += '<i class="iconfont ' + data[i].icon + '" data-icon="' + data[i].icon + '"></i>';
                } else {
                    ulHtml += '<i class="layui-icon" data-icon="' + data[i].icon + '">' + data[i].icon + '</i>';
                }
            }
            ulHtml += '<cite>' + data[i].title + '</cite>';
            ulHtml += '<span class="layui-nav-more"></span>';
            ulHtml += '</a>'
            ulHtml += '<dl class="layui-nav-child">';
            for (var j = 0; j < data[i].children.length; j++) {
                ulHtml += '<dd><a href="javascript:;" data-url="' + data[i].children[j].href + '">';
                if (data[i].children[j].icon != undefined && data[i].children[j].icon != '') {
                    if (data[i].children[j].icon.indexOf("icon-") != -1) {
                        ulHtml += '<i class="iconfont ' + data[i].children[j].icon + '" data-icon="' + data[i].children[j].icon + '"></i>';
                    } else {
                        ulHtml += '<i class="layui-icon" data-icon="' + data[i].children[j].icon + '">' + data[i].children[j].icon + '</i>';
                    }
                }
                ulHtml += '<cite>' + data[i].children[j].title + '</cite></a></dd>';
            }
            ulHtml += "</dl>"
        } else {
            ulHtml += '<a href="javascript:;" data-url="' + data[i].href + '">';
            if (data[i].icon != undefined && data[i].icon != '') {
                if (data[i].icon.indexOf("icon-") != -1) {
                    ulHtml += '<i class="iconfont ' + data[i].icon + '" data-icon="' + data[i].icon + '"></i>';
                } else {
                    ulHtml += '<i class="layui-icon" data-icon="' + data[i].icon + '">' + data[i].icon + '</i>';
                }
            }
            ulHtml += '<cite>' + data[i].title + '</cite></a>';
        }
        ulHtml += '</li>'
    }
    ulHtml += '</ul>';
    return ulHtml;
}



//初始化左侧导航
var _this = this;
$(".navBar").html(navBar(navs)).height($(window).height() - 230);
$(window).resize(function () {
    $(".navBar").height($(window).height() - 230);
});






