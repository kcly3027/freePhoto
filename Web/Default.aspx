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
            padding-top: 20px;
            padding-bottom: 40px;
        }

        /* Custom container */
        .container-narrow
        {
            margin: 0 auto;
            max-width: 700px;
        }

        .container-narrow > hr
        {
            margin: 30px 0;
        }

        /* Main marketing message and sign up button */
        .jumbotron
        {
            margin: 30px 0;
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

        /* Supporting marketing content */
        .marketing
        {
            margin: 60px 0;
        }

        .marketing p + h4
        {
            margin-top: 28px;
        }

    </style>
    <script src="/js/jquery.js"></script>
    <link href="/css/bootstrap-responsive.css" rel="stylesheet">

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/js/html5shiv.js"></script>
    <![endif]-->
    <link href="/tools/CropZoom/jquery-ui-1.7.2.custom.css" rel="stylesheet" />
    <script src="/tools/CropZoom/jquery-ui-1.8.custom.min.js"></script>
    <script src="/tools/CropZoom/jquery.cropzoom.js"></script>
    <%--<script src="/js/bootstrap.min.js"></script>--%>
</head>

<body>

    <div class="container-narrow">

        <div class="masthead">
            <ul class="nav nav-pills pull-right">
                <li class="active"><a href="/">首页</a></li>
                <li><a href="/订单">订单</a></li>
                <li><a href="/联系我们">联系我们</a></li>
            </ul>
            <h3 class="muted">免费照片</h3>
        </div>

        <hr>

        <div class="jumbotron">

            <div id="cropzoom_container" style="width:700px;height: 360px; overflow: hidden; position: relative; ">
                    
            </div>
            <br />
            <a class="btn btn-large btn-primary" href="#">查看预览</a>
            <a class="btn btn-large btn-success" href="#">确认订单</a>
        </div>

        <hr>

        <div class="row-fluid marketing">
            <div class="span6">
                <h4>Subheading</h4>
                <p>Donec id elit non mi porta gravida at eget metus. Maecenas faucibus mollis interdum.</p>

                <h4>Subheading</h4>
                <p>Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Cras mattis consectetur purus sit amet fermentum.</p>

                <h4>Subheading</h4>
                <p>Maecenas sed diam eget risus varius blandit sit amet non magna.</p>
            </div>

            <div class="span6">
                <h4>Subheading</h4>
                <p>Donec id elit non mi porta gravida at eget metus. Maecenas faucibus mollis interdum.</p>

                <h4>Subheading</h4>
                <p>Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Cras mattis consectetur purus sit amet fermentum.</p>

                <h4>Subheading</h4>
                <p>Maecenas sed diam eget risus varius blandit sit amet non magna.</p>
            </div>
        </div>

        <hr>

        <div class="footer">
            <p>&copy; Company 2013</p>
        </div>

    </div>
    <!-- /container -->

    <script type="text/javascript">
        $(function() {
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
                    source: '/img/test.jpg',
                    width: 450,
                    height: 800,
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
    </script>
</body>
</html>
