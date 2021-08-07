
var component = Vue.component('sys-pointertypebaseinput', {
    template: Vue.frameTemplate('AppBaseConfig/PointerTypeInput'),
    created: function () {
        this.formData = this.$route.params;
        this.formData.isActive = this.formData.isActive == undefined ? true : this.formData.isActive; 
        this.initPointerType();
        this.initUnitData();
    },
    data: function () {
        return {
            pointerTypeData: [],
            pointerTypeIndex: 0,
            unitIndex:0,
            unitData: [],
            formItem: {
                showPointerType:false,
                showUnit: false,
            },
            formData: {
                id: null,
                categoryCode: null,
                categoryName: null,
                itemName: null,
                classifyName: null,
                unitCode: null,
                unitName: null,
                isActive: true,
                categoryIndex: 0
            },
            formRules: {
                required: [
                    { required: true, trigger: 'onBlur' },
                ],
              
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
        onClickLeft: function () {
            this.$router.replace({
                name: 'sys-pointertypebase',
                params: this.formData
            });
            //处理浏览器返回需要进行两次
            this.$router.go(-1)
        },
        onClickRight: function () {
            this.$refs["formEl"].submit();
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
                //
                _this.unitData.forEach(function (item, index) {
                    if (item.dictCode == _this.formData.unitCode) {
                        _this.unitIndex = index;
                        return;
                    }
                }); 
            });
        },
        initPointerType: function () {
            var _this = this;
            abp.ajax({
                url: '/SysDict/GetSysDictListByDictType',
                data: JSON.stringify("PL"),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.pointerTypeData = data;
                //
                _this.pointerTypeData.forEach(function (item, index) {
                    if (item.dictCode == _this.formData.categoryCode) {
                        _this.pointerTypeIndex = index;
                        return;
                    }
                });
            });
        },
        doSubmit: function () {
            var _this = this;
            abp.ajax({
                url: '/AppBaseConfig/SaveBuildProjectCategoryConfig',
                data: JSON.stringify(_this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.tipSuccess('save');
                _this.onClickLeft();
            });
        },


    },
  

});
