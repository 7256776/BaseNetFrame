
var component = Vue.component('sys-pointertypeinput', {
    template: Vue.frameTemplate('AppBusiness/PointerTypeInput'),
    created: function () {
        //
        this.formSolutionData = this.$route.params;
        this.initUnitData();
        this.initPointerType();
    },
    data: function () {
        return {
            rightText:'新增',
            unitData:[],
            pointerTypeData: [],
            buildProjectCategoryData:[],
            activeSidebar: 0,
            unitIndex : 0,
            pointerTypeIndex: 0,
            mainDiv: true,
            formDiv: false,
            formItem: {
                showPointerType: false,
                showUnit: false,
                showCommodity: false,
            },
            formSolutionData: {
                id: null,
                solutionName: null,
                description: null
            },
            formData: {
                id: null,
                solutionId:null,
                categoryCode: null,
                categoryName: null,
                itemName: null,
                classifyName: null,
                unitCode: null,
                unitName: null,
                amount:0,
                price:0,
                budgetPrice:0,
                actualPrice:0,
                isActive: true,
                description: null,
                commodityList:[]
            },
            formRules: {
                required: [
                    { required: true, trigger: 'onBlur' },
                ], 
            },

            commodityName:'',
            cascaderValue: '',
            fieldNames: {
                text: 'dictContent',
                value: 'dictCode',
                children: 'items',
            },
            
        };
    },
    watch: {
        //监听
    },
    computed: {
        summation: function () {
            var price = 0;
            var amount = 0;
            if (!isNaN(parseFloat(this.formData.price))) {
                price = this.formData.price;
            }
            if (!isNaN(parseFloat(this.formData.amount))) {
                amount = this.formData.amount;
            }
            //
            this.formData.price = parseFloat(price);
            this.formData.amount = parseFloat(amount);
            //
            if (parseFloat(this.formData.actualPrice).toFixed(2) == parseFloat(this.formData.budgetPrice).toFixed(2)) {
                this.formData.actualPrice = (this.formData.price * this.formData.amount).toFixed(2);
            }
            this.formData.budgetPrice = (this.formData.price * this.formData.amount).toFixed(2);
            return this.formData.budgetPrice;
        }
    },
    methods: {
        onChange({ value }) {
            var _this = this;
            setTimeout(() => {
                _this.initPointerTypeConfig(value);
            }, 500);
        },
        onFinish({ selectedOptions }) { 
            this.formItem.showCommodity = false;
            this.commodityName = selectedOptions.map((option) => option.dictContent).join('/');
        },

        onClickLeft: function () {
            if (this.mainDiv) {
                this.$router.replace({
                    name: 'sys-solution',
                });
                this.$router.go(-1)
            }
            if (this.formDiv) {
                this.mainDiv = true;
                this.formDiv = false;
                this.rightText = '新增';
            }
        },
        onClickRight: function () {
            if (this.rightText == '保存') {
                this.$refs["formEl"].submit();
            } else {
                this.mainDiv = false;
                this.formDiv = true;
                this.rightText = '保存';
                this.formData.categoryCode = this.currentSelect.categoryCode;
                this.formData.categoryName = this.currentSelect.categoryName;
                this.formData.itemName = '';
                this.formData.classifyName = '';
                this.formData.unitCode = '';
                this.formData.unitName = '';
                this.formData.amount = 0;
                this.formData.price = 0;
                this.formData.budgetPrice = 0;
                this.formData.actualPrice = 0;
                this.formData.description = 0;
                this.formData.solutionId = this.formSolutionData.id;
                this.formData.id = null;
                this.formData.commodityList = [];

            }
        },
        doConfirmPointer: function (item, index) {
            this.formData.categoryCode = item.dictCode;
            this.formData.categoryName = item.dictContent;
            this.formItem.showPointerType = false
        },
        doConfirmUnit: function (item, index) {
            this.formData.unitCode = item.dictCode;
            this.formData.unitName = item.dictContent;
            this.formItem.showUnit = false
        },

        initUnitData: function () {
            var _this = this;
            abp.ajax({
                url: '/SysDict/GetSysDictListByDictType',
                data: JSON.stringify("DW"),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.unitData = data;
            });
        },
        initPointerType: function () {
            var _this = this;
            abp.ajax({
                url: '/SysDict/GetSysDictListByDictType',
                data: JSON.stringify("PL"),
                type: 'POST'
            }).done(function (data, res, e) {
               
                if (data && data.length > 0) {
                    //
                    _this.currentSelect = {
                        categoryCode: data[_this.activeSidebar].dictCode,
                        categoryName: data[_this.activeSidebar].dictContent
                    };
                    //
                    data.forEach(function (item, index) {
                        item['items'] = [];
                    });
                    _this.pointerTypeData = data;
                    //
                    _this.getBuildProjectCategoryDetail();
                }
            });
        },
        initPointerTypeConfig: function (categoryCode) {
            var _this = this;
            abp.ajax({
                url: '/AppBaseConfig/GetCommodityList',
                data: JSON.stringify({ categoryCode: categoryCode, isActive: null }),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.pointerTypeData.forEach(function (item, index) {
                    if (categoryCode == item.dictCode) {
                        item['items'] = [];
                        data.forEach(function (itemSub, index) {
                            item['items'].push({
                                'dictContent': itemSub.brand + ' - ' + itemSub.commodity,
                                'dictCode': itemSub.id,
                                //'items': []
                            });
                        });
                        console.log(item);
                    }
                });
            });
        },
        getBuildProjectCategoryDetail: function () {
            var _this = this;
            abp.ajax({
                url: '/AppBusiness/GetBuildProjectCategoryDetail',
                data: JSON.stringify({ solutionId: _this.formSolutionData.id, categoryCode: _this.currentSelect.categoryCode }),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.buildProjectCategoryData = data;
            });
        },
        getBuildProjectCategory: function () {
            var _this = this;
            abp.ajax({
                url: '/AppBusiness/GetBuildProjectCategory',
                data: JSON.stringify(this.formData.id),
                type: 'POST'
            }).done(function (data, res, e) {
                if (data && data.appBuildProjectCategoryToCommodityList && data.appBuildProjectCategoryToCommodityList.length > 0) {
                    var item = data.appBuildProjectCategoryToCommodityList[0];
                    _this.commodityName = data.categoryName + '/' + item.brand + ' - ' + item.commodity;
                    _this.cascaderValue = item.commodityId;
                }
            });
        },
        doEdit: function (item) {
            var _this = this;
            this.mainDiv = false;
            this.formDiv = true;
            this.rightText = '保存';
            this.formData = item;
            if (this.formDiv) {
                this.commodityName = '' ;
                this.cascaderValue = '';
                this.getBuildProjectCategory();
            }
            this.formData.solutionId = this.formSolutionData.id;
            //
            this.unitData.forEach(function (item, index) {
                if (item.dictCode == _this.formData.unitCode) {
                    _this.unitIndex = index;
                    return;
                }
            });
            //
            this.pointerTypeData.forEach(function (item, index) {
                if (item.dictCode == _this.formData.categoryCode) {
                    _this.pointerTypeIndex = index;
                    return;
                }
            });
        },
        doDel: function (item) {
            var _this = this;
            //
            this.$dialog.confirm({
                title: '提示',
                message: '确定删除?',
                beforeClose: function (action, done) {
                    if (action == 'confirm') {
                        var ids = [item.id]
                        abp.ajax({
                            url: '/AppBusiness/DelBuildProjectCategory',
                            data: JSON.stringify(ids),
                            type: 'POST'
                        }).done(function (data, res, e) {
                            //关闭窗口
                            done();
                            _this.getBuildProjectCategoryDetail();
                        });
                    }
                    if (action == 'cancel') {
                        done();
                    }
                }
            });
        },
        doSidebar: function (index, item) {
            this.activeSidebar = index;
            this.currentSelect = {
                categoryCode: item.dictCode,
                categoryName: item.dictContent
            };
            this.getBuildProjectCategoryDetail();
        },
        doSubmit: function () {
            var _this = this;
            this.formData.commodityList = [];
            if (this.cascaderValue) {
                this.formData.commodityList.push(this.cascaderValue);
            }

            abp.ajax({
                url: '/AppBusiness/SaveBuildProjectCategory',
                data: JSON.stringify(_this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.tipSuccess('save');
                _this.mainDiv = true;
                _this.formDiv = false;
                _this.rightText = '新增';
                _this.getBuildProjectCategoryDetail();
            });
        },
        toThousands: function (num) {
            var num = (num || 0).toString(), result = '';
            while (num.length > 3) {
                result = ',' + num.slice(-3) + result;
                num = num.slice(0, num.length - 3);
            }
            if (num) { result = num + result; }
            return result;
        },
        doStyle: function (index) {
            if (index == this.activeSidebar) {
                return { 'background-color': '#FAE1E1' };
            }
            return {};
        }

    }

});
