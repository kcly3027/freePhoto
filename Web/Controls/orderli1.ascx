<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="orderli1.ascx.cs" Inherits="freePhoto.Web.Controls.orderli1" %>
<script runat="server">
    private string GetPrintType(object ptype)
    {
        if (ptype.ToString() == "photo") return "相片纸";
        if (ptype.ToString() == "normal") return "普通纸";
        return "未知";
    }
    private string GetPrintType(object state, object filekey, object filetype)
    {
        switch (state.ToString())
        {
            case "已完成":
                return "";
            default:
                return "<a href='/upfile/" + filekey.ToString() + filetype.ToString() + " target='_blank' class='btn  btn-primary'><i class='icon-download-alt'></i>下载打印文件</a>";
        }
    }
</script>
<asp:Repeater runat="server" ID="Repeater1">
    <ItemTemplate>
        <tr>
        <td><%# Eval("OrderNo") %></td>
        <td class="left5" style="height: 25px;"><%# Eval("CreateDate","{0:yyyy-MM-dd}") %></td>
        <td><%# Eval("PrintNum") %></td>
        <td><%# GetPrintType(Eval("PrintType")) %></td>
        <td><%# Eval("Total_fee") %></td>
        <td><%# Eval("State") %></td>
        <td>
            <%# GetPrintType(Eval("State"),Eval("FileKey"),Eval("FileType")) %>
            <a href="orderinfo.aspx?o=<%# Eval("OrderNo") %>" class="btn">查看</a>
        </td>
            </tr>
    </ItemTemplate>
</asp:Repeater>