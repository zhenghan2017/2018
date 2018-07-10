const pomelo = require('pomelo');

// 初始化
var app = pomelo.createApp();

// gate配置参数
app.configure('production|development', 'gate', function(){
  app.set('connectorConfig',
    {
      connector : pomelo.connectors.hybridconnector,
      heartbeat : 3
    });
});

// connector配置参数
app.configure('production|development', 'connector', function(){
  app.set('connectorConfig',
    {
      connector : pomelo.connectors.hybridconnector,
      heartbeat : 3,
      // useDict : true,
      useProtobuf : true
    });
});

// connector配置参数
app.configure('production|development', 'area', function(){
  app.set('connectorConfig',
    {
      connector : pomelo.connectors.hybridconnector,
      heartbeat : 3,
      // useDict : true,
      useProtobuf : true
    });
});

// 开启app
app.start();
console.log('app start on ');

// 监听Node进程未捕获的错误，建议不要开启。
// process.on('uncaughtException', function (err) {
//   console.error(' Caught exception: ' + err.stack);
// });