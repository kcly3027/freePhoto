﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="freePhoto.Web.Admin._default" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>Bootstrap, from Twitter</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <!-- Le styles -->
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <link href="images/admin.css" rel="stylesheet" />
    <link href="/css/bootstrap-responsive.css" rel="stylesheet">
    <!--[if lt IE 7]><link rel="stylesheet" href="/css/bootstrap-ie6.min.css"><![endif]-->
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/js/html5shiv.js"></script>
    <![endif]-->
    <script src="/js/jquery.js"></script>
    <script type="text/javascript">
        function IFrameReSize(iframename) {
            var pTar = document.getElementById(iframename);
            if (pTar) {  //ff
                if (pTar.contentDocument && pTar.contentDocument.body.offsetHeight) {
                    pTar.height = pTar.contentDocument.body.offsetHeight;
                } //ie
                else if (pTar.Document && pTar.Document.body.scrollHeight) {
                    pTar.height = pTar.Document.body.scrollHeight;
                }
            }
        }
        //iframe宽度自适应
        function IFrameReSizeWidth(iframename) {
            var pTar = document.getElementById(iframename);
            if (pTar) {  //ff
                if (pTar.contentDocument && pTar.contentDocument.body.offsetWidth) {
                    pTar.width = pTar.contentDocument.body.offsetWidth;
                }  //ie
                else if (pTar.Document && pTar.Document.body.scrollWidth) {
                    pTar.width = pTar.Document.body.scrollWidth;
                }
            }
        }
        $(function () {
            $(".menu a").click(function () {
                $(".menu li").removeClass("active");
                $(this).parents("li").addClass("active");
            });
            setInterval(function () {
                IFrameReSize("iframe_main"); IFrameReSizeWidth("iframe_main");
            }, 100);
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
          <a class="brand" href="/">免费打印照片</a>
          <div class="nav-collapse collapse">
            <p class="navbar-text pull-right">
              账号： <a href="#" class="navbar-link"><%= CurrentStore.LoginName %></a>&nbsp;&nbsp;隶属于：<a href="#" class="navbar-link"><%= CurrentStore.StoreName %></a>&nbsp;&nbsp;<a href="loginout.aspx" class="navbar-link"><i class="icon-off"></i>退出</a>
            </p>
            <ul class="nav menu">
              <li class="active"><a href="/Admin/User/default.aspx" target="iframe_main">打印服务</a></li>
              <li><a href="/Admin/Store/default.aspx" target="iframe_main">分店信息</a></li>
              <li><a href="/Admin/editPwd.aspx" target="iframe_main">修改密码</a></li>
            </ul>
          </div><!--/.nav-collapse -->
        </div>
      </div>
    </div>

    <div class="container-fluid" id="wrap">
      <div class="row-fluid">
        <iframe style="border:0px;" src="user/default.aspx" width="100%" height="100%" name="iframe_main" id="iframe_main" onload='IFrameReSize("iframe_main");IFrameReSizeWidth("iframe_main");'></iframe>
      </div><!--/row-->
    </div>

    <div id="modal-gallery" class="modal modal-gallery hide fade" tabindex="-1">
        <div class="modal-header">
            <a class="close" data-dismiss="modal">&times;</a>
            <h3 class="modal-title"></h3>
        </div>
        <div class="modal-body"><div class="modal-image"></div></div>
        <div class="modal-footer">
            <a class="btn modal-download" target="_blank"><i class="icon-download"></i>下载</a>
        </div>
    </div>
    <script src="/js/bootstrap.min.js"></script>
  </body>
</html>
