<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addStore.aspx.cs" Inherits="freePhoto.Web.Admin.addStore" %>

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
    <style type="text/css">
      body {
        padding-top: 60px;
        padding-bottom: 40px;
      }
      .sidebar-nav {
        padding: 9px 0;
      }

      @media (max-width: 980px) {
        /* Enable use of floated navbar text */
        .navbar-text.pull-right {
          float: none;
          padding-left: 5px;
          padding-right: 5px;
        }
      }
    </style>
    <link href="/css/bootstrap-responsive.css" rel="stylesheet">

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/js/html5shiv.js"></script>
    <![endif]-->
    <script src="/js/jquery.js"></script>
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
              账号： <a href="#" class="navbar-link">lc_liuyang@163.com</a>&nbsp;&nbsp;隶属于：<a href="#" class="navbar-link">总店</a>&nbsp;&nbsp;<a href="#" class="navbar-link"><i class="icon-off"></i>退出</a>
            </p>
            <ul class="nav">
              <li><a href="/">首页</a></li>
              <li><a href="/Admin/User/default.aspx">打印服务</a></li>
              <li class="active"><a href="/Store/default.aspx">分店信息</a></li>
              <li><a href="/Admin/editPwd.aspx">修改密码</a></li>
            </ul>
          </div><!--/.nav-collapse -->
        </div>
      </div>
    </div>

    <div class="container-fluid">
      <div class="row-fluid">
        <div class="span3">
          <div class="well sidebar-nav">
            <ul class="nav nav-list">
              <li class="nav-header">分店信息</li>
              <li class="active"><a href="/Admin/Store/addStore.aspx">添加分店信息</a></li>
              <li><a href="/Admin/Store/default.aspx">分店列表</a></li>
              <li><a href="/Admin/Store/editStore.aspx">修改分店信息</a></li>
            </ul>
          </div><!--/.well -->
        </div><!--/span-->
        <div class="span9">
          <div class="row-fluid">
            <h3>添加分店信息</h3>
            <form class="form-horizontal">
                <div class="control-group">
                  <label class="control-label" for="inputWarning">分店名称</label>
                  <div class="controls">
                    <input type="text" id="inputWarning">
                    <span class="help-inline">请输入分店名称</span>
                  </div>
                </div>
                <div class="control-group">
                  <label class="control-label" for="inputWarning">分店地址</label>
                  <div class="controls">
                    <input type="text" id="inputWarning">
                    <span class="help-inline"><a href="">请在地图上选择饭店地址</a></span>
                  </div>
                </div>
                <div class="control-group">
                  <label class="control-label" for="inputWarning">分店账号</label>
                  <div class="controls">
                    <input type="text" id="inputWarning">
                    <span class="help-inline">请输入分店管理员账号</span>
                  </div>
                </div>
                <div class="control-group">
                  <label class="control-label" for="inputWarning">分店密码</label>
                  <div class="controls">
                    <input type="password" id="inputWarning">
                    <span class="help-inline">请输入分店管理员密码</span>
                  </div>
                </div>
                <div class="form-actions">
                  <button type="submit" class="btn btn-primary">保存</button>
                </div>
              </form>
          </div><!--/row-->
        </div><!--/span-->
      </div><!--/row-->

      <hr>

      <footer>
        <p>&copy; Company 2013</p>
      </footer>

    </div>
    <script src="/js/bootstrap.min.js"></script>
  </body>
</html>
