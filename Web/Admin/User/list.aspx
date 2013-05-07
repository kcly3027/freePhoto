<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="freePhoto.Web.Admin.User.list" %>

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
  </head>

  <body>
    <div class="container-fluid">
      <div class="row-fluid">
        <div class="navbar">
            <div class="navbar-inner">
                <ul class="nav">
                    <li><a href="javascript:;">用户邮箱：</a></li>
                </ul>
                <form class="navbar-form pull-left" id="form1" runat="server">
                    <input type="text" class="span8" id="txt_email" name="email">
                    <button type="submit" class="btn">查询</button>
                </form>
            </div>
        </div>
        <table class="table table-hover" id="dt_userlist">
            <thead>
            <tr>
                <th>#</th>
                <th>客户邮箱</th>
                <th>客户地址</th>
                <th>客户手机</th>
                <th>客户QQ</th>
                <th>操作</th>
            </tr>
            </thead>
            <tbody>
            <asp:Repeater runat="server" ID="Repeater1" OnItemDataBound="Repeater1_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td><%# i %></td>
                        <td><a href="mailto:<%# Eval("Email") %>"><%# Eval("Email") %></a></td>
                        <td><%# Eval("Address") %></td>
                        <td><%# Eval("Mobile") %></td>
                        <td><%# Eval("QQ") %></td>
                        <td>
                            <a href="userinfo.aspx?u=<%# Eval("UserID") %>" class="btn">查看</a>
                            <a href="javascript:void(0);" class="btn btn-danger" onclick="DelUser(this,<%# Eval("UserID") %>)">删除</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <% if (Repeater1.Items.Count == 0) {%><tr><td colspan="6" align="center"><div class="alert alert-error">暂无用户</div></td></tr><% } %>
            </tbody>
        </table>
      </div><!--/row-->
      <% if (PSize <= Record) {%>
        <div class="pagination pagination-centered" id="page_p">
        </div>
        <script type="text/javascript">
            function GetP(p) {
                var o = $("#txt_email").val();
                $.get("list.aspx?action=get", { p: p,email:o }, function (html) {
                    if(html != ""){
                        $("#dt_userlist tbody").html(html);
                        pg = new showPages('#page_p', 'pg1', p, <%=PSize%>, <%= Record%>,GetP);
                        pg.printHtml();        //显示页数
                    }
                });
            }
            var pg1 = new showPages('#page_p', 'pg1', 1, <%=PSize%>, <%= Record%>,GetP);
            pg1.printHtml();        //显示页数
        </script>
      <% } %>
    </div>
    <script type="text/javascript">
        function DelUser(obj,userid){
            var that = $(obj);
            if(confirm("警告！此动作将会该用户所有资料和订单！")){
                if(confirm("再次警告！此动作将会该用户所有资料和订单！")){
                    $.post("list.aspx?action=deluser", { userid:userid }, function (r) {
                        if(r.result){
                            alert(r.message);
                            that.parents("tr").remove();
                        }else{
                            alert(r.message);
                        }
                    },"json");
                }
            }
        }
    </script>
    <script src="/js/bootstrap.min.js"></script>
  </body>
</html>
