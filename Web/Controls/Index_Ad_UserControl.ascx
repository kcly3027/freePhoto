<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Index_Ad_UserControl.ascx.cs" Inherits="freePhoto.Web.Controls.Index_Ad_UserControl" %>
<style type="text/css">   
    #index_ad_left{  
        position:absolute;    
        top:100px;    
        left:16px;    
        width:100px;     
        z-index:99;
    }    
    #index_ad_right{  
        position:absolute;    
        top:100px; 
        right:16px;   
        width:100px;
        z-index:99;
    }
</style>
<script type="text/javascript">
    $(this).scroll(function () {    // 页面发生scroll事件时触发    
        var bodyTop = 0;
        if (typeof window.pageYOffset != 'undefined') {
            bodyTop = window.pageYOffset;
        }
        else if (typeof document.compatMode != 'undefined' && document.compatMode != 'BackCompat') {
            bodyTop = document.documentElement.scrollTop;
        }
        else if (typeof document.body != 'undefined') {
            bodyTop = document.body.scrollTop;
        }

        $("#index_ad_left").css("top", 100 + bodyTop)   // 设置层的CSS样式中的top属性, 注意要是小写，要符合“标准”    
        $("#index_ad_right").css("top", 100 + bodyTop)   // 设置层的CSS样式中的top属性, 注意要是小写，要符合“标准”    
    });
</script>
<% if(string.IsNullOrEmpty(left) == false) {%><div id="index_ad_left"><%= left %></div><% } %>
<% if(string.IsNullOrEmpty(right) == false) {%><div id="index_ad_right"><%= right %></div><% } %>