
var component = Vue.component('j-usersetting', {
    template: Vue.frameTemplate('J_Account/UserSettings'),
    created: function () {
        this.getUserInfo();
    },
    data: function () {
        return {
            userName: '',
            userAvatars: '',
            isShowImageList: false,
            formRules: {
                userNameCn: [
                    { required: true, message: '必填项', trigger: 'change' },
                    { min: 1, max: 30, message: '长度在 1 到 30 个字符', trigger: 'blur' }
                ],
                emailAddress: [
                    { type: 'email', message: '请输入正确的邮箱地址', trigger: 'blur,change' }
                ],
                newPass: [
                    { required: true, message: '必填项', trigger: 'change' },
                    { min: 6, max: 30, message: '密码长度在 6 到 30 个字符', trigger: 'blur' }
                ],
                oldPass: [
                    { required: true, message: '必填项', trigger: 'change' }
                ],
                checkPass: [
                    { required: true, message: '必填项', trigger: 'change' },
                    { min: 6, max: 30, message: '密码长度在 6 到 30 个字符', trigger: 'blur' }
                ]
            },
            formData: {
                id: null,
                userCode: null,
                userNameCn: null,
                sex: "1",
                emailAddress: null,
                phoneNumber: null,
                description: '',
                isActive: true
            },
            formPassData: {
                id: null,
                oldPass: null,
                newPass: null,
                checkPass: null
            }
        };
    },
    watch: {
       //监听
    },
    methods: {
        getUserInfo: function () {
            var _this = this;
           
            abp.ajax({
                url: '/J_Account/GetUserModel',
                data: JSON.stringify({ id: abp.session.userId }),
                type: 'POST'
            }).done(function (data) {
                _this.formData = data;
                _this.formPassData.id = data.id;
                _this.userName = data.userNameCn;
                _this.userAvatars = data.imageUrl;
                //页面加载完成后执行验证避免出现错误的验证提示
                _this.$nextTick(function () {
                    //_this.$refs.formMyUserData.validate();
                });
            });
        },
        doSaveForm: function () {
            var _this = this;
            this.$refs["formMyUserData"].validate(
                function (valid) {
                    if (valid) {
                        //
                        abp.ajax({
                            url: '/J_Account/SeetingUserInfo',
                            data: JSON.stringify(_this.formData)
                        }).done(function (data, res, e) {
                            abp.message.success('保存成功');
                            _this.userName = _this.formData.userNameCn;
                        });
                        //基本上可以忽略监控fail,abp已经完成了这部分处理
                        //fail(function (res, e) { });

                    } else {
                        //console.log('error submit!!');
                        return false;
                    }
                }
            );
        },
        doSaveAvatars: function (i) {
            var _this = this;
            abp.ajax({
                url: '/J_Account/SaveAvatars',
                data: JSON.stringify({ imgId: i }),
                type: 'POST'
            }).done(function (data) {
                Vue.prototype.GlobalAuthorizedEntity.user.refresh(i);
                _this.userAvatars = i;
                //刷新工具栏的头像
                abp.event.trigger('frame.userimg.ui.event');
                abp.message.success("您的头像更换完成!");
            });
        },
        doModifyPass: function () {
            var _this = this;
            this.$refs["formPassData"].validate(
                function (valid) {
                    if (valid) {
                        //
                        abp.ajax({
                            url: '/J_Account/UpdateUserPass',
                            data: JSON.stringify(_this.formPassData)
                        }).done(function (data, res, e) {
                            abp.message.success('密码修改成功');
                        });
                        //基本上可以忽略监控fail,abp已经完成了这部分处理
                        //fail(function (res, e) { });

                    } else {
                        //console.log('error submit!!');
                        return false;
                    }
                }
            );
        },
    }
});
