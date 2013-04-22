<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addStore.aspx.cs" Inherits="freePhoto.Web.Admin.addStore" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>后台管理</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <!-- Le styles -->
    <link href="/css/bootstrap.css" rel="stylesheet" />

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/js/html5shiv.js"></script>
    <![endif]-->
    <script src="/js/jquery.js"></script>
    <script src="/js/jquery.kcly.js"></script>
  </head>

  <body>

    <div class="container-fluid">
      <div class="row-fluid">
        <h3>添加分店信息</h3>
        <form class="form-horizontal" runat="server" id="form1"> 
            <div class="control-group">
                <label class="control-label" for="txt_shopname">分店名称</label>
                <div class="controls">
                <asp:TextBox runat="server" ID="txt_shopname"></asp:TextBox>
                <span class="help-inline" r="请输入分店名称">请输入分店名称</span>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="txt_address">分店地址</label>
                <div class="controls">
                <asp:TextBox runat="server" ID="txt_address"></asp:TextBox><asp:HiddenField runat="server" ID="hid_address" Value=""/>&nbsp;&nbsp;<a href="javascript:;" id="btn_selectmap">在地图上选择</a>
                <span class="help-block" r="请输入分店地址"></span>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="txt_username">分店账号</label>
                <div class="controls">
                <asp:TextBox runat="server" ID="txt_username"></asp:TextBox>
                <span class="help-inline" r="请输入分店管理员账号">请输入分店管理员账号</span>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="txt_pwd">分店密码</label>
                <div class="controls">
                <asp:TextBox runat="server" ID="txt_pwd" TextMode="Password"></asp:TextBox>
                <span class="help-inline" r="请输入分店管理员密码">请输入分店管理员密码</span>
                </div>
            </div>
            <div class="form-actions">
                <asp:Button runat="server" Text="登录"  CssClass="btn btn-primary" data-loading-text="提交中..." ID="btn_submit" OnClick="btn_submit_Click"/>
            </div>
        </form>
      </div><!--/row-->
    </div>
    
    <script src="/js/bootstrap.min.js"></script>
    <script type="text/javascript">
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
        function SelectedAddress(g) {
            if (g != undefined) {
                var v = g.lat + "|" + g.lng + "|" + g.zoom;
                $("#hid_address").val(v);
            }
        }
        $(function () {
            $("#btn_submit").click(function () {
                var v = $("#txt_shopname,#txt_address,#hid_address,#txt_username,#txt_pwd");
                if (kcly.Validate.validate(v, Msg.Show, Msg.Hide)) {
                    $("#btn_submit").button('loading');
                    /*
                    var $username = $("#username").val();
                    var $pwd = $("#pwd").val();
                    $.post("login.aspx?action=login", { $username: $username, $pwd: $pwd }, function () {
                    }, "json");
                    */
                    return true;
                }
                return false;
            });
            $("#btn_selectmap").click(function () {
                var v = $("#hid_address").val();
                window.top.SelectBaiduMap(v, SelectedAddress);
            });
        });
        $("#txt_shopname").addVerify("notnull", null, "分店名称不能为空");
        $("#txt_address").addVerify("notnull", null, "分店地址不能为空");
        $("#hid_address").addVerify("notnull", null, "请在地图上选择分店地址");
        $("#txt_username").addVerify("notnull", null, "请输入分店用户名");
        $("#txt_pwd").addVerify("notnull", null, "请输入密码").addVerify("checklength", { min: 6, max: 20 }, "密码长度要求6位到20位");
    </script>
  </body>
</html>
