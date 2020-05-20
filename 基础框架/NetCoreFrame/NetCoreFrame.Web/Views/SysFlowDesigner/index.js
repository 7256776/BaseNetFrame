var component = Vue.component('sys-flowdesigner',
    {
        template: Vue.frameTemplate('SysFlowDesigner/Index'),
        mounted: function () {
            var _this = this;
            this.$nextTick(function () {
                _this.jsPlumbInit()
            });
        },
        created: function () {

        },
        data: function () {
            return {
                jsPlumb: null,				//jsPlumb 实例
                resultData: '',					//保存数据JSON字符串
                // endpoint: {
                // 	uid: '',
                // 	domName: '',
                // 	isShow: true
                // },
                endpointList: [],				//所有流程节点对象
                //connectionList: [],
                canvasSize: 1,
                baseConfig: {
                    // isSource: true, 			                                                                                // 是否可以拖动（作为连线起点）
                    // isTarget: true, 			                                                                                // 是否可以放置（连线终点）
                    maxConnections: -1, 		                                                                            // 端点最大连接数量(数字) -1 = 无限
                    //连接线类型 样式种类有[Bezier],[Flowchart],[StateMachine ],[Straight ] 
                    connector:
						['Flowchart', { stub: 0, gap: 0, cornerRadius: 5, alwaysRespectStubs: false }],//具有90度转折点的流程线
                    //连接线属性
                    connectorOverlays: [
						// ['PlainArrow'],
						['Arrow', {
						    width: 10,				//箭头宽度
						    length: 10,				//箭头长度
						    location: 0.33,			//箭头具体终点距离
						    stroke: 'green',		//拖拽线颜色
						    // foldback: 0.5,		//箭头的流体大小			
						    events: {
						        //点击箭头事件
						        click: function (mouseEvent) {
						            console.log('连接线箭头点击事件');
						        }
						    }
						}],
						['Arrow', {
						    width: 10,
						    length: 10,
						    location: 0.66
						}],
						// ['Label', {
						// 	label: '线名称',	//可以是html
						// 	location: 0.5,	   //文字具体终点距离(默认50%的位置)

						// 	labelStyle: {
						// 		color: '#ffffff',
						// 		fill: 'green',
						// 		padding: '5px',
						// 	},
						// 	events: {
						// 		click: function (labelOverlay, originalEvent) {
						// 			console.log('连接线的lable点击事件');
						// 		},	//点击label事件
						// 		//tap: this.doClickConnectorLabel,	//该事件似乎与click一致
						// 	}
						// }],
						// 添加砖石节点 
						// ['Diamond', {
						// 	width: 10,				//箭头宽度
						// 	length: 10,				//箭头长度
						// 	location: 0.3,			//箭头具体终点距离
						// 	stroke: 'green',		//拖拽线颜色
						// 	events: {
						// 		dblclick: function (diamondOverlay, originalEvent) {
						// 			console.log('双击钻石叠加 : ' + diamondOverlay.component);
						// 		}
						// 	}
						// }]
                    ],
                    //整体链接线风格
                    paintStyle: {
                        // fill: '',			//填充颜色(根据连线之间产生色块填充)
                        // radius: 11,			//锚点半径
                        stroke: 'green',		//线条颜色
                        strokeWidth: 0.5,		//线条宽度
                        // joinstyle: 'round',
                        outlineStroke: 'green',	//轮廓颜色
                        outlineWidth: 0.5,		//轮廓宽度
                        opacity: 0.5,
                    },
                    //整体链接线悬停风格
                    hoverPaintStyle: {
                        fill: '',
                        // radius: 1,
                        stroke: 'red',			//线条颜色
                        strokeWidth: 1,			//线条宽度
                        outlineStroke: 'red',	//轮廓颜色
                        outlineWidth: 1,		//轮廓宽度

                    },
                    //锚点类型
                    endpoint: 'Dot',			//Dot Rectangle   // endpoint: ['Dot', { radius: 18, fill: '#000000' }],
                    // 锚点的样式
                    endpointStyle: {
                        fill: 'green', 			//端点填充颜色	透明=transparent
                        radius: 5,				//圆形锚点半径(适用于Dot)
                        stroke: 'green',		//线条颜色(适用于Rectangle)
                        strokeWidth: 2,			//线条宽度					
                        width: 10,				//(适用于Rectangle)
                        height: 10,				//(适用于Rectangle)
                        // outlineStroke: 'red',//轮廓颜色
                        // outlineWidth: 5,		//轮廓宽度
                    },
                    //锚点悬停样式
                    endpointHoverStyle: {
                        fill: 'red', 			//端点填充颜色	透明=transparent
                        radius: 5,				//圆形锚点半径(适用于Dot)
                        stroke: 'red',			//线条颜色(适用于Rectangle)
                        strokeWidth: 2,			//线条宽度					
                        width: 10,				//(适用于Rectangle)
                        height: 10,				//(适用于Rectangle)
                        // outlineStroke: 'red',//轮廓颜色
                        // outlineWidth: 5,		//轮廓宽度
                    },
                    //设置锚点或线上的覆盖标签(适用于EndpointOverlays(仅限锚点),Overlays(线与锚点)) 暂未使用
                    overlays: [
						['Label', { label: '锚点', location: [-0.5, -0.5] }],
						['Label', { label: '线或锚', location: 0.5 }],

                    ],

                    //*************************************暂留************************
                    //连接线的样式
                    connectorStyle: {

                        //dashstyle: '2 2',		//设置虚线
                        stroke: 'green',		//拖拽线填充颜色		
                        strokeWidth: 1,			//拖拽线宽度
                        outlineStroke: 'green',	//拖拽线边框颜色
                        outlineWidth: 1,		//拖拽线边框宽带
                        lineWidth: 11,
                        fill: '',				//连线外圈填充颜色
                        // outlineColor: '',
                    },
                    // 鼠标悬停在连接线上的样式	
                    connectorHoverStyle: {
                        strokeWidth: 1,
                        lineWidth: 1,
                        outlineStroke: 'red',
                        outlineWidth: 1,
                        fill: ''
                        // outlineColor: ''
                    },
                }
            }
        },
        watch: {
            //监听
        },
        computed: {
            //计算
        },
        methods: {
            jsPlumbDefault: function () {
                return {
                    MaxConnections: this.baseConfig.maxConnections,				//锚点最大连接数量(数字) -1 = 无限 
                    ConnectionsDetachable: false,								                //是否允许拖动断开连线
                    ConnectionOverlays: this.baseConfig.connectorOverlays,		//连接线属性
                    Connector: this.baseConfig.connector,						            //连接线类型
                    PaintStyle: this.baseConfig.paintStyle,						            //主体连线风格样式
                    HoverPaintStyle: this.baseConfig.hoverPaintStyle,			        //主体连线悬停风格样式
                    Endpoint: this.baseConfig.endpoint,							            //锚点类型
                    EndpointStyle: this.baseConfig.endpointStyle,				        //锚点样式
                    EndpointHoverStyle: this.baseConfig.endpointHoverStyle,		//锚点悬停样式
                    LabelStyle: {												                            //标签样式
                        color: 'red'
                    },

                    //如下参数 通常根据风格在做设置
                    // DragOptions: {},											                    //拖动设置
                    // DropOptions: { tolerance: 'touch',},						            //拖放设置

                    // EndpointOverlays: [],									                        //锚点遮罩层
                    // Endpoints: [null, null],									                    //数组形式的，[源端点，目标端点]
                    // EndpointStyles: [null, null],							                    //[源端点样式，目标端点样式]
                    // EndpointHoverStyles: [null, null],						                //[源端点鼠标经过样式，目标端点鼠标经过样式]

                    //如下参数 通常单独对象上设置
                    // Anchor: 'Bottom',										                        //锚点，即端点连接的位置
                    // Anchors: [null, null],									                        //多个锚点 [源锚点，目标锚点].

                    //如下参数 通常使用默认值
                    // Container: null,											                        //连线的容器
                    // DoNotThrowErrors: false,									                //是否抛出错误
                    // LogEnabled: false,										                        //是否启用日志
                    // ReattachConnections: false,								                //端点是否可以再次重新连接		
                    // RenderMode: 'svg',										                    //渲染模式，默认是svg
                    // Scope: 'jsPlumb_DefaultScope'							                //作用域，用来区分哪些端点可以连接，作用域相同的可以连接

                    // Overlays: [												                        //连接线和锚点的遮罩层样式(在锚点上显示标签等属性)
                    // 	['Label', { label: '锚点', location: [-0.5, -0.5] }]
                    // ],		

                }
            },
            //初始化jsPlumb库
            jsPlumbInit: function () {
                var _this = this
                jsPlumb.ready(function () {
                    //初始jsPlumb实例对象
                    _this.jsPlumb = jsPlumb.getInstance({
                        Container: 'sysDesignerContainer',		//设置容器
                    });
                    //设置设计器默认参数
                    _this.jsPlumb.importDefaults(_this.jsPlumbDefault());
                    //注册连接线事件
                    _this.registerConnectionEvent(_this.jsPlumb)
                });
            },
            //新增节点
            doAddEndpoint: function (endpointType) {
                var _this = this
                var endpointClass= this.setEndpointClass(endpointType)
                //创建节点对象到DOM
                //jsPlumbUtil 是jspulmb自带的工具类库,包含了常用函数.例如生成uid = Guid
                var uid = jsPlumbUtil.uuid();
                this.endpointList.push({
                    uid: uid,                                                                      //节点ID
                    endpointText: endpointClass.endpointText,                //节点标题 
                    endpointType: endpointType,                                     //节点类型 S=开始, E=结束, P=流程
                    isShow: true,                                                              //显示状态
                    style: { top: '11px', left: '11px' },                                  //设置初始坐标
                    endpointClass: endpointClass.endpointClass,              //设置节点样式(起点, 终点, 普通节点)
                    labelClass: endpointClass.endpointLabelClass             //设置节点文本样式(起点, 终点, 普通节点)
                });
                //
                this.$nextTick(function () {
                    //设置节点可拖动
                    _this.jsPlumb.draggable(
						[uid],
						{
						    filterExclude: true,
						    filter: '.item-endpoint-content',		//设置点击该区域可以拖动,该区域外是连线
						    opacity: 0.1,                                  //透明性
						    containment: true,	                    //拖动范围在父容器中
						    grid: [10, 10],                                //每次拖动X, Y 坐标值便于对齐 
						    // clone:true
						}
					);
                    if (endpointType == 'S' || endpointType == 'P') {
                        _this.setStartEndpoint(uid);
                    }
                    if (endpointType == 'E' || endpointType == 'P') {
                        _this.setEndEndpoint(uid);
                    }
                });
            },
            //设置锚点源
            setStartEndpoint: function (uid) {
                //设置源
                this.jsPlumb.makeSource(uid, {
                    uuid: uid,
                    allowLoopback: false,				//是否允许回环连接
                    filterExclude: false,				    //筛选执行规则 true 筛选为包含, false 筛选为不包含 设置创建拖放连接的筛选方式
                    filter: '.item-endpoint-content',	//筛选需要排除的元素 格式 class='.name', id='#name' , 元素标签例如 button
                    anchor: 'Continuous',				//连接方式 Continuous 连续
                });
            },
            //设置锚点目标
            setEndEndpoint: function (uid) {
                //设置目标,该方法可返回 endpoint 对象用于绑定事件
                this.jsPlumb.makeTarget(uid, {
                    uuid: uid,
                    allowLoopback: false,				//是否允许回环连接
                    uniqueEndpoint: false,				//采用(Continuous)连续连接时每次创建的连接点是否覆盖掉,还是分开显示
                    anchor: 'Continuous',               //连接方式 Continuous 连续
                });
            },
            setEndpointClass: function (endpointType) { 
                var endpointText = '未知';
                var endpointClass = [];
                var endpointLabelClass = [];
                if (endpointType == 'S') {
                    endpointText = '开始';
                    endpointClass = ['item-endpoint-start'];
                    endpointLabelClass = ['item-endpoint-dot-label'];
                } else if (endpointType == 'E') {
                    endpointText = '结束';
                    endpointClass = ['item-endpoint-end'];
                    endpointLabelClass = ['item-endpoint-dot-label'];
                } else if (endpointType == 'P') {
                    endpointText = '流程';
                }
                return { endpointText: endpointText, endpointClass: endpointClass, endpointLabelClass: endpointLabelClass };
            },
            //获取设置的流程图JSON数据对象
            doSaveFlow: function () {
                //获取所有节点对象的el	
                var elements = this.jsPlumb.getManagedElements()
                var elementsData = [];
                for (var key in elements) {
                    var item = elements[key];
                    elementsData.push({
                        uid: item.el.id,
                        endpointType: item.el.attributes.endpointType.value,
                        endpointText: item.el.innerText,
                        offsetTop: item.el.offsetTop,
                        offsetLeft: item.el.offsetLeft,
                    });
                }
                //获取所有连接线
                var conn = this.jsPlumb.getConnections()
                var connectionsData = [];
                conn.forEach(function (item, index) {
                    connectionsData.push({
                        sourceId: item.sourceId,
                        targetId: item.targetId
                    });
                });
                //返回节点对象
                var result = {
                    endpointList: elementsData,
                    connectionList: connectionsData
                };
                this.resultData = JSON.stringify(result);
            },
            //加载数据
            doLoadFlow: function () {
                var _this = this
                this.doClearFlow();
                var resultData = JSON.parse(_this.resultData);
                //此处采用嵌套获取最新Dom的方式. 后续建议优化代码
                _this.$nextTick(function () {
                    //首先设置页面节点element
                    resultData.endpointList.forEach(function (item, index) {
                        //
                        var endpointClass = _this.setEndpointClass(item.endpointType)
                        //
                        _this.endpointList.push({
                            uid: item.uid,                                                                                   //节点ID
                            endpointText: item.endpointText,                                                    //节点标题 
                            endpointType: item.endpointType,                                                   //节点类型 S=开始, E=结束, P=流程
                            isShow: true,                                                                                   //显示状态
                            style: { top: item.offsetTop + 'px', left: item.offsetLeft + 'px' },         //设置初始坐标
                            endpointClass: endpointClass.endpointClass,                                   //设置节点样式(起点, 终点, 普通节点)
                            labelClass: endpointClass.endpointLabelClass                                  //设置节点文本样式(起点, 终点, 普通节点)
                        });
                    });
                    //页面渲染完成后加载设置                                                                                         
                    _this.$nextTick(function () {
                        //暂停绘图初始化。
                        _this.jsPlumb.batch(function () {
                            //设置节点可拖动
                            resultData.endpointList.forEach(function (item, index) {
                                _this.jsPlumb.draggable(
                                    [item.uid],
                                    {
                                        filterExclude: true,					        //开始筛选,筛选条件在 filter 设置
                                        filter: '.item-endpoint-content',		//设置点击该区域可以拖动,该区域外是连线
                                        opacity: 0.1,                                 //透明性
                                        containment: true,	                    //拖动范围在父容器中
                                        grid: [10, 10],
                                    }
                                );
                            });
                            //设置节点
                            resultData.endpointList.forEach(function (item, index) {
                                if (item.endpointType == 'S' || item.endpointType == 'P') {
                                    _this.setStartEndpoint(item.uid);
                                }
                                if (item.endpointType == 'E' || item.endpointType == 'P') {
                                    _this.setEndEndpoint(item.uid);
                                }
                            });
                            //设置连线
                            resultData.connectionList.forEach(function (item, index) {
                                _this.jsPlumb.connect({ source: item.sourceId, target: item.targetId });
                            });
                        });
                    });
                });

            },
            //清除数据
            doClearFlow: function () {
                //this.jsPlumb.reset(false);                       //重置 清除所有节点 链线 事件
                this.jsPlumb.deleteEveryEndpoint();         //重置 清除所有节点 链线 
                //_this.jsPlumb.clear();
                //_this.jsPlumb.cleanupListeners();
                this.endpointList = [];
            },
            //注册锚点的所有事件 通过创建的锚点进行注册
            registerEndpointEvent: function (endpoint) {
                endpoint.bind('click', function (endpoint) {
                    console.log('锚点单击事件');
                });
                endpoint.bind('dblclick', function (endpoint) {
                    console.log('锚点双击事件');
                });
                endpoint.bind('contextmenu', function (endpoint) {
                    console.log('锚点点击右键事件');
                });
                endpoint.bind('mouseover', function (endpoint) {
                    console.log('锚点鼠标进入事件');
                });
                endpoint.bind('mousedown', function (endpoint) {
                    console.log('锚点按键点击事件');
                });
                endpoint.bind('mouseup', function (endpoint) {
                    console.log('锚点按键释放事件');
                });
                endpoint.bind('maxConnections', function (endpoint) {
                    console.log('锚点连接线条数最大限制后出发(该事件需注册到目标锚点)');
                });
            },
            //注册连接线的所有事件
            registerConnectionEvent: function (currentJsPlumb) {
                //注册开始连接前事件()
                currentJsPlumb.bind('beforeDrop', this.doConnBeforeDrop)
                //注册连线双击事件
                currentJsPlumb.bind('dblclick', this.doDblclickConnEvent);

                /**以下未使用事件**/
                //注册连接线连接事件
                currentJsPlumb.bind('connection', function (connection) { });
                //注册连线单击事件 
                currentJsPlumb.bind('click', function (connection) { });
                //注册拖动连接线事件
                currentJsPlumb.bind('connectionDrag', function (connection) { });
                //注册拖动连接线停止后事件
                currentJsPlumb.bind('connectionDragStop', function (connection) { });
                //注册连接线移除&断开事件
                currentJsPlumb.bind('connectionDetached', function (connection) { });
                //注册锚点单击事件
                currentJsPlumb.bind('endpointClick', function (connection) { });
                //注册锚点双击事件
                currentJsPlumb.bind('endpointDblClick', function (connection) { });
                //注册鼠标右击事件(锚点、连线、连线箭头) 
                currentJsPlumb.bind('contextmenu', function (connection) { });
                //注册画布大小变化事件
                currentJsPlumb.bind('zoom', function (connection) { });
                //注册节点连线被分离事件(通过事件 this.jsPlumb.deleteConnectionsForElement(uuid) 或 通过反向拖拽移除连接线时激活),返回 false 可阻止删除
                currentJsPlumb.bind('beforeDetach', function (connection) { });
                //注册连接失败事件
                currentJsPlumb.bind('connectionAborted', function (connection) { });
            },
            //连接线的双击事件
            doDblclickConnEvent: function (conn, originalEvent) {
                if (confirm('确定删除?')) {
                    this.jsPlumb.deleteConnection(conn, { doNotFireEvent: true, force: true });
                }
            },
            //连接线开始拖动时候(拦截器)
            doConnBeforeDrop: function (connection) {
                //console.log(connection);
                //获取所有已经存在的连接
                var conns = this.jsPlumb.getConnections();
                //验证不允许出现两节点重复连线
                var isExist = conns.some(function (item, index) {
                    if ((item.sourceId == connection.sourceId && item.targetId == connection.targetId) ||
						(item.sourceId == connection.targetId && item.targetId == connection.sourceId)) {
                        return true;
                    }
                    return false;
                });
                return !isExist;
            },

        },

    });
