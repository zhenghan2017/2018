const mysql = require("mysql");

const pool = mysql.createPool({
    host: 'localhost',
    user: 'root',
    password: 'root',
    database: '2018',
    port: '3306',
    // 设置正确的时区
    timezone:"08:00"
});

const dbHandler = function(sql) {
    return new Promise(function(resolve, reject) {
        pool.getConnection(function(err, conn) {
            if(err){
                reject({code: 500, err: err});
            }else{
                conn.query(sql,function(err, vals, fields) {
                    if(err) {
                        reject({code: 500, err: err});
                    }
                    //释放连接
                    conn.release();
                    //事件驱动回调
                    resolve(vals);
                });
            }
        });
    });
};

module.exports = dbHandler;