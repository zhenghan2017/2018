const express = require('express');
const bodyParser = require('body-parser');
const logger = require('morgan');
const socket = require('socket.io');

// 初始化app
let app = express();

const userRoute = require('./router/userRoute');

app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use('/user', userRoute);

app.listen(2018);
console.info('server start on 127.0.0.1:2018');