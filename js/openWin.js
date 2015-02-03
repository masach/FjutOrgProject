/*
-------------------------------------------------------------------------------
文件名称：openWin.js
说    明：JavaScript脚本，用于网页中弹出窗口的处理
版    本：1.0
修改纪录:
---------------------------------------------------------------------------
时间			修改人		说明
2002-8-29	libo		创建
2004-6-21	YuanYi		修改(多次点击只创建一个窗口、模式窗口的返回值)
------------------------------------------------------------------------------- 	
*/
/**新开窗口，指定大小，自动屏幕居中,如果w,h不送就默认开全屏幕,开全屏幕会留出任务栏 */
function NewWindow(url,title,w,h){ 
	var win = null;
	//如果宽高没送就说明是开全屏的，这时左与上都应该是0
	LeftPosition = (w) ? (screen.availWidth-w)/2 : 0; 
	TopPosition = (h) ? (screen.availHeight-h)/2 : 0; 	
	//如果宽高没送就说明是开全屏的，取屏幕可用的高度与宽度
	w=(w)?w:screen.availWidth-10;
	h=(h)?h:screen.availHeight-32;
	settings ='height='+h+',width='+w+',top='+TopPosition+',left='+LeftPosition+',scrollbars=yes,menubar=no,status=no,resizable=yes';
	win = window.open(url,title,settings) 
	win.focus();
} 
/*
用途：弹出模式窗口
	此功能只能在IE5.0以上浏览器使用。
	弹出窗口的风格为居中，没有状态栏，没有IE按钮，菜单,地址栏
输入：
	strUrl：  	弹出窗口内显示的网页的地址
	winWidth：	弹出窗口的宽度，单位为px
	winHeight:	弹出窗口的高度，单位为px
	middle:		弹出窗口是否要居中，默认不居中
返回：
	如果通过验证返回true,否则返回false	
*/
function showModal( strUrl,winWidth,winHeight,middle){
	if(middle==null)
	{
		showx = event.screenX - event.offsetX - 210 ; // + deltaX;
		showy = event.screenY - event.offsetY + 18; // + deltaY;
		return window.showModalDialog(	strUrl,
										"window",
										"dialogWidth:"+ winWidth + "px;"
										+ "dialogHeight:"+winHeight + "px;"
										+ "dialogLeft:"+showx+"px;"
										+ "dialogTop:"+showy+"px;"
										+ "directories:yes;help:no;status:no;resizable:no;scrollbars:yes;");
	}
	else
	{
		return window.showModalDialog(	strUrl,
										"window",
										"dialogWidth:"+ winWidth + "px;"
										+ "dialogHeight:"+winHeight + "px;"
										+ "directories:yes;help:no;status:no;resizable:no;scrollbars:yes;");	
	}
}

function openNewWindow2( strUrl,strTitle,winWidth,winHeight,type){
	if(type==null)
	{
		newwin = window.open(	strUrl,
							"popupnav",
							"width="+ winWidth + ","
							+ "height="+winHeight + ","
							+ "status=no,toolbar=no,menubar=no,location=no,scrollbars=yes");
	}
	else
	{
		newwin = window.open(	strUrl,
							"popupnav",
							"width="+ winWidth + ","
							+ "height="+winHeight + ","
							+ "status=no,menubar=yes,scrollbars=yes");
	}
  	if (newwin==null)	{
  		alert("科技计划项目管理系统提示1:\n\n您本地安装的拦截工具或者配置将系统的弹出窗口拦截，为了系统能正常使用，您必须关闭拦截程序。");
  		top.window.location="http://bz.fjkjt.gov.cn/space/%E7%B3%BB%E7%BB%9F%E4%BD%BF%E7%94%A8%E5%B8%AE%E5%8A%A9/%E5%85%A5%E9%97%A8%E6%8C%87%E5%8D%97/%E5%88%9D%E6%AC%A1%E4%BD%BF%E7%94%A8/%E5%BC%B9%E5%87%BA%E7%AA%97%E5%8F%A3%E6%8B%A6%E6%88%AA";
  	}							
	newwin.focus();	
}

function openNewWindow(url ,title , width , height, type){
	var _left= (screen.width - width)/2;
	var _top = (screen.height - height)/2;
	var newWindowInstance = null;
	if(type==0) {
	   newWindowInstance = window.open(url , title , 'toolbar=no,location=no,resizable=no,scrollbars=no,status=no,top='+ _top +',left='+ _left +',height=' + height + ',width=' + width + "'");
	   newWindowInstance.focus();
	}else if(type==1){
	   newWindowInstance = window.open(url , title , 'toolbar=no,location=no,resizable=yes,scrollbars=yes,status=no,top='+ _top +',left='+ _left +',height=' + height + ',width=' + width + "'");
	   newWindowInstance.focus();
	}else if(type==2){
	   newWindowInstance = window.open(url , title , 'fullscreen=3');		
	}
	else if(type==3){
	   newWindowInstance = window.open(url , title);		
	}	
  if (newWindowInstance==null)	{
  	alert("科技计划项目管理系统提示1:\n\n您本地安装的拦截工具或者配置将系统的弹出窗口拦截，为了系统能正常使用，您必须关闭拦截程序。");
  	top.window.location="http://bz.fjkjt.gov.cn/space/%E7%B3%BB%E7%BB%9F%E4%BD%BF%E7%94%A8%E5%B8%AE%E5%8A%A9/%E5%85%A5%E9%97%A8%E6%8C%87%E5%8D%97/%E5%88%9D%E6%AC%A1%E4%BD%BF%E7%94%A8/%E5%BC%B9%E5%87%BA%E7%AA%97%E5%8F%A3%E6%8B%A6%E6%88%AA";
  	return null;
  }
  return newWindowInstance;
}