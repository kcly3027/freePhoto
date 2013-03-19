<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="freePhoto.Web._Default" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>项目名称</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="/css/bootstrap.css" rel="stylesheet">
    <style type="text/css">
        body
        {
            /*background-color:#ede6e6;*/
            padding-top: 20px;
            padding-bottom: 40px;
        }
        .container-narrow
        {
            margin: 0 auto;
            max-width: 700px;
        }
        .jumbotron
        {
            text-align: center;
        }
        .jumbotron h1
        {
            font-size: 72px;
            line-height: 1;
        }

        .jumbotron .btn
        {
            font-size: 21px;
            padding: 14px 24px;
        }
        .marketing
        {
            margin: 60px 0;
        }

        .marketing p + h4
        {
            margin-top: 28px;
        }  
        #allmap img {
           max-width: none;
        }    
    </style>
    <script src="/js/jquery.js"></script>
    <link href="/css/bootstrap-responsive.css" rel="stylesheet">
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.4"></script>
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/js/html5shiv.js"></script>
    <![endif]-->
    <link href="/tools/CropZoom/jquery-ui-1.7.2.custom.css" rel="stylesheet" />
    <script src="/tools/CropZoom/jquery-ui-1.8.custom.min.js"></script>
    <script src="/tools/CropZoom/jquery.cropzoom.js"></script>
</head>
<body>
    <div class="container-narrow">
        <div class="masthead">
            <ul class="nav nav-pills pull-right">
                <li class="active"><a href="/">首页</a></li>
                <% if (IsChooseStore()) {%>
                <% if (IsLogin()) {%>
                <li><a href="/订单">订单</a></li>
                <li><a href="/订单">我的信息</a></li>
                <% }else{%>
                <li><a href="javascript:;" id="a_reg">注册</a></li>
                <%} %>
                <li><a href="/联系我们">联系我们</a></li>
                <% if (IsLogin()) {%>
                <li><a href="/订单">登出</a></li>
                <% }%>
                <% }else{%>
                <li><a href="/联系我们">联系我们</a></li>
                <%} %>
            </ul>
            <h3 class="muted">免费照片</h3>
        </div>
        <hr />
        <% if(IsChooseStore()){%>
        <div class="navbar">
            <div class="navbar-inner">
                <ul class="nav">
                    <li><a href="javascript:;">输入文字：</a></li>
                </ul>
                <form class="navbar-form pull-left">
                    <input type="text" class="span2">
                </form>
                <form class="navbar-form pull-right">
                    <button type="submit" class="btn"><i class="icon-upload"></i>上传照片</button>
                </form>
            </div>
        </div>
        <div class="jumbotron">
            <div id="cropzoom_container" style="width:700px;height: 360px; overflow: hidden; position: relative; "> 
                <img src="img/default.jpg" />
            </div>
            <br />
            <a class="btn btn-large btn-primary" href="#">查看预览</a>
            <a class="btn btn-large btn-success" href="#">确认订单</a>
        </div>
        <%}else{%>
        <div class="jumbotron">
            <h3>选择店面</h3><small>在上传图片之前，请先确定店面</small>
            <div id="allmap" style="width:100%;height:400px;"></div>
            <br />
            <a class="btn btn-large btn-primary" href="#">确定选择</a>
        </div>
        <%} %>
        <hr>

        <div class="row-fluid marketing">
            <div class="span6">
                <h4>每日免费</h4>
                <p>每日免费每日免费每日免费每日免费每日免费每日免费每日免费每日免费每日免费</p>

                <h4>免费打印</h4>
                <p>免费打印免费打印免费打印免费打印免费打印免费打印免费打印免费打印免费打印</p>

                <h4>杭州本地</h4>
                <p>杭州本地杭州本地杭州本地杭州本地杭州本地杭州本地杭州本地杭州本地杭州本地</p>
            </div>

            <div class="span6">
                <h4>每日免费</h4>
                <p>每日免费每日免费每日免费每日免费每日免费每日免费每日免费每日免费每日免费</p>

                <h4>免费打印</h4>
                <p>免费打印免费打印免费打印免费打印免费打印免费打印免费打印免费打印免费打印</p>

                <h4>杭州本地</h4>
                <p>杭州本地杭州本地杭州本地杭州本地杭州本地杭州本地杭州本地杭州本地杭州本地</p>
            </div>
        </div>

        <hr>

        <div class="footer">
            <p>&copy; Company 2013</p>
        </div>

    </div>
    <div id="Modal_Reg" class="modal hide fade" tabindex="-1" role="dialog" aria-hidden="true">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
        <h3>注册</h3>
      </div>
      <div class="modal-body">
        <form class="bs-docs-example form-horizontal">
            <div class="control-group">
              <label class="control-label" for="inputEmail">邮箱：</label>
              <div class="controls">
                <input type="email" id="inputEmail" placeholder="请输入您的邮箱">
              </div>
            </div>
          </form>
      </div>
      <div class="modal-footer">
          <button class="btn btn-primary" id="btn_reg">注册</button>
          <button class="btn" data-dismiss="modal" aria-hidden="true">关闭</button>
      </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#a_reg").click(function () { $("#Modal_Reg").modal('show') });
            $("#btn_reg").click(function () {

            });
            var cropzoom = $('#cropzoom_container').cropzoom({
                width: 700,
                height: 360,
                bgColor: '#fff',
                enableRotation: true,
                enableZoom: true,
                selector: {
                    w:430,
                    h:320,
                    showPositionsOnDrag: false,
                    showDimetionsOnDrag:false,
                    centered: true,
                    bgInfoLayer:'#000',
                    borderColor: 'blue',
                    animated: false,
                    maxWidth: 430,
                    maxHeight: 320,
                    borderColorHover: 'yellow'
                },
                image: {
                    source: '/img/default.jpg',
                    width: 1024,
                    height: 683,
                    minZoom: 30,
                    maxZoom: 150
                }
            });
            $("#crop").click(function(){
                cropzoom.send('crop_img.php', 'POST', {}, function(imgRet) {
                    $("#generated").attr("src", imgRet);
                });			   
            });
            $("#restore").click(function(){
                $("#generated").attr("src", "tmp/head.gif");
                cropzoom.restore();					  
            });

        });
        $(function () {
            //创建和初始化地图函数：
            function initMap() {
                createMap();//创建地图
                setMapEvent();//设置地图事件
                addMapControl();//向地图添加控件
                addMarker();//向地图中添加marker
            }

            //创建地图函数：
            function createMap() {
                var map = new BMap.Map("allmap");            // 创建Map实例
                map.centerAndZoom("杭州", 15);                     // 初始化地图,设置中心点坐标和地图级别。
                window.map = map;//将map变量存储在全局
            }

            //地图事件设置函数：
            function setMapEvent() {
                map.enableScrollWheelZoom();//启用地图滚轮放大缩小
            }

            //地图控件添加函数：
            function addMapControl() {
                //向地图中添加缩放控件
                var ctrl_nav = new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_LEFT, type: BMAP_NAVIGATION_CONTROL_LARGE });
                map.addControl(ctrl_nav);
                //向地图中添加缩略图控件
                var ctrl_ove = new BMap.OverviewMapControl({ anchor: BMAP_ANCHOR_BOTTOM_RIGHT, isOpen: 1 });
                map.addControl(ctrl_ove);
                //向地图中添加比例尺控件
                var ctrl_sca = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_BOTTOM_LEFT });
                map.addControl(ctrl_sca);
            }

            //标注点数组
            var markerArr = [{ StoreID: 1, title: "这是一格电", content: "我的备注", point: "120.171405|30.296202", isOpen: 0, icon: { w: 23, h: 25, l: 46, t: 21, x: 9, lb: 12 } }
            ];
            //创建marker
            function addMarker() {
                for (var i = 0; i < markerArr.length; i++) {
                    var json = markerArr[i];
                    var p0 = json.point.split("|")[0];
                    var p1 = json.point.split("|")[1];
                    var point = new BMap.Point(p0, p1);
                    var iconImg = createIcon(json.icon);
                    var marker = new BMap.Marker(point, { icon: iconImg });
                    var iw = createInfoWindow(i);
                    var label = new BMap.Label(json.title, { "offset": new BMap.Size(json.icon.lb - json.icon.x + 10, -20) });
                    marker.setLabel(label);
                    map.addOverlay(marker);
                    label.setStyle({
                        borderColor: "#808080",
                        color: "#333",
                        cursor: "pointer"
                    });

                    (function () {
                        var index = i;
                        var _iw = createInfoWindow(i);
                        var _marker = marker;
                        _marker.addEventListener("click", function () {
                            this.openInfoWindow(_iw);
                        });
                        _iw.addEventListener("open", function () {
                            window.SelectStore = json.StoreID;
                            alert(json.StoreID);
                            _marker.getLabel().hide();
                        })
                        _iw.addEventListener("close", function () {
                            //window.SelectStore = json.StoreID;
                            _marker.getLabel().show();
                        })
                        label.addEventListener("click", function () {
                            _marker.openInfoWindow(_iw);
                        })
                        if (!!json.isOpen) {
                            label.hide();
                            _marker.openInfoWindow(_iw);
                        }
                    })()
                }
            }
            //创建InfoWindow
            function createInfoWindow(i) {
                var json = markerArr[i];
                var iw = new BMap.InfoWindow("<b class='iw_poi_title' title='" + json.title + "'>" + json.title + "</b><div class='iw_poi_content'>" + json.content + "</div>");
                return iw;
            }
            //创建一个Icon
            function createIcon(json) {
                var icon = new BMap.Icon("http://app.baidu.com/map/images/us_mk_icon.png", new BMap.Size(json.w, json.h), { imageOffset: new BMap.Size(-json.l, -json.t), infoWindowOffset: new BMap.Size(json.lb + 5, 1), offset: new BMap.Size(json.x, json.h) })
                return icon;
            }

            initMap();//创建和初始化地图

            function myFun(result) {
                var cityName = result.name;
                if (cityName != "杭州市") {
                    alert("您当前处于" + cityName + "，我们的服务仅限于杭州市");
                }
            }
            var myCity = new BMap.LocalCity();
            myCity.get(myFun);
        });
    </script>
    <script type="text/javascript" src="/js/bootstrap.js"></script>
</body>
</html>
