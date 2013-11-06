<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feedbacklist.aspx.cs" Inherits="freePhoto.Web.Admin.User.feedbacklist" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>后台管理</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="/css/bootstrap.css" rel="stylesheet" />

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/js/html5shiv.js"></script>
    <![endif]-->
    <script src="/js/jquery.js"></script>
    <script src="../../js/showpage.js"></script>
  </head>

  <body>
    <div class="container-fluid">
      <div class="row-fluid">
            <table class="table table-hover" id="orderlist">
              <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">反馈时间</th>
                    <th scope="col">客户邮箱</th>
                    <th scope="col">反馈内容</th>
                    <th scope="col">操作</th>
                </tr>
              </thead>
              <tbody>
                <asp:Repeater runat="server" ID="Repeater1" OnItemDataBound="Repeater1_ItemDataBound">
                    <ItemTemplate>
                    <tr>
                        <td><%# i %></td>
                        <td><%# Eval("FTime","{0:yyyy-MM-dd HH:mm:ss}") %></td>
                        <td><%# Eval("Email") %></td>
                        <td><div style="display:none;" id="View_<%# Eval("FID") %>"><%# Eval("FContent") %></div><button class="btn btn-success" type="button" onclick="View('#View_<%# Eval("FID") %>')">查看反馈内容</button></td>
                        <td>
                            <a href="userinfo.aspx?u=<%# Eval("UserID") %>" class="btn">查看用户</a>
                            <a href="javascript:Del('<%# Eval("FID") %>')" class="btn btn-danger">删除</a>
                        </td>
                    </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <% if (Repeater1.Items.Count == 0) {%><tr><td colspan="5" align="center"><div class="alert alert-error">暂无反馈</div></td></tr><% } %>
              </tbody>
            </table>
          <% if (PSize <= Record) {%>
            <div class="pagination pagination-centered" id="page_p1">
            </div>
            <script type="text/javascript">
                function GetP1(p) {
                    $.get("feedbacklist.aspx?action=get", { p: p }, function (html) {
                        if(html != ""){
                            $("#orderlist tbody").html(html);
                            pg = new showPages('#page_p1', 'pg1', p, <%=PSize%>, <%= Record%>,GetP1);
                            pg.printHtml();        //显示页数
                        }
                    });
                }
                var pg1 = new showPages('#page_p1', 'pg1', 1, <%=PSize%>, <%= Record%>,GetP1);
                pg1.printHtml();        //显示页数
            </script>
            <% } %>
      </div><!--/row-->
    </div>
    <script src="/js/bootstrap.min.js"></script>
    <div class="modal hide fade" id="ViewModel">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
        <h3>反馈内容</h3>
      </div>
      <div class="modal-body">
        <p>One fine body…</p>
      </div>
      <div class="modal-footer">
        <a href="javascript:;" class="btn btn-primary" data-dismiss="modal" aria-hidden="true">确定</a>
      </div>
    </div>
    <script type="text/javascript">
        function View(id){
            $('#ViewModel').modal('show').find(".modal-body p").html($(id).html());
        }
        function Del(fid){
            if(confirm("确认要删除这条记录么？")){
                $.post(location.href,{"action":"del",FID:fid},function(result){
                    if(result == "success"){ alert("删除成功"); $("#View_"+fid).parents("tr").remove(); }
                    else{alert("删除失败");}
                });
            }
        }
    </script>
  </body>
</html>