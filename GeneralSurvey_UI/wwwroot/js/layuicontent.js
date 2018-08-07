
// 显示元素
var showDom = function (elem) {
    $(elem).show();
}

//隐藏元素
var hideDom = function (elem) {
    $(elem).hide();
}

var removeVerify = function () {
    $("input[name=OptionSet]").removeAttr("lay-verify");
    $("input[name=Charlength]").removeAttr("lay-verify");
}

// 输入框禁用
var disableInput = function (value) {
    //输入框
    if (value == "006" || value == "004") {
        $("input[name=OptionSet]").attr("lay-verify", "");
        $("input[name=CharactersSize]").attr("lay-verify", "required");
        showDom("#isnull");
        showDom("#Charlengths");
    }
    //选项框
    else if (value == "001" || value == "002" || value == "003") {
        showDom("#OptionSets");
        hideDom("#Charlengths");
        $("input[name=OptionSet]").attr("lay-verify", "spaces");
        $("input[name=CharactersSize]").attr("lay-verify", "");

    } else {

        $("input[name=OptionSet]").attr("lay-verify", "");
        $("input[name=CharactersSize]").attr("lay-verify", "");
        hideDom("#isnull");
        hideDom("#Charlengths");
        hideDom("#OptionSets");
    }


}

//表单提交
var ajaxLayui = function (options) {
    console.log("调用中....");
    $("input[name=TopicName]").keydown(function () {
        if ($(this).val() != "") {
            showDom("#topics");
        }
    });

  
    layui.use('form', function () {
        var form = layui.form  // 表单
            , layer = layui.layer // 基础
            , layedit = layui.layedit //编辑器
            , laydate = layui.laydate; //日期            

        form.verify({         
            spaces: function (value) {              
                if (value.indexOf("*") < 0) {
                    return '选项之间用 * 号隔开'
                }
            }
        });

        var verifys = 0;
        form.on('radio(interest)', function (data) {
            verifys++;
            //console.log(data.elem); //得到select原始Dom对象
            //console.log(data.othis); //得到美化后的Dom对象
            //console.log(data.value); //  得到选择的后的值
            disableInput(data.value);
        });
      

        //监听提交
        form.on('submit(formDemo)', function (data) {
            if (verifys == 0) {
                layer.msg('请选择题目类型', { icon: 5, time: 2000 });  
                return false;
            }
            ajax_request({
                data: {
                    interest: data.field["interest"], TopicName: data.field["TopicName"], CharactersSize: data.field["CharactersSize"], OptionSet: data.field["OptionSet"], Stides: data.field["Stides"], Isempty: data.field["Isempty"]
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