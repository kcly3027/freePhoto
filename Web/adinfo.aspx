<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adinfo.aspx.cs" Inherits="freePhoto.Web.adinfo" %>
<script runat="server">
    protected string GetPrintType(string type)
    {
        switch (type)
        {
            case "pthb":
                return "黑白A4普通";
            case "ptcs":
                return "彩色A4普通";
            default:
                return "彩色照片";
        }
        return "未知";
    }
</script>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>喷嚏客</title>
    <!-- #include file="inhtml/meta.html" -->
    <script src="js/jquery.kcly.js"></script>
</head>
<body>
    <div class="container-narrow">
        <div class="masthead">
            <ul class="nav nav-pills pull-right">
                <li class="active"><a href="/" >首页</a></li>
                <% if (IsLogin()) {%>
                    <li><a href="javascript:;" ref="myinfo">我的信息</a></li>
                <% }else{%>
                    <li><a href="javascript:;" id="a_reg">注册/登录</a></li>
                <%} %>
                <li><a href="/about.aspx">联系我们</a></li>
                <li><a href="/AdPut.aspx">广告投放</a></li>
                <% if (IsLogin()) {%>
                <li><a href="/loginout.aspx">登出</a></li>
                <% }%>
            </ul>
            <h2 class="muted">喷嚏客&nbsp;&nbsp;<small> 广告投放平台</small></h2>
        </div>
        <hr />
        <div class="jumbotron">
            <ul class="breadcrumb" style="text-align:left;"></ul>
            <div><h2>订单编号：<%= Model.OrderNo %></h2></div>
            <hr />
            <div class="tab-content span8" style="text-align:left;">
              <div class="tab-pane fade active in">
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
                      <% if (Model.State == "未付款") {%>
                      <div class="span4" style="padding-top:10px;">
                          <button class="btn btn-large btn-primary" type="button">立即支付</button>
                      </div>
                      <% }%>
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
                          <span class="label label-info">doc、docx文档暂时不提供预览</span>
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
            <div class="span4" style="text-align:left;">
                <div class="well well-small">
                    <h4>批准流程</h4> 广告投放前会被审查，以保证它符合我们的广告准则建议阅读:<a href="#">广告准则</a>
                </div>
                <div class="well well-small">
                    <h4>反馈</h4> 如果您对自助广告平台有任何意见或建议，请发email给我们。Email: <a href="#">feedback@pixboks.com</a>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="/js/bootstrap.js"></script>
    <!--登录模块-->
    <!-- #include file="inhtml/login.html" -->
    <!--用户设置模块-->
    <!-- #include file="inhtml/userset.html" -->
    <!--用户设置模块-->
    <!-- #include file="inhtml/adorders.html" -->
    <div id="DivUpBody" style="display:none">
        <div class="up">
            <div class="modal-body">
                正在上传,请稍候。。。
            </div>
        </div>
        <div class="modal-backdrop"></div>
    </div>
    <div id="AdBgBody" style="display:none">
        <div class="up">
            <div class="modal-body">
                正在提交,请稍候。。。
            </div>
        </div>
        <div class="modal-backdrop"></div>
    </div>
    <script type="text/javascript">
        var StepHtml = [['<a href="javascript:void(0);" id="a_step1"><i class="icon-star"></i>上传广告图</a>', '<span>上传广告图</span>'],
            ['<a href="javascript:void(0);" id="a_step2">选择投放范围<i class="icon-check"></i></a>', '<span>选择投放范围</span>'],
            ['<a href="javascript:void(0);" id="a_step3">选择投放预算<i class="icon-check"></i></a>', '<span>选择投放预算</span>']];
        function StepShow(_step) {
            var html = "";
            for (var i = 1; i <= StepHtml.length; i++) {
                html += '<li class="' + ((i == _step) ? 'active' : '') + '">' + ((i <= _step) ? StepHtml[i - 1][0] : StepHtml[i - 1][1]) + '<span class="divider">&gt;</span></li>';
            }
            html += '<li class="active"><a href="javascript:;">预览投放效果</a><i class="icon-ok"></i></li>';
            <% if (IsLogin()) {%>
            html += '<li style="float: right;"><a ref="myorder" href="javascript:;"><i class="icon-th-list"></i>投放广告列表</a></li>';
            <% }%>
            $(".breadcrumb").html(html);
        }
        $(function () {
            StepShow(4);
        });
    </script>
</body>
</html>