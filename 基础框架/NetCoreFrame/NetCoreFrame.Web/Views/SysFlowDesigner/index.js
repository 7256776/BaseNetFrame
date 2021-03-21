var component = Vue.component('sys-flowdesigner',
{
	template: Vue.frameTemplate('SysFlowDesigner/Index'),
	created: function () {
		var _this = this;
		//this.$nextTick(() => { })
	},
	mounted: function () {
		//初始设计器容器
		this.jsPlumbInit();
		//初始工具栏
		this.jsPlumbToolsInit();
		//禁用页面默认右键事件
		//document.oncontextmenu = function () { return false };

	},
	data: function () {
		return {
			jsPlumbToll: null,					 //适用于工具栏的jsPlumb 实例
			jsPlumbBase: null,				 //适用于设计器的jsPlumb 实例
			selectedRectangle: {
				isShow: false,
				style: {}
			},
			currentEndpoint: {},				//当前选择中的节点对象,该对象与节点对象集合同步
			currentMouse: {					//多选矩形框中间数据
				pageX: '',
				pageY: '',
				offsetX:'',
				offsetY: '',
			},
			endpointList: [],					//页面节点对象
			baseConfig: {
				// isSource: true, 			// 是否可以拖动（作为连线起点）
				// isTarget: true, 			// 是否可以放置（连线终点）
				maxConnections: -1, 		// 端点最大连接数量(数字) -1 = 无限
				//连接线类型 样式种类有[Bezier],[Flowchart],[StateMachine ],[Straight ] 
				connector:
					//['Straight'],
					//['StateMachine'],		//贝塞尔曲线
					['Flowchart', { stub: 0, gap: 0, cornerRadius: 5, alwaysRespectStubs: false }],//具有90度转折点的流程线
				//连接线属性
				connectorOverlays: [
					// ['PlainArrow'],
					['Arrow', {
						width: 10,				//箭头宽度
						length: 10,				//箭头长度
						location: 0.33,			//箭头具体终点距离
						stroke: 'green',		//拖拽线颜色
						//foldback: 0.5,		//箭头的流体大小			
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
					 //['Label', {
					 //	label: '线名称',	//可以是html
					 //	location: 0.5,	   //文字具体终点距离(默认50%的位置)

					 //	labelStyle: {
					 //		color: '#ffffff',
					 //		fill: 'green',
					 //		padding: '5px',
					 //	},
					 //	events: {
					 //		click: function (labelOverlay, originalEvent) {
					 //			console.log('连接线的lable点击事件');
					 //		},	//点击label事件
					 //		//tap: this.doClickConnectorLabel,	//该事件似乎与click一致
					 //	}
					 //}],
					// 添加菱形节点 
					 //['Diamond', {
					 //	width: 10,				//箭头宽度
					 //	length: 10,				//箭头长度
					 //	location: 0.5,			//箭头具体终点距离
					 //	stroke: 'green',		//拖拽线颜色
					 //	events: {
					 //		dblclick: function (diamondOverlay, originalEvent) {
					 //			console.log('双击钻石叠加 : ' + diamondOverlay.component);
					 //		}
					 //	}
					 //}]
				],
				//整体链接线风格
				paintStyle: {
					// fill: '#CCCCCC',			//填充颜色(根据连线之间产生色块填充)
					// radius: 11,			//锚点半径
					stroke: 'green',		//线条颜色
					strokeWidth: 0.5,		//线条宽度
					 //joinstyle: 'round',
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
				//锚点的样式
				endpointStyle: {
					fill: 'green', 				//端点填充颜色	透明=transparent
					radius: 3,					//圆形锚点半径(适用于Dot)
					stroke: 'green',			//线条颜色(适用于Rectangle)
					strokeWidth: 2,		//线条宽度					
					width: 10,					//(适用于Rectangle)
					height: 10,				//(适用于Rectangle)
					// outlineStroke: 'red',//轮廓颜色
					// outlineWidth: 5,		//轮廓宽度
				},
				//锚点悬停样式
				endpointHoverStyle: {
					fill: 'red', 			//端点填充颜色	透明=transparent
					radius: 4,				//圆形锚点半径(适用于Dot)
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
				//鼠标悬停在连接线上的样式	
				connectorHoverStyle: {
					strokeWidth: 1,
					lineWidth: 1,
					outlineStroke: 'red',
					outlineWidth: 1,
					fill: ''
					// outlineColor: ''
				},
			},

			workFlowData: {					//流程数据JSON对象
				id: '',
				workFlowName: '',
				workFlowEndpointList: [],
				workFlowConnectionList: []
			},
			tableOptions: {
				tabDialog: false,
				tableData: []
			},
			isFlowPage: true,
			isSettingPage: false,
		}
	},
	watch: {
		//监听
	},
	computed: {
		//计算
	},
	methods: {
		doStyle: function () {
			//ToDo 测试 设置流程图背景颜色
			this.endpointList.forEach(function (item,index) {
				Vue.set(item.style, 'background', 'rgba(203, 201, 99, 0.5)')
			});
		},
		//初始工具栏jsPlumb库
		jsPlumbToolsInit: function () {
			var _this = this
			//初始化拖拽对象
			var katavorioBase = new Katavorio({
				getPosition: function (el) {
					//获取拖拽坐标
					return [el.offsetLeft, el.offsetTop];
				},
				setPosition: function (el, p) {
					//设置拖拽后坐标
					el.style.left = p[0] + "px";
					el.style.top = p[1] + "px";
				},
				getSize: function (el) {
					//拖拽后两次触发 分别获取拖拽组件Size 与 所在容器Size
					// console.log('Height' + el.offsetHeight+' Width'+ el.offsetWidth);
					return [el.offsetWidth, el.offsetHeight];
				},
				addClass: function (el, c) {
					//推拽开始添加样式事件
				},
				removeClass: function (el, c) {
					//推拽是否移除样式事件				
				},
				bind: function (obj, type, fn) {
					//初始绑定拖拽组件的事件(mousedown, mousemove, mouseup)
					// console.log("bind")
					if (obj.addEventListener)
						obj.addEventListener(type, fn, false);
					else if (obj.attachEvent) {
						obj["e" + type + fn] = fn;
						obj[type + fn] = function () {
							obj["e" + type + fn](window.event);
						};
						obj.attachEvent("on" + type, obj[type + fn]);
					}
				},
				unbind: function (obj, type, fn) {
					//拖拽完成后释放注册的事件(mousemove, mouseup)
					// console.log("unbind")
					if (obj.removeEventListener)
						obj.removeEventListener(type, fn, false);
					else if (obj.detachEvent) {
						obj.detachEvent("on" + type, obj[type + fn]);
						obj[type + fn] = null;
						obj["e" + type + fn] = null;
					}
				},
				fireEvent: function () {
					//console.log(arguments);
				},
				intersects: function (r1, r2) {
					//验证推拽的对象是否已经进入目标容器
					// console.log('intersects r1=' + r1 + ' r2=' + r2);
					//此处不做验证返回true会动态触发事件
					var x1 = r1.x, x2 = r1.x + r1.w, y1 = r1.y, y2 = r1.y + r1.h,
						a1 = r2.x, a2 = r2.x + r2.w, b1 = r2.y, b2 = r2.y + r2.h;
					//包含在设计器容器中返回true
					return (x1 > a1 && x1 < a2 && y1 > b1 && y2 < b2)
				}

			});
			//注册 designerToolsDrag 容器内的组件可以被拖动
			katavorioBase.draggable(designerToolsDrag.querySelectorAll(".item-endpoint-tool"), {
				clone: true,
				parent: designerToolsDrag,

				start: function (event) {
					// console.log('start')
					// event.e		//事件对象
					// event.el		//拖拽对象
					// event.drag	//当前拖拽的对象
					//设置工具栏拖拽到容器开始时的样式
					var dragEl = event.drag.getDragElement();
					var endpointType = event.el.attributes.endpointType.value;
					dragEl.style.opacity = 0.8;
					dragEl.className = dragEl.className + " item-endpoint";
					if (endpointType == 'S') {
						dragEl.className = dragEl.className + " item-endpoint-start";
					} else if (endpointType == 'E') {
						dragEl.className = dragEl.className + " item-endpoint item-endpoint-end";
					}
				},
				stop: function (event) {
					// console.log('stop')
				},
				beforeStart: function (event) {
					//console.log('beforeStart')
				},
			});
			//目标容器
			katavorioBase.droppable(designerBody, {
				// clone: true,
				// parent: designerToolsDrag,
				
				//Katavorio注入了事件intersects并通过该事件进行验证返回true后才会触发该事件
				drop: function (event) {
					//拖拽对象放置在容器对象上触发事件
					// console.log('designerContainer-drop')
					var dragEl = event.drag.getDragElement()
					var endpointType = dragEl.attributes.endpointType.value;
					//此处positionX=205是设计器浮动距离
					var positionX = event.drop.pagePosition[0];
					//
					var offsetLeft = dragEl.offsetLeft -  positionX + event.drop.el.scrollLeft;
					var offsetTop = dragEl.offsetTop + event.drop.el.scrollTop; 
					_this.doAddEndpoint(endpointType, offsetLeft, offsetTop);
				},
				over: function (event) {
					//拖拽对象进入容器对象上触发事件
					// console.log('designerContainer-over')
				},
				out: function (event) {
					//拖拽对象鼠标在容器对象上释放触发事件
					// console.log('designerContainer-out')
				},

			});
		
		},
		//获取jsPlumb库默认设置	
		jsPlumbDefault: function () {
			return {
				MaxConnections: this.baseConfig.maxConnections,				//锚点最大连接数量(数字) -1 = 无限 
				ConnectionsDetachable: false,								//是否允许拖动断开连线
				ConnectionOverlays: this.baseConfig.connectorOverlays,		//连接线属性
				Connector: this.baseConfig.connector,						//连接线类型
				PaintStyle: this.baseConfig.paintStyle,						//主体连线风格样式
				HoverPaintStyle: this.baseConfig.hoverPaintStyle,			//主体连线悬停风格样式
				Endpoint: this.baseConfig.endpoint,							//锚点类型
				EndpointStyle: this.baseConfig.endpointStyle,				//锚点样式
				EndpointHoverStyle: this.baseConfig.endpointHoverStyle,		//锚点悬停样式

				LabelStyle: {												//标签样式
					color: 'red'
				},

				//如下参数 通常根据风格在做设置
				// DragOptions: {},											//拖动设置
				// DropOptions: { tolerance: 'touch',},						//拖放设置

				// EndpointOverlays: [],									//锚点遮罩层
				// Endpoints: [null, null],									//数组形式的，[源端点，目标端点]
				// EndpointStyles: [null, null],							//[源端点样式，目标端点样式]
				// EndpointHoverStyles: [null, null],						//[源端点鼠标经过样式，目标端点鼠标经过样式]

				//如下参数 通常单独设置
				// Anchor: 'Bottom',										//锚点，即端点连接的位置
				// Anchors: [null, null],									//多个锚点 [源锚点，目标锚点].

				//如下参数 通常使用默认值
				// Container: null,											//连线的容器
				// DoNotThrowErrors: false,									//是否抛出错误
				// LogEnabled: false,										//是否启用日志
				// ReattachConnections: false,								//端点是否可以再次重新连接		
				// RenderMode: 'svg',										//渲染模式，默认是svg
				// Scope: 'jsPlumb_DefaultScope'							//作用域，用来区分哪些端点可以连接，作用域相同的可以连接


				// Overlays: [												//连接线和锚点的遮罩层样式(在锚点上显示标签等属性)
				// 	['Label', { label: '锚点', location: [-0.5, -0.5] }]
				// ],		

			}
		},
		//初始设计器容器jsPlumb库
		jsPlumbInit: function () {
			var _this = this
			jsPlumb.ready(function () {
				//初始jsPlumb实例对象
				_this.jsPlumbBase = jsPlumb.getInstance(
					{
						Container: 'designerContainer',		//设置容器
					}
				);
				//设置设计器默认参数
				_this.jsPlumbBase.importDefaults(_this.jsPlumbDefault());
				//注册设计器全局事件
				_this.registerConnectionEvent(_this.jsPlumbBase)
			});

		},
		//移除设计器中的鼠标右键事件
		doDesignerContextmenu: function (event) {
			//不要执行与事件关联的默认动作(该注册事件的容器不出现默认右键菜单)
			if (event.preventDefault) {
				event.preventDefault();
			} else {
				event.returnValue = false;
            }
		},
		//容器内鼠标拖拽设置选择窗体矩形			
		doDesignerMousedown: function (event) {
			//点击鼠标右键默认取消所有选中状态
			if (event.button==2) {
				this.clearEndpointSelectState();
				return;
            }
			//按下鼠标初始化
			//获取当前鼠标按下时,目标元素相对于视窗的位置集合
			var rect = event.currentTarget.getBoundingClientRect();
			//计算获取鼠标在该容器的坐标
			this.currentMouse.offsetX = event.clientX - rect.left;
			this.currentMouse.offsetY = event.clientY - rect.top;
			//
			this.selectedRectangle.isShow = false;
		},
		doDesignerMouseup: function (event) {
			var _this = this;
			//释放鼠标初始化
			this.currentMouse.offsetX = '';
			this.currentMouse.offsetY = '';
			this.selectedRectangle.isShow = false;

			//清空jsPlumb对象中所设置的选择节点
			this.jsPlumbBase.clearDragSelection();
			//矩形框完成选择后记录所有被选择的节点到jsPlum对象中(此时当拖拽其中一个节点将同步更新链接线以及节点的坐标)
			this.endpointList.forEach(function (item, index) {
				if (item.isSelect)
					_this.jsPlumbBase.addToDragSelection(item.uid);
			});
		},
		doDesignerMousemove: function (event) {
			var _this = this;
			// 未按下鼠标时结束方法
			if (this.currentMouse.offsetX == '' || this.currentMouse.offsetY == '') {
				return;
			}
			////获取设计器element的相对位置
			//var designerContainerEl = this.$refs["designerBody"];
			//// 滚动条的位置
			//var scrollX = designerContainerEl.scrollLeft;
			//var scrollY = designerContainerEl.scrollTop;

			//获取当前鼠标移动时,目标元素相对于视窗的位置集合
			var rect = event.currentTarget.getBoundingClientRect();
			var pxx = event.clientX - rect.left;
			var pyy = event.clientY - rect.top;

			var px = this.currentMouse.offsetX;
			var py = this.currentMouse.offsetY;
			// 移动一次获取一次矩形宽高
			var h = pyy - py;
			var w = pxx - px;

			// 创建矩形div，只创建一次
			if (!this.selectedRectangle.isShow) {
				this.selectedRectangle.isShow = true;

			}
			// 获取选择框element的相对位置
			var selectedRectangleEl = this.$refs["selectedRectangle"];
			//
			this.selectedRectangle.style = {};
			// 画出矩形
			if (h < 0 && w >= 0) {
				this.selectedRectangle.style["height"] = (-h) + "px";
				this.selectedRectangle.style["width"] = w + "px";
				this.selectedRectangle.style["left"] = px + "px";
				this.selectedRectangle.style["top"] = pyy + "px";
			}
			else if (h >= 0 && w < 0) {
				this.selectedRectangle.style["height"] = h + "px";
				this.selectedRectangle.style["width"] = (-w) + "px";
				this.selectedRectangle.style["left"] = pxx + "px";
				this.selectedRectangle.style["top"] = py - "px";
			}
			else if (h < 0 && w < 0) {
				this.selectedRectangle.style["height"] = (-h) + "px";
				this.selectedRectangle.style["width"] = (-w) + "px";
				this.selectedRectangle.style["left"] = pxx + "px";
				this.selectedRectangle.style["top"] = pyy + "px";
			}
			else {
				this.selectedRectangle.style["height"] = h + "px";
				this.selectedRectangle.style["width"] = w + "px";
				this.selectedRectangle.style["left"] = px + "px";
				this.selectedRectangle.style["top"] = py  + "px";
			}

			if (w < 0) {
				w = 0 - w;
			}
			if (h < 0) {
				h = 0 - h;
			}

			//获取矩形四个点的坐标
			var x1 = selectedRectangleEl.offsetLeft, y1 = selectedRectangleEl.offsetTop,//左上
				x2 = x1 + w, y2 = y1,																						//右上
				x3 = x1 + w, y3 = y1 + h,																				//左下
				x4 = x1, y4 = y1 + h;																						//右下

			//console.log("x1=" + x1 + "y1=" + y1 + " x2=" + x2 + "y2=" + y2 + " x3=" + x3 + "y3=" + y3 + " x4=" + x4 + "y4=" + y4);
		
			this.endpointList.forEach(function (item, index) {
				//获取节点四点的坐标	 
				var endpointX1 = parseInt(item.offsetLeft), endpointY1 = parseInt(item.offsetTop),					//左上坐标
					endpointX2 = endpointX1 + item.size[1], endpointY2 = endpointY1,							//右上坐标
					endpointX3 = endpointX1 + item.size[1], endpointY3 = endpointY1 + item.size[0],	//右下坐标
					endpointX4 = endpointX1, endpointY4 = endpointY1 + item.size[0],							//左下坐标
					isSelect = false;//是否选中

				// console.log("x1=" + x1 + "y1=" + y1 + " x2=" + x2 + "y2=" + y2 + " x3=" + x3 + "y3=" + y3 + " x4=" + x4 + "y4=" + y4);
				 
				if ((endpointX1 > x1 && endpointY1 > y1) && (endpointX1 < x2 && endpointY1 > y2) && (endpointX1 < x3 && endpointY1 < y3) && (endpointX1 > x4 && endpointY1 < y4)) {
					// console.log("正向 左上角");
					isSelect = true;
				}
				else if ((endpointX2 > x1 && endpointY2 > y1) && (endpointX2 < x2 && endpointY2 > y2) && (endpointX2 < x3 && endpointY2 < y3) && (endpointX2 > x4 && endpointY2 < y4)) {
					// console.log("正向 右上角");
					isSelect = true;
				}
				else if ((endpointX3 > x1 && endpointY3 > y1) && (endpointX3 < x2 && endpointY3 > y2) && (endpointX3 < x3 && endpointY3 < y3) && (endpointX3 > x4 && endpointY3 < y4)) {
					// console.log("正向 右下角");
					isSelect = true;
				}
				else if ((endpointX4 > x1 && endpointY4 > y1) && (endpointX4 < x2 && endpointY4 > y2) && (endpointX4 < x3 && endpointY4 < y3) && (endpointX4 > x4 && endpointY4 < y4)) {
					// console.log("正向 左下角");
					isSelect = true;
				}
				else if ((x1 > endpointX1 && y1 > endpointY1) && (x1 < endpointX2 && y1 > endpointY2) && (x1 < endpointX3 && y1 < endpointY3) && (x1 > endpointX4 && y1 < endpointY4)) {
					// console.log("下方,右方进入");
					isSelect = true;
				}
				else if ((x2 > endpointX1 && y2 > endpointY1) && (x2 < endpointX2 && y2 > endpointY2) && (x2 < endpointX3 && y2 < endpointY3) && (x2 > endpointX4 && y2 < endpointY4)) {
					// console.log("左边进入");
					isSelect = true;
				}
				else if ((x3 > endpointX1 && y3 > endpointY1) && (x3 < endpointX2 && y3 > endpointY2) && (x3 < endpointX3 && y3 < endpointY3) && (x3 > endpointX4 && y3 < endpointY4)) {
					// console.log("上方进入");
					isSelect = true;
				}
				else if ((x4 > endpointX1 && y4 > endpointY1) && (x4 < endpointX2 && y4 > endpointY2) && (x4 < endpointX3 && y4 < endpointY3) && (x4 > endpointX4 && y4 < endpointY4)) {
					// console.log("未知");
					isSelect = true;
				}
				else if ((x1 > endpointX1 && y1 < endpointY1) && (x2 < endpointX2 && y2 < endpointY2) && (x3 < endpointX3 && y3 > endpointY3) && (x4 > endpointX4 && y4 > endpointY4)) {
					// console.log("中间竖");
					isSelect = true;
				}
				else if ((endpointX1 > x1 && endpointY1 < y1) && (endpointX2 < x2 && endpointY2 < y2) && (endpointX3 < x3 && endpointY3 > y3) && (endpointX4 > x4 && endpointY4 > y4)) {
					// console.log("中间横");
					isSelect = true;
				}
				 
				//设置选择后的样式
				_this.setEndpointSelectState(isSelect, item);
			});
		},
		
		//新增节点
		doAddEndpoint: function (endpointType, offsetLeft, offsetTop) {
			var _this = this
			if (!this.verifyEndpoint(endpointType)) {
				return;
            }
			var endpointText = '未知';
			var endpointClass = [];
			var endpointLabelClass = [];
			//ToDo 组件宽高是通过Class确定的,此处可以扩展到配置信息中
			var size = [];
			if (endpointType == 'S') {
				endpointText = '开始';
				endpointClass = ['item-endpoint-start'];
				endpointLabelClass = ['item-endpoint-dot-label'];
				size = [50, 50];
			} else if (endpointType == 'E') {
				endpointText = '结束';
				endpointClass = ['item-endpoint-end'];
				endpointLabelClass = ['item-endpoint-dot-label'];
				size = [50, 50];
			} else if (endpointType == 'P') {
				endpointText = '流程';
				size = [50, 150];
			}

			var top = 10;
			var left = 10;
			var designerEl = this.$refs["designerContainer"];
			//此处减去的是布局样式中的偏移量
			left = parseInt(offsetLeft) - parseInt(designerEl.offsetLeft);
			top = parseInt(offsetTop) - parseInt(designerEl.offsetTop);
			 
			//创建节点对象到dom
			//jsPlumbUtil 是jspulmb自带的工具类库,包含了常用函数.例如生成uid
			var uid = jsPlumbUtil.uuid();	//uid = Guid
			var endpointObj = {
				uid: uid,
				endpointText: endpointText,						//节点显示文本
				endpointType: endpointType,					//节点类型 S=开始, E=结束, P=流程
				style: { top: top + 'px', left: left + 'px' },		//初始坐标
				size: size,														//组件大小
				offsetLeft: left,												//记录初始坐标
				offsetTop: top,											//记录初始坐标
				endpointClass: endpointClass,					//节点样式
				labelClass: endpointLabelClass,					//节点文本样式
				isShow: true,												//是否显示
				isSelect: false												//是否选择中	
			}
			this.endpointList.push(endpointObj);
			//记录所选择的节点
			this.currentEndpoint = endpointObj;

			//注册拖拽事件
			this.$nextTick(function () {

				//设置节点可拖动
				_this.jsPlumbBase.draggable(
					[uid],
					{
						filterExclude: true,
						filter: '.item-endpoint-content',		//设置点击该区域可以拖动,该区域外是连线
						opacity: 0.1,			//透明度
						containment: true,	//拖动范围在父容器中
						grid: [10, 10],

						//beforeStart: function (event) {
						//	console.log("----beforeStart----")
						//},
						start: function (event) {
							// console.log("----start----");
						},
						//拖拽事件
						drag: _this.endpointDragEvent,
						stop: _this.endpointStopEvent,
					}
				);
				//设置各个流程的锚点属性
				if (endpointType == 'S' || endpointType == 'P') {
					_this.setStartEndpoint(uid);
				}
				if (endpointType == 'E' || endpointType == 'P') {
					_this.setEndEndpoint(uid);
				}
			});

		},
		//删除单个节点
		doDeleteEndEndpoint: function (event) {
			if (confirm('确定要删除吗') == false) {
				return;
			}
			var uid = event.currentTarget.getAttribute("data-id");
			//移除节点相关连接线
			this.jsPlumbBase.deleteConnectionsForElement(uid, { fireEvent: true, forceDetach: false });
			this.jsPlumbBase.remove(uid);

			//ToDo 对vue数据对象删除会触发jsPlumb内部机制导致额外删除其他节点
			this.endpointList.forEach(function (item, index) {
				if (uid == item.uid) {
					item.isShow = false;
					//_this.endpointList.splice(index, 1) 
					return;
				}
			});

		},
		//验证节点是否重复添加
		verifyEndpoint: function (endpointType) {
			if (endpointType == "P") {
				return true;
            }
			var data = this.endpointList.find(function (item, index) {
				if (item.endpointType == endpointType) {
					return true;
				}
			});
			if (data) {
				var msg = endpointType == "S" ? "开始" : "结束";
				this.tipShow('warn', msg + '节点仅允许一个');
				return false;
			}
			return true;
        },
		//容器内节点拖拽事件
		endpointDragEvent: function (event) {
			// console.log("----drag----"); 
			//查询当前拖拽节点
			var currentEndpoint = this.endpointList.find(function (item, index) {
				if (event.el.id == item.uid) {
					return true;
				}
			});

			if (currentEndpoint) {
				//记录所选择的节点
				this.currentEndpoint = currentEndpoint;
				this.currentEndpoint.offsetLeft = event.pos[0];
				this.currentEndpoint.offsetTop = event.pos[1];
				//如:当前推拽节点未被矩形框选中时,清空其他已选中节点 (适用于拖拽单个未被选择的节点)
				if (!currentEndpoint.isSelect) {
					this.clearEndpointSelectState();
				}
			}
		},
		//容器内节点拖拽停止后事件
		endpointStopEvent: function (event) {
			//
			var elements = this.jsPlumbBase.getManagedElements();
			//拖拽完成后刷新每个节点的坐标
			this.endpointList.forEach(function (item, index) {
				item.offsetLeft = elements[item.uid].el.offsetLeft;
				item.offsetTop = elements[item.uid].el.offsetTop;
			});
		},
		//设置节点选中状态以及样式
		setEndpointSelectState: function (isSelect, item) {
			if (isSelect) {
				var currentEndpoint = item.endpointClass.find(function (itemClass, index) { return itemClass == "selected-endpoint"; });
				if (!currentEndpoint) {
					item.endpointClass.push("selected-endpoint");
				}
				item.isSelect = true;
			} else {
				item.endpointClass = item.endpointClass.filter(function (itemClass, index) { return itemClass != "selected-endpoint"; })
				item.isSelect = false;
			}
		},
		//清空所有节点选中状态以及样式
		clearEndpointSelectState: function () {
			var _this = this;
			this.endpointList.forEach(function (item, index) {
				_this.setEndpointSelectState(false, item);
			});
			//清空jsPlumb对象中所设置的选择节点
			this.jsPlumbBase.clearDragSelection();
		},
		//节点点击事件
		doClickEndpoint: function (endpoint) {
			this.currentEndpoint = endpoint;
		},

		//设置锚点源
		setStartEndpoint: function (uid) {
			//返回锚点对象可以通过该锚点进行注册事件
			var endpoint = this.jsPlumbBase.makeSource(uid, {
				uuid: uid,
				allowLoopback: false,				//是否允许回环连接
				filterExclude: false,				//筛选执行规则 true 筛选为包含, false 筛选为不包含
				filter: '.item-endpoint-content',	//筛选需要排除的元素 格式 class='.name', id='#name' , 元素标签例如 button
				anchor: 'Continuous',				//连接方式 Continuous 连续
			});
		},
		//设置锚点目标
		setEndEndpoint: function (uid) {
			//设置目标,该方法可返回 endpoint 对象用于绑定事件
			var endpoint = this.jsPlumbBase.makeTarget(uid, {
				uuid: uid,
				allowLoopback: false,				//是否允许回环连接
				uniqueEndpoint: false,				//采用(Continuous)连续连接时每次创建的连接点是否覆盖掉,还是分开显示
				anchor: 'Continuous',
			});
		},
		//注册制定锚点的所有事件 通过创建的锚点进行注册
		registerEndpointEvent: function (endpoint) {
			endpoint.bind('click', function (endpoint) {
				// console.log('锚点:单击事件');
			});
			endpoint.bind('dblclick', function (endpoint) {
				// console.log('锚点:双击事件');
			});
			endpoint.bind('contextmenu', function (endpoint) {
				// console.log('锚点:点击右键事件');
			});
			endpoint.bind('mouseover', function (endpoint) {
				// console.log('锚点:鼠标进入事件');
			});
			endpoint.bind('mousedown', function (endpoint) {
				// console.log('锚点:按键点击事件');
			});
			endpoint.bind('mouseup', function (endpoint) {
				// console.log('锚点:按键释放事件');
			});
			endpoint.bind('maxConnections', function (endpoint) {
				// console.log('锚点:连接线条数最大限制后出发(该事件需注册到目标锚点)');
			});
		},
		//注册连接线的所有事件
		registerConnectionEvent: function (currentJsPlumb) {
			//注册开始连接前事件()
			currentJsPlumb.bind('beforeDrop', this.doConnBeforeDropEvent)
			//注册连线双击事件
			currentJsPlumb.bind('dblclick', this.doDblclickConnEvent);
			//注册连接线连接事件
			currentJsPlumb.bind('connection', function (connection) {
				// console.log('连接线:连接事件');
			});
			//注册连线单击事件 
			currentJsPlumb.bind('click', function (connection) {
				// console.log('连接线:的单击事件');
			});
			//注册拖动连接线事件
			currentJsPlumb.bind('connectionDrag', function (connection) {
				// console.log('连接线:拖拽开始');
			});
			//注册拖动连接线停止后事件
			currentJsPlumb.bind('connectionDragStop', function (connection) {
				// console.log('连接线:拖拽结束');
			});
			//注册连接线移除&断开事件
			currentJsPlumb.bind('connectionDetached', function (connection) {
				// console.log('连接线:移除&断开事件');
			});
			//注册锚点单击事件
			currentJsPlumb.bind('endpointClick', function (connection) {
				// console.log('锚点:锚点单击事件');
			});
			//注册锚点双击事件
			currentJsPlumb.bind('endpointDblClick', function (connection) {
				// console.log('锚点:锚点双击事件');
			});
			//注册鼠标右击事件(锚点、连线、连线箭头) 
			currentJsPlumb.bind('contextmenu', function (connection) {
				// console.log('注册鼠标右击事件(锚点、连线、连线箭头) ');
			});
			//注册画布大小变化事件
			currentJsPlumb.bind('zoom', function (connection) {
				console.log('画布大小变化事件');
			});
			//注册节点连线被分离事件(通过事件 this.jsPlumb.deleteConnectionsForElement(uuid) 或 通过反向拖拽移除连接线时激活)
			currentJsPlumb.bind('beforeDetach', function (connection) {
				// console.log('节点连线被分离事件');
				//返回 false 可阻止删除
				return true;
			});
			//注册连接失败事件
			currentJsPlumb.bind('connectionAborted', function (connection) {
				// console.log('连接失败事件');
			});

		},
		//连接线的双击事件
		doDblclickConnEvent: function (conn, originalEvent) {
			if (confirm('确定删除?')) {
				this.jsPlumbBase.deleteConnection(conn, { doNotFireEvent: true, force: true });
			}
		},
		//连接线开始拖动时候(拦截器)
		doConnBeforeDropEvent: function (connection) {
			//console.log(connection);
			//获取所有已经存在的连接
			var conns = this.jsPlumbBase.getConnections();
			//验证不允许出现两节点重复连线
			var isExist = conns.some(function (item, index) {
				if (
					(item.sourceId == connection.sourceId && item.targetId == connection.targetId) ||
					(item.sourceId == connection.targetId && item.targetId == connection.sourceId)
				) {
					return true;
				}
				return false;
			});
			return !isExist;
		},
		//加载数据 ToDo 可针对新增节点进行代码优化
		doLoadFlow: function () {
			var _this = this
			var resultData = _this.workFlowData;
			//载入前先清空所有相关流程设计的数据
			this.clearFlow();
			this.$nextTick(function () {
				//重新设置获取的流程数据
				_this.workFlowData = resultData;
				//首先设置页面节点element
				resultData.workFlowEndpointList.forEach(function (item, index) {
					//节点变量
					var endpointClass = [];
					var endpointLabelClass = [];
					//ToDo 组件宽高是通过Class确定的,此处可以扩展到配置信息中
					var size = [];
					//根据节点类型初始化节点
					if (item.endpointType == 'S') {
						endpointClass = ['item-endpoint-start'];
						endpointLabelClass = ['item-endpoint-dot-label'];
						size = [50, 50];
					} else if (item.endpointType == 'E') {
						endpointClass = ['item-endpoint-end'];
						endpointLabelClass = ['item-endpoint-dot-label'];
						size = [50, 50];
					} else if (item.endpointType == 'P') {
						size = [50, 150];
					}
					//
					_this.endpointList.push({
						uid: item.uid,
						endpointText: item.endpointText,										//节点显示文本
						endpointType: item.endpointType,										//节点类型 S=开始, E=结束, P=流程
						style: { top: item.offsetTop + 'px', left: item.offsetLeft + 'px' },	//初始坐标
						size: size,																//组件大小
						offsetLeft: item.offsetLeft,
						offsetTop: item.offsetTop,
						endpointClass: endpointClass,											//节点样式
						labelClass: endpointLabelClass,											//节点文本样式
						isShow: true,															//是否显示
						isSelect: false															//是否选择中	
					});
				});

				//页面渲染完成后加载设置
				this.$nextTick(function () {
					var uids = [];
					//暂停绘图进行初始化。
					_this.jsPlumbBase.batch(function () {
						//设置节点
						resultData.workFlowEndpointList.forEach(function (item, index) {
							uids.push(item.uid);
							if (item.endpointType == 'S' || item.endpointType == 'P') {
								_this.setStartEndpoint(item.uid);
							}
							if (item.endpointType == 'E' || item.endpointType == 'P') {
								_this.setEndEndpoint(item.uid);
							}
						});
						//设置节点可拖动
						_this.jsPlumbBase.draggable(
							uids,
							{
								filterExclude: true,
								filter: '.item-endpoint-content',	//设置点击该区域可以拖动,该区域外是连线
								opacity: 0.1,						//透明度
								containment: true,					//拖动范围在父容器中
								grid: [10, 10],						//拖动单位像素距离(用于对齐)
								drag: _this.endpointDragEvent,		//拖拽事件
								stop: _this.endpointStopEvent,
							}
						);
						//设置连线
						resultData.workFlowConnectionList.forEach(function (item, index) {
							_this.jsPlumbBase.connect({ source: item.sourceId, target: item.targetId });
						});
					});
				});
			});

		},
		//获取设置的流程图JSON数据对象
		getFlowData: function () {
			//获取所有节点对象的el	
			var elements = this.jsPlumbBase.getManagedElements()
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
			var conns = this.jsPlumbBase.getConnections()
			var connectionsData = [];
			conns.forEach(function (item, index) {
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
			return result
		},
		// 获取流程列表
		getWorkFlowList: function () {
			var _this = this;
			this.tableOptions.tabDialog = true;
			//
			this.$nextTick(function () {

				abp.ajax({
					url: '/SysFlowDesigner/GetWorkFlowSettingList',
					type: 'POST'
				}).done(function (data, res, e) {
					_this.tableOptions.tableData = data;
				});

			});
		},
		//获取流程对象
		getWorkFlow: function (row) {
			var _this = this;
			this.tableOptions.tabDialog = false;
			//
				abp.ajax({
					url: '/SysFlowDesigner/GetWorkFlowSetting',
					data: JSON.stringify(row.id),
					type: 'POST'
				}).done(function (data, res, e) {
					_this.workFlowData = data;
					_this.doLoadFlow();
				});
		},
		//删除流程
		doDeleteWorkFlow: function (row) {
			var _this = this;

			this.$confirm('确定删除所选择的流程设计?', '提示', {
				//confirmButtonText: '确定',
				//cancelButtonText: '取消',
				type: 'warning'
			}).then(function () {
				//
				abp.ajax({
					url: '/SysFlowDesigner/DeleteWorkFlowSetting',
					data: JSON.stringify(row.id),
					type: 'POST'
				}).done(function (data, res, e) {
					_this.tipSuccess('del');
					if (_this.workFlowData.id == row.id) {
						_this.workFlowData.id = '';
					}
					_this.getWorkFlowList();
				});
			}).catch(function (action) {
			    //取消操作必须有避免js链式调用报异常
			});
		},
		//保存配置数据
		doSaveFlow: function () {
			//
			if (!this.workFlowData.workFlowName) {
				this.tipShow('warn', '请输入流程名称');
				this.$refs["workFlowName"].focus()
				return;
			}
			//
			var flowData = this.getFlowData();
			this.workFlowData.workFlowEndpointList = flowData.endpointList;
			this.workFlowData.workFlowConnectionList = flowData.connectionList;
			//
			var _this = this;
			var url = "/SysFlowDesigner/InserWorkFlowSetting";
			if (this.workFlowData.id) {
				url = "/SysFlowDesigner/UpdataWorkFlowSetting";
            }

			abp.ajax({
				url: url,
				data: JSON.stringify(_this.workFlowData),
				type: 'POST'
			}).done(function (data, res, e) {
				//保存成功获取返回id
				_this.workFlowData.id = data;
				_this.tipSuccess('save');
			});

		},
		//清空所有流程设计配置数据
		doClearFlow: function () {
			var _this = this;
			this.$confirm('确定清空当前流程设计?', '提示', {
				//confirmButtonText: '确定',
				//cancelButtonText: '取消',
				type: 'warning'
			}).then(function () {
				_this.clearFlow()
			}).catch(function (action) {
			    //取消操作必须有避免js链式调用报异常
			});
		},
		//
		doSettingFlow: function (command) {
			var _this = this;

			if (this.isFlowPage) {
				this.isFlowPage = !this.isFlowPage;
				this.$nextTick(function () {
					setTimeout(function () {
						_this.isSettingPage = !_this.isSettingPage;
					}, 300)
				});
            } else {
				this.isSettingPage = !this.isSettingPage;
				this.$nextTick(function () {
					setTimeout(function () {
						_this.isFlowPage = !_this.isFlowPage;
					}, 300)
				});
            }

		},
		//清除数据
		clearFlow: function () {
			//删除所有锚点(节点),连接线
			this.jsPlumbBase.deleteEveryConnection();
			//删除所有节点
			this.jsPlumbBase.deleteEveryEndpoint();
			//清空所有选择的拖拽节点
			this.jsPlumbBase.clearDragSelection();
			//清空节点对象
			this.endpointList = [];
			//清空当前选中的节点对象
			this.currentEndpoint = {};
			//清空表单数据
			this.workFlowData = {
				id: '',
			};
		},


	}
});
