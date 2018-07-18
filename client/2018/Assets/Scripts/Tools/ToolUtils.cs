using System;
using System.IO;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool
{
    [Serializable]
    internal class KeyValuesInfo
    {
        //配置信息
        public List<KeyValuesItem> ConfigInfo = null;
    }
    [Serializable]
    internal class KeyValuesItem
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
        /// 通过地址读取Json文件保存到内存
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
                Debug.LogError("getConfigByJson Read JsonPath " + _path + " is error");
                //throw new JsonAnlysisException(GetType() + "/InitAndAnalysisJson()/Json Analysis Exception ! Parameter jsonPath=" + jsonPath);
            }
            //数据加载到AppSetting 集合中
            foreach (KeyValuesItem nodeInfo in keyvalueInfoObj.ConfigInfo)
            {
                _AppSetting.Add(nodeInfo.Key, nodeInfo.Value);
            }
            return _AppSetting;
        }

        public static bool SetFile(Dictionary<string, string> saveInfo, string path)
        {
            Dictionary<int, KeyValuesItem> configMsg = new Dictionary<int, KeyValuesItem>();
           
            //item.Key = saveInfo["Tip"];
            // configMsg.Add(0, item);
            
            JsonData _saveData = new JsonData();
            int conut = 0;
            _saveData["ConfigInfo"] = new JsonData();
            JsonData arrayData = new JsonData();
            arrayData.SetJsonType(JsonType.Array);
            foreach (var temp in saveInfo)
            {
                JsonData _data = new JsonData();
                Debug.Log(temp.Key + "  " + temp.Value);
                _data["Key"] = temp.Key;
                _data["Value"] = temp.Value;
                arrayData.Add(_data);
            }
            _saveData["ConfigInfo"] = arrayData;
            string values = JsonMapper.ToJson(_saveData);
            //找到当前路径
            Debug.Log("保存的Json: " + values);
            FileInfo file = new FileInfo(path);
            //判断有没有文件，有则打开文件，，没有创建后打开文件
            StreamWriter sw = file.CreateText();
            //将转换好的字符串存进文件，
            sw.WriteLine(values);
            //注意释放资源
            sw.Close();
            sw.Dispose();

            return true;
        }
    }
}
