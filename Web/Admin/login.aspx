<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="freePhoto.Web.Admin.login" %>


<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>登录</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <!-- Le styles -->
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
      body {
        padding-top: 40px;
        padding-bottom: 40px;
        background-color: #f5f5f5;
      }
      .form-signin {
        max-width: 600px;
        padding: 19px 29px 29px;
        margin: 0 auto 20px;
        background-color: #fff;
        border: 1px solid #e5e5e5;
        -webkit-border-radius: 5px;
           -moz-border-radius: 5px;
                border-radius: 5px;
        -webkit-box-shadow: 0 1px 2px rgba(0,0,0,.05);
           -moz-box-shadow: 0 1px 2px rgba(0,0,0,.05);
                box-shadow: 0 1px 2px rgba(0,0,0,.05);
      }
      .form-signin input[type="text"],
      .form-signin input[type="password"] {
        font-size: 16px;
        height: auto;
        margin-bottom: 15px;
        padding: 7px 9px;
      }
    </style>
    <link href="/css/bootstrap-responsive.min.css" rel="stylesheet" />
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/js/html5shiv.js"></script>
    <![endif]-->
    <script src="/js/jquery.js"></script>
    <script src="/js/jquery.kcly.js"></script>
    <script type="text/javascript">
        var Msg = {};
        Msg.Show = function (msg, input) {
            input.parents(".control-group").addClass("error").find(".help-block").text(msg);
        }
        Msg.Hide = function (input) {
            input.parents(".control-group").removeClass("error").addClass("success").find(".help-block").text("验证成功");
        }
        $(function () {
            $("#fat-btn").click(function () {
                if (kcly.Validate.validate($("#username,#pwd"), Msg.Show, Msg.Hide)) {
                    $("#fat-btn").button('loading');
                    var $username = $("#username").val();
                    var $pwd = $("#pwd").val();
                    $.post("login.aspx?action=login", { $username: $username, $pwd: $pwd }, function () {
                    }, "json");
                }
            });
            $("#username").addVerify("notnull", null, "用户名不能为空!").focus(function () {
                $(this).parents(".control-group").removeClass("error").find(".help-block").text("请输入用户名，用户名不能为空");
            });
            $("#pwd").addVerify("notnull", null, "密码不能为空!").addVerify("checklength", { min: 6, max: 20 }, "用户名不能为空!").focus(function () {
                $(this).parents(".control-group").removeClass("error").find(".help-block").text("请输入密码，最少6位");
            });
        });
    </script>
  </head>

  <body>
    <div class="container">
        <form class="form-horizontal form-signin">
          <input type="hidden" name="isPost" value="1" />
          <fieldset>
	        <legend>请登录</legend>
	        <div class="control-group">
	          <label class="control-label" for="input01">用户名：</label>
	          <div class="controls">
                  <input type="text" class="input-xlarge" name="username" id="username" placeholder="请输入用户名">
	              <p class="help-block">请输入用户名，用户名不能为空</p>
	          </div>
	        </div>

	        <div class="control-group">
	          <label class="control-label" for="input01">密码：</label>
	          <div class="controls">
	            <input type="password" class="input-xlarge" name="pwd" id="pwd" placeholder="请输入密码">
	            <p class="help-block">请输入密码，最少6位</p>
	          </div>
	        </div>
            <div class="form-actions" style="background-color: #fff;border:0px;">
                <button id="fat-btn" data-loading-text="loading..." class="btn btn-primary" type="button">登录</button>
            </div>
          </fieldset>
        </form>
    </div>
    <script src="/js/bootstrap.min.js"></script>
  </body>
</html>
