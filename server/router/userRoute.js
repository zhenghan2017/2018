const express = require('express');
const router = express.Router();
const moment = require('moment');
const jwt = require('jsonwebtoken');
const base = require('../public/util/base');
const user = require('../module/user');
const configResponse = require('../config/configResponse.json');
const configToken = require('../config/configToken.json');
const redisClient =require('../public/util/redisClient');
const _client = redisClient.start();

// 注册
router.post('/register', function(req, res, next) {
  const params = ['account', 'password'];
  const flag = base.paramsJudge(req.body, params);
  if(flag) {
    return res.send({
      code: configResponse.paramsError.code,
      msg: configResponse.paramsError.msg
    });
  }
  const userMsg = {
    name: req.body.account,
    password: req.body.password,
    from: base.getClintIp(req)
  }
  user.createUser(userMsg)
    .then(function(reply) {
      res.send(configResponse.normalResponse);
    })
    .catch(function(err) {
      res.send({code: 500, msg: err});
    })
});

// 修改密码 
router.post('/update_password', function(req, res, next) {
  const params = ['uid', 'oldPassword', 'newPassword'];
  const flag = base.paramsJudge(req.body, params);
  if(flag) {
    return res.send({
      code: configResponse.paramsError.code,
      msg: configResponse.paramsError.msg
    });
  }
  const uid = req.body.uid;
  const oldPassword = req.body.oldPassword;
  const newPassword = req.body.newPassword;
  user.selectUserById(uid) 
    .then(function(reply) {
      let _password = reply[0].password;
      if(_password !== oldPassword) {
        return res.send({
          code: configResponse.oldPasswordError.code,
          msg: configResponse.oldPasswordError.msg
        });
      }
      const userMsg = {
        uid: uid,
        password: newPassword
      };
      return user.updatePassword(userMsg);
    })
    .then(function(reply) {
      res.send({code: 200, msg: 'OK'});
    })
    .catch(function(err) {
      res.send({code: 500, msg: err})
    })
});

// 注销
router.post('/cancellation', function(req, res, next) {
  const params = ['uid'];
  const flag = base.paramsJudge(req.body, params);
  if(flag) {
    return res.send({
      code: configResponse.paramsError.code,
      msg: configResponse.paramsError.msg
    });
  }
  const uid = req.body.uid;
  user.deleteUser(uid)
    .then(function(reply) {
      res.send(configResponse.normalResponse);
    })
    .catch(function(err) {
      res.send({code: 500, msg: err})
    })
});

// 检测登录的IP是否跟上次相同
router.post('/check_abnormal_login', function(req, res, next) {
  const params = ['account'];
  const flag = base.paramsJudge(req.body, params);
  if(flag) {
    return res.send({
      code: configResponse.paramsError.code,
      msg: configResponse.paramsError.msg
    });
  }
  const account = req.body.account;
  user.selectUserByName(account)
    .then(function(reply) {
      let currentIp = base.getClintIp(req);
      let lastLoginIp = reply[0].lastLoginIp;
      let code = 200;
      let msg = 'OK';
      if(currentIp !== lastLoginIp) {
        code = configResponse.abnormalLogin.code;
        msg = configResponse.abnormalLogin.msg;
      }
      res.send({code: code, msg: msg});
    })
    .catch(function(err) {
      res.send(err);
    })
});

// 登录
router.post('/login', function(req, res, next) {
  const params = ['account', 'password'];
  const flag = base.paramsJudge(req.body, params);
  if(flag) {
    return res.send({
      code: configResponse.paramsError.code,
      msg: configResponse.paramsError.msg
    });
  }
  const account = req.body.account;
  const password = req.body.password;
  let results = {};
  // 验证用户信息
  user.selectUserByName(account)
    .then(function(reply) {
      if(password !== reply[0].password) {
        return res.send({
          code: configResponse.userInfoError.code, 
          msg: configResponse.userInfoError.msg
        });
      }
      results = reply[0];
      let currentDay = moment().format('YYYY-MM-DD');
      const userMsg = {
        uid: results.id,
        loginCount: results.loginCount + 1,
        lastLoginIp:  base.getClintIp(req),
        lastLoginTime: currentDay
      }
      return user.updateLoginParams(userMsg);
    })
    .then(function(reply) {
      // token负载
      const payLoad = {
        uid: results.id,
        name: results.name
      };
      // 生成token
      results.token = jwt.sign(
        payLoad,
        configToken.secret
      );
      // 去除密码等敏感信息
      delete results.password;
      // 将token信息存入redis
      _client.set(payLoad.uid + payLoad.name, results.token, function(err, vals) {
        if(err) {
          Promise.reject(err);
        }
      });
      res.send({code: 200, msg: 'OK', results: results});
    })
    .catch(function(err) {
      res.send({code: 500, msg: err});
    })
});

module.exports = router;