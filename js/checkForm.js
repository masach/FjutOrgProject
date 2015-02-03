/*
小数点后限制4位
*/
	function pointlimit(e){
		var keyvalue = String.fromCharCode(event.keyCode);
    if("1234567890.".indexOf(keyvalue) > -1){
			var patten = /^-?\d+\.?\d{0,3}$/;
			
	 		if(patten.test(e.value)){
	 			return true;
	 		}else{
	 	    return false;
	 	  }
 	  }
	}

/*
检查输入域的值是否由中文，英文，数字组成.
*/

function checkCommonField(obj)
{
    if (obj == null) return false;
    if (obj.value == "") return false;
    return checkCommonChar(obj.value);
}

/*
检查输入值是否由中文，英文，数字组成.
*/

function checkCommonChar(val)
{
    var exp = new RegExp("^\\w+$"); //检查英文字符正则表达式
    var exp2 = new RegExp("^[\u4E00-\u9FA5\uF900-\uFA2D]*$"); //检查中文字符正则表达式
    var retEnChar;
    var retChChar;
    var tmp;
    if (val == null || val == '') return false;
    tmp = val.replace(/[\u4E00-\u9FA5\uF900-\uFA2D]/,''); //先删除中文字符	
    retEnChar = exp.test(tmp);
    tmp = val.replace(/[a-zA-Z0-9]/g,''); //再删除英文及数字字符	
    retChChar = exp2.test(tmp);	
    return (retEnChar||retChChar);
}

/*
-------------------------------------------------------------------------------
文件名称：checkForm.js
说    明：JavaScript脚本，验证表单中的数据项
版    本：1.0
修改纪录:
---------------------------------------------------------------------------
时间		修改人		说明
2003-2-26	zhangcun		创建
------------------------------------------------------------------------------- 	
*/

function checkForm(objFrm){
	var len = 0;
	len = objFrm.elements.length;

	var i = 0;
	var objCheck;
	//文本框
	for(i = 0; i < len; i ++){
		objCheck = objFrm.elements[i];
		if(objCheck.type =="text" && !f_checkTextValid(objCheck) ){
			return false;			
		}
	}
	//下拉框
	for(i = 0; i < len; i ++){
		objCheck = objFrm.elements[i];
		if(objCheck.type =="select-one" && !f_checkSelectValid(objCheck) ){
			return false;			
		}
	}
	//时间段有效
	if( f_checkStartAndEndDate(objFrm) == false) return false;
	
	return true;
}
function f_checkSelectValid(obj){
	//alert("check select");
	if(obj.options.length <= 0){
		alert("下拉选框无数据!");
		return false;	
	} 
	return true;
}
function f_checkStartAndEndDate(frm){
	var len = frm.elements.length;
	if(len == null && len == 0) return true;
	var i=0;
	var temp;
	var objCheck;
	var objStartDate;
	var objEndDate;
	//alert("start date period check");
	try{
		for(i=0; i< len ; i++){
			objCheck = frm.elements[i];
			temp = objCheck.name;
			if( temp.indexOf("startDate") >0 ||temp.indexOf("beginDate")>0 )
				objStartDate = objCheck;
			if( temp.indexOf("endDate") > 0 )
				objEndDate = objCheck;
				
		}
		//alert(objStartDate.value);
		//alert(objEndDate.value);
		if(objStartDate.value==null || objStartDate.value =="" || objStartDate.value ==null || objStartDate.value ==""){
			return true;
		}
		return checkTwoDate(objStartDate.value, objEndDate.value);	
		//alert("end date period check");
	}catch(E){}
	return true;
}

function f_checkTextValid(obj){
	//不能为空
	if(obj.getAttribute("isNeed") != null){
		if(f_isNotNull(obj) == false) return false;
	}
	//不能超过长度
	if(obj.getAttribute("maxlength") != null){
		if(f_checkLength(obj) == false) return false;
	}
	var checkType ="";
	checkType = obj.getAttribute("checkType");
	if(checkType==null||checkType=="") return true;
	//
	if (checkType.indexOf("number") >=0){
		if(f_isNumber(obj) == false)  return false;
		if(f_checkNumType(obj,checkType) == false)  return false;
		
	}
	//
	if (checkType.indexOf("positive") >=0){
		if(f_isNumber(obj) == false)  return false;
		if(f_isPositive(obj)==false)  return false;
		if(f_checkNumType(obj,checkType) == false)  return false;
		
	}
	if (checkType.indexOf("date") >=0){
		if(f_checkDate(obj) == false) return false;
		
	}
	
	/*
	switch(checkType){
		case "number": if(f_isNumber(obj) == false) return false;break;
		case "date": if(f_checkDate(obj) == false) return false;break;
		default:
	}
	*/
	return true;
}

function f_isNotNull(obj){
	if(obj.value == ""){
		f_alert(obj, " 不允许为空值！");
		return false;
	}
	return true;
}

function f_isNumber(obj){
	if(isNaN(obj.value)){
		 f_alert(obj," 应为数值类型");
		return false;		
	}
	return true;

}
function f_checkDate(obj) {
	if(checkDate(obj.value) ==false){
		 f_alert(obj," 不是合法日期格式！");
		return false;		
	}
	return true;
}

function f_checkLength(obj){
	if(getTotalBytes(obj) > Math.abs( obj.getAttribute("maxlength") ) ){
		 f_alert(obj," 超出长度限制!");
		return false;		
	}
	return true;
	
}

function  f_alert(obj,alertStr){
	var fielName = obj.getAttribute("fieldName");
	if(fielName == null)
		fielName = "";
	alert(fielName + "\n" +alertStr);
	obj.select();
	obj.focus();
}

function f_checkNumType(obj, numType){
	//假设: 已经进行数字类型判断
	
	var strTemp;
	var numpric;
	var numLen;
	var strArr;
	var defaultLen = 19;
	var defaultpric = 5;

	try{
		if(numType == null|| numType =="") return f_checkNumLenPrec(obj,defaultLen, defaultpric);
		if(numType.indexOf("(") < 0 || numType.indexOf(")") < 0 ) return f_checkNumLenPrec(obj,defaultLen, defaultpric);
		strTemp = numType.substr( numType.indexOf("(") + 1 ,numType.indexOf(")") - numType.indexOf("(") -1 );
		if(strTemp == null||strTemp =="") return f_checkNumLenPrec(obj,defaultLen, defaultpric);
		strArr = strTemp.split(",");	
		numLen = Math.abs( strArr[0] ); 
		numpric = Math.abs( strArr[1] );	
		return f_checkNumLenPrec(obj,numLen, numpric);
	}catch(e){
		alert("in f_checkNumType = " + e);
		 return f_checkNumLenPrec(obj,defaultLen, defaultpric);
	}

}

function f_checkNumLenPrec(obj, len, pric){
	var numReg;
	var value = obj.value;
	var strValueTemp, strInt, strDec;	
	//alert(value + "=====" + len + "====="+ pric);
	try{	
		
		numReg =/[\-]/;
		strValueTemp = value.replace(numReg, "");
		strValueTemp = strValueTemp.replace(numReg, "");
		//整数
		if(pric==0){
			numReg =/[\.]/;
			//alert(numReg.test(value));
			if(numReg.test(value) == true){
				f_alert(obj, "输入必须为整数类型!");
				return false;	
			}			
		}
		
		if(strValueTemp.indexOf(".") < 0 ){
			//alert("lennth==" + strValueTemp);		
			if(strValueTemp.length >( len - pric)){
				f_alert(obj, "整数位不能超过"+ (len - pric) +"位");
				return false;
			}
		
		}else{
			strInt = strValueTemp.substr( 0, strValueTemp.indexOf(".") );		
			//alert("lennth==" + strInt);		
			if(strInt.length >( len - pric)){
				f_alert(obj, "整数位不能超过"+ (len - pric) +"位");
				return false;
			}		

			strDec = strValueTemp.substr( (strValueTemp.indexOf(".")+1), strValueTemp.length );		
			//alert("pric==" + strDec);		
			if(strDec.length > pric){
				f_alert(obj, "小数位不能超过"+  pric +"位");
				return false;
			}		
		}
		
		return true;
	}catch(e){
		alert("in f_checkNumLenPrec = " + e);
		return false;
	}	
}

function f_isPositive(obj){
	var numReg =/[\-]/;
	if(numReg.test(obj.value) == true){
		f_alert(obj, "必须为正数!");
		return false;
	}
	return true;
	
}


/*
function selectedCheckboxCount(form)
功能说明：对Form中选中的可选项计数

参数说明：
form:指定的表单
*/
function selectedCheckboxCount(form){
	var length =0;
	var i=0;
	var count =0;	
	eles = form.elements;
	while(i<eles.length){
		obj= eles.item(i);
		type = obj.type;
		if(type.indexOf("checkbox")!=-1){
			if(obj.checked){
				count++;
			}
		}
		i++;
	}	
	return count;
}

function getSelectedCheckboxValue(form){
	var length =0;
	var i=0;
	var k=0;	
	var ret=new Array();
	eles = form.elements;
	while(i<eles.length){
		obj= eles.item(i);			
		type = obj.type;		
		if(type.indexOf("checkbox")!=-1){
			if(obj.checked){
				ret[k++]=obj.value;;
			}
		}
		i++;
	}
	return ret;
}

//得到字节长度
function getByteLen(str)
{
        var l = str.length;
        var n = l;
        for ( var i=0; i<l; i++ )
				
                if ( str.charCodeAt(i) <0 || str.charCodeAt(i) >255 )
                        n=n+1;
        return n
}

/*
说明：
1.清除表格中的数据(0.0 和 0)
2.如果cell中没有数据，则自动加上一个空格
3.清除空白行的checkbox

参数：
clearzero:是否清除"0"、"0.0"，false不清除、true清除（默认为true）
tablename:要清除的表格名字，默认为sortTable
*/
function clear_table(clearzero,tablename)
{
	var tobject;
	if(tablename==null)
		tobject=gmobj("sortTable");
	else
		tobject=gmobj(tablename);
	//如果table未定义，则不进行过滤
	
	if(tobject==null)
		return;
		
	
	//如果函数调用参数为空，表示要清除0、0.0；反之，不要清除0、0.0。
	var clear = (clearzero==null)?true:clearzero;

	//清除0、0.0，填补空格
	var rows = tobject.rows;
	var j=0;
	for(var i=0;i<rows.length;i++)
	{
		//取得第一个cell的属性clear，如果为1，表示该行没有数据，则清除该行所有数据
		
			while(tobject.rows[i].cells[j] != null)
			{
				if(clear)
				{
					if(tobject.rows[i].cells[j].innerHTML==0 ||tobject.rows[i].cells[j].innerHTML==0.0||tobject.rows[i].cells[j].innerHTML=="")
						tobject.rows[i].cells[j].innerText=" ";
				}
				else
				{
					if (tobject.rows[i].cells[j].innerHTML=="")
						tobject.rows[i].cells[j].innerText=" ";
				}
				j++;
			}
			j=0;
		
	}
    return true;
}

function gmobj(mtxt)  /* Get object by object name */
{
  if (document.getElementById) {
      m=document.getElementById(mtxt);
  } else if (document.all) {
      m=document.all[mtxt];
  } else if (document.layers) {
      m=document.layers[mtxt];
  }
  return m;
}
/********************************************************************************************
* 身份证号码验证程序（JAVASCRIPT版）
*    时间：   2005年9月23日
*    身份证号码验证程序
*    函数一：checkCard(cId) 身份号码检验函数
*    参数cId为要验证的身份证号码，返回值为true时，验证通过。
*    对一些位置保留了alert()的注释，可供朋友调试使用
*    函数二：C15ToC18(c15)  15位身份证号码升级为18位函数
*        参数c15为要转换的15位身份证号码，返回值为转化后的18位号码
*         如果参数不是有效15位号码，返回值为"undefined"。
*    函数三：isdate(intYear,intMonth,intDay) 日期检验函数
*        参数分别是年月日，返回值为true时，验证通过。
************************************************************************************************/
//检查年月日是否是合法日期
function isdate(intYear,intMonth,intDay)
{
    if(isNaN(intYear)||isNaN(intMonth)||isNaN(intDay)) return false;
    if(intMonth>12||intMonth<1) return false; 
    if ( intDay<1||intDay>31)return false; 
    if((intMonth==4||intMonth==6||intMonth==9||intMonth==11)&&(intDay>30)) return false;  
    if(intMonth==2)
    {  
	     if(intDay>29) return false;
	     if((((intYear%100==0)&&(intYear%400!=0))||(intYear%4!=0))&&(intDay>28))return false;
    } 
    return true;  
} 
//检查是否身份证号码
function IsCard(cId) 
{	 
   var pattern;
   if (cId.length==15)
   {
       pattern= /^\d{15}$/;//正则表达式,15位且全是数字
       if (pattern.exec(cId)==null)
       {          
          return false;
       }
       if (!isdate("19"+cId.substring(6,8),cId.substring(8,10),cId.substring(10,12)))
       {         
          return false;
	   }   
    }
    else if (cId.length==18)
    {
           pattern= /^\d{17}(\d|x|X)$/;//正则表达式,18位且前17位全是数字，最后一位只能数字,x,X
           if (pattern.exec(cId)==null)
           {               
               return false;
           }
           if (!isdate(cId.substring(6,10),cId.substring(10,12),cId.substring(12,14)))
           {               
               return false;
           }           
    }
    else
    {       
       return false;
    }
    return true;   
} 
//检查身份证是否是正确格式
function checkCard(cId) 
{	 
   var pattern;
   if (cId.length==15)
   {
       pattern= /^\d{15}$/;//正则表达式,15位且全是数字
       if (pattern.exec(cId)==null)
       {
          alert("15位身份证号码必须全为数字！")
          return false;
       }
       if (!isdate("19"+cId.substring(6,8),cId.substring(8,10),cId.substring(10,12)))
       {
         alert("身份证号码中出生日期不正确") 
          return false;
				}   
    }
    else if (cId.length==18)
    {
           pattern= /^\d{17}(\d|x|X)$/;//正则表达式,18位且前17位全是数字，最后一位只能数字,x,X
           if (pattern.exec(cId)==null)
           {
               alert("18位身份证号码前17位必须为数字！")
               return false;
           }
           if (!isdate(cId.substring(6,10),cId.substring(10,12),cId.substring(12,14)))
           {
               alert("身份证号码中出生日期不正确") 
               return false;
           }
           var strJiaoYan  =[  "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2"];
           var intQuan =[7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1];
           var intTemp=0;
           for(i = 0; i < cId.length - 1; i++)
              intTemp +=  cId.substring(i, i + 1)  * intQuan[i];  
           intTemp%=11;
           if(cId.substring(cId.length -1,cId.length).toUpperCase()!=strJiaoYan[intTemp])
           {
              // if(confirm("当前输入身份证号码可能不正确，是否确认保存？")==true)
               return confirm("当前输入身份证号码可能不正确，是否确认保存？");
           }
    }
    else
    {
       alert("当前输入身份证号码长度不正确，请重新输入！")
       return false;
    }
    return true;   
}
//升级15位身份证号码到18位
function C15ToC18(c15)
{
   var cId=c15;
   if (c15.length==15)
   {
	      cId=c15.substring(0,6)+"19"+c15.substring(6,15);
	      var strJiaoYan  =["1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2"];
          var intQuan =[7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2,1];
	      var intTemp=0;
	      for(i = 0; i < cId.length; i++)
	      {	      	      	 
	         intTemp += parseInt(cId.substring(i, i + 1),10)  * intQuan[i];  
	      }
	      intTemp %= 11;
	      cId+=strJiaoYan[intTemp];	      
	}
    return cId;
}
//验证电子邮件的合法性
function isEmail(sEmail)
{
	if (sEmail=='')
	{
    return false; 
  }
  if (sEmail.indexOf ('@',0) == -1 || sEmail.indexOf ('.',0) == -1)
  {
  return false;
  }
  return true;
}
function isValidateDate(str)
{
  var date = Date.parse(str);
  if (isNaN(date)) {
    date = Date.parse(str.replace(/-/g,"/")); // 识别日期格式：YYYY-MM-DD 
    if (isNaN(date)) date = 0;
  }
  if(date==0) return false;
  return true;
}
function CheckValidateChar(str,strMsg)
{
   var Result=false;		
   if(str.indexOf("'")>=0 || str.indexOf("\\")>=0 || str.indexOf("\"")>=0 || str.indexOf("<")>=0 || str.indexOf(">")>=0 || str.indexOf("&")>=0)
   {
       alert(strMsg+"包含了\" ' < > & \\等非法字符。\r\n请修改为中文字符后再试！");
	   Result=true;
   }
   return Result;
}
//取浮点数的小数位数，四舍五入的方式
function RoundFloat(value, count)
{
   var Result=value;
   var iD=1;
   if(count<=0)
   {
      Result=Math.round(Result);
   }
   else
   {
	   for(var i=0;i<count;i++)
	   {
	      iD=iD*10;
	   }
	   Result=Result*iD;
	   Result=Math.round(Result);
	   Result=Result/iD;
   }
   return Result;
}
/*
-------------------------------------------------------------------------------
文件名称：checkForm.js
说    明：JavaScript脚本，将金额转换为大写
版    本：1.0
修改纪录:
参    数：currencyDigits：待转化的数字；strPrefix：转换结果的前缀，可以不输入该参数，也可输入：人民币
---------------------------------------------------------------------------
时间		修改人		说明
2003-2-26	周开庆		修改
------------------------------------------------------------------------------- 	
*/
function convertCurrency(currencyDigits,strPrefix) 
{
	// 常数:
	 var MAXIMUM_NUMBER = 99999999999.99;
	 // Predefine the radix characters and currency symbols for output:
	 var CN_ZERO = "零";
	 var CN_ONE = "壹";
	 var CN_TWO = "贰";
	 var CN_THREE = "叁";
	 var CN_FOUR = "肆";
	 var CN_FIVE = "伍";
	 var CN_SIX = "陆";
	 var CN_SEVEN = "柒";
	 var CN_EIGHT = "捌";
	 var CN_NINE = "玖";
	 var CN_TEN = "拾";
	 var CN_HUNDRED = "佰";
	 var CN_THOUSAND = "仟";
	 var CN_TEN_THOUSAND = "万";
	 var CN_HUNDRED_MILLION = "亿";
	 if(strPrefix==null)
	    strPrefix="";
	 var CN_SYMBOL = strPrefix;
	 var CN_DOLLAR = "元";
	 var CN_TEN_CENT = "角";
	 var CN_CENT = "分";
	 var CN_INTEGER = "整";
 
	// Variables:
	 var integral; // Represent integral part of digit number.
	 var decimal; // Represent decimal part of digit number.
	 var outputCharacters; // The output result.
	 var parts;
	 var digits, radices, bigRadices, decimals;
	 var zeroCount;
	 var i, p, d;
	 var quotient, modulus;
	 
	// Validate input string:
	 currencyDigits = currencyDigits.toString();
	 if (currencyDigits == "") {
	  alert("Empty input!");
	  return "";
	 }
	 if (currencyDigits.match(/[^,.\d]/) != null) {
	  alert("Invalid characters in the input string!");
	  return "";
	 }
	 if ((currencyDigits).match(/^((\d{1,3}(,\d{3})*(.((\d{3},)*\d{1,3}))?)|(\d+(.\d+)?))$/) == null) {
	  alert("Illegal format of digit number!");
	  return "";
	 }
	 
	// Normalize the format of input digits:
	 currencyDigits = currencyDigits.replace(/,/g, ""); // Remove comma delimiters.
	 currencyDigits = currencyDigits.replace(/^0+/, ""); // Trim zeros at the beginning.
	 // Assert the number is not greater than the maximum number.
	 if (Number(currencyDigits) > MAXIMUM_NUMBER) 
	 {
		  alert("Too large a number to convert!");
		  return "";
	 }
	 
	// Process the coversion from currency digits to characters:
	 // Separate integral and decimal parts before processing coversion:
	 parts = currencyDigits.split(".");
	 if (parts.length > 1) {
	  integral = parts[0];
	  decimal = parts[1];
	  // Cut down redundant decimal digits that are after the second.
	  decimal = decimal.substr(0, 2);
	 }
	 else {
	  integral = parts[0];
	  decimal = "";
	 }
	 // Prepare the characters corresponding to the digits:
	 digits = new Array(CN_ZERO, CN_ONE, CN_TWO, CN_THREE, CN_FOUR, CN_FIVE, CN_SIX, CN_SEVEN, CN_EIGHT, CN_NINE);
	 radices = new Array("", CN_TEN, CN_HUNDRED, CN_THOUSAND);
	 bigRadices = new Array("", CN_TEN_THOUSAND, CN_HUNDRED_MILLION);
	 decimals = new Array(CN_TEN_CENT, CN_CENT);
	 // Start processing:
	 outputCharacters = "";
	 // Process integral part if it is larger than 0:
	 if (Number(integral) > 0) {
	  zeroCount = 0;
	  for (i = 0; i < integral.length; i++) {
	   p = integral.length - i - 1;
	   d = integral.substr(i, 1);
	   quotient = p / 4;
	   modulus = p % 4;
	   if (d == "0") {
	    zeroCount++;
	   }
	   else {
	    if (zeroCount > 0)
	    {
	     outputCharacters += digits[0];
	    }
	    zeroCount = 0;
	    outputCharacters += digits[Number(d)] + radices[modulus];
	   }
	   if (modulus == 0 && zeroCount < 4) {
	    outputCharacters += bigRadices[quotient];
	   }
	  }
	  outputCharacters += CN_DOLLAR;
	 }
	 // Process decimal part if there is:
	 if (decimal != "") {
	  for (i = 0; i < decimal.length; i++) {
	   d = decimal.substr(i, 1);
	   if (d != "0") {
	    outputCharacters += digits[Number(d)] + decimals[i];
	   }
	  }
	 }
	 // Confirm and return the final output string:
	 if (outputCharacters == "") {
	  outputCharacters = CN_ZERO + CN_DOLLAR;
	 }
	 if (decimal == "") {
	  outputCharacters += CN_INTEGER;
	 }
	 outputCharacters = CN_SYMBOL + outputCharacters;
	 return outputCharacters;
}

/*  
*    ForDight(Dight,How):数值格式化函数，Dight要  
*    格式化的数字，How要保留的小数位数。  
*/  
function  ForDight(Dight,How)  
{  
   Dight  =  Math.round  (Dight*Math.pow(10,How))/Math.pow(10,How);  
   return  Dight;  
}
