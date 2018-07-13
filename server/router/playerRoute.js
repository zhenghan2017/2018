const express = require('express');
const router = express.Router();
const base = require('../public/util/base');
const configResponse = require('../config/configResponse.json');
const player = require('../module/player');

/**
 * 检测该用户下是否具备玩家信息
 * 
 * @param {Bigint}  uid  用户ID
 * 
 */
router.post('/check_player_exist', function(req, res, next) {
  const params = ['uid'];
  const flag = base.paramsJudge(req.body, params);
  if(flag) {
    return res.send({
      code: configResponse.paramsError.code,
      msg: configResponse.paramsError.msg
    });
  }
  const uid = req.body.uid;
  player.checkIsExist(uid)
    .then(function(reply) {
      let code = 200;
      let msg = 'OK';
      let results = reply[0];
      if(reply.length === 0) {
        code = configResponse.playerNotFoundError.code;
        msg = configResponse.playerNotFoundError.msg;
        results = {};
      }
      res.send({code: code, msg: msg, results: results});
    })
    .catch(function(err) {
      res.send({code: 500, msg: err});
    })
});

/**
 * 玩家类型枚举
 * 
 */
router.post('/select_player_type', function(req, res, next) {
  player.getAllPlayerType()
    .then(function(reply) {
      res.send({
        code: 200,
        msg: 'OK',
        results: reply
      });
    })
    .catch(function(err) {
      res.send({code: 500, msg: err});
    })
});

/**
 * 创建玩家
 * 
 * @param {Bigint} uid  用户id
 * @param {Bigint}  kid  类型ID
 * @param {String}  name  名称
 * 
 */
router.post('/create_player', function(req, res, next) {
  const params = ['uid', 'kid', 'name'];
  const flag = base.paramsJudge(req.body, params);
  if(flag) {
    return res.send({
      code: configResponse.paramsError.code,
      msg: configResponse.paramsError.msg
    });
  }
  const kid = req.body.kid;
  const uid = req.body.uid;
  player.getPlayerTypeByKid(kid)
    .then(function(reply) {
      let item = reply[0];
      const playerMsg = {
        userId: uid,
        kindId: kid,
        name: req.body.name,
        attackValue: item.attackValue,
        defenceValue: item.defenceValue,
        walkSpeed: item.walkSpeed,
        attackSpeed: item.attackSpeed,
        attackScope: item.attackScope,
        hp: item.hp,
        mp: item.mp,
        maxHp: item.hp,
        maxMp: item.mp
      };
      return player.createPlayer(playerMsg);
    })
    .then(function(reply) {
      return player.getPlayerInfoByUid(uid);
    })
    .then(function(reply) {
      res.send({
        code: 200,
        msg: 'OK',
        results: reply[0]
      });
    })
    .catch(function(err) {
      res.send({code: 500, msg: err});
    })
});

/** 
 * 修改玩家昵称
 * 
 * @param {Bigint} pid 玩家ID
 * @param {String} name 新的昵称
 * 
*/
router.post('/update_player_name', function(req, res, next) {
  const params = ['pid', 'name'];
  const flag = base.paramsJudge(req.body, params);
  if(flag) {
    return res.send({
      code: configResponse.paramsError.code,
      msg: configResponse.paramsError.msg
    });
  }
  const playerMsg = {
    pid: req.body.pid,
    name: req.body.name
  }
  player.updatePlayerNameByPid(playerMsg)
    .then(function(reply) {
      res.send(configResponse.normalResponse);
    })
    .catch(function(err) {
      res.send({code: 500, msg: err});
    })
});

/** 
 * 删除玩家信息
 * 
 * @param {Bigint}  pid 玩家ID
 * 
*/
router.post('/delete_player', function(req, res, next) {
  const params = ['pid'];
  const flag = base.paramsJudge(req.body, params);
  if(flag) {
    return res.send({
      code: configResponse.paramsError.code,
      msg: configResponse.paramsError.msg
    });
  }
  const pid = req.body.pid;
  player.deletePlayer(pid)
    .then(function(reply) {
      res.send(configResponse.normalResponse);
    })
    .catch(function(err) {
      res.send({code: 500, msg: err});
    })
});

module.exports = router;