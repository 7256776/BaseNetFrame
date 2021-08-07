
var component = Vue.component('sys-commodity', {
    template: Vue.frameTemplate('AppBaseConfig/Commodity'),
    created: function () {
        //
        this.formCommodityData = this.$route.params;
        if (this.formCommodityData && this.formCommodityData.categoryIndex) {
            this.activeSidebar = this.formCommodityData.categoryIndex;
        }
        this.initPointerType();
    },
    data: function () {
        return {
            rightText:'新增',
            showPreview:false,
            imagesPreview: [],
            pointerTypeData: [],
            commodityData:[],
            activeSidebar: 0,
            formCommodityData: {
                id: null,
                categoryIndex: 0,
            },
            formSolutionData: {
                id: null,
                solutionName: null,
                description: null
            },
        };
    },
    watch: {
        //监听
    },
    computed: {
   
    },
    methods: {
        onClickLeft: function () { 
            this.$router.replace({
                name: 'sys-home',
            });
            this.$router.go(-1)
        },
        onClickRight: function () {
            this.$router.push({
                name: 'sys-commodityinput'
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
                    //
                    _this.currentSelect = {
                        categoryCode: _this.pointerTypeData[_this.activeSidebar].dictCode,
                        categoryName: _this.pointerTypeData[_this.activeSidebar].dictContent
                    };
                    _this.getCommodity();
                }
            });
        },
        getCommodity: function () { 
            var _this = this;
            abp.ajax({
                url: '/AppBaseConfig/GetCommodityList',
                data: JSON.stringify({ categoryCode: _this.currentSelect.categoryCode }),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.commodityData = data;
            });
        },
        doEdit: function (item) {
            item['categoryIndex'] = this.activeSidebar;
            this.$router.push({
                name: 'sys-commodityinput',
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
                        abp.ajax({
                            url: '/AppBaseConfig/DelCommodity',
                            data: JSON.stringify(item.id),
                            type: 'POST'
                        }).done(function (data, res, e) {
                            //关闭窗口
                            done();
                            _this.getCommodity();
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
            this.getCommodity();
        },
        thumbUrl: function (item) {
            if (!item.filePathThumbnail) {
                return "";
            }
            return location.origin + item.filePathThumbnail;
        },
        doClickThumb: function (item) {
            var _this = this;
            this.showPreview = true;
            abp.ajax({
                url: '/AppBusiness/GetFiles',
                data: JSON.stringify(item.id),
                type: 'POST'
            }).done(function (data, res, e) {
                data = data || [];
                _this.imagesPreview = [];
                data.forEach(function (item, index) {
                    _this.imagesPreview.push(location.origin + item.filePathPreview);
                });
            });
        }

        
    }

});
