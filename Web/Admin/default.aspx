<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="freePhoto.Web.Admin._default" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>后台管理</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <!-- Le styles -->
    <link href="/css/bootstrap.css" rel="stylesheet" />
    <link href="images/admin.css" rel="stylesheet" />
    <!--[if lt IE 7]><link rel="stylesheet" href="/css/bootstrap-ie6.min.css"><![endif]-->
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/js/html5shiv.js"></script>
    <![endif]-->
    <script src="/js/jquery.js"></script>
    <script type="text/javascript">
        function body_size() {
            var height = $(window).height();
            $('#iframe_main').css('height', height - 66);
        }

        $(function () {
            body_size();
        });
        $(window).resize(function () {
            body_size();
        });
        $(window).scroll(function () {
            body_size();
        });
        $(function () {
            $(".menu a").click(function () {
                $(".menu li").removeClass("active");
                $(this).parents("li").addClass("active");
            });
        });
    </script>
  </head>

  <body>

    <div class="navbar navbar-inverse navbar-fixed-top">
      <div class="navbar-inner">
        <div class="container-fluid">
          <button type="button" class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="brand" href="/" style="padding:0px;"><img src="/img/logo.png" style="height:50px;" /></a>
          <div class="nav-collapse collapse">
            <p class="navbar-text pull-right">
              账号： <a href="#" class="navbar-link"><%= CurrentStore.LoginName %></a>&nbsp;&nbsp;隶属于：<a href="#" class="navbar-link"><%= CurrentStore.StoreName %></a>&nbsp;&nbsp;<a href="loginout.aspx" class="navbar-link"><i class="icon-off"></i>退出</a>
            </p>
            <ul class="nav menu">
              <li class="active"><a href="/Admin/User/default.aspx" target="iframe_main">打印服务</a></li>
              <li><a href="/Admin/Store/default.aspx" target="iframe_main">分店信息</a></li>
              <li><a href="/Admin/editPwd.aspx" target="iframe_main">修改密码</a></li>
              <% if (IsMain) {%>
                <li><a href="/Admin/set/default.aspx" target="iframe_main">系统管理</a></li>
              <% } %>
            </ul>
          </div><!--/.nav-collapse -->
        </div>
      </div>
    </div>

    <div class="container-fluid" id="wrap">
      <div class="row-fluid">
        <iframe style="border:0px;" src="user/default.aspx" width="100%" height="100%" name="iframe_main" id="iframe_main"></iframe>
      </div><!--/row-->
    </div>

    <div id="Modal_BaiduMap" class="modal hide fade" tabindex="-1" role="dialog" aria-hidden="true" style="width:830px;margin-left:-415px;">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
        <h3>选择位置</h3>
      </div>
      <div class="modal-body" style="height:500px;max-height:500px;">
          <iframe id="iframe_map" border="0" style="height:460px;border:0px;display:none;" width="100%" src=""></iframe>
          <iframe id="iframe_Loading" border="0" style="height:460px;border:0px;" width="100%" src="/loading.html"></iframe>
      </div>
    </div>
    <script type="text/javascript">
        function SelectBaiduMap(param,callback) {
            Loading("show");
            var src = "Store/baidumap.aspx";
            if(param != undefined && param != "") src = "Store/baidumap.aspx?local=" +param;
            $("#iframe_map").attr("src", src); 
            $("#Modal_BaiduMap").modal('show').on('hidden', function () {
                var g = window.top.SelectMapInfo;
                callback(g);
            }); return false;
        }
        function Loading(action) {
            if (action == "show") $("#iframe_Loading").show().prev().hide();
            if (action == "hide") $("#iframe_Loading").hide().prev().show();
        }
    </script>
    <script src="/js/bootstrap.min.js"></script>
  </body>
</html>
