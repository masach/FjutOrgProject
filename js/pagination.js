/**
 * ��ҳjavascript�ؼ���ʹ���෽ʽ������ҳ����
 * �ڷ�ҳ��ҳ������Ҫһ����ҳ��FORM���ڶ���FORM������������HTML���룺

<form name="testPage" method="POST" action="BNLOG.BNLOG_testPage.do" onsubmit="return false;">
<html:hidden property="PageCond/begin" />
<html:hidden property="PageCond/length" />
<html:hidden property="PageCond/count" />
<script>
var myPage = new page();
myPage.init("testPage", "PageCond/begin", "PageCond/length", "PageCond/count");
</script>

���У�
    PageCond/begin �ǿ�ʼ��¼������͵���ҳ��BL������ begin ������Ĭ��ֵΪ 0
    PageCond/length ��ÿҳ��¼��������͵���ҳ��BL������ length ������Ĭ��ֵΪ 10��
                    Ҳ������<html:input property="PageCond/length" />��Ϊ����������ÿҳ�ļ�¼��
    PageCond/count �ǲ�ѯ������ܼ�¼��������ܼ�¼�������ڻ���С��1����ҳ��BL�������Զ������ѯ���
                   ���ܼ�¼������� count �� 'noCount' ���ߴ��� 1����ҳ��BL��������������ܼ�¼��
                   ���ڸ��Ӳ�ѯ�����ҽ�����ǳ����ʱ�򣬽������'noCount'���������ܼ�¼��������ٶ�
����������������ʼ���󣬾Ϳ��Դ�����ҳ�Ķ����ˣ�
��ҳ���󴴽�����Ҫ���� init �������г�ʼ����
��ʼ���������Ƿ�ҳ��FORM��������ƺ�begin,length,count����TEXT���������
ע�⣺��ʼ����ҳ������Ҫ����������(begin,length,count)��ʼ����

��ʼ���ɹ���Ϳ���ʹ�÷�ҳ������

<a href="javascript:myPage.firstPage();" >��ҳ</a>&nbsp;
���� myPage.firstPage() ��ת����һҳ

<a href="javascript:myPage.previousPage();" >��ҳ</a>&nbsp;
���� myPage.firstPage() ��ת����һҳ

<a href="javascript:myPage.nextPage();" >��ҳ</a>&nbsp;
���� myPage.firstPage() ��ת����һҳ

<a href="javascript:myPage.lastPage();" >βҳ</a>
���� myPage.firstPage() ��ת�����һҳ

<script>if (!myPage.noCount) document.write('�ܹ�' + myPage.count + '��');</script>
����ʹ��script�����ʾ��ѯ�����������

��<input type="text" name="pageno" size=2 value=<script>document.write(myPage.current)</script> >ҳ if (!myPage.noCount)<script>document.write('/��'+myPage.total+'ҳ')</script>
���� myPage.current ��ǰҳ��
���� myPage.total   ��ҳ�� �����û�м�����ܼ�¼������û����ҳ����


<input type="text" name="pageno" size=2 > <input type="button" onclick="myPage.goPage('pageno');" value="go!" name="gopage">
���� myPage.goPage('pageno') ��ת��ָ����ҳ�ţ�pageno������ҳ�ŵ�TEXT��������

 *
 */
function page() 
{
    this.frm;           //��ҳ��ѯFORM�Ķ���
    this.beginTxt;      //��ҳ��ѯFORM�е�begin TEXT���󣨿���ʹ���صĶ���
    this.lengthTxt;     //��ҳ��ѯFORM�е�length TEXT���󣨿���ʹ���صĶ���
    this.countTxt;      //��ҳ��ѯFORM�е�count TEXT���󣨿���ʹ���صĶ���

    this.begin;         //��ҳ��ѯ��ʼ��¼λ��
    this.length;        //ÿҳ��ʾ��¼��
    this.count;         //��ѯ����ܼ�¼��
    this.current;   	//��ǰҳ��
    this.total;     	//�ܹ�ҳ��
    this.noCount;       //��ҳ����û���ܼ�¼��
    
    this.init = initPage;
    this.nextPage = nextPage;
    this.previousPage = previousPage;
    this.firstPage = firstPage;
    this.lastPage = lastPage;
    this.goPage = goPage;
    this.go = go;
    this.printPageCode = printPageCode;

    /*
     * ��ҳ��ѯ�Ĺ��캯��
     * @param frmName ��ҳ��ѯ��FORM������
     * @param beginText FORM��begin TEXT��������
     * @param lengthText FORM��length TEXT��������
     * @param countText FORM��count TEXT��������
     */
    function initPage(frmName, beginText, lengthText, countText)
    {
        this.noCount = false;
        this.frm = document.forms[frmName];
        this.beginTxt = this.frm.elements[beginText];
        this.lengthTxt = this.frm.elements[lengthText];
        this.countTxt = this.frm.elements[countText];
        
        if(this.beginTxt.value==null ||this.beginTxt.value==""){
        	this.begin = 0;
        }else{
        	this.begin = parseInt(this.beginTxt.value);
    	}
        if(this.lengthTxt.value==null ||this.lengthTxt.value==""){
        	this.length=10;
        }else{
        	this.length = parseInt(this.lengthTxt.value);
    	}
        	
        if(this.countTxt.value==null ||this.countTxt.value==""){
        	this.count = 0;
        }else{
        	this.count = parseInt(this.countTxt.value);
    	}

        if (this.countTxt.value == "noCount") {
            this.noCount = true;
            this.total = "";
            this.count = -2;
        }
        if (this.count < 1) {
            this.noCount = true;
            this.total = 1;
            this.count = -2;
        }
        if (!this.noCount) {
            this.total = Math.floor(this.count/this.length);
            if (this.count%this.length != 0) {
                this.total++;
            }
        }
        this.current = Math.floor(this.begin/this.length) + 1;
    }

    function nextPage()
    {
        this.go(this.current + 1);
    }
    
    function previousPage()
    {
        this.go(this.current - 1);
    }

    function firstPage()
    {
        this.go(1);
    }

    function lastPage()
    {
        this.go(this.total);
    }

    function goPage(pageNo) /* ���������ҳ�ţ�pageNo������ҳ������������� */
    {        
        if (!isNumber(this.frm.elements[pageNo].value)){
        	alert("����ȷ������ת��ҳ�룡")
        	this.frm.elements[pageNo].select();
        	return;
        }
        var page = parseInt(this.frm.elements[pageNo].value);
        if(page==NaN || page==undefined)
        	this.count=0;
        this.go(page);
    }

    function go(page)
    {
    	if (!isNumber(this.lengthTxt.value)) {
    		alert("ÿҳ���������������������0������")
        	this.lengthTxt.select();
    		return;
    	}
        var inputLen = parseInt(this.lengthTxt.value);
        
        if (inputLen < 1) {
            alert("ÿҳ��¼�������������������0������")
            return;
        }
    
        if (inputLen != this.length) {
            this.length = inputLen;
            if (this.noCount) { //����ı���ÿҳ��ʾ��¼������û��ͳ�Ƴ��ܼ�¼������¼��0��ʼ��ѯ
                this.beginTxt.value = 0;
                this.frm.submit();
                return;
            }
            this.total = Math.floor(this.count/this.length);
            if (this.count%this.length != 0) {
                this.total++;
            }
        }
    
        var gono = page;
        if (gono<1)
            gono=1;
        if (!this.noCount) {
            if (gono>this.total)
                gono=this.total;
        }
            
        this.beginTxt.value = (gono - 1) * this.length;
        this.frm.submit();
    }
	
	function printPageCode(currRowCount)
    {
        var htmltxt="";
        var currCount=currRowCount;
        if(currCount==NaN || currCount==undefined || currCount == null){
        	return;	
        }

        if(this.current>1){
        	htmltxt += "��<a href='javascript:myPage.firstPage();' >��ҳ</a>��";
        	htmltxt += "��<a href='javascript:myPage.previousPage();' >��ҳ</a>��";
        }
        
        if (!this.noCount){
	        if (this.current<this.total ) {
	            htmltxt += "��<a href='javascript:myPage.nextPage();' >��ҳ</a>��";
        		htmltxt += "��<a href='javascript:myPage.lastPage();' >βҳ</a>��";
	        }
    	}else{
    		if (this.length<=currCount){
    			htmltxt += "��<a href='javascript:myPage.nextPage();' >��ҳ</a>��";
    		}
    	}		
        
        htmltxt += "��<input type='text' name='pageno' onkeydown='goPageNo(this);' size=2 value='"+this.current+"'>ҳ";
        
        if (!this.noCount){
        	htmltxt += "/��"+this.total+"ҳ";
        	htmltxt += "��"+myPage.count + "��";
        }
        	
        document.write( htmltxt);
    }
}
function goPageNo(obj)
{    
    if(event.keyCode==13)
    { 
       myPage.goPage("pageno");
    }
}
function isNumber( s ){   
	var regu = "^[0-9]+$";
	var re = new RegExp(regu);
	if (s.search(re) != -1) {
	   return true;
	} else {
	   return false;
	}
}
