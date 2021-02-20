/*
渲染平铺模式下子菜单的布局
例:原始数据属于嵌套格式的菜单数据
菜单1
    菜单1_1
        菜单1_1_1
    菜单1_2
    菜单1_3
菜单2
    菜单2_1
        菜单2_1_1
    菜单2_2
菜单3
    菜单3_1
    菜单3_2
菜单4
    菜单4_1
    菜单4_2

**********************************************************************************************
转换后的数据格式为平铺的方式,平铺的列数可以自定义(注:列的数量仅限 1,2,3,4,6 几个数字)

菜单1                      菜单2                菜单3
菜单1_1                  菜单2_1             菜单3_1
菜单1_1_1              菜单2_1_1          菜单3_2
菜单1_2                  菜单2_2             ---------
菜单1_3                  ---------
---------
菜单4                     
菜单4_1                
菜单4_2              

 */
Vue.component('sys-topmenufullsub', {
    template: "<ul class= 'mega-menu-submenu' > "+
                        "<li v-for='itemSub in getItemsFull()' >" +
                            "<router-link v-bind:to='toUrl(itemSub.url)'  v-if='!itemSub.isRow'>  " +
                                "<i v-bind:class='icoClass(itemSub.icon)'></i> "+
                                "{{ itemSub.displayName }}  " +
                            "</router-link > " +
                            "<span v-if='itemSub.isRow'>&nbsp;</span>"+
                        "</li>" +
                    "</ul>",
     components: {
         
     },
    props: {
        menusdata: {
            type: Array
        },
        menusIndex: {
            type: Number,
            default: 0
        },
        menusCol: {
            type: Number,
            default: '4'
        }
    },
    created: function () {
        //再次注册子菜单展开事件
        this.$nextTick(function () {
            Layout.initMainMenuDropdown();
        })
    },
    data: function () {
        return {
        
        };
    },
    watch: {

    },
    methods: {
        getItemsFull: function () {
            var _this = this;
            var data = [];
            //获取当前渲染的div索引
            var dataIndex = this.menusIndex - 1;
            //循环获取总数据集合中的菜单列表
            while (this.menusdata[dataIndex]) {
                //添加额外属性用于分组菜单分割线
                _this.menusdata[dataIndex]["isRow"] = false;
                //添加当前二级菜单对象
                data.push(_this.menusdata[dataIndex]);
                //遍历二级菜单所有子节点转换层级数据为平面数据
                _this.addSubMenus(_this.menusdata[dataIndex].items, data)
                //添加空行对象区分菜单分组
                data.push({ isRow: true });
                //根据所设置的列数来获取下一个分组的菜单对象以及所包含的子菜单
                dataIndex = dataIndex + _this.menusCol;
            }
            return data;
        },
        addSubMenus: function (item, data) {
            //遍历所有子菜单
            var addMenus = function (item, data) {
                item.forEach(function (item, index) {
                    data.push(item);
                    //递归获取所有子节点菜单路由
                    addMenus(item.items, data)
                });
            };
            addMenus(item, data);
        },
        icoClass: function (ico) {
            return "fa " + ico + " fa-fw";
        },
        toUrl: function (url) {
            //如果路由设置了 / 默认不做任何操作
            if (url == '/' || url.length <= 1) {
                return '';
            }
            return url;
        }
       

    }
});
