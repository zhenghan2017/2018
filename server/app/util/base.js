const base = {
  paramsJudge: function(postParamsObject, filterArr) {
    let flag = false;
    for(let i = 0; i < filterArr.length; i++) {
      if(!postParamsObject[filterArr[i]]) {
        flag = true;
        break;
      }
    }
    return flag;
  }
}

module.exports = base;