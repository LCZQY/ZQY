/************************************************************排版页面JS
 * 
 * */





//主方法
var scriptjs = function (option) {
    
    layui.use('table', function () {
        var table = layui.table;

        //方法级渲染
        table.render({
            elem: '#LAY_table_user'
            , url: option.jsonT
            , cols: [[
                { checkbox: true, fixed: true }
                , { field: 'stide', title: '题号', }
                , { field: 'topicName', title: '题目', }
                , { field: 'setsettingId', title: '题干类型', }
                , { field: 'charactersSize', title: '字数限制', }
                , { field: 'optionText', title: '选项名称', }
                , { field: 'id', title: '操作', toolbar: '#barDemo' }
            ]]
            , id: 'testReload'
            , page: true
            , height: 'full-200'
            , skin: 'line' //表格风格
        });


        // 删除，修改
        table.on('tool(useruv)', function (obj) {
            var data = obj.data;
            if (obj.event === 'del') {
                layer.confirm('是否确定删除', function (index) {
                    ajax_request({
                        url: option.del,
                        data: { id: data["id"] },
                        callback: function (pic) {
                            if (pic.code == 200) {
                                obj.del();
                                layer.close(index);
                            } else {
                                console.log(pic);
                                layer.msg("服务器出错了，请重试");
                            }
                        }
                    });
                });
            } else if (obj.event === 'edit') {
                var href = option.edit + "/?id=" + data["id"];
                editContent(href);
            }
        });
    });
}


//修改
var editContent = function (href) {
    console.log(href)
    var index = layer.open({
        type: 2,
        content: href,
        area: ['1220px', '505px'],
        maxmin: true
    });
    layer.full(index);
}
