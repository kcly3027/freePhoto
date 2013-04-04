﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userinfo.aspx.cs" Inherits="freePhoto.Web.Admin.User.userinfo" %>

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
          <p>用户邮箱：<strong><%= Model.Email %></strong>&nbsp;&nbsp;<span class="label label-info"><abbr title="今日可使用免费额度">3</abbr></span></p>
          <p>用户地址：<strong><%= Model.Address %></strong></p>
          <p>用户手机：<strong><%= Model.Mobile %></strong></p>
          <p>用户QQ：<strong><%= Model.QQ %></strong></p>
          <p>注册时间：<strong><%= Model.RegTime.ToString("yyyy-MM-dd") %></strong></p>
          <p>用户状态：<strong><%= Model.IsCheck ? "已激活":"<a class='btn' href='javascript:;' onclick='active();' title='发送激活邮件'>未激活</a>" %></strong></p>
          <div class="tab-content">
            <div class="tab-pane active" id="orderlist">
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th scope="col">订单号</th>
                            <th scope="col" style="height: 30px;">下单时间</th>
                            <th scope="col">收货人</th>
                            <th scope="col">联系方式</th>
                            <th scope="col">状态</th>
                            <th scope="col">操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="Repeater1">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("OrderNo") %></td>
                                    <td class="left5" style="height: 25px;"><%# Eval("CreateDate","{0:yyyy-MM-dd}") %></td>
                                    <td><%# Eval("Person") %></td>
                                    <td><%# Eval("Mobile") %></td>
                                    <td><%# Eval("State") %></td>
                                    <td><a href='orderinfo.aspx?o=<%# Eval("OrderNo") %>' class="btn">查看</a></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <% if (Repeater1.Items.Count == 0) {%><tr><td colspan="6" align="center"><div class="alert alert-error">暂无订单</div></td></tr><% } %>
                    </tbody>
                </table>
                <% if (PSize <= Record1) {%>
                <div class="pagination pagination-centered" id="page_p1">
                </div>
                <script type="text/javascript">
                    function GetP1(p) {
                        var o = $("#orderno").val();
                        $.get("orders.aspx?action=getp1", { p1: p,o:o }, function (html) {
                            if(html != ""){
                                $("#orderlist table tbody").html(html);
                                pg = new showPages('#page_p1', 'pg1', p, <%=PSize%>, <%= Record1%>,GetP1);
                                pg.printHtml();        //显示页数
                            }
                        });
                    }
                    var pg1 = new showPages('#page_p1', 'pg1', 1, <%=PSize%>, <%= Record1%>,GetP1);
                    pg1.printHtml();        //显示页数
                </script>
                <% } %>
            </div>
          </div>
      </div>
    </div>
    <script src="/js/bootstrap.min.js"></script>
  </body>
</html>