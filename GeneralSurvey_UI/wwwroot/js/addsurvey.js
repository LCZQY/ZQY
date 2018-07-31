
/************添加问卷*****************/


//
var addFont = function (index) {
    $("li").find("div").eq(index).attr("class", "i");
    $("li").find("span").eq(index).attr("class", "tsl");
}

var changeContent = function (option) {
    console.log(option);

    $("#nextStep01").click(function () {
        addFont(1);
        $("#firstStep").hide();
        $("#twoStep").attr("src", option.twoStep).parent().show();
        $(".active").text();
        
    });

}
