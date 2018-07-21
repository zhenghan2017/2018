const player = require('../module/player');
const redisClient = require('../public/util/redisClient');
const Protobuf = require('protobufjs');
const game = Protobuf.loadSync('./config/GameLogicProtocolData.proto');
const lobby = Protobuf.loadSync('./config/LobbyProtocolData.proto');
const configProtocolNum = require('../config/configProtocolNum.json');
const messageHeadLength = 6;

const messageHandler = {
  // 玩家连接返回
  // 玩家ID传来，先查询redis数据库，若有对象直接从redis中取出
  // 若redis中没有，进行mysql查询，并将玩家数据写入redis
  respPlayerConnect: function(_message) {
    const messageReq = lobby.lookup('lobbyProtocolDataPackage.DATA_LOBBY_REQPLAYERCONNECT');
    const messageResp = lobby.lookup('lobbyProtocolDataPackage.DATA_LOBBY_RESPPLAYERCONNECT');
    const _data = messageReq.decode(_message.dataBuf);
    const playerId = parseInt(_data.playerid);
    const resp = {
      nickname: '',
      roomid: 10000,
      flag: 0
    }
    return redisClient.hgetAllKey(playerId)
      .then(function(reply) {
        if(reply) {
          
        }
        return player.getPlayerInfoByPid(playerId);
      })
      .then(function(reply) {
        let flag = 1;
        if(reply) {
          resp.nickname = reply[0].name;
          flag = 0;
        } 
        resp.flag = flag;
        const _message = messageResp.create(resp);
        const dataBuffer = messageResp.encode(_message).finish();
        // 6为标准报文长度 = 4字节的报文数据总长度 + 2字节的协议号
        const dataLength= messageHeadLength + dataBuffer.length;
        const respBuffer = Buffer.alloc(dataLength);
        respBuffer.writeInt32LE(dataLength, 0);
        respBuffer.writeInt16LE(configProtocolNum.CMD_LOBBY_RESPPLAYERCONNECT, 4);
        dataBuffer.copy(respBuffer, messageHeadLength, 0, dataBuffer.length);
        return respBuffer;
      })
      .catch(function(err) {
        console.log(err);
         // 打断promise专用err = 0
        if(err !== 0) {
          return({code: 500, err: err});
        }
      })
  }
}

module.exports = messageHandler;