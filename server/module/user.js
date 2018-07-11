const dbHandler = require('../public/util/mysql');

const user = {
  // 新增用户信息
  createUser: function(userMsg) {
    const name = userMsg.name;
    const password = userMsg.password;
    const from = userMsg.from;
    
    const sql = `insert into user(name, password, lastLoginIp) values("${name}", "${password}", "${from}")`;
    return dbHandler(sql);
  },

  // 删除用户信息
  deleteUser: function(uid) {
    const sql = `delete from user where id = ${uid}`;
    return dbHandler(sql); 
  },

  // 修改密码
  updatePassword: function(userMsg) {
    const uid = userMsg.uid;
    const password = userMsg.password;
    const sql = `update user set password = "${password}" where id = ${uid}`;
    return dbHandler(sql);
  },

  // 根据用户ID查询用户信息
  selectUserById: function(uid) {
    const sql = `select * from user where id = ${uid}`;
    return dbHandler(sql);
  },

  // 根据用户名密码查询用户信息
  selectUserByName: function(uname) {
    const sql = `select * from user where name = "${uname}"`;
    return dbHandler(sql);
  },

  // 更新用户信息
  updateLoginParams: function(userMsg) {
    const uid = userMsg.uid;
    const loginCount = userMsg.loginCount;
    const lastLoginIp = userMsg.lastLoginIp;
    const lastLoginTime = userMsg.lastLoginTime;
    const sql = `update user set loginCount = ${loginCount}, lastLoginIp = "${lastLoginIp}", lastLoginTime = "${lastLoginTime}" where id = ${uid}`;
    return dbHandler(sql);
  }
};

module.exports = user;
