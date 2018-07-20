const net = require('net');
const messageHandler = require('./messageHandler');
const configProtocolNum = require('../config/configProtocolNum.json');


// 监听ip端口
const _serverIP = "127.0.0.1";
const _serverPort = 9876;

// 报文常量
const _messageDataLengthOffset = 0;
const _messageProtocloNumOffset = 4;
const _messageDataOffset = 6;

const _socketServer = {
  start: function() {
    let socketServer = net.createServer(function(socket) {
      console.log("Accepting connection: " + socket.remoteAddress + ":" + socket.remotePort );
      
      socket.write("Login server based on Node.js success!");
      
      socket.on("data", function(data) {
          let _message = _socketServer.read(data);
          if(_message.protocolNum === configProtocolNum.CMD_LOBBY_REQPLAYERCONNECT) {
              messageHandler.respPlayerConnect(_message);
          }
      });
      
      // 客户端调用closesocket或强制关闭时会导致服务器端会发射"close"事件，但是它发生在"error"事件之后。
      // socket.remoteAddress和socket.remotePort是undefined.
      socket.on("close", function(data) {
          console.log(`${socket.remoteAddress}:${socket.remotePort} logout`);
      });
      
      // 必须监听一个“error”事件，否则在客户端调用closesocket或强制关闭时服务器端会发射“error"事件。
      // 如果没有该事件的处理程序，进程便会异常终止。
      socket.on("error", function(error, data) {
          if(error) {
              console.log(`Occurs an error: ${error}`);
          }
      });
    });
  
    socketServer.on('close', function(data) {
      console.log(`Net server close`);
    });
    
    socketServer.on('error', function(err) {
      console.log(err);
    });

    socketServer.listen(_serverPort, _serverIP);
    console.log(`Net server listening at ${_serverIP}:${_serverPort}`);
  },

  write: function() {

  },

  read: function(buffer) {
    // 报文组成格式： 报文数据长度（4个字节） + 协议号（2个字节） + 数据（N个字节）
    let _buffer = Buffer.from(buffer);
    let _len = _buffer.length;
    let obj = {
      dataLength: 0,
      protocolNum: 0,
      dataBuf: ''
    };
    obj.dataLength = _buffer.readInt32LE(_messageDataLengthOffset);
    obj.protocolNum = _buffer.readInt16LE(_messageProtocloNumOffset);
    obj.dataBuf = _buffer.slice(_messageDataOffset, _len);
    return obj;
  }
}

module.exports = _socketServer;