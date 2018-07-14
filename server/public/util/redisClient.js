const configRedis = require('../../config/configRedis.json');
const redis = require('redis');
const _client = redis.createClient(configRedis);

const redisClient = {
  // 设置键值
  setKey: function(key, value, expire) {
    return new Promise(function(resolve, reject) {
      let _expire = expire || '';
      _client.set(key, value, function(err, results) {
        if(err) {
          reject({code: 500, err: err});
        }
        // 如果有传时间参数则设置过期时间，否则不设置
        if(_expire) {
          _client.expire(key, expire, function(err, results) {
            if(err) {
              reject({code: 500, err: err});
            }
            resolve('OK');
          })
        }
        resolve('OK');
      })
    })
  },

  // 获取键值
  getKey: function(key) {
    return new Promise(function(resolve, reject) {
      _client.get(key, function(err, results) {
        if(err) {
          reject({code: 500, err: err});
        }
        resolve(results);
      })
    })
  },

  // 设置键值的生命周期
  setTTLForKey: function(key, expire) {
    return new Promise(function(resolve, reject) {
      _client.expire(key, expire, function(err, results) {
        if(err) {
          reject({code: 500, err: err});
        }
        resolve('OK');
      })
    })
  },

  // 获取键值的剩余生命周期
  getTTLForKey: function(key) {
    return new Promise(function(resolve, reject) {
      _client.TTL(key, function(err, results) {
        if(err) {
          reject({code: 500, err: err});
        }
        resolve(results);
      })
    })
  }
}
  
module.exports = redisClient;