<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="userli.ascx.cs" Inherits="freePhoto.Web.Controls.userli" %>
<asp:Repeater runat="server" ID="Repeater1" OnItemDataBound="Repeater1_ItemDataBound">
    <ItemTemplate>
        <tr>
            <td><%# i %></td>
            <td><a href="mailto:<%# Eval("Email") %>"><%# Eval("Email") %></a></td>
            <td><%# Eval("Address") %></td>
            <td><%# Eval("Mobile") %></td>
            <td><%# Eval("QQ") %></td>
            <td><a href="userinfo.aspx?u=<%# Eval("UserID") %>" class="btn">查看</a></td>
        </tr>
    </ItemTemplate>
</asp:Repeater>