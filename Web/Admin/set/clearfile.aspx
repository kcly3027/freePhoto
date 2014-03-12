<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="clearfile.aspx.cs" Inherits="freePhoto.Web.Admin.set.clearfile" %>

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

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/js/html5shiv.js"></script>
    <![endif]-->
    <script src="/js/jquery.js"></script>
  </head>

  <body>

    <div class="container-fluid">
      <div class="row-fluid">
        <h3>清理冗余文件</h3>
        <div class="span4">
            <button type="button" class="btn btn-primary" data-loading-text="处理中..." id="Button1">清除冗余文件【昨天】</button>
            <br />
            <div class="alert alert-warning">
                <strong>注意！</strong>
                该操作会将昨天以前的上传文件删除，可能导致用户创建订单失败。
            </div>
        </div>
        <div class="span4">
            <button type="button" class="btn btn-primary" data-loading-text="处理中..." id="clearFile">清除冗余文件【5分钟】</button>
            <br />
            <div class="alert alert-warning">
                <strong>注意！</strong>
                该操作会将5分钟内未使用的上传文件删除，可能导致用户创建订单失败。
            </div>
        </div>
      </div><!--/row-->
    </div>
    
    <script src="/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#clearFile").click(function () {
                $("#clearFile").button("loading");
                $.post("../admin.ashx?action=ClearFile", {}, function (r) {
                    alert(r.message);
                    $("#clearFile").button("reset");
                }, "json");
            });
            $("#Button1").click(function () {
                $("#Button1").button("loading");
                $.post("../admin.ashx?action=ClearFile1", {}, function (r) {
                    alert(r.message);
                    $("#Button1").button("reset");
                }, "json");
            });
        });
    </script>
  </body>
</html>
