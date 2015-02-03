/**
*<p>常用的工具js文件</p>
*<p>作者：薛云腾</p>
*<p>version 1.0</p>
*/

function formatDate(format,fdate){		/*将时间格式化，根据format形式格式化时间fdate，默认形式为y-m-d h:i:s*/
	var fTime,fStr = 'ymdhis',formatStr=format;
	if(!formatStr){
		formatStr="y-m-d h:i:s";
	}
	if(fdate){
		fTime = new Date(fdate);
	}else{
		fTime = new Date();
	}
	
	var formatArr = [
		fTime.getFullYear().toString(),
		doubleDigit((fTime.getMonth()+1).toString()),
		doubleDigit(fTime.getDate().toString()),
		doubleDigit(fTime.getHours().toString()),
		doubleDigit(fTime.getMinutes().toString()),
		doubleDigit(fTime.getSeconds().toString())
	];
	for(var i=0; i<formatArr.length;i++){
		formatStr=formatStr.replace(fStr.charAt(i),formatArr[i]);
	}
	
	return formatStr;
} 

function doubleDigit(digit){    /*将不足两位的数字变为两位,否则返回本身*/
	if(digit!=null){
		if(digit.length<2){
			return "0"+digit;
		}
	}
	return digit;
}


var xyt = $.extend({}, xyt);	/* 定义全局对象，类似于命名空间或包的作用 */

/**格式化日期时间*/
Date.prototype.format = function(format) {
	if (isNaN(this.getMonth())) {
		return '';
	}
	if (!format) {
		format = "yyyy-MM-dd hh:mm:ss";
	}
	var o = {
		/* month */
		"M+" : this.getMonth() + 1,
		/* day */
		"d+" : this.getDate(),
		/* hour */
		"h+" : this.getHours(),
		/* minute */
		"m+" : this.getMinutes(),
		/* second */
		"s+" : this.getSeconds(),
		/* quarter */
		"q+" : Math.floor((this.getMonth() + 3) / 3),
		/* millisecond */
		"S" : this.getMilliseconds()
	};
	if (/(y+)/.test(format)) {
		format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
	}
	for ( var k in o) {
		if (new RegExp("(" + k + ")").test(format)) {
			format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
		}
	}
	return format;
};

xyt.getWebRootPath = function(){		/*获取web项目的根路径*,s使用方法为xyt.getWebRootPath()*/
	var curWwwPath = window.document.location.href;
	var pathName = window.document.location.pathname;
	var pos = curWwwPath.indexOf(pathName);
	var localhostPath = curWwwPath.substring(0,pos);
	var projectName = pathName.substring(0, pathName.substr(1).indexOf('/') + 1);
	return (localhostPath + projectName);
}

xyt.getProName = function() {   /**returns 项目名称(/项目名)，使用方法:xyt.getProName();*/
	return window.document.location.pathname.substring(0, window.document.location.pathname.indexOf('\/', 1));
};

xyt.nameSpace = function() {   /**增加命名空间功能,使用方法：xyt.nameSpace('jQuery.bbb.ccc','jQuery.eee.fff');*/
	var o = {}, d;
	for ( var i = 0; i < arguments.length; i++) {
		d = arguments[i].split(".");
		o = window[d[0]] = window[d[0]] || {};
		for ( var k = 0; k < d.slice(1).length; k++) {
			o = o[d[k + 1]] = o[d[k + 1]] || {};
		}
	}
	return o;
};

/**生成UUID returns UUID字符串*/
xyt.random4 = function() {
	return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
};
xyt.UUID = function() { 
	return (xyt.random4() + xyt.random4() + "-" + xyt.random4() + "-" + xyt.random4() + "-" + xyt.random4() + "-" + xyt.random4() + xyt.random4() + xyt.random4());
};

/**判断浏览器是否是IE并且版本小于8*/
xyt.isLessThanIe8 = function() {
	return ($.browser.msie && $.browser.version < 8);
};

/**将form表单元素的值序列化成对象*/
xyt.serializeObject = function(form) {
	var o = {};
	$.each(form.serializeArray(), function(index) {
		if (o[this['name']]) {
			o[this['name']] = o[this['name']] + "," + this['value'];
		} else {
			o[this['name']] = this['value'];
		}
	});
	return o;
};

/**将JSON对象转换成字符串*/
xyt.jsonToString = function(o) {
	var r = [];
	if (typeof o == "string")
		return "\"" + o.replace(/([\'\"\\])/g, "\\$1").replace(/(\n)/g, "\\n").replace(/(\r)/g, "\\r").replace(/(\t)/g, "\\t") + "\"";
	if (typeof o == "object") {
		if (!o.sort) {
			for ( var i in o)
				r.push(i + ":" + obj2str(o[i]));
			if (!!document.all && !/^\n?function\s*toString\(\)\s*\{\n?\s*\[native code\]\n?\s*\}\n?\s*$/.test(o.toString)) {
				r.push("toString:" + o.toString.toString());
			}
			r = "{" + r.join() + "}";
		} else {
			for ( var i = 0; i < o.length; i++)
				r.push(obj2str(o[i]));
			r = "[" + r.join() + "]";
		}
		return r;
	}
	return o.toString();
};



/**
*以下是easyui扩展
*/
 
 
/*扩展validatebox，添加验证两次密码功能,value为validatebox中的值*/
$.extend($.fn.validatebox.defaults.rules, {     
	eqPassword : {
		validator : function(value, param) {
			return value == $(param[0]).val();
		},
		message : '密码不一致！'
	}
});

/**使panel和datagrid在加载时提示*/
$.fn.panel.defaults.loadingMessage = '加载中....';
$.fn.datagrid.defaults.loadMsg = '加载中....';

/**用于datagrid/treegrid/tree/combogrid/combobox/form加载数据出错时的操作*/
var easyuiErrorFunction = function(XMLHttpRequest) {
	$.messager.progress('close');
	$.messager.alert('错误', XMLHttpRequest.responseText);
};
$.fn.datagrid.defaults.onLoadError = easyuiErrorFunction;
$.fn.treegrid.defaults.onLoadError = easyuiErrorFunction;
$.fn.tree.defaults.onLoadError = easyuiErrorFunction;
$.fn.combogrid.defaults.onLoadError = easyuiErrorFunction;
$.fn.combobox.defaults.onLoadError = easyuiErrorFunction;
$.fn.form.defaults.onLoadError = easyuiErrorFunction;

/**防止panel/window/dialog组件超出浏览器边界*/
var easyuiPanelOnMove = function(left, top) {
	var l = left;
	var t = top;
	if (l < 1) {
		l = 1;
	}
	if (t < 1) {
		t = 1;
	}
	var width = parseInt($(this).parent().css('width')) + 14;
	var height = parseInt($(this).parent().css('height')) + 14;
	var right = l + width;
	var buttom = t + height;
	var browserWidth = $(window).width();
	var browserHeight = $(window).height();
	if (right > browserWidth) {
		l = browserWidth - width;
	}
	if (buttom > browserHeight) {
		t = browserHeight - height;
	}
	$(this).parent().css({/* 修正面板位置 */
		left : l,
		top : t
	});
};
$.fn.dialog.defaults.onMove = easyuiPanelOnMove;
$.fn.window.defaults.onMove = easyuiPanelOnMove;
$.fn.panel.defaults.onMove = easyuiPanelOnMove;

/**
 * 扩展datagrid，添加动态增加或删除Editor的方法
 * 例子如下，第二个参数可以是数组
 * datagrid.datagrid('removeEditor', 'cpwd');
 * datagrid.datagrid('addEditor', [ { field : 'ccreatedatetime', editor : { type : 'datetimebox', options : { editable : false } } }, { field : 'cmodifydatetime', editor : { type : 'datetimebox', options : { editable : false } } } ]);*/
$.extend($.fn.datagrid.methods, {
	addEditor : function(jq, param) {
		if (param instanceof Array) {
			$.each(param, function(index, item) {
				var e = $(jq).datagrid('getColumnOption', item.field);
				e.editor = item.editor;
			});
		} else {
			var e = $(jq).datagrid('getColumnOption', param.field);
			e.editor = param.editor;
		}
	},
	removeEditor : function(jq, param) {
		if (param instanceof Array) {
			$.each(param, function(index, item) {
				var e = $(jq).datagrid('getColumnOption', item);
				e.editor = {};
			});
		} else {
			var e = $(jq).datagrid('getColumnOption', param);
			e.editor = {};
		}
	}
});