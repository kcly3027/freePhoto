/*
Example
----------------------
var pg = new showPages('pg');
pg.pageCount = 12; //定义总页数(必要)
pg.argName = 'p';    //定义参数名(可选,缺省为page)
pg.printHtml();        //显示页数
*/
function showPage(target, page, psize, record, toPage) {
    this.target = target;
    this.page = page;
    this.psize = psize;
    this.record = record;
    this.toPage = toPage;
    this.pageCount = 0;    //总页数
    this.argName = 'p'; //参数名
}

showPages.prototype.getPage = function(){ //丛url获得当前页数,如果变量重复只获取最后一个
    var args = location.search;
    var reg = new RegExp('[\?&]?' + this.argName + '=([^&]*)[&$]?', 'gi');
    var chk = args.match(reg);
    this.page = RegExp.$1;
}
showPages.prototype.checkPages = function(){ //进行当前页数和总页数的验证
    if (isNaN(parseInt(this.page))) this.page = 1;
    if (isNaN(parseInt(this.pageCount))) this.pageCount = 1;
    if (this.page < 1) this.page = 1;
    if (this.pageCount < 1) this.pageCount = 1;
    if (this.page > this.pageCount) this.page = this.pageCount;
    this.page = parseInt(this.page);
    this.pageCount = parseInt(this.pageCount);
}
showPages.prototype.createHtml = function(mode){ //生成html代码
    var strHtml = '', prevPage = this.page - 1, nextPage = this.page + 1;

    strHtml += '<ul>';
    if (prevPage < 1) {
        strHtml += '<li class="disabled"><a href="javascript:;">«</a></li>';
        strHtml += '<li class="disabled"><a href="javascript:;">‹</a></li>';
    } else {
        strHtml += '<li><a href="javascript:' + this.name + '.toPage(1);">«</a></li>';
        strHtml += '<li><a href="javascript:' + this.name + '.toPage(' + prevPage + ');">‹</a></li>';
    }
    if (this.page % 10 == 0) {
        var startPage = this.page - 9;
    } else {
        var startPage = this.page - this.page % 10 + 1;
    }
    if (startPage > 10) strHtml += '<li title="上十页"><a href="javascript:' + this.name + '.toPage(' + (startPage - 1) + ');">...</a></li>';
    for (var i = startPage; i < startPage + 10; i++) {
        if (i > this.pageCount) break;
        if (i == this.page) {
            strHtml += '<li  class="active" title="页 ' + i + '"><a href="javascript:;">' + i + '</a></li>';
        } else {
            strHtml += '<li title="页 ' + i + '"><a href="javascript:' + this.name + '.toPage(' + i + ');">' + i + '</a></li>';
        }
    }
    if (this.pageCount >= startPage + 10) strHtml += '<li title="下十页"><a href="javascript:' + this.name + '.toPage(' + (startPage + 10) + ');">...</a></li>';
    if (nextPage > this.pageCount) {
        strHtml += '<li class="disabled"><a href="javascript:;">›</a></li>';
        strHtml += '<li class="disabled"><a href="javascript:;">»</a></li>';
    } else {
        strHtml += '<li><a href="javascript:' + this.name + '.toPage(' + nextPage + ');">›</a></li>';
        strHtml += '<li><a href="javascript:' + this.name + '.toPage(' + this.pageCount + ');">»</a></li>';
    }
    strHtml += '</ul>';
    return strHtml;
}
showPages.prototype.createUrl = function (page) { //生成页面跳转url
    if (isNaN(parseInt(page))) page = 1;
    if (page < 1) page = 1;
    if (page > this.pageCount) page = this.pageCount;
    var url = location.protocol + '//' + location.host + location.pathname;
    var args = location.search;
    var reg = new RegExp('([\?&]?)' + this.argName + '=[^&]*[&$]?', 'gi');
    args = args.replace(reg,'$1');
    if (args == '' || args == null) {
        args += '?' + this.argName + '=' + page;
    } else if (args.substr(args.length - 1,1) == '?' || args.substr(args.length - 1,1) == '&') {
        args += this.argName + '=' + page;
    } else {
        args += '&' + this.argName + '=' + page;
    }
    return url + args;
}
showPages.prototype.toPage = function(page){ //页面跳转
    var turnTo = 1;
    if (typeof(page) == 'object') {
        turnTo = page.options[page.selectedIndex].value;
    } else {
        turnTo = page;
    }
    self.location.href = this.createUrl(turnTo);
}
showPages.prototype.printHtml = function(){ //显示html代码
    this.getPage();
    this.checkPages();
    this.showTimes += 1;
    document.write('<div class="pagination pagination-centered">'+ this.createHtml() +'</div>');
}