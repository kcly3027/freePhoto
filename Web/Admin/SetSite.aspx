<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetSite.aspx.cs" Inherits="freePhoto.Web.Admin.SetSite" %>

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
    <script src="../js/jquery.kcly.js"></script>
  </head>

  <body>
    <div class="container-fluid">
      <div class="row-fluid">
        <div class="span3">
          <div class="well sidebar-nav">
            <ul class="nav nav-list">
              <li class="nav-header">修改密码</li>
              <li class="active"><a href="#">修改密码</a></li>
            </ul>
          </div><!--/.well -->
        </div><!--/span-->
        <div class="span9">
          <div class="row-fluid">
            
          </div><!--/row-->
        </div><!--/span-->
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
        $(function () {
            $("#btn_submit").click(function () {
                var v = $("#txt_oldpwd,#txt_newpwd,#txt_newpwd1");
                if (kcly.Validate.validate(v, Msg.Show, Msg.Hide)) {
                    $("#btn_submit").button('loading');
                    var txt_oldpwd = $("#txt_oldpwd").val();
                    var txt_newpwd = $("#txt_newpwd").val();
                    $.post("editPwd.aspx?action=eidt", { oldpwd: txt_oldpwd, newpwd: txt_newpwd }, function (r) {
                        if (r.result) {
                            alert("密码修改成功"); location.reload();
                        } else {
                            alert(r.message);
                            $(".control-group").removeClass("success").find(".help-inline").text("");
                        }
                    }, "json");
                    return true;
                }
                return false;
            });
        });

        $("#txt_oldpwd").addVerify("notnull", null, "请输入旧密码").addVerify("checklength", { min: 6, max: 20 }, "密码长度要求6位到20位");
        $("#txt_newpwd").addVerify("notnull", null, "请输入新密码").addVerify("checklength", { min: 6, max: 20 }, "密码长度要求6位到20位");
        $("#txt_newpwd1").addVerify("notnull", null, "请输入新密码").addVerify("equal", { obj: "#txt_newpwd" }, "请确认密码");
    </script>
  </body>
</html>