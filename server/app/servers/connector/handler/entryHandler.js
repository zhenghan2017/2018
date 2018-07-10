module.exports = function(app) {
	return new Handler(app);
};

let Handler = function(app) {
		this.app = app;
};

let handler = Handler.prototype;

handler.enter = function(msg, session, next) {
	let self = this;
	let areaId = msg.areaId;
	let userId = msg.userId;
	let sessionService = self.app.get('sessionService');

	// 将场景id存入session
	session.set('areaId', areaId);
	session.push('areaId', function(err) {
		if(err) {
			return console.error('set areaId for session service failed! error is : ', err.stack);
		}
	});

	// 进行远程调用
	self.app.rpc.area.areaRemote.add(session, self.app.get('serverId'), areaId, userId, function(err, results) {
		if(err) {
			return console.error('add online count failed! error is : ', err.stack);
		}
		next(null, {
			code: 200,
			msg: '进入服务器成功'
		})
	})
}