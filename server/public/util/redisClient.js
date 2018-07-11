const configRedis = require('../../config/configRedis.json');
const redis = require('redis');

const redisClient = {
  start: function(configRedis) {
    return redis.createClient(configRedis);
  }
}
  
module.exports = redisClient;