var component = Vue.component('sys-flowconfig',
{
    template: Vue.frameTemplate('SysFlowDesigner/FlowConfig'),
    components: {
        flowuserselect: componentAssemble.FlowUserSelect,
    },
	created: function () {
		this.getWorkFlowRoleDataList();
	},
	mounted: function () {
	    var data = this.$refs["userDataGrid"];
	},
	data: function () {
		return {
			roleOptions: {
				tableData: [],
				currentRoleId: '',
				formDialog: false,
			},
			roleFormData: {
				id: '',
				flowRoleName: '',
				isActive: true,
				description: '',
			},
			formRules: {
				flowRoleName: [
					{ required: true, message: '必填项', trigger: 'blur' },
					{ min: 1, max: 50, message: '长度在 1 到 50 个字符', trigger: 'blur' }
				],
			},
			userTableOptions: {
			    searchTxt: '',
			    tableData:[]
			},

			flowUserSelectOptions: {
			    visible: false,
			    selectData: []
			},

			
			
		}
	},
	watch: {
		//监听
	},
	computed: {
		//计算
	},
	methods: {
		doNav: function (page) {
			if (page == 'WorkFlowRole') {
				this.getWorkFlowRoleDataList();
			} else if (page == '') {
			
			} 
		},
		getWorkFlowRoleDataList: function () {
			var _this = this;
			abp.ajax({
				url: '/SysFlowDesigner/GetWorkFlowRoleList',
			}).done(function (data) {
				//
				_this.roleOptions.currentRoleId = null;
				if (data.length > 0) {
					_this.roleOptions.currentRoleId = data[0].id;
					_this.getUserByRole(_this.roleOptions.currentRoleId);
				}
				//
				_this.roleOptions.tableData = data;
			});
		},
		doRoleSaveForm: function () {
			var _this = this;
			this.$refs["formRoleData"].validate(
				function (valid) {
					if (valid) {
						//
						abp.ajax({
							url: '/SysFlowDesigner/SaveWorkFlowRole',
							data: JSON.stringify(_this.roleFormData),
							type: 'POST'
						}).done(function (data, res, e) {
							_this.roleOptions.formDialog = false
							_this.tipSuccess('save');
							_this.getWorkFlowRoleDataList();
						});
					} else {
						return false;
					}
				});
		},
		doRoleAdd: function () {
			this.roleOptions.formDialog = true;
			var _this = this;
			this.$nextTick(function () {
				_this.$refs.formRoleData.resetFields();
			});
		
		},
		doRoleEdit: function () {
			var _this = this;
			this.roleOptions.formDialog = true;
			abp.ajax({
				url: '/SysFlowDesigner/GetRoleModel',
				data: JSON.stringify(_this.roleOptions.currentRoleId)
			}).done(function (data, res, e) {
				if (!data) {
					_this.tipShow('error', '未获取到数据');
					return;
				}
				_this.roleFormData = data;
				_this.$refs["formRoleData"].clearValidate();
			});
		},
		doRoleDel: function () {
			var _this = this;
			if (!this.roleOptions.currentRoleId) {
				this.tipShow('error', 'IsSelect');
				return;
			}
			this.$confirm('确定删除?', '提示', {
				type: 'warning'
			}).then(function () {
				abp.ajax({
					url: '/SysFlowDesigner/DelRoleModel',
					data: JSON.stringify(_this.roleOptions.currentRoleId)
				}).done(function (data, res, e) {
					_this.tipSuccess('del');
					_this.getWorkFlowRoleDataList();
				});
			});

		},
		getUserByRole: function (id) {
			var _this = this;
			this.roleOptions.currentRoleId = id;
			//abp.ajax({
			//	url: '/SysAuthorization/GetApiClientByResource',
			//	data: JSON.stringify(id)
			//}).done(function (data, res, e) {
			//	_this.clientTableOptions.clientTableData = data;
			//});
		},

		doLinkUser: function () {
			var _this = this;
			this.flowUserSelectOptions.visible = true;

		},
		doDelLinkUser: function () {

		    this.flowUserSelectOptions.visible = false;
		},


		//setNodeIco: function (node) {
		//	if (node.level == 1) {
		//		return 'fa fa-institution';
		//	}
		//	if (node.data.orgType == 1) {
		//		return 'fa fa-building';
		//	}
		//	if (node.data.orgType == 2) {
		//		return 'fa fa-group';
		//	}
		//	return 'fa fa-th';
		//},
		//doNodeClick: function (data, node, e) {
		//    this.getFlowUserList();
		//},
		//getFlowUserList: function () {
		//    var _this = this;
		//    var data = this.$refs["treeData"].getCurrentNode();
		//    //
		//    this.userPageOptions.params.orgCode = data.orgCode; 
		//    abp.ajax({
		//        url: '/SysFlowDesigner/GetFlowUserPaging',
		//        data: JSON.stringify(this.userPageOptions)
		//        //type: 'POST'
		//    }).done(function (data) {
		//        _this.userPageOptions.total = data.totalCount;
		//        _this.userTableOptions.tableData = data.resultData;
		//        _this.userTableOptions.selectRow = {};
		//        _this.userTableOptions.selectRows = [];
		//    });
		//},
		//handleSizeChange: function (val) {
		//    this.userPageOptions.pageSize = val;
		//    this.getFlowUserList();
		//},
		//handleCurrentChange: function (val, e) {
		//    this.userPageOptions.pageIndex = val;
		//    this.getFlowUserList();
		//},
		//filterNode(value, data) {
		//    if (!value)
		//        return true;
		//    return data.orgName.indexOf(value) !== -1;
		//},
		//filtOrgList: function (val) {
		//    this.$refs["treeData"].filter(val);
		//}
        

	}
});
