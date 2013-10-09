<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="freePhoto.Web.service.alipay._default" %>


<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>跳转到支付宝</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="/css/bootstrap.css" rel="stylesheet">
    <style type="text/css">
      body {
        padding-top: 40px;
        padding-bottom: 40px;
        background-color: #f5f5f5;
      }
    </style>
    <script src="/js/jquery.js"></script>
    <!--[if lt IE 7]><link rel="stylesheet" href="/css/bootstrap-ie6.min.css"><![endif]-->
    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
        <script src="/js/html5shiv.js"></script>
    <![endif]-->
  </head>

  <body>

    <div class="container">
      <div class="alert alert-info" style="text-align:center;">
          <%= PageError %>
      </div>
    </div>
  </body>
</html>
