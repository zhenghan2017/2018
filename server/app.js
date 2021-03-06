const express = require('express');
const bodyParser = require('body-parser');
const logger = require('morgan');
const socketServer = require('./component/socket');
const player = require('./module/player');
const redisClient = require('./public/util/redisClient');

// 初始化app
let app = express();

// 定义路由
const userRoute = require('./router/userRoute');
const playerRoute = require('./router/playerRoute');

// 使用中间件
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));

// 使用路由
app.use('/user', userRoute);
app.use('/player', playerRoute);

// http服务开启
app.listen(2018);
console.info(`Express server listening at 127.0.0.1:2018`);

// socket服务开启
socketServer.start();
