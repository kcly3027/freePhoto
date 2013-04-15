<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="printPhoto.aspx.cs" Inherits="freePhoto.Web.Admin.User.printPhoto" %>
<script runat="server">
    private string GetPrintType(object ptype)
    {
        if (ptype.ToString() == "photo") return "相片纸";
        if (ptype.ToString() == "normal") return "普通纸";
        return "未知";
    }
    private string GetPrintType(object state, object filekey, object filetype)
    {
        switch (state.ToString())
        {
            case "已完成":
                return "";
            default:
                return "<a href='/upfile/" + filekey.ToString() + filetype.ToString() + " target='_blank' class='btn  btn-primary'><i class='icon-download-alt'></i>下载打印文件</a>";
        }
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
  </head>

  <body>
    <div class="container-fluid">
      <div class="row-fluid">
            <div class="navbar">
                <div class="navbar-inner">
                    <ul class="nav">
                        <li><a href="javascript:;">订单编号：</a></li>
                    </ul>
                    <form class="navbar-form pull-left" id="form1" method="post" action="printPhoto.aspx?p=1">
                        <input type="text" class="span5" id="txt_order" name="order" value="<%= OrderNo %>">
                        <select name="type" id="type" class="span4">
                            <option value=""<% if (OType == "") {%> selected="selected"<%} %>>全部订单</option>
                            <option value="未付款"<% if (OType == "未付款") {%> selected="selected"<%} %>>未付款</option>
                            <option value="已付款"<% if (OType == "已付款") {%> selected="selected"<%} %>>已付款</option>
                            <option value="已取件"<% if (OType == "已取件") {%> selected="selected"<%} %>>已取件</option>
                        </select>
                        <button type="submit" class="btn">查询</button>
                    </form>
                </div>
            </div>
            <table class="table table-hover" id="orderlist">
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
                            <%# GetPrintType(Eval("State"),Eval("FileKey"),Eval("FileType")) %>
                            <a href="orderinfo.aspx?o=<%# Eval("OrderNo") %>" class="btn">查看</a>
                        </td>
                    </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <% if (Repeater1.Items.Count == 0) {%><tr><td colspan="7" align="center"><div class="alert alert-error">暂无订单</div></td></tr><% } %>
              </tbody>
            </table>
          <% if (PSize <= Record) {%>
            <div class="pagination pagination-centered" id="page_p1">
            </div>
            <script type="text/javascript">
                function GetP1(p) {
                    var order = $("#txt_order").val();
                    var type = $("#type").val();
                    $.get("printPhoto.aspx?action=get", { p: p,order:order,type:type }, function (html) {
                        if(html != ""){
                            $("#orderlist tbody").html(html);
                            pg = new showPages('#page_p1', 'pg1', p, <%=PSize%>, <%= Record%>,GetP1);
                            pg.printHtml();        //显示页数
                        }
                    });
                }
                var pg1 = new showPages('#page_p1', 'pg1', 1, <%=PSize%>, <%= Record%>,GetP1);
                pg1.printHtml();        //显示页数
            </script>
            <% } %>
      </div><!--/row-->
    </div>
    <script src="/js/bootstrap.min.js"></script>
  </body>
</html>
