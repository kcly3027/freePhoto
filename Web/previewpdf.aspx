<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="previewpdf.aspx.cs" Inherits="freePhoto.Web.previewpdf" %>
<script runat="server">
    string imgName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        imgName = Request["file"];
    }
</script>

<!doctype html>
<html>
    <head> 
        <title>文件预览</title>                
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
        <style type="text/css" media="screen"> 
			html, body	{ height:100%; }
			body { margin:0; padding:0; overflow:auto; }   
			#flashContent { display:none; }
        </style> 
        <link href="/js/tools/flexpaper/flexpaper.css" rel="stylesheet" />
        <script src="/js/jquery.js"></script>
        <script src="/js/tools/flexpaper/flexpaper.js"></script>
        <script src="/js/tools/flexpaper/flexpaper_handlers.js"></script>
    </head> 
    <body>  
    	<div style="position:absolute;left:10px;top:10px;">
			<div id="documentViewer" class="flexpaper_viewer" style="position:relative;width:850px;height:500px"></div>
	        
	        <script type="text/javascript">
	            function getDocumentUrl(document) {
	                return "/convertpdf/{doc}".replace("{doc}", document);
	            }

	            var startDocument = "Paper";

	            $('#documentViewer').FlexPaperViewer(
                 {
                     config: {
                         PDFFile: escape(getDocumentUrl('<%= imgName %>')),
                         Scale: 0.6,
                         ZoomTransition: 'easeOut',
                         ZoomTime: 0.5,
                         ZoomInterval: 0.2,
                         FitPageOnLoad: false,
                         FitWidthOnLoad: false,
                         FullScreenAsMaxWindow: false,
                         ProgressiveLoading: true,
                         MinZoomSize: 0.2,
                         MaxZoomSize: 5,
                         SearchMatchAll: false,
                         InitViewMode: 'Portrait',
                         RenderingOrder: 'flash,html',

                         ViewModeToolsVisible: true,
                         ZoomToolsVisible: true,
                         NavToolsVisible: true,
                         CursorToolsVisible: true,
                         SearchToolsVisible: true,

                         localeChain: 'zh_CN'
                     }
                 });
	        </script>  
        </div>
   </body> 
</html> 
