<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addorder.aspx.cs" Inherits="freePhoto.Web.addorder" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>喷嚏客</title>
    <!-- #include file="/inhtml/meta.html" -->
    <script src="/js/jquery.kcly.js"></script>
</head>
<body>
    <div class="container-narrow">
        <div class="masthead">
            <ul class="nav nav-pills pull-right ">
                <li class="active"><a href="/">首页</a></li>
                <!-- #include file="inhtml/menu.html" -->
            </ul>
            <h2 class="muted"><img src="img/logo.png" /></h2>
        </div>
        <hr />
        <% if (IsLogin()) {%>
        <div class="alert alert-success">
            <strong>提醒!</strong>
            <% if(NormalCount>0){%>您共有<%= NormalCount %>张免费普通纸，今天已经使用了<%= NormalCountNow %>张。&nbsp;&nbsp;<%} %>
            <% if(FreePhoto>0){%>您共有<%= FreePhoto %>张免费相片纸，今天已经使用了 <%= FreePhotoNow %>张。<br /><%} %>
        </div>
        <% } %>
        <!-- #include file="inhtml/addorder.html" -->
        <!-- #include file="inhtml/memo.html" -->
    </div>
    <div class="footer clearfix">
        <hr>
        <p style="text-align:center;"><a target="_blank" href="http://www.miibeian.gov.cn">浙ICP备13026483号</a></p>
        <p>&copy; 喷嚏客</p>
    </div>
    <!--登录模块-->
    <!-- #include file="inhtml/login.html" -->
    <!--用户设置模块-->
    <!-- #include file="inhtml/userset.html" -->
    <!--订单列表模块-->
    <!-- #include file="inhtml/orders.html" -->
    <script type="text/javascript" src="/js/bootstrap.js"></script>
    <script type="text/javascript">
        NoLoginStr = "登录/注册后才能正常下单";
    </script>
</body>
</html>