<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderinfo.aspx.cs" Inherits="freePhoto.Web.Admin.User.orderinfo" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>Bootstrap, from Twitter</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/css/bootstrap-responsive.css" rel="stylesheet">

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/js/html5shiv.js"></script>
    <![endif]-->
    <script src="/js/jquery.js"></script>
    <script src="../../js/showpage.js"></script>
  </head>

  <body>
    <div class="container-fluid">
      <div class="row-fluid">
        <h3>订单编号：<strong><%= OrderModel.OrderNo %></strong></h3>
        <ul id="myTab" class="nav nav-tabs">
            <li class="active"><a href="#orderinfo" data-toggle="tab">订单信息</a></li>
            <li><a href="#userinfo" data-toggle="tab">用户信息</a></li>
        </ul>
          <div class="tab-content">
            <div class="tab-pane active" id="orderinfo">
                <div class="span10">
                    <div class="span5 thumbnail"><img src="/upimages/showcrop/<%= ImgModel.ImgKey %>.jpg" alt=""></div>
                    <div class="span5">
                        <div class="caption">
                            <p>收件人名称：<strong><%= OrderModel.Person %></strong></p>
                            <p>收件人手机：<strong><%= OrderModel.Mobile %></strong></p>
                            <p>收件人地址：<strong><%= OrderModel.Address %></strong></p>
                            <p>订单状态：<strong><%= OrderModel.State %></strong></p>
                            <%if (OrderModel.State == "已支付" || OrderModel.State == "已取件") {%>
                            <p>支付宝订单号：<strong><%= OrderModel.AlipayNo %></strong></p>
                            <p>支付时间：<strong><%= OrderModel.PayDate.ToString("yyyy-MM-dd") %></strong></p>
                            <% } %>
                            <p><a href="#" class="btn btn-primary"><i class="icon-download-alt"></i>下载打印图</a><%if (OrderModel.State != "已取件") {%> <a href="#" class="btn">已取件</a><%} %></p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tab-pane" id="userinfo">
                <p>用户邮箱：<strong><%= Model.Email %></strong></p>
                <p>用户地址：<strong><%= Model.Address %></strong></p>
                <p>用户手机：<strong><%= Model.Mobile %></strong></p>
                <p>用户QQ：<strong><%= Model.QQ %></strong></p>
                <p>注册时间：<strong><%= Model.RegTime.ToString("yyyy-MM-dd") %></strong></p>
                <p>用户状态：<strong><%= Model.IsCheck ? "已激活":"<a class='btn' href='javascript:;' onclick='active();' title='发送激活邮件'>未激活</a>" %></strong></p>
            </div>
          </div>
      </div>
    </div>
    <script src="/js/bootstrap.min.js"></script>
  </body>
</html>