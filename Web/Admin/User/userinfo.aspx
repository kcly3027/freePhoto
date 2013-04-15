<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userinfo.aspx.cs" Inherits="freePhoto.Web.Admin.User.userinfo" %>
<script runat="server">
    private string GetPrintType(object ptype)
    {
        if (ptype.ToString() == "photo") return "相片纸";
        if (ptype.ToString() == "normal") return "普通纸";
        return "未知";
    }
</script>
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
    <script src="../../js/showpage.js"></script>
    <script type="text/javascript">
        function active(){
            $.post("userinfo.aspx?u=<%= Model.UserID %>&action=active",{},function(r){
                alert(r.message);
            },"json");
        }
        function GetPwd(){
            $.post("userinfo.aspx?u=<%= Model.UserID %>&action=restpwd",{},function(r){
                alert(r.message);
            },"json");
        }
    </script>
  </head>

  <body>
    <div class="container-fluid">
      <div class="row-fluid">
          <p>用户邮箱：<strong><%= Model.Email %></strong>&nbsp;&nbsp;<span class="label label-info"><abbr title="今日可使用免费额度">3</abbr></span></p>
          <p>用户地址：<strong><%= Model.Address %></strong></p>
          <p>用户手机：<strong><%= Model.Mobile %></strong></p>
          <p>用户QQ：<strong><%= Model.QQ %></strong></p>
          <p>注册时间：<strong><%= Model.RegTime.ToString("yyyy-MM-dd") %></strong></p>
          <p>用户状态：<strong><%= Model.IsCheck ? "已激活":"<a class='btn' href='javascript:;' onclick='active();' title='发送激活邮件'>未激活</a>" %><a class='btn' href='javascript:;' onclick='GetPwd();' title='重置密码'>重置密码</a></strong></p>
          <div class="tab-content">
            <div class="tab-pane active" id="orderlist">
                <table class="table table-hover">
                    <thead>
                        <tr>
                            <th scope="col">订单号</th>
                            <th scope="col" style="height: 30px;">下单时间</th>
                            <th scope="col">打印份数</th>
                            <th scope="col">打印纸</th>
                            <th scope="col">支付金额</th>
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
                                <td><%# Eval("PrintNum") %></td>
                                <td><%# GetPrintType(Eval("PrintType")) %></td>
                                <td><%# Eval("Total_fee") %></td>
                                <td><%# Eval("State") %></td>
                                <td>
                                    <a href="/upfile/<%# Eval("FileKey") %><%# Eval("FileType") %>" target="_blank" class="btn  btn-primary"><i class="icon-download-alt"></i>下载打印文件</a>
                                    <a href="orderinfo.aspx?o=<%# Eval("OrderNo") %>" class="btn">查看</a>
                                </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <% if (Repeater1.Items.Count == 0) {%><tr><td colspan="7" align="center"><div class="alert alert-error">暂无订单</div></td></tr><% } %>
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