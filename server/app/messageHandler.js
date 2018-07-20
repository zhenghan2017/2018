const player = require('../module/player');
const redisClient = require('../public/util/redisClient');
const Protobuf = require('protobufjs');
const game = Protobuf.loadSync('./config/GameLogicProtocolData.proto');
const lobby = Protobuf.loadSync('./config/LobbyProtocolData.proto');

const messageHandler = {
  // 玩家连接返回
  // 玩家ID传来，先查询redis数据库，若有对象直接从redis中取出
  // 若redis中没有，进行mysql查询，并将玩家数据写入redis
  respPlayerConnect: function(_message) {
    const DATA_LOBBY_REQPLAYERCONNECT = lobby.lookup('lobbyProtocolDataPackage.DATA_LOBBY_REQPLAYERCONNECT');
    const DATA_LOBBY_RESPPLAYERCONNECT = lobby.lookup('lobbyProtocolDataPackage.DATA_LOBBY_RESPPLAYERCONNECT');
    const _data = DATA_LOBBY_REQPLAYERCONNECT.decode(_message.dataBuf);
    const playerId = parseInt(_data.playerid);
    const resp = {
      name: '',
      roomId: 10000,
      flag: 0
    }
    redisClient.hgetAllKey(playerId)
      .then(function(reply) {
        if(reply) {
          
        }
        return player.getPlayerInfoByPid(playerId);
      })
      .then(function(reply) {
        let flag = DATA_LOBBY_RESPPLAYERCONNECT.UNKNOW_USERID;
        if(reply) {
          resp.name = reply[0].name;
          flag = DATA_LOBBY_RESPPLAYERCONNECT.SUCCESS;
        } 
        resp.flag = flag;
        
      })
      .catch(function(err) {
        console.log(err);
      })   
  }
}

module.exports = messageHandler;