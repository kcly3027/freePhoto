<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="orderli1.ascx.cs" Inherits="freePhoto.Web.Controls.orderli1" %>
<asp:Repeater runat="server" ID="Repeater1">
    <ItemTemplate>
        <tr>
            <td><%# Eval("OrderNo") %></td>
            <td class="left5" style="height: 25px;"><%# Eval("CreateDate","{0:yyyy-MM-dd}") %></td>
            <td><%# Eval("Person") %></td>
            <td><%# Eval("Mobile") %></td>
            <td><%# Eval("State") %></td>
            <td>
                <a href="<%# Eval("OrderNo") %>" class="btn  btn-primary"><i class="icon-download-alt"></i>下载打印图</a>
                <a href="orderinfo.aspx?o=<%# Eval("OrderNo") %>" class="btn">查看</a>
            </td>
        </tr>
    </ItemTemplate>
</asp:Repeater>