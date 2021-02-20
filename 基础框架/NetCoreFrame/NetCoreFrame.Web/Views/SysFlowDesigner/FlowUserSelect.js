var component = Vue.component('sys-flowuserselect',
{
    template: Vue.frameTemplate('SysFlowDesigner/FlowUserSelect'),
    created: function () {
	},
    mounted: function () {
	},
	props: {
	    value: {
	        type: Array,
	        default: function () {
	            return [];
	        }
	    },
	    visible: {
	        type: Boolean,
	        default: false
	    },
	},
	data: function () {
	    return {
	        treeOptions: {
	            treeData: [],
	            isChild: false,
	            orgCode: ''
	        },
	        defaultProps: {
	            children: 'childrenOrg',
	            label: 'orgName',
	            value: 'orgId'
	        },
	        userPageOptions: {
	            params: {
	                userCodeOrName: '',
					orgCode: '',
					orgNode: '',
					isInclude:false
	            },
	            pageIndex: 1,
	            pageSize: 20,
	            total: 0
	        },
	        userTableOptions: {
	            gridHeight: 0,
	            tableData: [],
	            selectRows: []
	        },
	    }
	},
	computed: {
	    //
	},
	watch: {
	    //监听
	},
	methods: {
	    closeDialog: function () {
	        this.$emit('update:visible', false);
            //触发关闭后事件
	        this.$emit('closed');
	    },
        //显示窗口时
	    enter: function (el, done) {
	        var _this = this;
	        //通过容器定义高度
	        this.$nextTick(function () {
	            var grid = _this.$refs["userDataGrid"];
	            _this.userTableOptions.gridHeight = grid.$parent.$el.clientHeight - 80;
				_this.initOrg();
				_this.filterFlowUserList();
	        });
	    },
        //点击模式窗体右上角 关闭图标 触发
	    beforeCloseDialog: function (done) {
	        this.closeDialog();
	        // done 是控件的关闭事件,此处忽略
	    },
        //初始组织机构
		initOrg: function () {
			var _this = this;
			abp.ajax({
				url: '/SysFlowDesigner/GetSysOrg',
			}).done(function (data, res, e) {
				if (!data) {
					_this.tipShow('error', '未获取到数据');
					return;
				}
				_this.treeOptions.treeData = data;
			});
		},
        //设置org图标
		setNodeIco: function (node) {
			if (node.level == 1) {
				return 'fa fa-institution';
			}
			if (node.data.orgType == 1) {
				return 'fa fa-building';
			}
			if (node.data.orgType == 2) {
				return 'fa fa-group';
			}
			return 'fa fa-th';
		},
		//
		filterFlowUserList: function () {
			this.userPageOptions.pageIndex = 1;
			this.getFlowUserList();
		},
        //
		getFlowUserList: function () {
		    var _this = this;
			var data = this.$refs["treeData"].getCurrentNode();
			if (data) {
				//
				this.userPageOptions.params.orgCode = data.orgCode;
				this.userPageOptions.params.orgNode = data.orgNode; 
            }
		    abp.ajax({
		        url: '/SysFlowDesigner/GetSysUserPaging',
		        data: JSON.stringify(_this.userPageOptions)
		        //type: 'POST'
		    }).done(function (data) {
		        _this.userPageOptions.total = data.totalCount;
		        _this.userTableOptions.tableData = data.resultData;
		        _this.userTableOptions.selectRows = [];
		    });
		},
		doRowSelectChange: function (selection) {
		    this.userTableOptions.selectRows = selection
		},
		doRowclick: function (row, event, column) {
		    //设置选中行
		    this.$refs["userDataGrid"].toggleRowSelection(row);
		},
		selectRow: function () {
		    //设置选中行
		    var _this = this;
		    this.userTableOptions.selectRows.forEach(function (selectItem, selectIndex) {
		        var isUse = _this.value.some(function (item, index) {
		            if (item.userId == selectItem.userId) {
		                return true;
		            }
		        });
		        if (!isUse) {
		            _this.value.push(selectItem);
		        }
		    });
		    this.closeDialog();
		},
		handleSizeChange: function (val) {
		    this.userPageOptions.pageSize = val;
		    this.getFlowUserList();
		},
		handleCurrentChange: function (val, e) {
		    this.userPageOptions.pageIndex = val;
		    this.getFlowUserList();
		},
		filterNode: function (value, data) {
		    if (!value)
		        return true;
		    return data.orgName.indexOf(value) !== -1;
		},
		filtOrgList: function (val) {
		    this.$refs["treeData"].filter(val);
		}
        

	}
});
