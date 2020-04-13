
var component = Vue.component('sys-userinfoextens', {
    template: Vue.frameTemplate('SysComponents/UserInfoExtens'),
    created: function () {
       
    },
    mounted: function () {
     
    },
    props: {
        value: {
            type: Object
        }
    },
    data: function () {
        return {
            formRulesEx: {
                userCodeEx: [
                    { required: true, message: '必填项', trigger: 'blur' },
                ],
                userNameEx: [
                    { required: true, message: '必填项', trigger: 'blur' },
                ]
            },
            formDataEx: {
                userCodeEx: null,
                userNameEx: null
            }
        };
    },
    watch: {
        value: function () {
            this.formDataEx = this.value;
        }
    },
    computed: {
        //计算
    },
    methods: {
        doResetFields: function () {
            var _this = this;
            this.$nextTick(function () {
                if (_this.$refs.formUserDataEx) {
                    _this.$refs.formUserDataEx.resetFields();
                }
            });
        },
        //保存提交操作(扩展开组件时候重写该方法)
        doSubmitForm: function (user) {
            var isValidate = true;
            if (this.$refs["formUserDataEx"]) {
                //同步验证
                var resValid = this.$refs["formUserDataEx"].validate();
                //返回验证结果
                isValidate = resValid._v;
            }

            var res = {
                isSubmit: isValidate,
                formDate: this.formDataEx
            }
            return res;
        }
        //函数
    }


});
