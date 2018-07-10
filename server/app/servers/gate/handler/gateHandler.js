const dispatcher = require('../../../util/dispatcher');

module.exports = function(app) {
	return new Handler(app);
};

let Handler = function(app) {
	this.app = app;
};

let handler = Handler.prototype;

/**
 * 选择连接路由器
 *
 * @param {Object} msg: 客户端消息
 * @param {Object} session: 会话对象
 * @param {Function} next: 控制权转移
 *
 */
handler.queryEntry = function(msg, session, next) {
	const uid = msg.uid;
	if(!uid) {
		next(null, {
			code: 500
		});
		return;
	}
	// 得到全部的连接服务器数组
	const connectorsArr = this.app.getServersByType('connector');
	if(!connectorsArr || connectorsArr.length === 0) {
		next(null, {
			code: 500
		});
		return;
	}
	// 通过哈希运算进行取余得到连接的服务器对象
	const res = dispatcher.dispatch(uid, connectorsArr);
	next(null, {
		code: 200,
		host: res.host,
		port: res.clientPort
	});
};
