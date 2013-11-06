<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="siteoption.aspx.cs" Inherits="freePhoto.Web.Admin.set.siteoption" %>

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
      <link href="../../js/kindeditor-4.1.9/themes/simple/simple.css" rel="stylesheet" />
      <script src="../../js/kindeditor-4.1.9/kindeditor-all-min.js"></script>
      <script src="../../js/kindeditor-4.1.9/lang/zh_CN.js"></script>
      <script type="text/javascript">
          var editor_left, editor_right;
          KindEditor.ready(function (K) {
              editor_left = K.create('textarea[name="txt_left"]', {
                  resizeType: 1,
                  uploadJson: '/js/kindeditor-4.1.9/asp.net/upload_json.ashx',
                  allowPreviewEmoticons: false,
                  allowImageUpload: true,
                  items: [
                      'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                      'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                      'insertunorderedlist', '|', 'emoticons', 'image', 'link']
              });
              editor_right = K.create('textarea[name="txt_right"]', {
                  resizeType: 1,
                  uploadJson: '/js/kindeditor-4.1.9/asp.net/upload_json.ashx',
                  allowPreviewEmoticons: false,
                  allowImageUpload: true,
                  items: [
                      'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                      'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                      'insertunorderedlist', '|', 'emoticons', 'image', 'link']
              });
          });
      </script>
  </head>

  <body>

    <div class="container-fluid">
      <div class="row-fluid">
        <h3>首页通知设置</h3>
        <form class="form-horizontal" runat="server" id="form1"> 
            <div class="control-group">
                <label class="control-label" for="txt_left">左侧通知</label>
                <div class="controls">
                <asp:TextBox runat="server" ID="txt_left" TextMode="MultiLine"></asp:TextBox>
                <span class="help-inline" r="请输入左侧通知"></span>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="txt_right">右侧通知</label>
                <div class="controls">
                <asp:TextBox runat="server" ID="txt_right" TextMode="MultiLine"></asp:TextBox>
                <span class="help-block" r="请输入左侧通知"></span>
                </div>
            </div>
            <div class="form-actions">
                <asp:Button runat="server" Text="保存"  CssClass="btn btn-primary" data-loading-text="提交中..." ID="btn_submit" OnClick="btn_submit_Click"/>
            </div>
        </form>
      </div><!--/row-->
    </div>
    
    <script src="/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btn_submit").click(function () {
                $("#btn_submit").button('loading');
                return true;
            });
        });
    </script>
  </body>
</html>