const express = require('express');
const router = express.Router();
const moment = require('moment');
const jwt = require('jsonwebtoken');
const base = require('../public/util/base');
const user = require('../module/user');
const configResponse = require('../config/configResponse.json');
const configToken = require('../config/configToken.json');
const redisClient =require('../public/util/redisClient');

/**
 * 注册功能
 * 
 * @param {String}  account  用户名
 * @param {String}  password 密码
 * @param {String}  contact  联系方式
 * 
 */
router.post('/register', function(req, res, next) {
  const params = ['account', 'password', 'contact'];
  const flag = base.paramsJudge(req.body, params);
  if(flag) {
    return res.json({
      code: configResponse.paramsError.code,
      msg: configResponse.paramsError.msg
    });
  }
  const userMsg = {
    name: req.body.account,
    password: req.body.password,
    contact: req.body.contact,
    from: base.getClintIp(req)
  }
  user.createUser(userMsg)
    .then(function(reply) {
      res.json(configResponse.normalResponse);
    })
    .catch(function(err) {
      res.json(err);
    })
});

/**
 * 修改密码功能
 * 
 * @param {Bigint}  uid  用户ID
 * @param {String}  oldPassword 旧密码
 * @param {String}  newPassword  新密码
 * 
 */
router.post('/update_password', function(req, res, next) {
  const params = ['uid', 'oldPassword', 'newPassword'];
  const flag = base.paramsJudge(req.body, params);
  if(flag) {
    return res.json({
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
        return Promise.reject({
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
      res.json({code: 200, msg: 'OK'});
    })
    .catch(function(err) {
      res.json(err);
    })
});

/**
 * 注销功能
 * 
 * @param {Bigint}  uid  用户ID
 * 
 */
router.post('/cancellation', function(req, res, next) {
  const params = ['uid'];
  const flag = base.paramsJudge(req.body, params);
  if(flag) {
    return res.json({
      code: configResponse.paramsError.code,
      msg: configResponse.paramsError.msg
    });
  }
  const uid = req.body.uid;
  user.deleteUser(uid)
    .then(function(reply) {
      res.json(configResponse.normalResponse);
    })
    .catch(function(err) {
      res.json(err);
    })
});

/**
 * 登录功能
 * 
 * @param {String}  account  用户名
 * @param {String}  password 密码
 * 
 */
router.post('/login', function(req, res, next) {
  const params = ['account', 'password'];
  const flag = base.paramsJudge(req.body, params);
  if(flag) {
    return res.json({
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
      // 检验用户信息是否正确
      if(reply.length === 0 || password !== reply[0].password) {
        return Promise.reject({
          code: configResponse.userInfoError.code, 
          msg: configResponse.userInfoError.msg
        });
      }
      // 验证登录是否异常
      let currentIp = base.getClintIp(req);
      let lastLoginIp = reply[0].lastLoginIp;
      if(lastLoginIp && currentIp !== lastLoginIp) {
        return Promise.reject({
          code: configResponse.abnormalLoginError.code, 
          msg: configResponse.abnormalLoginError.msg
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
      return redisClient.setKey(payLoad.uid + payLoad.name, results.token);
    })
    .then(function(reply) {
      moment(results.lastLoginTime).format('YYYY-MM-DD');
      res.json({code: 200, msg: 'OK', results: results});
    })
    .catch(function(err) {
      res.json(err);
    })
});

module.exports = router;