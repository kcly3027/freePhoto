<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orders.aspx.cs" Inherits="freePhoto.Web.orders" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="/css/bootstrap.min.css" rel="stylesheet">
    <script src="/js/jquery.js" type="text/javascript"></script>
    <link href="/css/bootstrap-responsive.min.css" rel="stylesheet">
    <!--[if lt IE 7]><link rel="stylesheet" href="/css/bootstrap-ie6.min.css"><![endif]-->
    <script type="text/javascript" src="/js/bootstrap.js"></script>
    <script src="/js/showpage.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            window.top.Loading("hide");
            $("a[ref='page'],input[type='submit']").click(function () {
                window.top.Loading("show");
            });
        });
    </script>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="navbar">
                <div class="navbar-inner">
                    <ul class="nav">
                        <li><a href="javascript:;">订单号：</a></li>
                    </ul>
                    <form class="navbar-form pull-left" id="form1" runat="server">
                        <input type="text" class="span4" id="orderno" name="o">
                        <button type="submit" class="btn">查询</button>
                    </form>
                </div>
            </div>

            <ul class="nav nav-tabs">
                <li class="active"><a href="#div1" data-toggle="tab">订单一览</a></li>
                <li><a href="#div2" data-toggle="tab">未付款订单</a></li>
                <li><a href="#div3" data-toggle="tab">未取件订单</a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane active" id="div1">
                    <table class="table table-hover" style="height:256px">
                        <thead>
                            <tr>
                                <th scope="col">订单号</th>
                                <th scope="col" style="height: 30px;">下单时间</th>
                                <th scope="col">打印份数</th>
                                <th scope="col">状态</th>
                                <th scope="col">&nbsp;</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="Repeater1">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("OrderNo") %></td>
                                        <td class="left5" style="height: 25px;"><%# Eval("CreateDate","{0:yyyy-MM-dd}") %></td>
                                        <td><%# Eval("PrintNum") %></td>
                                        <td><%# Eval("State") %></td>
                                        <td><a href="/oinfo.aspx?o=<%# Eval("OrderNo") %>" target="_blank" class="btn">查看</a></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <% if (Repeater1.Items.Count == 0) {%><tr><td colspan="5" align="center"><div class="alert alert-error">暂无订单</div></td></tr><% } %>
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
                                    $("#div1 table tbody").html(html);
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
                <div class="tab-pane" id="div2">
                    <table class="table table-hover" style="height:256px">
                        <thead>
                            <tr>
                                <th scope="col">订单号</th>
                                <th scope="col" style="height: 30px;">下单时间</th>
                                <th scope="col">打印份数</th>
                                <th scope="col">状态</th>
                                <th scope="col">&nbsp;</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="Repeater2">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("OrderNo") %></td>
                                        <td class="left5" style="height: 25px;"><%# Eval("CreateDate","{0:yyyy-MM-dd}") %></td>
                                        <td><%# Eval("PrintNum") %></td>
                                        <td><%# Eval("State") %></td>
                                        <td><a href="/oinfo.aspx?o=<%# Eval("OrderNo") %>" target="_blank" class="btn">查看</a></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <% if (Repeater2.Items.Count == 0) {%><tr><td colspan="5" align="center"><div class="alert alert-error">暂无订单</div></td></tr><% } %>
                        </tbody>
                    </table>
                    <% if (PSize <= Record2) {%>
                    <div class="pagination pagination-centered" id="page_p2">
                    </div>
                    <script type="text/javascript">
                        function GetP2(p) {
                            var o = $("#orderno").val();
                            $.get("orders.aspx?action=getp2", { p2: p, o:o }, function (html) {
                                if(html != ""){
                                    $("#div2 table tbody").html(html);
                                    pg2 = new showPages('#page_p2', 'pg2', p, <%=PSize%>, <%= Record2%>,GetP2);
                                    pg2.printHtml();        //显示页数
                                }
                            });
                        }
                        var pg2 = new showPages('#page_p2', 'pg2', 1, <%=PSize%>, <%= Record2%>,GetP2);
                        pg2.printHtml();        //显示页数
                    </script>
                    <% } %>
                </div>
                <div class="tab-pane" id="div3">
                    <table class="table table-hover" style="height:256px">
                        <thead>
                            <tr>
                                <th scope="col">订单号</th>
                                <th scope="col" style="height: 30px;">下单时间</th>
                                <th scope="col">打印份数</th>
                                <th scope="col">状态</th>
                                <th scope="col">&nbsp;</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="Repeater3">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("OrderNo") %></td>
                                        <td class="left5" style="height: 25px;"><%# Eval("CreateDate","{0:yyyy-MM-dd}") %></td>
                                        <td><%# Eval("PrintNum") %></td>
                                        <td><%# Eval("State") %></td>
                                        <td><a href="/oinfo.aspx?o=<%# Eval("OrderNo") %>" target="_blank" class="btn">查看</a></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <% if (Repeater3.Items.Count == 0) {%><tr><td colspan="5"><div class="alert alert-error">暂无订单</div></td></tr><% } %>
                        </tbody>
                    </table>
                    <% if (PSize <= Record3) {%>
                    <div class="pagination pagination-centered" id="page_p3">
                    </div>
                    <script type="text/javascript">
                        function GetP3(p) {
                            var o = $("#orderno").val();
                            $.get("orders.aspx?action=getp3", { p3: p, o: o }, function (html) {
                                if(html != ""){
                                    $("#div3 table tbody").html(html);
                                    pg3 = new showPages('#page_p3', 'pg3', p, <%=PSize%>, <%= Record3%>,GetP3);
                                    pg3.printHtml();        //显示页数
                                }
                            });
                        }
                        var pg3 = new showPages('#page_p2', 'pg3', 1, <%=PSize%>, <%= Record3%>,GetP3);
                        pg3.printHtml();        //显示页数
                    </script>
                    <% } %>
                </div>
            </div>


        </div>
    </div>

</body>
</html>
