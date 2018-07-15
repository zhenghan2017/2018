const express = require('express');
const bodyParser = require('body-parser');
const logger = require('morgan');
const socket = require('socket.io');
const jwt = require('jsonwebtoken');
const redisClient = require('./public/util/redisClient');
const configToken = require('./config/configToken.json');
const configResponse = require('./config/configResponse.json');
const player = require('./module/player');

// 初始化app
let app = express();
var server = require('http').Server(app);

// 定义路由
const userRoute = require('./router/userRoute');
const playerRoute = require('./router/playerRoute');

// 使用中间件
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));

// 用户登录验证（token）
// app.use(function(req, res, next) {
//   const reqUrl = req.originalUrl;
//   if(reqUrl !== '/user/register' && reqUrl !== '/user/check_abnormal_login' && reqUrl !== '/user/login') {
//     const tokenFromUser = req.headers['access-token'];
//     jwt.verify(tokenFromUser, configToken.secret, function(err, decode) {
//       if(err) {
//         return res.send({
//           code: configResponse.tokenError.code, 
//           msg: `${configResponse.tokenError.msg}  详细信息：${err.message}`
//         });
//       }
//       const key = decode.uid + decode.name;
//       redisClient.getKey(key)
//         .then(function(reply) {
//           if(reply === tokenFromUser) {
//             req._decode = decode;
//             next();
//           }
//         })
//         .catch(function(err) {
//           res.send({code: 500, err: err});
//         })
//     })
//   } else {
//     next();
//   }
// });

// 使用路由
app.use('/user', userRoute);
app.use('/player', playerRoute);

app.listen(2018);

let socketClient = socket(server);

// socket事件监听

// socket连接事件
socketClient.on('connection', function(socket) {
  const socketId = socket.id;

  // 初始化玩家信息
  socket.on('onInitUserInfo', function(uid) {
    let userinfo = '';
    player.getPlayerInfoByUid(uid)
      .then(function(reply) {
        userinfo = reply[0];
        return redisClient.setKey(socketId, userInfo);
      })
      .then(function(reply) {
        socket.emit('onInitUserInfo', {
          code: 200, 
          msg: 'OK', 
          results: userinfo
        });
      })
      .catch(function(err) {
        socket.emit('error', err);
      })
    
  });

  // 监听玩家移动事件
  socket.on('onMove', function(userInfo) {
    redisClient.setKey(socketId, userInfo)
      .then(function(reply) {
        socket.emit('connection', configResponse.normalResponse);
      })
      .catch(function(err) {
        socket.emit('error', err);
      })
  });

   // 传输玩家信息
   socket.on('onReconnect', function() {
    redisClient.getKey(socketId)
      .then(function(reply) {
        socket.emit('connection', {
          code: 200,
          msg: 'OK',
          results: reply
        });
      })
      .catch(function(err) {
        socket.emit('error', err);
      })
  });

  // 监听用户离开事件
  socket.on('onLoginout', function() {
    redisClient.deleteKey(socketId, userInfo)
      .then(function(reply) {
        socket.emit('connection', configResponse.normalResponse);
      })
      .catch(function(err) {
        socket.emit('error', err);
      })
  });

  // 监听用户断开连接
  socket.on('disconnect', function(reason) {
    socket.emit('disconnect', configResponse.socketDisconnectError);
  });

  // 监听用户socket发生错误
  socket.on('error', function(error) {
    socket.emit('error', {code: 8, msg: error});
  });
})

console.info('server start on 127.0.0.1:2018');