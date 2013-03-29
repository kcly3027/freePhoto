<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orders.aspx.cs" Inherits="freePhoto.Web.orders" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="/css/bootstrap.css" rel="stylesheet">
    <script src="/js/jquery.js" type="text/javascript"></script>
    <link href="/css/bootstrap-responsive.css" rel="stylesheet">
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
                        <input type="text" class="span4">
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
                    <table class="table table-hover" style="height:260px">
                        <thead>
                            <tr>
                                <th scope="col">订单号</th>
                                <th scope="col" style="height: 30px;">下单时间</th>
                                <th scope="col">收货人</th>
                                <th scope="col">联系方式</th>
                                <th scope="col">状态</th>
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
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <% if (Repeater1.Items.Count == 0) {%><tr><td colspan="5" align="center"><div class="alert alert-error">暂无订单</div></td></tr><% } %>
                        </tbody>
                    </table>
                    <script type="text/javascript">
                        var pg = new showPages('pg');
                        pg.pageCount = 12; //定义总页数(必要)
                        pg.argName = 'p1';    //定义参数名(可选,缺省为page)
                        pg.printHtml();        //显示页数
                    </script>
                    <div class="pagination pagination-centered">
                        <ul>
                            <li class="disabled"><a href="#">«</a></li>
                            <li class="active"><a href="#">1</a></li>
                            <li><a href="#">2</a></li>
                            <li><a href="#">3</a></li>
                            <li><a href="#">4</a></li>
                            <li><a href="#">5</a></li>
                            <li><a href="#">»</a></li>
                        </ul>
                    </div>
                </div>
                <div class="tab-pane" id="div2">
                    <table class="table table-hover" style="height:260px">
                        <thead>
                            <tr>
                                <th scope="col">订单号</th>
                                <th scope="col" style="height: 30px;">下单时间</th>
                                <th scope="col">收货人</th>
                                <th scope="col">联系方式</th>
                                <th scope="col">状态</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="Repeater2">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("OrderNo") %></td>
                                        <td class="left5" style="height: 25px;"><%# Eval("CreateDate","{0:yyyy-MM-dd}") %></td>
                                        <td><%# Eval("Person") %></td>
                                        <td><%# Eval("Mobile") %></td>
                                        <td><%# Eval("State") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <% if (Repeater2.Items.Count == 0) {%><tr><td colspan="5" align="center"><div class="alert alert-error">暂无订单</div></td></tr><% } %>
                        </tbody>
                    </table>
                    <div class="pagination pagination-centered">
                        <ul>
                            <li class="disabled"><a href="#">«</a></li>
                            <li class="active"><a href="#">1</a></li>
                            <li><a href="#">2</a></li>
                            <li><a href="#">3</a></li>
                            <li><a href="#">4</a></li>
                            <li><a href="#">5</a></li>
                            <li><a href="#">»</a></li>
                        </ul>
                    </div>
                </div>
                <div class="tab-pane" id="div3">
                    <table class="table table-hover" style="height:260px">
                        <thead>
                            <tr>
                                <th scope="col">订单号</th>
                                <th scope="col" style="height: 30px;">下单时间</th>
                                <th scope="col">收货人</th>
                                <th scope="col">联系方式</th>
                                <th scope="col">状态</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="Repeater3">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("OrderNo") %></td>
                                        <td class="left5" style="height: 25px;"><%# Eval("CreateDate","{0:yyyy-MM-dd}") %></td>
                                        <td><%# Eval("Person") %></td>
                                        <td><%# Eval("Mobile") %></td>
                                        <td><%# Eval("State") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <% if (Repeater3.Items.Count == 0) {%><tr><td colspan="5"><div class="alert alert-error">暂无订单</div></td></tr><% } %>
                        </tbody>
                    </table>
                    <div class="pagination pagination-centered">
                        <ul>
                            <li class="disabled"><a href="#">«</a></li>
                            <li class="active"><a href="#">1</a></li>
                            <li><a href="#">2</a></li>
                            <li><a href="#">3</a></li>
                            <li><a href="#">4</a></li>
                            <li><a href="#">5</a></li>
                            <li><a href="#">»</a></li>
                        </ul>
                    </div>
                </div>
            </div>


        </div>
    </div>

</body>
</html>
