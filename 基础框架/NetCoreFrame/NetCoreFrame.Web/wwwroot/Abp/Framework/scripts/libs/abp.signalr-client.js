var abp = abp || {};
(function () {

    var zjf = new signalR.HubConnectionBuilder().withUrl("/signalr").build();

    // 检查是否定义
    if (!signalR) {
        return;
    }

    // 创建命名空间
    abp.signalr = abp.signalr || {};
    abp.signalr.hubs = abp.signalr.hubs || {};

    // 配置连接
    function configureConnection(connection) {
        // 设置公共集线器
        abp.signalr.hubs.common = connection;

        // 如果集线器断开，重新连接
        connection.onclose(function (e) {
            if (e) {
                abp.log.debug('signalr连接错误关闭: ' + e);
            }
            else {
                abp.log.debug('signalr断开后');
            }

            if (!abp.signalr.autoConnect) {
                return;
            }

            setTimeout(function () {
                connection.start();
            }, 5000);
        });

        // 注册获得通知事件
        connection.on('getNotification', function (notification) {
            abp.log.debug('注册 abp.notifications.received 事件!');
            abp.event.trigger('abp.notifications.received', notification);
        });
    }

    // 连接服务器
    abp.signalr.connect = function () {
        var url = abp.signalr.url || '/signalr';

        //开始连接
        startConnection(url, configureConnection)
            .then(function (connection) {
                abp.log.debug('连接到SignalR 服务器!'); //TODO: Remove log
                abp.event.trigger('abp.signalr.connected');
                // 调用集线器上的Register方法
                connection.invoke('register').then(function () {
                    abp.log.debug('注册 SignalR 到服务器!'); //TODO: Remove log
                });
            })
            .catch(function (error) {
                abp.log.debug(error.message);
            });
    };

    // Starts a connection with transport fallback - if the connection cannot be started using
    // the webSockets transport the function will fallback to the serverSentEvents transport and
    // if this does not work it will try longPolling. If the connection cannot be started using
    // any of the available transports the function will return a rejected Promise.
    function startConnection(url, configureConnection) {
        if (abp.signalr.remoteServiceBaseUrl) {
            url = abp.signalr.remoteServiceBaseUrl + url;
        }

        // Add query string: https://github.com/aspnet/SignalR/issues/680
        if (abp.signalr.qs) {
            url += '?' + abp.signalr.qs;
        }

        return function start(transport) {
            abp.log.debug('启动连接 使用' + signalR.HttpTransportType[transport] + ' 传输');
            var connection = new signalR.HubConnectionBuilder()
                .withUrl(url, transport)
                .build();
            if (configureConnection && typeof configureConnection === 'function') {
                configureConnection(connection);
            }

            return connection.start()
                .then(function () {
                    return connection;
                })
                .catch(function (error) {
                    abp.log.debug('无法使用 ' + signalR.HttpTransportType[transport] + ' 传输. ' + error.message);
                    if (transport !== signalR.HttpTransportType.LongPolling) {
                        return start(transport + 1);
                    }

                    return Promise.reject(error);
                });
        }(signalR.HttpTransportType.WebSockets);
    }

    abp.signalr.startConnection = startConnection;

    if (abp.signalr.autoConnect === undefined) {
        abp.signalr.autoConnect = true;
    }

    if (abp.signalr.autoConnect) {
        abp.signalr.connect();
    }

})();
