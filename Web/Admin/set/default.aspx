<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="freePhoto.Web.Admin.set._default" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>后台管理</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="/css/bootstrap.css" rel="stylesheet" />

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/js/html5shiv.js"></script>
    <![endif]-->
    <script src="/js/jquery.js"></script>
    <script type="text/javascript">
          function body_size() {
              var height = $(window).height();
              $('#iframe1').css('height', height - 41);
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
    <div class="container-fluid">
        <div class="row-fluid">
        <div class="span3">
            <div class="well sidebar-nav">
            <ul class="nav nav-list menu">
                <li class="nav-header">系统管理</li>
                <li><a href="/Admin/set/siteoption.aspx" target="iframe1">首页通知设置</a></li>
                <li><a href="/Admin/set/clearfile.aspx" target="iframe1">冗余文件清理</a></li>
                <li class="active"><a href="/Admin/set/chart.aspx" target="iframe1">使用时间统计</a></li>
                <li><a href="/Admin/set/userToExcel.aspx" target="_blank">用户资料表</a></li>
            </ul>
            </div><!--/.well -->
        </div><!--/span-->
        <div class="span9">
            <div class="row-fluid">
                <iframe src="chart.aspx" style="border:0px;" src="list.aspx" width="100%" height="100%" name="iframe1" id="iframe1"></iframe>
            </div><!--/row-->
        </div><!--/span-->
        </div><!--/row-->
    </div>
  </body>
</html>
