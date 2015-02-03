var reportMgr;
var tabs;

$(function() {
	
	tabs = $('#tabs').tabs({
		border : false
	});
       
        reportMgr = $('#reportMgr').tree({
		data:[{
			"id":1,
			"text":"社科项目申请书",
			"attributes":{  
		           "url":""
		        }
			
		      },{
			"id":2,
			"text":"科技项目申请书",
                         "attributes":{  
		           "url":""
		        }
		     }],
		onClick : function(node) {
			addTab(node);
		}
	 });
	
});

function addTab(node) {
	if (tabs.tabs('exists', node.text)) {
		tabs.tabs('select', node.text);
		refreshTab(node.text);
	} else {
		tabs.tabs('add',{
			title : node.text,
			content : "<iframe src='"+node.attributes.url+"' frameborder='0' style='width:100%;height:100%; border:0;' ></iframe>",
			border : false,
			fit : true,
			tools : [ {
				iconCls : 'icon-mini-refresh',
				handler : function() {
					refreshTab(node.text);
				}
			},{
				iconCls : 'icon-mini-close-default',
				handler : function() {
					tabs.tabs('close',node.text);
				}
			}]
		});
	}
}

function refreshTab(title){
	var tab = tabs.tabs('getTab',title);
	tabs.tabs('update',{
		tab:tab,
		options:tab.panel('options')
	});
}
