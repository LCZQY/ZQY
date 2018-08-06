
// 添加禁用属性
var addAttr = function (elem) {
    $(elem).attr({
        "disabled": "true",
    }).css("border", "#8a8787 solid thin");

}

// 删除禁用属性
var deleteAttr = function (elem) {
    $(elem).removeAttr("disabled").attr("lay-verify", "").css("border", "none");//required
}



// 输入框禁用
var disableInput = function (value) {
    console.log(value);
    switch (value) {
        case "001":
        case "003":
        case "002":
            addAttr("#Charlength");
            deleteAttr("input[name=OptionSet]");
            break;
        case "005":
        case "009":
        case "010":
        case "011":
            addAttr("#Charlength");
            addAttr("input[name=OptionSet]");
            break;
        case "006":
        case "004":
            deleteAttr("#Charlength");
            addAttr("input[name=OptionSet]");
            break;
        case "007":
        case "008":
            addAttr("#Charlength");
            addAttr("input[name=OptionSet]");
            break;
    }
}




//表单提交
var ajaxLayui = function (options) {
    console.log("调用中....");
    layui.use('form', function () {
        var form = layui.form  // 表单
            , layer = layui.layer // 基础
            , layedit = layui.layedit //编辑器
            , laydate = layui.laydate; //日期            


        form.verify({
            options: function (value) {
                if (value == "") {
                    return '请选择选项类型'
                }
            },
            spaces: function (value) {

                if (value.indexOf("*") < -1) {
                    return '选项之间用 * 号隔开'
                }
            }
        });

        form.on('radio(interest)', function (data) {

            //console.log(data.elem); //得到select原始Dom对象
            //console.log(data.othis); //得到美化后的Dom对象
            //console.log(data.value); //  得到选择的后的值
            disableInput(data.value);
        });

        //监听提交
        form.on('submit(formDemo)', function (data) {

            ajax_request({
                data: {
                    interest: data.field["interest"], TopicName: data.field["TopicName"], CharactersSize: data.field["CharactersSize"], OptionSet: data.field["OptionSet"], Stides: data.field["Stides"]
                },
                url: options.ajax,
                callback: function (data) {
                    console.log(data);
                    if (data.code == 200) {
                        //成功后直接跳转到下一步 
                        alertAction(options.succeed);
                    } else {
                        layer.msg("添加失败,请在仔细检查");
                    }
                }
            });
            return false;
        });
    });
}
//弹出框设置
var alertAction = function (url) {
    console.log(url);
    layui.use('layer', function () {
        layer = layui.layer;
        //询问框

        layer.confirm('添加成功,您可以选择继续添加或是查看', {
            btn: ['继续添加', '确定本次添加', '取消'] //按钮
        }, function () {
            // 直接替换成下一步
            window.location.reload();
        }, function () {
            // 直接替换成下一步
            var enelmnt = parent.document.getElementById("twoStep").setAttribute("src", url);
            parent.document.getElementsByTagName("li")[2].childNodes[0].setAttribute("class", "i");
            parent.document.getElementsByTagName("li")[2].childNodes[1].setAttribute("class", "tsl");
        });

    });
}