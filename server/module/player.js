const dbHandler = require('../public/util/mysql');

const player = {
  // 检测用户是否存在玩家信息
  checkIsExist: function(uid) {
    const sql = `select * from model_player where userId = ${uid}`;
    return dbHandler(sql);
  },

  // 返回全部玩家类型信息
  getAllPlayerType: function() {
    const sql = `select * from model_player_type`;
    return dbHandler(sql);
  },

  // 根据玩家类型ID获取玩家类型基础参数
  getPlayerTypeByKid: function(kid) {
    const sql = `select * from model_player_type where id = ${kid}`;
    return dbHandler(sql);
  },

  // 根据用户ID获取玩家信息
  getPlayerInfoByUid: function(uid) {
    const sql = `select * from model_player where userId = ${uid}`;
    return dbHandler(sql);
  },

  // 创建玩家
  createPlayer: function(playMsg) {
    const userId = playMsg.userId;
    const kindId = playMsg.kindId;
    const name = playMsg.name;
    const attackValue = playMsg.attackValue;
    const defenceValue = playMsg.defenceValue;
    const walkSpeed = playMsg.walkSpeed;
    const attackSpeed = playMsg.attackSpeed;
    const attackScope = playMsg.attackScope;
    const hp = playMsg.hp;
    const mp = playMsg.mp;
    const maxHp = playMsg.hp;
    const maxMp = playMsg.mp;
    
    const sql = `insert into 
    model_player(userId, kindId, name, attackValue, defenceValue, walkSpeed, attackSpeed, attackScope, hp, mp, maxHp, maxMp)
    values (${userId}, ${kindId}, "${name}", ${attackValue}, ${defenceValue}, ${walkSpeed}, 
    ${attackSpeed}, ${attackScope}, ${hp}, ${mp}, ${maxHp}, ${maxMp})`;
    return dbHandler(sql);
  },

  // 通过玩家ID修改玩家昵称
  updatePlayerNameByPid: function(playMsg) {
    const name = playMsg.name;
    const pid = playMsg.pid;
    const sql = `update model_player set name = "${name}" where id = ${pid}`;
    return dbHandler(sql);
  },

  // 删除玩家信息
  deletePlayer: function(pid) {
    const sql = `delete from model_player where id = ${pid}`;
    return dbHandler(sql);
  }
}

module.exports = player;