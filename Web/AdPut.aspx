<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdPut.aspx.cs" Inherits="freePhoto.Web.AdPut" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>喷嚏客-免费云印</title>
    <!-- #include file="inhtml/meta.html" -->
    <script src="js/jquery.kcly.js"></script>
    <script src="js/jquery.ajaxfileupload.js"></script>
    <script src="js/tools/My97DatePicker/WdatePicker.js"></script>
    <link href="js/tools/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" />
</head>
<body>
    <div class="container-narrow">
        <div class="masthead">
            <ul class="nav nav-pills pull-right">
                <li class="active"><a href="/" >首页</a></li>
                <% if (IsLogin()) {%>
                    <li><a href="javascript:;" ref="myinfo">我的信息</a></li>
                <% }else{%>
                    <li><a href="javascript:;" id="a_reg">注册/登录</a></li>
                <%} %>
                <li><a href="/about.aspx">联系我们</a></li>
                <li><a href="/AdPut.aspx">广告投放</a></li>
                <% if (IsLogin()) {%>
                <li><a href="/loginout.aspx">登出</a></li>
                <% }%>
            </ul>
            <h2 class="muted"><img src="img/logo.png" />&nbsp;&nbsp;<small> 广告投放平台</small></h2>
        </div>
        <hr />
        <div class="jumbotron">
            <ul class="breadcrumb" style="text-align:left;">
            </ul>
            <div class="tab-content span8" style="text-align:left;">
              <div class="tab-pane fade active in" id="step1">
                <form class="form-horizontal">
                    <fieldset>
                    <legend><h3>上传广告</h3></legend>
                  <div class="control-group">
                    <label class="control-label" for="txt_AdKeyWord">广告关键词：</label>
                    <div class="controls">
                        <input type="text" id="txt_AdKeyWord" placeholder="请用逗号分隔">
                        <span class="help-block" r="例如：儿童 玩具 奶粉。请用逗号分隔">例如：儿童 玩具 奶粉。请用逗号分隔</span>
                    </div>
                  </div>
                  <div class="control-group">
                    <label class="control-label" for="inputPassword">广告设计图：</label>
                    <div class="controls">
                        <div class="fileupdiv">
                            <button type="button" class="btn" data-loading-text="提交中..." id="uploadify"><i class="icon-upload"></i>上传文件</button>
                            <input type="file" class="ufile" name="ufile" id="btn_upfile" onchange="UpFile(this)" />
                            <input type="hidden" id="hid_FileKey" value="" />
                        </div>
                        <span class="help-block" r="请上传广告图">请上传广告图</span>
                    </div>
                  </div>
                  <div class="control-group">
                    <label class="control-label" for="inputPassword">版权声明：</label>
                    <div class="controls">
                        <blockquote style="padding-bottom:10px;margin-bottom:0px;">
                          <div style="font-size:14px;">广告设计图中的数字图像受著作权法保护。图盒遵守中国和国际版权法的规定，不会为没有得到版权或已经授权的图片发布广告. 
                        提交此广告时，我承诺我拥有此广告里面所有元素的版权，或者已经得到著作权人的授权.
                          </div>  
                        </blockquote>
                        <div class="row">
                            <label class="radio inline span1"><input type="radio" name="ro_agree" id="ro_agree1" value="1" checked="checked">我同意</label>
                            <label class="radio inline span1"><input type="radio" name="ro_agree" id="ro_agree2" value="2">我不同意</label>
                        </div>
                        <div class="alert" style="display:none;margin-top:8px;margin-bottom:0px;" id="ro_agree_alert">
                          <strong>警告</strong> 您必须认同我们的版权声明！
                        </div>
                    </div>
                  </div>
                  <div>
                    <ul class="pager">
                        <li><a href="javascript:void(0);" onclick="CheckStep(2,1)">下一步</a></li>
                    </ul>
                  </div>
                  </fieldset>
                </form>
              </div>
              <div class="tab-pane fade" id="step2">
                <form class="form-horizontal">
                    <fieldset>
                    <legend><h3>投放范围</h3></legend>
                  <div class="control-group">
                    <label class="control-label" for="txt_AdBeginTime">开始日期：</label>
                    <div class="controls">
                        <input type="text" id="txt_AdBeginTime" onfocus="WdatePicker({minDate:'%y-%M-%d',maxDate:'#F{$dp.$D(\'txt_AdEndTime\')}'})" class="Wdate" placeholder="<%= DateTime.Now.ToString("yyyy-MM-dd") %>" value="<%= DateTime.Now.ToString("yyyy-MM-dd") %>">
                        <span class="help-block" r="请选择广告投放的开始日期">请选择广告投放的开始日期</span>
                    </div>
                  </div>
                  <div class="control-group">
                    <label class="control-label" for="txt_AdEndTime" >结束日期：</label>
                    <div class="controls">
                        <input type="text" id="txt_AdEndTime" class="Wdate" onFocus="WdatePicker({minDate:'#F{$dp.$D(\'txt_AdBeginTime\')||\'%y-%M-%d\'}'})" />
                        <span class="help-block" r="请选择广告投放的结束日期">请选择广告投放的结束日期</span>
                    </div>
                  </div>
                  <div class="control-group">
                    <label class="control-label">选择店面：</label>
                    <div class="controls">
                        <select multiple="multiple" style="margin-bottom:3px;" id="s1">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <option value="<%# Eval("StoreID") %>"><%# Eval("StoreName") %></option>
                                </ItemTemplate>
                            </asp:Repeater>
                        </select>
                        <select multiple="multiple" id="s_StoreID"> 
                        </select>
                        <span class="help-block" r="双击选择店面">双击选择店面</span>
                    </div>
                  </div>
                  <div class="control-group">
                    <label class="control-label" for="inputPassword">投放比例：</label>
                    <div class="controls">
                        <div class="input-prepend input-append">
                          <span class="add-on">男：</span>
                          <input class="span1" id="txt_nan" type="text" value="50" maxlength="3" max="100" min="0">
                          <span class="add-on">%</span>
                        </div>
                        <div class="input-prepend input-append">
                          <span class="add-on">女：</span>
                          <input class="span1" id="txt_nv" type="text" value="50" maxlength="3" max="100" min="0">
                          <span class="add-on">%</span>
                        </div>
                        <span class="help-block" r="请填写男女投放比例">请填写男女投放比例</span>
                    </div>
                  </div>
                  <div>
                      <ul class="pager">
                        <li><a href="javascript:void(0);" onclick="CheckStep(1,10)">上一步</a></li>
                        <li><a href="javascript:void(0);" onclick="CheckStep(3,2)">下一步</a></li>
                      </ul>
                  </div>
                  </fieldset>
                </form>
              </div>
              <div class="tab-pane fade" id="step3">
                <form class="form-horizontal">
                    <fieldset>
                    <legend><h3>投放预算</h3></legend>
                  <div class="control-group">
                    <label class="control-label" for="txt_AdName">广告名称：</label>
                    <div class="controls">
                        <input type="text" id="txt_AdName" placeholder="">
                        <span class="help-block" r="请填写男女投放比例">请填写广告名称</span>
                    </div>
                  </div>
                  <div class="control-group">
                    <label class="control-label" for="inputEmail">打印纸：</label>
                    <div class="controls">
                        <label class="radio">
                          <input type="radio" name="or_PrintType" id="Radio1" value="pthb" checked="checked">
                          黑白A4普通(<%= freePhoto.Web.AppCode.ConstData.PTHBPaper %>元)
                        </label>
                        <label class="radio">
                          <input type="radio" name="or_PrintType" id="Radio2" value="ptcs">
                          彩色A4普通(<%= freePhoto.Web.AppCode.ConstData.PTCSPaper %>元)
                        </label>
                        <label class="radio">
                          <input type="radio" name="or_PrintType" id="Radio3" value="xpcs">
                          彩色照片(<%= freePhoto.Web.AppCode.ConstData.XPCSPaper %>元)
                        </label>
                    </div>
                  </div>
                  <div class="control-group">
                    <label class="control-label" for="txt_PrintNum">预计可投放：</label>
                    <div class="controls">
                        <input type="text" id="txt_PrintNum" style="width:30px;" min="1" max="100000">
                    </div>
                  </div>
                  
                    <div>
                        <ul class="pager">
                          <li><a href="javascript:void(0);" onclick="CheckStep(2,10)">上一步</a></li>
                          <li><a href="javascript:void(0);" onclick="CheckStep(3,3)">完成提交</a></li>
                        </ul>
                    </div>
                  </fieldset>
                </form>
              </div>
            </div>
            <div class="span4" style="text-align:left;">
                <div class="well well-small">
                    <h4>批准流程</h4> 广告投放前会被审查，以保证它符合我们的广告准则建议阅读:<a href="#">广告准则</a>
                </div>
                <div class="well well-small">
                    <h4>反馈</h4> 如果您对自助广告平台有任何意见或建议，请发email给我们。Email: <a href="#">ad@pentike.com</a>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="/js/bootstrap.js"></script>
    <!--登录模块-->
    <!-- #include file="inhtml/login.html" -->
    <!--用户设置模块-->
    <!-- #include file="inhtml/userset.html" -->
    <!--用户设置模块-->
    <!-- #include file="inhtml/adorders.html" -->
    <div id="DivUpBody" style="display:none">
        <div class="up">
            <div class="modal-body">
                正在上传,请稍候。。。
            </div>
        </div>
        <div class="modal-backdrop"></div>
    </div>
    <div id="AdBgBody" style="display:none">
        <div class="up">
            <div class="modal-body">
                正在提交,请稍候。。。
            </div>
        </div>
        <div class="modal-backdrop"></div>
    </div>
    <script type="text/javascript">
        var StepHtml = [['<a href="javascript:void(0);" id="a_step1"><i class="icon-star"></i>上传广告图</a>', '<span>上传广告图</span>'],
            ['<a href="javascript:void(0);" id="a_step2">选择投放范围<i class="icon-check"></i></a>', '<span>选择投放范围</span>'],
            ['<a href="javascript:void(0);" id="a_step3">选择投放预算<i class="icon-check"></i></a>', '<span>选择投放预算</span>']];
        function StepShow(_step) {
            var html = "";
            for(var i = 1;i<= StepHtml.length;i++){
                html += '<li class="' + ((i == _step) ? 'active' : '') + '">' + ((i <= _step) ? StepHtml[i - 1][0] : StepHtml[i - 1][1]) + '<span class="divider">&gt;</span></li>';
            }
            html += '<li>预览投放效果</li>';
            <% if (IsLogin()) {%>
            html += '<li style="float: right;"><a ref="myorder" href="javascript:;"><i class="icon-th-list"></i>投放广告列表</a></li>';
            <% }%>
            $(".breadcrumb").html(html);
            $("#step" + _step).addClass("active in").siblings().removeClass("active in");
        }
        function UpFile(obj) {
            $("#uploadify").button('loading');
            $("#DivUpBody").show();
            $.ajaxFileUpload({
                url: "/upimg.ashx?action=upfile",
                data: {},
                fileElementId: obj, secureuri: false, dataType: "json",
                success: function (r, status) {
                    $("#uploadify").button('reset');
                    $("#DivUpBody").hide();
                    if (r.result) {
                        alert("文件上传成功");
                        $("#hid_FileKey").val(r.obj.FileKey);
                        $("#uploadify").parents(".control-group").find(".help-block").text($(obj).val());
                        $("#uploadify").html('<i class="icon-upload"></i>重新上传');
                    } else {
                        alert(r.message);
                    }
                },
                error: function (data, status, sender) {
                    alert("上传失败！");
                    $("#uploadify").button('reset');
                    $("#DivUpBody").hide();
                }
            });
        }
        var Msg = {};
        Msg.Show = function (msg, input) {
            input.focus(function () {
                var help_block = $(this).parents(".control-group").removeClass("error").find(".help-block");
                help_block.text(help_block.attr("r"));
            }).parents(".control-group").addClass("error").find(".help-block").text(msg);
        }
        Msg.Hide = function (input) {
            input.parents(".control-group").removeClass("error");
        }

        function CheckStep(step, ck) {
            if (ck == 0 || ck == 1) {
                if (kcly.Validate.validate($("#txt_AdKeyWord,#hid_FileKey"), Msg.Show, Msg.Hide)) {
                    var checkedValue = $("input[name='ro_agree']:checked").val();
                    if (checkedValue != "1") { $("#ro_agree_alert").show(); return 1; }
                } else { return 1; }
            }
            if (ck == 0 || ck == 2) {
                if (!kcly.Validate.validate($("#txt_AdBeginTime,#txt_AdEndTime,#s_StoreID,#txt_nan,#txt_nv"), Msg.Show, Msg.Hide)) {
                    return 2;
                }
            }
            if (ck == 0 || ck == 3) {
                if (kcly.Validate.validate($("#txt_AdName,#txt_PrintNum"), Msg.Show, Msg.Hide)) {
                    
                    var AdKeyWord = $("#txt_AdKeyWord").val();
                    var FileKey = $("#hid_FileKey").val();
                    var AdBeginTime = $("#txt_AdBeginTime").val();
                    var AdEndTime = $("#txt_AdEndTime").val();
                    var AdStore = $("#s_StoreID").val().join(",");
                    var NanNvBL = $("#txt_nan").val() + '|' + $("#txt_nv").val();
                    var AdName = $("#txt_AdName").val();
                    var PrintNum = $("#txt_PrintNum").val();
                    var PrintType = $(":radio[name='or_PrintType']:checked").val();
                    $("#AdBgBody").show();
                    $.post("AdPut.aspx?action=Create", {
                        AdKeyWord: AdKeyWord,FileKey: FileKey,AdBeginTime: AdBeginTime,
                        AdEndTime: AdEndTime, AdStore: AdStore, NanNvBL: NanNvBL, AdName: AdName, PrintNum: PrintNum, PrintType: PrintType
                    }, function (r) {
                        if (r.result) {
                            alert("订单创建成功!\r\n您的订单号为：" + r.message);
                            location.href = "/adinfo.aspx?o=" + r.message;
                        } else {
                            if (r.message == "login") { alert("登录后才能正常下单"); $("#a_reg").click(); }
                            else alert(r.message);
                        }
                        $("#AdBgBody").hide();
                    }, "json");

                } else { return 3; }
            }
            StepShow(step);
            return 0;
        }
        $(function () {
            StepShow(1);
            $("#txt_AdKeyWord").addVerify("notnull", null, "请输入广告关键词");
            $("#hid_FileKey").addVerify("notnull", null, "请上传广告图");
            $("#txt_AdBeginTime").addVerify("notnull", null, "请选择广告投放的开始日期");
            $("#txt_AdEndTime").addVerify("notnull", null, "请选择广告投放的结束日期");
            $("#s_StoreID").addVerify("notnull", null, "请选择所要投放的店面");
            $("#txt_nan").LimitNumber().addVerify("notnull", null, "请填写男女投放比例").addVerify("numbersize", { min: 0, max: 100 }, "投放比例应该在0到100之间");
            $("#txt_nv").LimitNumber().addVerify("notnull", null, "请填写男女投放比例").addVerify("numbersize", { min: 0, max: 100 }, "投放比例应该在0到100之间");
            $("#txt_AdName").addVerify("notnull", null, "请输入广告名称");
            $("#txt_PrintNum").LimitNumber().addVerify("notnull", null, "请输入预投放量").addVerify("test", "PlusInt", "预投放量应该为正整数");
            $(":radio[name='ro_agree']").change(function () {
                var value = $(this).val();
                if (value == "2") { $("#ro_agree_alert").show(); }
                else $("#ro_agree_alert").hide();
            });
            $("#btn_upfile").click(function () {
                $(this).parents(".control-group").removeClass("error");
            });
            $("#s1").dblclick(function () {
                $(this).find("option:selected").appendTo("#s_StoreID");
            });
            $("#s_StoreID").dblclick(function () {
                $(this).find("option:selected").appendTo("#s1");
            });
            $("#txt_nan").blur(function () {
                var value = $(this).val();
                if (parseInt(value) !== NaN && parseInt(value) <= 100 && parseInt(value) >= 0) {
                    $("#txt_nv").val((100 - parseInt(value)));
                } else {
                    $("#txt_nan,#txt_nv").val("50");
                }
            });
            $("#txt_nv").blur(function () {
                var value = $(this).val();
                if (parseInt(value) !== NaN && parseInt(value) <= 100 && parseInt(value) >= 0) {
                    $("#txt_nan").val((100 - parseInt(value)));
                } else {
                    $("#txt_nan,#txt_nv").val("50");
                }
            });
            $("#btn_upfile").hover(function () {
                $("#uploadify").addClass("btn-primary");
            }, function () {
                $("#uploadify").removeClass("btn-primary");
            });
        });
    </script>
</body>
</html>