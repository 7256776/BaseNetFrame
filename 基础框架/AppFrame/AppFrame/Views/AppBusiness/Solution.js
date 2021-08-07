
var component = Vue.component('sys-solution', {
    template: Vue.frameTemplate('AppBusiness/Solution'),
    created: function () {
        this.initSolution();
    },
    components: {
        //[ImagePreview.Component.name]: ImagePreview.Component,
    },
    data: function () {
        return {
            solution: {
                solutionName: '',
            },
            solutionList: [],
            images: [],
            show:false
        };
    },
    watch: {
        //监听
    },
    computed: {
       
    },
    methods: {
        doImagePreview: function (item, filePath) {
            this.show = true;
            this.images.push(location.origin + item[filePath]);
        },
        onClickLeft: function () {
            this.$router.push({
                name: 'sys-home',
            });
        },
        onClickRight: function () {
            this.$router.push({
                name: 'sys-solutioninput'
            });
        },
        initSolution: function () {
            var _this = this;
            abp.ajax({
                url: '/AppBusiness/GetSolutionList',
                data: JSON.stringify(_this.solution),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.solutionList = data;
            });
        },
        doEdit: function (item) {
            this.$router.push({
                name: 'sys-solutioninput',
                params: item.appSolutionData
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
                        var ids = [item.appSolutionData.id]
                        abp.ajax({
                            url: '/AppBusiness/DelSolution',
                            data: JSON.stringify(ids),
                            type: 'POST'
                        }).done(function (data, res, e) {
                            //关闭窗口
                            done();
                            _this.initSolution();
                        });
                    }
                    if (action == 'cancel') {
                        done();
                    }

                }
            });
        },
        thumbUrl: function (item,filePath) {
            return location.origin + item[filePath];
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

    }

});
