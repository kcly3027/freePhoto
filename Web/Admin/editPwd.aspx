<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editPwd.aspx.cs" Inherits="freePhoto.Web.Admin.editPwd" %>

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
    <link href="/css/bootstrap-responsive.css" rel="stylesheet">

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
            <ul class="nav nav-list">
              <li class="nav-header">修改密码</li>
              <li class="active"><a href="#">修改密码</a></li>
            </ul>
          </div><!--/.well -->
        </div><!--/span-->
        <div class="span9">
          <div class="row-fluid">
            <form>
            <h2 class="form-signin-heading">Please sign in</h2>
            <input type="text" class="input-block-level span4" placeholder="请输入用户名"><br />
            <input type="password" class="input-block-level span4" placeholder="请输入密码"><br />
            <input type="password" class="input-block-level span4" placeholder="请输入新密码"><br />
            <button class="btn btn-primary" type="submit">修改</button>
            </form>
          </div><!--/row-->
        </div><!--/span-->
      </div><!--/row-->
    </div>
    <script src="/js/bootstrap.min.js"></script>
  </body>
</html>
