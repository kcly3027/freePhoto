<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="feedbackli.ascx.cs" Inherits="freePhoto.Web.Controls.feedbackli" %>
<asp:Repeater runat="server" ID="Repeater1">
    <ItemTemplate>
    <tr>
        <td><%# i %></td>
        <td><%# Eval("FTime","{0:yyyy-MM-dd HH:mm:ss}") %></td>
        <td><%# Eval("Email") %></td>
        <td><div style="display:none;" id='View_<%# Eval("FID") %>'><%# Eval("FContent") %></div><button type="button" class="btn btn-success" onclick="View('#View_<%# Eval("FID") %>')">查看反馈内容</button></td>
        <td>
            <a href="userinfo.aspx?u=<%# Eval("UserID") %>" class="btn">查看用户</a>
            <a href="adorderinfo.aspx?o=<%# Eval("FID") %>" class="btn btn-danger">删除</a>
        </td>
    </tr>
    </ItemTemplate>
</asp:Repeater>