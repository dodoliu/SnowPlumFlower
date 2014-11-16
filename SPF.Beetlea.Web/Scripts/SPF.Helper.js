/*
* SPF 公用方法
*/
var SPF = (window["SPF"] || {});

/*
* 获取URL参数值
* @param {String} key 需要查询的参数名
* @param {String} url URL地址
* @return {String} 如果解析失败则返回 null
*/
SPF.GetQueryString = function (key, url) {
    var result = (url || location.search).match(new RegExp("[\?\&]" + key + "=([^\&]+)", "i"));
    if (result == null || result.length < 1) {
        return "";
    }
    return result[1];
};
