<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="map.aspx.cs" Inherits="KnetApps.CanYin.Manager.info.map" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        html,body{margin:0;padding:0;}
        #dituContent{width:600px;height:400px;margin-top:10px;border:solid 1px #ccc;}
        .iw_poi_title {color:#CC5522;font-size:14px;font-weight:bold;overflow:hidden;padding-right:13px;white-space:nowrap}
        .iw_poi_content {font:12px arial,sans-serif;overflow:visible;padding-top:4px;white-space:-moz-pre-wrap;word-wrap:break-word}
    </style>
    <script type="text/javascript" src="http://api.map.baidu.com/api?key=&v=1.1&services=true"></script>
</asp:Content>

<asp:Content ID="bodyButton" ContentPlaceHolderID="bodyButton" runat="server">
    <div class="tit red">提示：在下方Google地图上点击，以指定餐厅位置。</div>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="bodyContent" runat="server">
<div id="dituContent"></div>
<input type="hidden" value="" id="t_MapCoordinate" />
<script type="text/javascript">
    <%if(CurrentStore!=null){%>
    var MapCoordinate = '<%= CurrentStore.Longitude %>,<%= CurrentStore.Latitude %>,<%= CurrentStore.Zoom %>';
    <%}else{%>
    var MapCoordinate = ',,';
    <%} %>
    
    //创建和初始化地图函数：
    function initMap(cityName) {
        createMap(cityName); //创建地图
        setMapEvent(); //设置地图事件
        addMapControl(); //向地图添加控件
    }

    //创建地图函数：
    function createMap(cityName) {
        var map = new BMap.Map("dituContent"); //在百度地图容器中创建一个地图
        //map.setCenter(cityName);
        var point = new BMap.Point(116.40982, 39.718413); //定义一个中心点坐标
        map.centerAndZoom(point, 16); //设定地图的中心点和坐标并将地图显示在地图容器中

        window.map = map; //将map变量存储在全局
    }
    //地图事件设置函数：
    function setMapEvent() {
        map.enableDragging(); //启用地图拖拽事件，默认启用(可不写)
        map.enableScrollWheelZoom(); //启用地图滚轮放大缩小
        map.enableDoubleClickZoom(); //启用鼠标双击放大，默认启用(可不写)
        map.enableKeyboard(); //启用键盘上下左右键移动地图
        map.addEventListener("click", showInfo);
    }
    var gc = new BMap.Geocoder();
    function showInfo(e) {
        if (window.marker) {
            map.removeOverlay(marker);
        }
        addMarker(e, map.getZoom());
        <% if(string.IsNullOrEmpty(m)){%>
            $.post("map.aspx?action=set", { lng: e.point.lng, lat: e.point.lat, zoom: map.getZoom() }, function () {
                $("#t_MapCoordinate").val(e.point.lng + ',' + e.point.lat + ',' + map.getZoom());
            });
        <%}else{%>
            gc.getLocation(e.point, function (rs) {
                var addComp = rs.addressComponents;
                window.top.SelectMap = addComp.city + addComp.district + addComp.street + addComp.streetNumber;
                window.top.VMsg.Close("SelectMap");
            });
        <%} %>
    }

    //地图控件添加函数：
    function addMapControl() {
        //向地图中添加缩放控件
        var ctrl_nav = new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_LEFT, type: BMAP_NAVIGATION_CONTROL_LARGE });
        map.addControl(ctrl_nav);
    }
    //标注点数组
    var markerArr = { title: "餐厅位置", content: "公司地址" };
    //创建marker
    function addMarker(json, zoom) {
        map.panTo(json.point); //设定地图的中心点和坐标并将地图显示在地图容器中
        map.zoomTo(zoom);
        json.icon = { w: 23, h: 25, l: 0, t: 21, x: 9, lb: 12 };
        var p0 = json.point.lng;
        var p1 = json.point.lat;
        var point = new BMap.Point(p0, p1);
        var marker = new BMap.Marker(point);
        var label = new BMap.Label(markerArr.title, { "offset": new BMap.Size(json.icon.lb - json.icon.x + 10, -20) });
        marker.setLabel(label);
        map.addOverlay(marker);
        label.setStyle({
            borderColor: "#808080",
            color: "#333",
            cursor: "pointer"
        });
        window.marker = marker;
    }

    function myFun(result) {
        var cityName = result.name;
        initMap(cityName); //创建和初始化地图
        if (MapCoordinate != ",,") {
            var s = {};
            s.point = new BMap.Point(MapCoordinate.split(",")[0], MapCoordinate.split(",")[1]);
            addMarker(s, MapCoordinate.split(",")[2]);
        }
    }
    var myCity = new BMap.LocalCity();
    myCity.get(myFun);
</script>
</asp:Content>