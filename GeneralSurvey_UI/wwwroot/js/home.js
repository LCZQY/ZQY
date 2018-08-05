
var layer;
layui.use(['layer', 'form'], function () {
    layer = layui.layer
        , form = layui.form;

});

/// ajax 
var ajax_request = function (options) {

    $.ajax({
        type: "post",
        async: false,
        datatype: "application/json",
        data: options.data,
        url: options.url,
        success: function (data, textStatus) {
            options.callback(data)
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(textStatus);
            layer.msg("请求失败");
        }
    });
}
/// 页面内容的替换
var questionbody = function (url) {
    console.log(url.datagrids);
    $("#AddSurvey").click(function () {

        $("iframe").attr("src", url.add);
    });
    $("#LookSurvey").click(function () {
        $("iframe").attr("src", url.look);
    });

    $("#DataGrid").click(function () {

        $("iframe").attr("src", url.datagrids);
    })

    $("#view").click(function () {

        window.open(url.views);
    });
}




// 选项卡的增删
var tabContent = function () {

    layui.use('element', function () {
        var $ = layui.jquery
            , element = layui.element; //Tab的切换功能，切换事件监听等，需要依赖element模块

        //触发事件
        var active = {
            tabAdd: function () {
                //新增一个Tab项
                element.tabAdd('demo', {
                    title: '新选项' + (Math.random() * 1000 | 0) //用于演示
                    , content:'<iframe src="Admin/Index" frameborder="0" scrolling="no" style=" width:100%; height:100%;"> </iframe>'
                    , id: new Date().getTime() //实际使用一般是规定好的id，这里以时间戳模拟下
                })
            }
            , tabDelete: function (othis) {
                //删除指定Tab项
                element.tabDelete('demo', '44'); //删除：“商品管理”


                othis.addClass('layui-btn-disabled');
            }
            , tabChange: function () {
                //切换到指定Tab项
                element.tabChange('demo', '22'); //切换到：用户管理
            }
        };

        $('.site-demo-active').on('click', function () {
            var othis = $(this), type = othis.data('type');
            active[type] ? active[type].call(this, othis) : '';
        });

        //Hash地址的定位
        var layid = location.hash.replace(/^#test=/, '');
        element.tabChange('test', layid);

        element.on('tab(test)', function (elem) {
            location.hash = 'test=' + $(this).attr('lay-id');
        });

    });

}