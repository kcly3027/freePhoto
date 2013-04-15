<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="freePhoto.Web.Admin.set._default" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>Bootstrap, from Twitter</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="/css/bootstrap.min.css" rel="stylesheet" />

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/js/html5shiv.js"></script>
    <![endif]-->
    <script src="/js/jquery.js"></script>
  </head>

  <body>
    <div class="container-fluid">
      <div class="row-fluid">
        <div class="span3">
          <div class="well sidebar-nav">
            <ul class="nav nav-list menu">
              <li class="nav-header">站点设置</li>
              <li><a href="/Admin/Store/addStore.aspx" target="iframe1">清除</a></li>
              <li class="active"><a href="/Admin/Store/list.aspx" target="iframe1">分店列表</a></li>
            </ul>
          </div><!--/.well -->
        </div><!--/span-->
        <div class="span9">
          <div class="row-fluid">
              <iframe style="border:0px;" src="list.aspx" width="100%" height="100%" name="iframe1" id="iframe1"></iframe>
          </div><!--/row-->
        </div><!--/span-->
      </div><!--/row-->
    </div>
    <script src="/js/bootstrap.min.js"></script>
  </body>
</html>
