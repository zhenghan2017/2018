using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool
{
    [Serializable]
    internal class KeyValuesInfo
    {
        //配置信息
        public List<KeyValuesNode> ConfigInfo = null;
    }
    [Serializable]
    internal class KeyValuesNode
    {
        //键
        public string Key = null;
        //值
        public string Value = null;
    }

    public class ToolUtils
    {

        /// <summary>
        /// 查找子节点
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="_name"></param>
        /// <returns></returns>
        public static Transform seekNodeByName(GameObject parent,string _name)
        {
            Transform searchTrans = null;                   //查找结果
            searchTrans = parent.transform.Find(_name);
            if (searchTrans == null)
            {
                foreach (Transform trans in parent.transform)
                {
                    searchTrans = seekNodeByName(trans.gameObject, _name);
                    if (searchTrans != null)
                    {
                        return searchTrans;
                    }
                }
            }
            return searchTrans;
        }

        /// <summary>
        /// 获取子节点（对象）脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="_name"></param>
        /// <returns></returns>
        public static T seekNodeComponetByName<T>(GameObject parent, string _name) where T : Component
        {
            Transform searchTranformNode = null;            //查找特定子节点

            searchTranformNode = seekNodeByName(parent, _name);
            if (searchTranformNode != null)
            {
                return searchTranformNode.gameObject.GetComponent<T>();
            }
            else
            {
                return null;
            }


        }

        /// <summary>
        /// 通过地址读取Json文件保存到本地
        /// Json的格式应为键值对应
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public static Dictionary<string,string> getConfigByJson(string _path)
        {
            Dictionary<string, string> _AppSetting = new Dictionary<string, string>();
            KeyValuesInfo keyvalueInfoObj = null;
            TextAsset configInfo = null;

            //参数检查
            if (string.IsNullOrEmpty(_path)) return null;
            //解析Json 配置文件
            try
            {
                configInfo = Resources.Load<TextAsset>(_path);
                keyvalueInfoObj = JsonUtility.FromJson<KeyValuesInfo>(configInfo.text);
            }
            catch
            {
                Debug.LogError("getConfigByJson Read JsonPath " + _path + "is error");
                //throw new JsonAnlysisException(GetType() + "/InitAndAnalysisJson()/Json Analysis Exception ! Parameter jsonPath=" + jsonPath);
            }
            //数据加载到AppSetting 集合中
            foreach (KeyValuesNode nodeInfo in keyvalueInfoObj.ConfigInfo)
            {
                _AppSetting.Add(nodeInfo.Key, nodeInfo.Value);
            }
            return _AppSetting;
        }
    }
}
