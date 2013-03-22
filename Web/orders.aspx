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
                <li><a href="#div2" data-toggle="tab">未取件订单</a></li>
                <li><a href="#div3" data-toggle="tab">已取件订单</a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane active" id="div1">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th scope="col">订单号</th>
                                <th scope="col" style="height: 30px;">上传时间</th>
                                <th scope="col">状态</th>
                                <th scope="col">收货人</th>
                                <th scope="col">联系方式</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>0000000000012971</td>
                                <td class="left5" style="height: 25px;">2013/3/20 23:39:41</td>
                                <td>订单已接受
                                </td>
                                <td>M</td>
                                <td>-- 13733897701
                                </td>
                            </tr>
                            <tr>
                                <td>0000000000012971</td>
                                <td class="left5" style="height: 25px;">2013/3/20 23:39:41</td>
                                <td>订单已接受
                                </td>
                                <td>M</td>
                                <td>-- 13733897701
                                </td>
                            </tr>
                            <tr>
                                <td>0000000000012971</td>
                                <td class="left5" style="height: 25px;">2013/3/20 23:39:41</td>
                                <td>订单已接受
                                </td>
                                <td>M</td>
                                <td>-- 13733897701
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="tab-pane" id="div2">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th scope="col">订单号</th>
                                <th scope="col" style="height: 30px;">上传时间</th>
                                <th scope="col">状态</th>
                                <th scope="col">收货人</th>
                                <th scope="col">联系方式</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>0000000000012971</td>
                                <td class="left5" style="height: 25px;">2013/3/20 23:39:41</td>
                                <td>订单已接受
                                </td>
                                <td>M</td>
                                <td>-- 13733897701
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="tab-pane" id="div3">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th scope="col">订单号</th>
                                <th scope="col" style="height: 30px;">上传时间</th>
                                <th scope="col">状态</th>
                                <th scope="col">收货人</th>
                                <th scope="col">联系方式</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>0000000000012971</td>
                                <td class="left5" style="height: 25px;">2013/3/20 23:39:41</td>
                                <td>订单已接受
                                </td>
                                <td>M</td>
                                <td>-- 13733897701
                                </td>
                            </tr>
                            <tr>
                                <td>0000000000012971</td>
                                <td class="left5" style="height: 25px;">2013/3/20 23:39:41</td>
                                <td>订单已接受
                                </td>
                                <td>M</td>
                                <td>-- 13733897701
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>


        </div>
    </div>

</body>
</html>
