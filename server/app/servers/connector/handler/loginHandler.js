const base = require('../../../util/base.js');
const configError = require('../../../../config/configError.json');

// 暴露登录处理模块
module.exports = function(app) {
  return new LoginHandler(app);
}

// 构造函数
function LoginHandler(app) {
  this.app = app;
}

let handle = LoginHandler.prototype;

handle.login = function(msg, session, next) {
  const paramsArr = ['account', 'password'];
  const flag = base.paramsJudge(msg, paramsArr);
  if(flag) {
    return next(null, {
      code: configError.paramsError.code, 
      msg: configError.paramsError.msg
    });
  }
  let self = this;
  const account = msg.account;
  const password = msg.password;
  // 核对用户信息操作，返回用户信息
  if(account !== 'han' && password !== 'han') {
    next(null, {
      code: configError.userInfoError.code,
      msg: configError.userInfoError.msg
    });
    return;
  }
  // 往session中绑定用户信息
  var sessionService = self.app.get('sessionService');
  // 判断是否重复登录
  if( !! sessionService.getByUid(userId)) {
		return next(null, {
			code: 500,
			msg: '您已经登录过了，请勿重复登录'
		});
	}
	// 往session中绑定用户ID
	sessionService.bing(userId);
  next(null, {
    code: 200,
    msg: '登录成功',
    results: {
      userInfo: {
        userId: 0,
        account: 'han'
      },
      areaInfo: [{
        name: '服务器1',
        value: 0
      },{
        name: '服务器2',
        value: 1
      },{
        name: '服务器3',
        value: 2
      }]
    }
  });
}