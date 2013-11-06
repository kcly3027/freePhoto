<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="PayResult.aspx.cs" Inherits="freePhoto.Web.PayResult" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>喷嚏客-校园精确营销领航者</title>
    <!-- #include file="inhtml/meta.html" -->
</head>
<body>
    <div class="container-narrow">
        <div class="masthead">
            <ul class="nav nav-pills pull-right">
                <li><a href="/">首页</a></li>
                <li class="active"><a href="/about.aspx">联系我们</a></li>
                <%--<li><a href="/AdPut.aspx">广告投放</a></li>--%>
            </ul>
            <h2 class="muted"><img src="img/logo.png" /></h2>
        </div>
        <hr />
        <div class="jumbotron">
            <h1><%= ht %></h1>
            <% if(NoError) {%>
            <p class="lead"></p>
            <a class="btn <%= IsSuccess ? "btn-success" : "btn-danger" %> " href="<%= Link %>">返回订单</a>
            <% } %>
          </div>
        <!-- #include file="inhtml/memo.html" -->
    </div>
    <div class="footer">
        <hr>
        <p style="text-align:center;"><a target="_blank" href="http://www.miibeian.gov.cn">浙ICP备13026483号</a></p>
        <p>&copy; 喷嚏客</p>
    </div>
    <script type="text/javascript" src="/js/bootstrap.js"></script>
</body>
</html>

