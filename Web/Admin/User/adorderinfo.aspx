<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adorderinfo.aspx.cs" Inherits="freePhoto.Web.Admin.User.adorderinfo" %>
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
        function DoneOrder() {
            if (confirm("请确认，该操作会导致订单状态改变为【已完成】，并且会删除打印文件！")) {
                $.post("../admin.ashx?action=DoneAdOrder", { o: '<%= Model.OrderNo %>' }, function (r) {
                    if (r.result) {
                        alert(r.message);
                        location.reload();
                    } else {
                        alert(r.message);
                    }
                }, "json");
            }
        }
    </script>
  </head>

  <body>
    <div class="container-fluid">
      <div class="span8">
        <div><h2>订单编号：<%= Model.OrderNo %>&nbsp;&nbsp;<small>订单状态:<%= Model.State %></small></h2></div>
            <hr />
            <div class="row">
                <div class="span8">
                    <h3>订单明细</h3>
                </div>
                <div class="span2">
                    <h4>打印纸：<%= GetPrintType(Model.PrintType) %></h4>
                    <h4>单价：<em><%= Model.Price %></em>元</h4>
                </div>
                <div class="span2">
                    <h4>打印数量：<em><%= Model.PrintNum %></em>份</h4>
                    <h4>需付款：<span class="label label-important"><%= Model.Total_fee %></span>元</h4>
                </div>
                <div class="span4" style="padding-top:10px;">
                    <% if (Model.State != "已完成") {%>
                    <a href="/upfile/<%= Model.FileKey %><%= Model.FileType %>" target="_blank" class="btn  btn-primary"><i class="icon-download-alt"></i>下载打印文件</a>
                    <button class="btn btn-warning" type="button" onclick="DoneOrder()">确认完成</button>
                    <% } %>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="span4">
                    <h4>广告预览</h4>
                    <hr />
                    <% if (Model.State == "已完成") {%>
                    <span class="label label-success">订单已完成，源文件已删除</span>
                    <% }else{%>
                        <% if (freePhoto.Web.DbHandle.OrderTools.IsWord(Model.FileType)) {%>
                    <span class="label label-warning">doc、docx文档暂时不提供预览</span>
                        <% }else{%>
                    <img src="/convertimg/<%= Model.FileKey %><%= Model.FileType %>" class="img-polaroid" style="max-width:300px;max-height:300px;">
                        <%} %>
                    <%} %>
                </div>
                <div class="span4">
                    <h4>详细信息</h4>
                    <hr />
                    <h5>广告名称：<em><%= Model.AdName %></em></h5>
                    <h5>广告关键字：<em><%= Model.AdKeyWord %></em></h5>
                    <h5>日期：<em><%= Model.AdBeginTime.ToString("yyyy/MM/dd") %>一<%= Model.AdEndTime.ToString("yyyy/MM/dd") %></em></h5>
                    <h5>投放比例：<em>男（<%= Model.NanNvBL.Split('|')[0] %>%）/女（<%= Model.NanNvBL.Split('|')[1] %>%）</em></h5>
                    <dl>
                        <dt>选择店面：</dt>
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate><dd><%# Eval("StoreName") %></dd></ItemTemplate>
                        </asp:Repeater>
                    </dl>

                </div>
            </div>
        </div>
      </div>
    <script src="/js/bootstrap.min.js"></script>
  </body>
</html>