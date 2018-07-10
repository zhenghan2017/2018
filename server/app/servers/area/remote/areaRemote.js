module.exports = function(app) {
  return new AreaTestRemote(app);
}

let AreaTestRemote = function(app) {
  this.app = app;
}

let remote = AreaTestRemote.prototype;

remote.add = function(serverId, areaId, userId, callBack) {
  // 数据库中某服务器，某地图增加一个ID为userId的人，在线人数加1
  // 返回当前在线人数
  callBack(null, 2);
}