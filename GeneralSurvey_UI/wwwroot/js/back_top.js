
/*****************************************************************
 *  原生回到顶部************************* */





function backTop() {
    window.addEventListener("load", function () {
        var btn = document.getElementById("btn");
        btn.addEventListener("mouseover", moveIn, false);
        btn.addEventListener("mouseout", moveOut, false);

        function moveIn() {
         
            btn.style.color = "black"; //修改的是内联样式,具有最高的优先级;
            btn.style.textIndent = "0em";
            btn.style.backgroundImage = "none";
            btn.style.backgroundColor = "#ffffff";
        }
        function moveOut() {
            btn.style.textIndent = "-9999em";
            btn.style.backgroundImage = "";
        }

        function goTop(acceleration, time) { //修改参数可调整返回顶部的速度
            acceleration = acceleration || 0.1;
            time = time || 10;
            var speed = 1 + acceleration;
            function getScrollTop() { //取得滚动条的竖直距离
                return document.documentElement.scrollTop || document.body.scrollTop;
            }
            function setScrollTop(value) { //设置滚动条的竖直距离,实现效果的关键就是在很短的间隔时间内不断地修改滚动条的竖直距离,以实现滚动效果
                document.documentElement.scrollTop = value;
                document.body.scrollTop = value;
            }
            window.onscroll = function () {
                var scrollTop = getScrollTop();
                if (scrollTop > 100) { //判断滚动条距离窗口顶部多远时显示出来,这里是100px
                    btn.style.display = "block";
                } else {
                    btn.style.display = "none";
                }
            };
            btn.onclick = function () {
                var timer = setInterval(function () {
                    setScrollTop(Math.floor(getScrollTop() / speed)); //这行代码是关键,取得滚动条竖直距离,除以speed后再给滚动条设置竖直距离
                    if (getScrollTop() == 0)
                        clearInterval(timer);
                }, time);
            };
        }
        goTop(0.2, 8);
    }, false);

    //当然,还有其他的实现方法,下面给出其他方法的关键代码

    btn.onclick = function () {
        clearInterval(timer);
        var timer = setInterval(function () {
            var now = scrollTop; //滚动条竖直距离
            speed = (0 - now) / 10;
            speed = Math.floor(speed);
            if (now == 0);
            clearInterval(timer);
            document.documentElement.scrollTop = now + speed; //标准模式下的浏览器
            document.body.scrollTop = now + speed; //怪异模式下的浏览器
        }, 15);
    }
}