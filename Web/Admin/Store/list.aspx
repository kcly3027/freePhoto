<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="freePhoto.Web.Admin.Store.list" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>Bootstrap, from Twitter</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/css/bootstrap-responsive.css" rel="stylesheet">

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/js/html5shiv.js"></script>
    <![endif]-->
    <script src="/js/jquery.js"></script>
  </head>
  <body>
    <div class="container-fluid">
      <div class="row-fluid">
        <div class="span9">
          <div class="row-fluid">
            <h3>分店信息列表</h3>
            <table class="table table-hover">
              <thead>
                <tr>
                  <th>#</th>
                  <th>分店名称</th>
                  <th>分店管理员</th>
                  <th>编辑</th>
                </tr>
              </thead>
              <tbody>
                  <asp:Repeater runat="server" ID="Repeater1" OnItemDataBound="Repeater1_ItemDataBound">
                      <ItemTemplate>
                        <tr>
                          <td><%# i %></td>
                          <td><%# Eval("StoreName") %></td>
                          <td><%# Eval("LoginName") %></td>
                          <td><a href="editStore.aspx?d=<%# Eval("StoreID") %>" class="btn"><i class="icon-edit"></i>编辑</a></td>
                        </tr>
                      </ItemTemplate>
                  </asp:Repeater>
              </tbody>
            </table>
          </div><!--/row-->
        </div><!--/span-->
      </div><!--/row-->
    </div>
    <script src="/js/bootstrap.min.js"></script>
  </body>
</html>