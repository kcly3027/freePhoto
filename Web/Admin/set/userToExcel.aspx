<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userToExcel.aspx.cs" Inherits="freePhoto.Web.Admin.set.userToExcel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>        table
        {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>  
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
                <table border="1">
                <thead>
                    <tr><td colspan="8" align="center"><h1>用户统计表</h1></td></tr>
                    <tr>
                        <td align="center">用户邮箱</td>
                        <td align="center">用户地址</td>
                        <td align="center">用户QQ</td>
                        <td align="center">用户手机</td>
                        <td align="center">注册时间</td>
                        <td align="center">打印次数</td>
                        <td align="center">打印张数</td>
                        <td align="center">最后使用时间</td>
                    </tr>
                </thead>
                <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center"><%# Eval("Email") %></td>
                    <td align="center"><%# Eval("Address") %></td>
                    <td align="center"><%# Eval("QQ") %></td>
                    <td align="center"><%# Eval("Mobile") %></td>
                    <td align="center"><%# Eval("RegTime","{0:yyyy-MM-dd HH:mm}") %></td>
                    <td align="center"><%# Eval("p2") %></td>
                    <td align="center"><%# Eval("p") %></td>
                    <td align="center"><%# Eval("mt","{0:yyyy-MM-dd HH:mm}") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
                <tfoot>
                    <tr><td colspan="8">共：【<%=Repeater1.Items.Count  %>】个用户</td></tr>
                </tfoot>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
