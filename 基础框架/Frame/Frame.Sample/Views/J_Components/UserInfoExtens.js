
var component = Vue.component('j-userinfoextens', {
    template: Vue.frameTemplate('J_Components/UserInfoExtens'),
    created: function () {
       
    },
    props: {
        value: {
            type: Object
        },
    },
    data: function () {
        return {
            formDataEx: {
                userNameEx: '',
                job: ''
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
                _this.$refs.formUserDataEx.resetFields();
            });
        },
        doSubmitForm: function (user) {
            var res = {
                isSubmit: true,
                formDate: this.formDataEx
            }
            return res;
        }
        //函数
    }


});
