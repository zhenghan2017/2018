const configRedis = require('../../config/configRedis.json');
const redis = require('redis');
const _client = redis.createClient(configRedis);

const redisClient = {
  // 检测键值是否存在
  checkIsExist: function(key) {
    return new Promise(function(resolve, reject) {
      _client.exists(key, function(err, results) {
        if(err) {
          reject({code: 500, err: err});
        }
        let flag = true;
        if(results !== 1) {
          flag = false;
        }
        resolve(flag);
      })
    })
  },

  // 设置键值
  setKey: function(key, value, expire) {
    return new Promise(function(resolve, reject) {
      let _expire = expire || '';
      _client.set(key, value, function(err, results) {
        if(err) {
          reject({code: 500, err: err});
        }
      })
      // 如果有传时间参数则设置过期时间，否则不设置
      if(_expire) {
        _client.expire(key, expire, function(err, results) {
          if(err) {
            reject({code: 500, err: err});
          }
          resolve('OK');
        })
      } else {
        resolve('OK');
      }
    })
  },

   // 设置键值
   hsetKey: function(key, hash, expire) {
    return new Promise(function(resolve, reject) {
      let _expire = expire || '';
      for(let _key in hash) {
        _client.hset(key, _key, hash[_key], function(err, results) {
          if(err) {
            reject({code: 500, err: err});
          }
        })
      }
      // 如果有传时间参数则设置过期时间，否则不设置
      if(_expire) {
        _client.expire(key, expire, function(err, results) {
          if(err) {
            reject({code: 500, err: err});
          }
          resolve('OK');
        })
      } else {
        resolve('OK');
      }
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

  // 获取hash中的单个字段
  hgetKey: function(key, field) {
    return new Promise(function(resolve, reject) {
      _client.hget(key, field, function(err, results) {
        if(err) {
          reject({code: 500, err: err});
        }
        resolve(results);
      })
    })
  },

  // 获取hash中的全部字段
  hgetAllKey: function(key) {
    return new Promise(function(resolve, reject) {
      _client.hgetall(key, function(err, results) {
        if(err) {
          reject({code: 500, err: err});
        }
        let value = results;
        if(!results) {
          value = null;
        }
        resolve(value);
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
  },

  // 删除键值
  deleteKey: function(key) {
    return new Promise(function(resolve, reject) {
      _client.del(key, function(err, results) {
        if(err) {
          reject({code: 500, err: err});
        }
        resolve(results);
      })
    })
  }
}
  
module.exports = redisClient;