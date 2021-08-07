
var component = Vue.component('sys-pointertypebase', {
    template: Vue.frameTemplate('AppBaseConfig/PointerTypeConfig'),
    created: function () {
        this.initPointerType();
        this.currentSelect = this.$route.params;
    },
    data: function () {
        return {
            isTabbar: false,
            activeSidebar:0,
            pointerTypeData: [],
            pointerTypeConfig: [],
            currentSelect: {},
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
                name: 'sys-home',
            });
        },
        onClickRight: function () {
            this.$router.push({
                name: 'sys-pointertypebaseinput',
                params: {
                    categoryCode: this.currentSelect.categoryCode,
                    categoryName: this.currentSelect.categoryName,
                    categoryIndex: this.activeSidebar
                }
            });
        },
        doSidebar: function (index, item) {
            this.activeSidebar = index;
            this.currentSelect = {
                categoryIndex: index,
                categoryCode: item.dictCode,
                categoryName: item.dictContent
            };
            this.initPointerTypeConfig();
        },
        doEdit: function (item) {
            item.categoryIndex = this.activeSidebar ;
            this.$router.push({
                name: 'sys-pointertypebaseinput',
                params: item
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
                            url: '/AppBaseConfig/DelBuildProjectCategoryConfig',
                            data: JSON.stringify(ids),
                            type: 'POST'
                        }).done(function (data, res, e) {
                            //关闭窗口
                            done();
                            _this.initPointerTypeConfig();
                        });
                    }
                    if (action == 'cancel') {
                        done();
                    }
                }
            });
        },
        initPointerTypeConfig: function () {
            var _this = this;
            abp.ajax({
                url: '/AppBaseConfig/GetBuildProjectCategoryConfigList',
                data: JSON.stringify({ categoryCode: _this.currentSelect.categoryCode, isActive: null }),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.pointerTypeConfig = data;
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
                if (_this.pointerTypeData && _this.pointerTypeData.length > 0) {
                    var rowIndex = 0;
                    if (_this.currentSelect && _this.currentSelect.categoryIndex) {
                        rowIndex = _this.currentSelect.categoryIndex;
                    }
                    _this.activeSidebar = rowIndex;
                    _this.currentSelect = {
                        categoryIndex: rowIndex,
                        categoryCode: _this.pointerTypeData[rowIndex].dictCode,
                        categoryName: _this.pointerTypeData[rowIndex].dictContent
                    };
                    _this.initPointerTypeConfig();
                }
            });
        },
        doStyle: function (index) {
            if (index == this.activeSidebar) {
                return { 'background-color': '#FAE1E1' };
            }
            return {};
        },

    },
  

});
