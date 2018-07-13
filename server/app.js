const express = require('express');
const bodyParser = require('body-parser');
const logger = require('morgan');
const socket = require('socket.io');
const jwt = require('jsonwebtoken');
const redisClient = require('./public/util/redisClient');
const configToken = require('./config/configToken.json');
const configResponse = require('./config/configResponse.json');


// 初始化app
let app = express();

// 定义路由
const userRoute = require('./router/userRoute');
const playerRoute = require('./router/playerRoute');

// 使用中间件
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));

// 用户登录验证（token）
app.use(function(req, res, next) {
  const reqUrl = req.originalUrl;
  if(reqUrl !== '/user/register' && reqUrl !== '/user/check_abnormal_login' && reqUrl !== '/user/login') {
    const tokenFromUser = req.headers['access-token'];
    jwt.verify(tokenFromUser, configToken.secret, function(err, decode) {
      if(err) {
        return res.send({
          code: configResponse.tokenError.code, 
          msg: `${configResponse.tokenError.msg}  详细信息：${err.message}`
        });
      }
      const key = decode.uid + decode.name;
      redisClient.getKey(key)
        .then(function(reply) {
          if(reply === tokenFromUser) {
            req._decode = decode;
            next();
          }
        })
        .catch(function(err) {
          res.send({code: 500, err: err});
        })
    })
  } else {
    next();
  }
});

// 使用路由
app.use('/user', userRoute);
app.use('/player', playerRoute);

app.listen(2018);
console.info('server start on 127.0.0.1:2018');