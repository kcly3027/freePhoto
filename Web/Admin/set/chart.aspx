<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chart.aspx.cs" Inherits="freePhoto.Web.Admin.set.chart" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>后台管理</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <!-- Le styles -->
    <link href="/css/bootstrap.css" rel="stylesheet" />

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="/js/html5shiv.js"></script>
    <![endif]-->
    <script src="/js/jquery.js"></script>
    <script src="/js/tools/Highcharts/js/highcharts.js"></script>
    <script src="/js/tools/Highcharts/js/modules/exporting.js"></script>
  </head>

  <body>

    <div class="container-fluid">
      <div class="row-fluid">
          <div id="container" style="min-width: 400px; height: 400px; margin: 0 auto;"></div>
      </div><!--/row-->
    </div>
    
    <script src="/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#container').highcharts({
                chart: {
                    type: 'line',
                    marginRight: 130,
                    marginBottom: 45
                },
                title: {
                    text: '用户使用时段表',
                    x: -20 //center
                },
                xAxis: {
                    title: {
                        text: '使用时间（小时【0-23】)'
                    },
                    categories: ['0', '1', '2', '3', '4', '5',
                        '6', '7', '8', '9', '10', '12', '13', '14', '15', '16', '17', '18',
                        '19', '20', '21', '22', '23']
                },
                yAxis: {
                    title: {
                        text: '打印次数'
                    },
                    plotLines: [{
                        value: 0,
                        width: 1
                    }]
                },
                tooltip: {
                    valueSuffix: '次'
                },
                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'top',
                    x: -10,
                    y: 100,
                    borderWidth: 0
                },
                series: [{
                    name: '使用次数',
                    data: [<%=ChartStr%>]
                }]
            });
        });
    </script>
  </body>
</html>
