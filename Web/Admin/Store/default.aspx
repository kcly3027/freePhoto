<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="freePhoto.Web.Admin.Store._default" %>

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
              <li class="nav-header">分店信息</li>
              <% if (IsMain) {%>
              <li><a href="/Admin/Store/addStore.aspx" target="iframe1">添加分店信息</a></li>
              <li class="active"><a href="/Admin/Store/list.aspx" target="iframe1">分店列表</a></li>
                <% }else{%>
              <li class="active"><a href="/Admin/Store/editStore.aspx?d=<%= CurrentStore.StoreID %>" target="iframe1">分店列表</a></li>
                <%} %>
            </ul>
          </div><!--/.well -->
        </div><!--/span-->
        <div class="span9">
          <div class="row-fluid">
              <% if (IsMain) {%>
              <iframe style="border:0px;" src="list.aspx" width="100%" height="100%" name="iframe1" id="iframe1"></iframe>
                <% }else{%>
              <iframe style="border:0px;" src="/Admin/Store/editStore.aspx?d=<%= CurrentStore.StoreID %>" width="100%" height="100%" name="iframe1" id="iframe1"></iframe>
                <%} %>
          </div><!--/row-->
        </div><!--/span-->
      </div><!--/row-->
    </div>
    <script src="/js/bootstrap.min.js"></script>
  </body>
</html>
