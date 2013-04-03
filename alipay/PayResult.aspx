<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayResult.aspx.cs" Inherits="Web.UserCenter.VServices.alipay.PayResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>应用中心</title>
    <style>
        table th 
        {
            text-align:center;
        }
        .jifen_cx{ border:1px solid #92cadb; background:#f9feff; margin:0 auto;padding-bottom:10px; text-align:left; overflow:hidden; font-size:14px;}
        .jifen_cx_bt{padding-left:10px; height:30px; line-height:30px; border-bottom:1px solid #daf0f4; color:#000; font-size:18px; margin:0 10px;}
        .jifen_cz_01{ background:#fff; border:1px solid #d4f0f5; padding:10px;}
        .jifen_cz_01_1,.jifen_cz_01_2{ width:350px; height:120px; margin:0 auto;}
        .jifen_cz_01_1 p,.jifen_cz_01_2 p{ line-height:30px; padding-left:100px; padding-top:35px;}
        .jifen_cz_01_1 p span,.jifen_cz_01_2 p span{ font-weight:bold; font-size:16px; color:#000;}
        .jifen_cz_01_1 p a,.jifen_cz_01_2 p a{ color:#319de6;}
        .jifen_cz_01_1 p a:hover,.jifen_cz_01_2 p a:hover{ text-decoration:underline;}
        .jifen_cz_01_1{ background:url(../../Styles/airpay/ok.png) no-repeat left center;}
        .jifen_cz_01_2{ background:url(../../Styles/airpay/error.png) no-repeat left center;}
    </style>
</head>
<body>
    <div class="jifen_cx" style="height:600px;">
        <% if (Info != ""){ %>
	    <div class="jifen_cx_bt">账号充值</div>
	    <div class="jifen_cz_01">
            <% if (Info.ToLower() == "true"){ %>
            <div class="jifen_cz_01_1">
        	    <p><span>恭喜您，充值成功！</span><br /><a href="/Default.aspx">继续充值</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="/Default.aspx">返回用户中心</a></p>
            </div>
            <% } else { %>
            <div class="jifen_cz_01_2">
        	    <p><span>充值失败，请重新充值！</span><br /><a href="/Default.aspx">重新充值</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="/Default.aspx">返回用户中心</a></p>
            </div>
            <% } %>
        </div>
        <% } else { %>
	    <div class="jifen_cz_01">
            <div class="jifen_cz_01_2">
        	    <p><span><%if(string.IsNullOrEmpty(ErrorInfo)){%>抱歉，无法完成充值！<%} else {%><%= ErrorInfo %><%} %></span><br /><a href="../Pay.aspx">重新充值</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="/Default.aspx">返回用户中心</a></p>
            </div>
        </div>
        <% } %>
    </div>
</body>
</html>
