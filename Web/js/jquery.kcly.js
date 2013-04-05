/**
 * User: kcly
 */
if(!window["kcly"]){
	window["kcly"] = { emptyFn: function () { }, returnFn: function (v) { return function () { return v; } } };
}
(function($,window,Util,undefined){
    var validate=Util.Validate={
        /**
        * 判断非空
        * @param val 要判断的值
        */
        notnull: function(val) {//判断非空
            if (val == null || val == undefined || $.trim(val.toString()) == "")
                return false;
            else
                return true;
        },
        /**
        * 判断是否为IP地址（IPV4）
        * @param val 要判断的值
        */
        ipaddress: function(val) {//判断为IP地址
            if ($.trim(val) == "") return false;
            if (validate.regExps.IsIP.test(val)) {
                if (RegExp.$1 < 256 && RegExp.$2 < 256 && RegExp.$3 < 256 && RegExp.$4 < 256) return true;
            }
            return false;
        },
        /**
         * 比较值
         * @param v
         * @param param
         */
        equal:function(v,param){
            if(!param)
                return true;
            var v2=null;
            if(param.obj)
            {
                if(!param.obj.jquery)
                    v2=$(param.obj);
                else v2=param.obj;
                if(v2.length)
                    v2= v2.val();
            }else{
                v2=param.val||param;
            }
            return $.trim(v)== $.trim(v2);

        },
        /**
         * 密码强度
         * @param v
         * @param param
         */
        rating:function(v,param){
            var val = $(this).val();
            var level = checkPasswdRate(val);
            var pwd_level = 1;
            var className = "pwd-strength";
            if (level > 0 && level < 3) {
                className = "pwd-strength-week";
            }
            if (level > 2) {
                if (level < 4) {
                    className = "pwd-strength-normal";
                    pwd_level = 2;
                } else {
                    className = "pwd-strength-strong";
                    pwd_level = 3;
                }
            }

            $("#pwd_level").val(pwd_level);
            $("#passwd-strong").removeClass().addClass("pwd-strength "+className);
            return true;
        },
        /**
         * 比较值
         * @param v
         * @param param
         */
        notequal:function(v,param){
            return !validate.equal.call(this,v,param);
        },
        /**
        * 判断是否为身份证
        * @param val 要判断的值
        * @param len 身份证号码长度
        */
        idcard:function(val,len){//身份证验证
            var aCity={
                11:"\u5317\u4eac",
                12:"\u5929\u6d25",
                13:"\u6cb3\u5317",
                14:"\u5c71\u897f",
                15:"\u5185\u8499\u53e4",
                21:"\u8fbd\u5b81",
                22:"\u5409\u6797",
                23:"\u9ed1\u9f99\u6c5f ",
                31:"\u4e0a\u6d77",
                32:"\u6c5f\u82cf",
                33:"\u6d59\u6c5f",
                34:"\u5b89\u5fbd",
                35:"\u798f\u5efa",
                36:"\u6c5f\u897f",
                37:"\u5c71\u4e1c",
                41:"\u6cb3\u5357",
                42:"\u6e56\u5317 ",
                43:"\u6e56\u5357",
                44:"\u5e7f\u4e1c",
                45:"\u5e7f\u897f",
                46:"\u6d77\u5357",
                50:"\u91cd\u5e86",
                51:"\u56db\u5ddd",
                52:"\u8d35\u5dde",
                53:"\u4e91\u5357",
                54:"\u897f\u85cf ",
                61:"\u9655\u897f",
                62:"\u7518\u8083",
                63:"\u9752\u6d77",
                64:"\u5b81\u590f",
                65:"\u65b0\u7586",
                71:"\u53f0\u6e7e",
                81:"\u9999\u6e2f",
                82:"\u6fb3\u95e8",
                91:"\u56fd\u5916 "
            } ,
            iSum=0 , info="" , sBirthday="" , d;
            if(!validate.regExps.IsIDCard.test(val)) return false;
            val=val.replace(/[xX]$/i,"a");
            if(aCity[parseInt(val.substr(0,2))]==null)return false;
            if(len && val.length!=len) return false;

            if(val.length==18){
                for(var i=17;i>=0;i--){
                    iSum+=(Math.pow(2,i)%11)*parseInt(val.charAt(17-i),11);
                }
                if(iSum%11!=1) return false;
                sBirthday=val.substr(6,4)+"/"+Number(val.substr(10,2))+"/"+Number(val.substr(12,2));
            }
            else {
                sBirthday="19"+val.substr(6,2)+"/"+Number(val.substr(8,2))+"/"+Number(val.substr(10,2));
            }

            d=new Date(sBirthday);
            if(sBirthday!=(d.getFullYear()+"/"+(d.getMonth()+1)+"/"+d.getDate()))return false;

            return true;
        },
        /**
        * 判断长度是否符合规则
        * @param val 要判断的值
        * @param param 长度描述的对象结构：
        *     {
        *        max : 数值,
        *        min ：数值,
        *        equal : 数值
        *     }
        * 当存在max或min时忽略equal，执行判断为   min <= len <= max, len == equal
        */
        checklength: function(val, param) {//判断字符串长度是否符合要求
            var len;
            param=param||{};
            if (typeof (param) == "string"){
                eval("param=" + param);
            }
            if (param.NoReplace){
                len = val.length;
            }else{
                len = val.replace(/[^\x00-\xff]/g, "^^").length;
            }

            if (param.max && param.min) {
                return len <= param.max && len >= param.min;
            }
            if (param.max) {
                return len <= param.max;
            }
            if (param.min) {
                return len >= param.min;
            }
            if (param.equal) {
                return len == param.equal;
            }
            return true;
        },
        /**
        * 判断数值大小是否符合要求
        * @param val 要判断的值
        * @param param 数值大小描述的对象结构：
        *     {
        *        max : 数值,
        *        min ：数值
        *     }
        * 执行判断为   min <= val <= max
        */
        numbersize: function (val, param) {//判断数值大小是否符合要求
            if (this.test(val, "Float")) {
                var num = val * 1;
                if (typeof (param) == "string"){
                    eval("param=" + param);
                }
                if (param.max && param.min) {
                    return num >= param.min && num <= param.max
                }
                if (param.max) {
                    return num <= param.max;
                }
                if (param.min) {
                    return num >= param.min;
                }
            }
            return false;
        },
        /**
        * 通过正则去判断值是否符合要求
        * @param val 要判断的值
        * @param param 可以是默认正则regExps中的名称，可以是一个正则字符串
        */
        test: function(val, param) {
            if (typeof (param) == "string"){
                if(validate.regExps[param]){
                    return validate.regExps[param].test(val);
                }else{
                    param=new RegExp(param);
                    return param.test(val);
                }
            }else{
                return param.test(val);
            }
        },
        /**
        * 判断是否电话号码
        * @param val 要判断的值
        * @param type 值可以是Phone或MPhone,或者不填，如果不填则可以是电话号码也可以是手机号码，如果非以上值则直接不通过。
        */
        isphone:function(val,type){
            return ((!type||type=="Phone")&&validate.regExps.IsPhone.test(val))||
            ((!type||type=="MPhone")&&validate.regExps.IsMobilePhone.test(val));
        },
        /**
         * 通用正则集合
         */
        regExps: {
            NotNInt: /^\d+$/, //非负整数
            PlusInt: /^[0-9]*[1-9][0-9]*$/, //正整数
            NotPInt: /^((-\d+)|(0+))$/, //非正整数
            NegInt: /^-[0-9]*[1-9][0-9]*$/, //负整数
            Int: /^-?\d+$/, //整数
            NotNFloat: /^\d+(\.\d+)?$/, //非负浮点数
            PlusFloat: /^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$/, //正浮点数
            NotPFloat: /^((-\d+(\.\d+)?)|(0+(\.0+)?))$/, //非正浮点数
            NegFloat: /^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$/, //负浮点数
            Float: /^(-?\d+)(\.\d+)?$/, //浮点数
            IsEmail: /^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/, //邮箱
            IsIDCard: /^[1-9](\d{14}|\d{16}[\dxX])$/,//身份证号
            IsIP: /^(\d+)\.(\d+)\.(\d+)\.(\d+)$/,//IP地址
            IsPhone: /^(((\()?\d{2,4}(\))?[-(\s)*]){0,2})?(\d{7,8})$/, //固定电话号码
            IsMobilePhone: /^((\(\d{3}\))|(\d{3}\-))?(13\d{9}$)|(15\d{9}$)|(18\d{9}$)|(147\d{8}$)/, //移动电话
            IsZipCode: /^[1-9]\d{5}$/, //邮政编码
            IsDate: /((^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(10|12|0?[13578])([-\/\._])(3[01]|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(11|0?[469])([-\/\._])(30|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(0?2)([-\/\._])(2[0-8]|1[0-9]|0?[1-9])$)|(^([2468][048]00)([-\/\._])(0?2)([-\/\._])(29)$)|(^([3579][26]00)([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][0][48])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][0][48])([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][2468][048])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][2468][048])([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][13579][26])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][13579][26])([-\/\._])(0?2)([-\/\._])(29)$))/,
            IsUrl: /http(s)?:\/\/([\w-]+\.)+[\w-]+(\/[\w- .\/?%&=]*)?/,
            IsChinese: /[\u4e00-\u9fa5]/, //中文
            IsLogin: /^[a-zA-Z0-9_\u4e00-\u9fa5]{1,}$/,//字母数字下环线和汉字
            IsEnglish: /^[A-Za-z]+$/,//英文
            IsSpecial: /^[<>]/
        }
    };


    //*******************jQuery扩展部分****************************

    var _dataKey="kcly-validate";//保存规则的key

    /**
     * 执行验证
     */
    $.fn.validate = function() {
        if(this.length==0){
            return true;
        }
        if(this.length>1){
            return this.eq(0).validate();
        }

        var ds = this.data(_dataKey),
        v = this.val(), f, d, rs;
        if(!ds){
            return true;
        }//没有验证规则
        if(ds["notnull"])//先验证是否非空
        {
            d = $.extend(true, {}, ds["notnull"]);
            if(d.param[0]&&d.param[0].test&&typeof (d.param[0].test) == "function")
               f=d.param[0].test;
            else
                f = validate.notnull;
            d.param.unshift(v);
            if ((rs=f.apply(this, d.param))!==true){
                return rs||d.msg;
            }
        }else if(v=="") {
            return true;
        }//如果允许为空且为空则返回验证通过
        var test=/^#.*?#$/;//##包裹的为特殊规则，自行定义验证
        for (var k in ds) {
            if(k=="notnull"||test.test(k)) continue;//规则为notNull或者以##包裹的则跳过
            f = validate[k];
            d = $.extend(true, {}, ds[k]);
            if (!f || typeof (f) != "function") {
                if(~k.indexOf("#test#")){
                    f = validate.test;
                    d.param = [k.replace("#test#","")];
                }else {
                    if(d.param[0]&&d.param[0].test&&typeof (d.param[0].test) == "function")
                        f=d.param[0].test;
                }
            }
            d.param.unshift(v);
            if ((rs=f.apply(this, d.param))!==true){
                return rs||d.msg;
            }
        }
        return true;
    }

    //测试某个字符是属于哪一类.
    function CharMode(iN){
        if (iN >= 48 && iN <= 57) //数字
            return 1;
        if (iN >= 65 && iN <= 90) //大写字母
            return 2;
        if (iN >= 97 && iN <= 122) //小写
            return 4;
        else
            return 8; //特殊字符
    }

    //计算出当前密码当中一共有多少种模式
    function bitTotal(num){
        modes = 0;
        for (i = 0; i < 4; i++) {
            if (num & 1)
                modes++;
            num >>>= 1;
        }
        return modes;
    }

    //返回密码的强度级别
    function checkPasswdRate(str){
        if (str.length <= 5)
            return 0; //密码太短
        Modes = 0;
        for (i = 0; i < str.length; i++) {
            //测试每一个字符的类别并统计一共有多少种模式.
            Modes |= CharMode(str.charCodeAt(i));
        }
        return bitTotal(Modes);
    }

    /**
    * 整理规则
    * @param testModel 用于验证的方法名称
    * @param param 传入的参数，单个数据或数组
    * @param msg 验证失败时返回的错误信息
    */
    function _makeParams(testModel,param,msg){
        if (testModel == "test")
            testModel ="#test#"+param;
        if (param == null||param==undefined) param = [];
        else if (!$.isArray(param)) param = [param];
        return  {
            key : testModel ,
            param : param,
            msg : msg
        };
    }

    /**
    * 添加规则
    * @param testModel 用于验证的方法名称，如果传的是一个数组且只传一个参数，
    *                   则按添加多个规则处理格式：[{key:testModel,param:param,msg:msg},
    *                                           {key:testModel,param:param,msg:msg}...]
    * @param param 传入的参数，单个数据或数组
    * @param msg 验证失败时返回的错误信息
    */
    $.fn.addVerify = function(testModel, param, msg) {
        var ds={};
        //var ds = this.data(_dataKey) || {},o;
        if(arguments.length==1&&$.isArray(testModel))
        {
            for(var i=0,c=testModel.length;i<c;i++)
            {
                o=testModel[i];
                o=_makeParams(o.key,o.param,o.msg);
                ds[o.key]=o;
            }
        }else{
            o=_makeParams(testModel,param,msg);
            ds[o.key]=o;
        }

        return this.each(function(){
            var d=$.data(this,_dataKey) || {};
            d=$.extend(true,d,ds);
            $.data(this,_dataKey,d);
        });
    }

    /**
    * 删除规则
    * @param testModel 要删除的验证规则名称
    * @param param 传入的参数，主要是删除test方法时使用
    */
    $.fn.removeVerify = function(testModel,param) {
        if (testModel) {
            if (testModel == "test")
                testModel ="#test#"+param;
            var ds = this.data(_dataKey) || {};
            ds[testModel] = null;
            delete (ds[testModel]);
        } else
            this.data(_dataKey, null);
        return this;
    }

    /**
    * 获取规则
    * @param testModel 要获取的验证规则名称
    * @param param 传入的参数，主要是获取test规则时使用
    */
    $.fn.getVerify= function(testModel,param) {
        if (testModel) {
            if (testModel == "test")
                testModel ="#test#"+param;
            var ds = this.data(_dataKey) || {};
            return ds[testModel];
        } else
            return this.data(_dataKey);
    }


    /**
     * 验证demos的指定规则
     * @param doms 要参与验证的对象组
     * @param errorCallback 验证失败回调
     * @param successCallback 验证成功回调
     * @return {Boolean}  有失败的情况则返回false，否则返回true
     */
    validate.validate=function(doms,errorCallback,successCallback){
        if(!doms.jquery)
        {
            doms=$(doms);
        }
        var tf=true;
        doms.each(function(){
            var $t=$(this),rs=$t.validate();
            if(rs!==true&&rs!="")
            {//错误
                tf=false;
                (errorCallback||Util.emptyFn).call(this,rs,$t);
            }else
            {
                (successCallback||Util.emptyFn).call(this,$t);
            }
        });
        return tf;
    }


})(jQuery, window, kcly);