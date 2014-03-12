<%@ Page Language="C#" AutoEventWireup="true"  %>
<script runat="server">
    protected bool IsGet = false;
    protected string myLocal = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        myLocal = Request["local"];
        IsGet = string.IsNullOrEmpty(myLocal);
    }
</script>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<style type="text/css">
body, html,#allmap {width: 100%;height: 100%;overflow: hidden;margin:0;}
#l-map{height:100%;width:78%;float:left;border-right:2px solid #bcbcbc;}
#r-result{height:100%;width:20%;float:left;}
</style>
<script type="text/javascript" src="http://api.map.baidu.com/api?v=1.4"></script>
<title>点击地图获取当前经纬度</title>
</head>
<body>
<div id="allmap"></div>
</body>
</html>
<script type="text/javascript">
    var map = new BMap.Map("allmap");
    map.centerAndZoom("杭州", 11);
    map.addControl(new BMap.NavigationControl());  //添加默认缩放平移控件
    map.enableDragging();//启用地图拖拽事件，默认启用(可不写)
    map.enableScrollWheelZoom();//启用地图滚轮放大缩小
    map.enableDoubleClickZoom();//启用鼠标双击放大，默认启用(可不写)
    map.enableKeyboard();//启用键盘上下左右键移动地图
    function showInfo(e) {
        AddMarker(e.point);
    }
    map.addEventListener("click", showInfo);

    var marker = undefined;
    function AddMarker(point) {
        if (marker != undefined) map.removeControl(marker);
        marker = new BMap.Marker(point);  // 创建标注
        map.addOverlay(marker);              // 将标注添加到地图中
        var zoom = map.getZoom();
        window.top.SelectMapInfo = { lat: point.lat, lng: point.lng, zoom: zoom };
    }
    window.top.Loading("hide");
    //function pageload(){
        <% if (IsGet == false){%>
        AddMarker({lat:<%= myLocal.Split('|')[0] %>,lng:<%= myLocal.Split('|')[1] %>});
        map.setZoom(<%= myLocal.Split('|')[2] %>);
        <%}%>
    //}
</script>