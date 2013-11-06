<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feedback.aspx.cs" Inherits="freePhoto.Web.feedback" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>喷嚏客</title>
    <!-- #include file="/inhtml/meta.html" -->
    <script src="/js/jquery.kcly.js"></script>
</head>
<body>
    <div class="container-narrow">
        <div class="masthead">
            <ul class="nav nav-pills pull-right ">
                <li><a href="/">首页</a></li>
                <!-- #include file="inhtml/menu.html" -->
            </ul>
            <h2 class="muted"><img src="img/logo.png" /></h2>
        </div>
        <hr />
        <% if (IsLogin()) {%>
        <div class="alert alert-success">
            <strong>提醒!</strong>
            <% if(NormalCount>0){%>您共有<%= NormalCount %>张免费普通纸，今天已经使用了<%= NormalCountNow %>张。&nbsp;&nbsp;<%} %>
            <% if(FreePhoto>0){%>您共有<%= FreePhoto %>张免费相片纸，今天已经使用了 <%= FreePhotoNow %>张。<br /><%} %>
        </div>
        <% } %>
        <div class="well well-small">
            <div class="row-fluid">
            <h3 style="text-align:center;">请填写反馈内容</h3><br />
            <form class="form-horizontal" runat="server" id="form1"> 
                <div class="control-group">
                    <label class="control-label" for="txt_content">反馈内容：</label>
                    <div class="controls">
                        <textarea id="txt_content" name="txt_content" style="margin: 0px; width: 579px; height: 196px;"></textarea>
                        <span class="help-inline" r=""></span>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label" for="txt_code">验证码：</label>
                    <div class="controls">
                        <img src="/ValidateCode.aspx" id="CheckCodeImg" style="vertical-align: middle;" />&nbsp;&nbsp;
                        <a class="btn btn-info" onclick="$('#CheckCodeImg').attr('src','/ValidateCode.aspx?'+Math.random());">看不清？点一下</a>
                        <br />
                        <input style="margin-top:10px;" type="text" id="txt_code" name="txt_code" value="" maxlength="4"/>
                        <span class="help-inline" r=""></span>
                    </div>
                </div>
                <div class="form-actions">
                    <button type="button" class="btn btn-primary" data-loading-text="提交中..." id="btn_submit">确定提交</button>
                </div>
                <input type="hidden" value="" id="hid_filekey" name="hid_filekey" />
            </form>
            <input type="button" id="upload_btncanel" style="display:none" />
            </div><!--/row-->
        </div>
    </div>
    <div class="footer clearfix">
        <hr>
        <p style="text-align:center;"><a target="_blank" href="http://www.miibeian.gov.cn">浙ICP备13026483号</a></p>
        <p>&copy; 喷嚏客</p>
    </div>
    <!--登录模块-->
    <!-- #include file="inhtml/login.html" -->
    <!--用户设置模块-->
    <!-- #include file="inhtml/userset.html" -->
    <!--订单列表模块-->
    <!-- #include file="inhtml/orders.html" -->
    <div class="modal hide fade" id="ResultModel">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
        <h3>反馈成功</h3>
      </div>
      <div class="modal-body">
        <p>One fine body…</p>
      </div>
      <div class="modal-footer">
        <a href="javascript:;" class="btn btn-primary" data-dismiss="modal" aria-hidden="true">确定</a>
      </div>
    </div>
    <script type="text/javascript" src="/js/bootstrap.js"></script>
    <script type="text/javascript">
        NoLoginStr = "只有注册用户才能反馈问题!";
        $(function () {
            var Msg = {};
            Msg.Show = function (msg, input) {
                input.focus(function () {
                    var help_inline = $(this).parents(".control-group").removeClass("error").find(".help-inline");
                    help_inline.text(help_inline.attr("r"));
                }).parents(".control-group").addClass("error").find(".help-inline").text(msg);
            }
            Msg.Hide = function (input) {
                input.parents(".control-group").removeClass("error").addClass("success").find(".help-inline").text("验证成功");
            }
            $("#btn_submit").click(function () {
                var v = $("#txt_content,#txt_code");
                if (kcly.Validate.validate(v, Msg.Show, Msg.Hide)) {
                    $("#btn_submit").button('loading');
                    var _content = $("#txt_content").val();
                    var _code = $("#txt_code").val();
                    $.post(location.href, { "action":"To",content:_content,code:_code }, function (r) {
                        if (r.result) {
                            $('#ResultModel').modal('show').find(".modal-body p").html("反馈成功，谢谢您的关注!<br/>");
                        } else {
                            if (r.message == "login") { alert("登录后才能反馈问题"); $("#a_reg").click(); }
                            else alert(r.message);
                        }
                        $("#btn_submit").button('reset');
                    }, "json");
                    return true;
                }
                return false;
            });
            $("#txt_content").addVerify("notnull", null, "请输入反馈内容").addVerify("checklength", { min: 1, max: 2000 }, "请输入反馈内容1-12个字符");
            $("#txt_code").addVerify("notnull", null, "请输入验证码").addVerify("checklength", { min: 4 }, "请输入正确的验证码");;
            $('#ResultModel').on('hidden', function () {
                location.href = "/addorder.aspx";
            })
        });
    </script>
</body>
</html>