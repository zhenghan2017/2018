const base = {
  // 参数判断
  paramsJudge: function(postParamsObject, filterArr) {
    let flag = false;
    for(let i = 0; i < filterArr.length; i++) {
      if(!postParamsObject[filterArr[i]]) {
        flag = true;
        break;
      }
    }
    return flag;
  },

  // 返回终端IP地址
  getClintIp: function (req) {
    let ip = 
      req.headers['x-forwarded-for'] ||
      req.connection.remoteAddress ||
      req.socket.remoteAddress ||
      req.connection.socket.remoteAddress || 
      '';
    ip = ip.match(/\d+.\d+.\d+.\d+/);
    ip = ip ? ip.join('.') : null;
    return ip;
  }
}

module.exports = base;