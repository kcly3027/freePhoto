<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderinfo.aspx.cs" Inherits="freePhoto.Web.Admin.User.orderinfo" %>
<script runat="server">
    private string GetPrintType(string ptype)
    {
        if (ptype == "photo") return "相片纸";
        if (ptype == "normal") return "普通纸";
        return "未知";
    }
    private string GetPreview(string fileExt, string filekey)
    {
        return freePhoto.Web.DbHandle.OrderTools.GetPreview(fileExt, filekey);
    }
</script>
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
    <script src="../../js/showpage.js"></script>
    <script type="text/javascript">
        function DoneOrder() {
            if (confirm("请确认，该操作会导致订单状态改变为【已完成】，并且会删除打印文件！")) {
                $.post("../admin.ashx?action=DoneOrder", {o:'<%= OrderModel.OrderNo %>'}, function (r) {
                    if (r.result) {
                        alert(r.message);
                        location.reload();
                    } else {
                        alert(r.message);
                    }
                }, "json");
            }
        }
        function active() {
            $.post("userinfo.aspx?u=<%= Model.UserID %>&action=active", {}, function (r) {
                alert(r.message);
            }, "json");
        }
    </script>
  </head>

  <body>
    <div class="container-fluid">
      <div class="row-fluid">
        <ul id="myTab" class="nav nav-tabs">
            <li class="active"><a href="#orderinfo" data-toggle="tab">订单信息</a></li>
            <li><a href="#userinfo" data-toggle="tab">用户信息</a></li>
        </ul>
          <div class="tab-content">
            <div class="tab-pane active" id="orderinfo">
                <div class="span10">
                    <table class="table table-striped table-bordered">
                      <tbody>
                          <tr><td colspan="2" style="text-align:center;"><h2>订单编号：<%= OrderModel.OrderNo %>&nbsp;&nbsp;<small>订单状态：<%= OrderModel.State %></small></h2></td></tr>
                          <tr><td rowspan="5" style="text-align:center;"><em>订单基本信息</em></td><td>取件人：<%= OrderModel.Person %></td></tr>
                          <tr><td>取件人手机：<%= OrderModel.Mobile %></td></tr>
                          <tr><td>取件人地址：<%= OrderModel.Address %></td></tr>
                          <tr><td>打印纸：<%= GetPrintType(OrderModel.PrintType) %></td></tr>
                          <tr><td>打印份数：<%= OrderModel.PrintNum %></td></tr>
                          <tr><td rowspan="4" style="text-align:center;"><em>在线支付</em></td><td>免费打印：<%= OrderModel.FreeCount %></td></tr>
                          <tr><td>需付费：<%= OrderModel.PayCount %></td></tr>
                          <tr><td>单张价钱：<%= OrderModel.Price %></td></tr>
                          <tr><td>合计：<%= OrderModel.Total_fee %></td></tr>
                      </tbody>
                    </table>
                    <form class="form-horizontal">
                        <div class="form-actions">
                            <% if (OrderModel.State == "已完成") {%>
                            <span class="label label-success">订单已完成，源文件已删除</span>
                            <% }else{%>
                                <a href="/upfile/<%= OrderModel.FileKey %><%= OrderModel.FileType %>" target="_blank" class="btn  btn-primary"><i class="icon-download-alt"></i>下载打印文件</a>
                                <button type="button" onclick="DoneOrder()" class="btn btn-warning">确认完成</button>
                                
                                <% if (freePhoto.Web.DbHandle.OrderTools.IsWord(OrderModel.FileType)) {%>
                                <span class="label label-warning">doc、docx文档暂时不提供预览</span>
                                <% }else{%>
                                <a href="<%= GetPreview(OrderModel.FileType,OrderModel.FileKey) %>" class="btn">预览文件</a>
                                <%} %>
                            <%} %>
                        </div>
                    </form>
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