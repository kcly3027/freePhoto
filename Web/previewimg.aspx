﻿<%@ Page Language="C#" AutoEventWireup="true"%>
<script runat="server">
    string imgName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        imgName = Request["file"];
    }
</script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <img src="/convertimg/<%= imgName %>" />
    </div>
    </form>
</body>
</html>
