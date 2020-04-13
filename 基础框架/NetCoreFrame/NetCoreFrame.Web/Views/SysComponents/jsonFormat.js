
Vue.component('sys-jsonformat', {
    template: "<div>"+
                        "<div class='col-md-12 ' v-html='jsonObjectFn'> </div>" +  
                    "</div>",
    props: {
        jsonObj: {
            type: Object,
            default: null
        }
    },
    updated: function () {
        //html加载完成后注册事件
        this.doFolding();
    },
    created: function () {
       
    },
    data: function () {
        return {
            typeEnum : {
                TYPE_STRING: "string",
                TYPE_INT: "number",
                TYPE_OBJECT: "object",
                TYPE_BOOLEAN: "boolean",
            } 
        };
    },
    watch: {
     
    },
    computed: {
        jsonObjectFn: function () {
            if (!this.jsonObj) {
                return
            }
            var i = Object.keys(this.jsonObj).length;
            var data = { "日志消息": "当前日志没有参数信息" };
            if (i > 0) {
                data = this.jsonObj;
            }
            return this.formatJson(data);
        }
    },
    methods: {
        //jsonObj JSON.parse 元素
        formatJson: function (jsonObj) {
            //缩减单元
            var tabIndex = 2;
            var innerhtml = "", idx = 0, isArray = jsonObj instanceof Array, length = 0;
            //console.log("1:" + isArray);

            if (jsonObj != null) {
                length = Object.keys(jsonObj).length;
            }
            for (var obj in jsonObj) {

                var isD = idx + 1 != length;

                var preInnerHtml = "";
              
                if (typeof jsonObj[obj] === this.typeEnum.TYPE_OBJECT) {
                    if (isArray) {
                        preInnerHtml = this.getObjectArrayDiv(this.formatJson(jsonObj[obj], tabIndex + 1), isD);
                    } else {
                        preInnerHtml = this.getObjectDiv(obj, this.formatJson(jsonObj[obj], tabIndex + 1), isD);
                    }

                } else {
                    //console.log("2" + isArray);
                    if (isArray) {
                        preInnerHtml = this.getArrayDiv(jsonObj[obj], isD);
                    } else {
                        preInnerHtml = this.getDiv(obj, jsonObj[obj], isD);
                    }
                }
                innerhtml += preInnerHtml;
                idx++;
            }
            return this.getPanel(innerhtml, tabIndex, isArray, length);
        },
        getDiv: function (key, value, isD) {
            return "<div>" + this.getTitleSpan(key) + ":" + this.getValueSpan(value) + (isD ? "," : "") + "</div>";;
        },
        getObjectDiv: function (key, value, isD) {
            return "<div>" + this.getTitleSpan(key) + ":" + value + (isD ? "," : "") + "</div>";;
        },
        getObjectArrayDiv: function (value, isD) {
            return "<div>" + value + (isD ? "," : "") + "</div>";;
        },
        getArrayDiv: function (value, isD) {
            return "<div>" + this.getValueSpan(value) + (isD ? "," : "") + "</div>";;
        },
        getTitleSpan: function (value) {
            return "<span style='color: #92278f;font-weight: bold;'>\"" + value + "\"</span>";
        },
        getValueSpan: function (value) {
            var type = typeof value;

            switch (type) {
                case this.typeEnum.TYPE_STRING:
                    return "<span style='color: #3ab54a;font-weight: bold;'>\"" + value + "\"</span>";
                case this.typeEnum.TYPE_INT:
                    return "<span style='color: #25aae2;font-weight: bold;'>" + value + "</span>";
                case this.typeEnum.TYPE_BOOLEAN:
                    return "<span style='color: #555;'>" + value + "</span>";
            }
            return "error";
        },
        getPanel: function (innerHtml, tabIndex, isArray, index) {
            if (isArray) {
                return "<span class=\"\"><i ref='datajson' name='jsonFormat' style=\"" + this.getStyleSuo() +"\">-</i>[</span><div style=\"" + this.getStyle(tabIndex) + "\">" + innerHtml + "</div><label style=\" display: none;\">Array <span style='color: #25aae2; font-weight: bold;'>" + index + "<span></label><span>]</span>";
            } else {
                return "<span class=\"\"><i ref='datajson' name='jsonFormat'  style=\"" + this.getStyleSuo()+"\">-</i>{</span><div style=\"" + this.getStyle(tabIndex) + "\">" + innerHtml + "</div><label style=\" display: none;\">Object{...}</label><span>}</span>";
            }
        },
        getStyle: function (i) {
            if (i == 0) {
                i = 1;
            }
            return "margin-left: " + i + "0px; ";
        },
        getStyleSuo: function () {
            var panelSuo =
                "padding: 0 2px; " +
                "cursor: pointer;" +
                "display: inline-block;" +
                "font: normal normal normal 14px/ 1 FontAwesome;" +
                "font-size: inherit;" +
                "text-rendering: auto;" +
                "-webkit-font-smoothing: antialiased;" +
                "border: 1px solid #555555;" +
                "border-radius: 3px;";
            return panelSuo;
        },
        doFolding: function () {
            var data = document.getElementsByName('jsonFormat');
            //注册缩进事件
            for (var i = 0; i < data.length; i++) {
                data[i].onclick = function (e, n) {
                    var isState = e.target.innerHTML === "-" ? true : false;
                    //设置标签
                    if (isState)
                        e.target.innerHTML = "+"
                    else
                        e.target.innerHTML = "-"
                    //设置收缩标签
                    var parentEl = e.target.parentNode.parentNode.childNodes;
                    //
                    for (var i = 0; i < parentEl.length; i++) {
                        if (parentEl[i].localName === 'div')
                            parentEl[i].hidden = isState;
                        if (parentEl[i].localName === 'label')
                            parentEl[i].hidden = !isState;
                    }
                };
            }
        }

    }
});
