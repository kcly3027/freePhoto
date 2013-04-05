<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="orderli.ascx.cs" Inherits="freePhoto.Web.Controls.orderli" %>
<asp:Repeater runat="server" ID="Repeater1">
    <ItemTemplate>
        <tr>
            <td><%# Eval("OrderNo") %></td>
            <td class="left5" style="height: 25px;"><%# Eval("CreateDate","{0:yyyy-MM-dd}") %></td>
            <td><%# Eval("PrintNum") %></td>
            <td><%# Eval("State") %></td>
            <td><a href="/oinfo.aspx?o=<%# Eval("OrderNo") %>" target="_blank" class="btn">查看</a></td>
        </tr>
    </ItemTemplate>
</asp:Repeater>