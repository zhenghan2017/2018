using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool
{
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
    }
}
