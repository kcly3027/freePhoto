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
    <% if(IsChooseStore() == false){%><script type="text/javascript" src="http://api.map.baidu.com/api?v=1.4"></script><%} %>
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
                <!-- #include file="inhtml/menu.html" -->
            </ul>
            <h3 class="muted">免费照片</h3>
        </div>
        <hr />
        <!-- #include file="inhtml/choosestore.html" -->
        <!-- #include file="inhtml/upimages.html" -->
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
    <!-- #include file="inhtml/login.html" -->
    <script type="text/javascript" src="/js/bootstrap.js"></script>
</body>
</html>
