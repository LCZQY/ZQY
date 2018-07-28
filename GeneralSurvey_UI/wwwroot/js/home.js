﻿
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

var questionbody = function (url) {
    console.log(url.view);
    $("#ViewContent").click(function () {     
        $("iframe").attr("src", url.view);
    });
    $("#QuestionContent").click(function () { 
        $("iframe").attr("src", url.question);
    });
    $("#LayoutWebContent").click(function () {
        $("iframe").attr("src", url.layout);
    })
}




