/*
-------------------------------------------------------------------------------
�ļ����ƣ�openWin.js
˵    ����JavaScript�ű���������ҳ�е������ڵĴ���
��    ����1.0
�޸ļ�¼:
---------------------------------------------------------------------------
ʱ��			�޸���		˵��
2002-8-29	libo		����
2004-6-21	YuanYi		�޸�(��ε��ֻ����һ�����ڡ�ģʽ���ڵķ���ֵ)
------------------------------------------------------------------------------- 	
*/
/**�¿����ڣ�ָ����С���Զ���Ļ����,���w,h���;�Ĭ�Ͽ�ȫ��Ļ,��ȫ��Ļ������������ */
function NewWindow(url,title,w,h){ 
	var win = null;
	//������û�;�˵���ǿ�ȫ���ģ���ʱ�����϶�Ӧ����0
	LeftPosition = (w) ? (screen.availWidth-w)/2 : 0; 
	TopPosition = (h) ? (screen.availHeight-h)/2 : 0; 	
	//������û�;�˵���ǿ�ȫ���ģ�ȡ��Ļ���õĸ߶�����
	w=(w)?w:screen.availWidth-10;
	h=(h)?h:screen.availHeight-32;
	settings ='height='+h+',width='+w+',top='+TopPosition+',left='+LeftPosition+',scrollbars=yes,menubar=no,status=no,resizable=yes';
	win = window.open(url,title,settings) 
	win.focus();
} 
/*
��;������ģʽ����
	�˹���ֻ����IE5.0���������ʹ�á�
	�������ڵķ��Ϊ���У�û��״̬����û��IE��ť���˵�,��ַ��
���룺
	strUrl��  	������������ʾ����ҳ�ĵ�ַ
	winWidth��	�������ڵĿ�ȣ���λΪpx
	winHeight:	�������ڵĸ߶ȣ���λΪpx
	middle:		���������Ƿ�Ҫ���У�Ĭ�ϲ�����
���أ�
	���ͨ����֤����true,���򷵻�false	
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
  		alert("�Ƽ��ƻ���Ŀ����ϵͳ��ʾ1:\n\n�����ذ�װ�����ع��߻������ý�ϵͳ�ĵ����������أ�Ϊ��ϵͳ������ʹ�ã�������ر����س���");
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
  	alert("�Ƽ��ƻ���Ŀ����ϵͳ��ʾ1:\n\n�����ذ�װ�����ع��߻������ý�ϵͳ�ĵ����������أ�Ϊ��ϵͳ������ʹ�ã�������ر����س���");
  	top.window.location="http://bz.fjkjt.gov.cn/space/%E7%B3%BB%E7%BB%9F%E4%BD%BF%E7%94%A8%E5%B8%AE%E5%8A%A9/%E5%85%A5%E9%97%A8%E6%8C%87%E5%8D%97/%E5%88%9D%E6%AC%A1%E4%BD%BF%E7%94%A8/%E5%BC%B9%E5%87%BA%E7%AA%97%E5%8F%A3%E6%8B%A6%E6%88%AA";
  	return null;
  }
  return newWindowInstance;
}