using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Tool;

namespace UIForm
{
    public class UIObject : MonoBehaviour
    {
        private UIType _CurUIType = new UIType();

        public UIType CurUIType
        {
            get { return _CurUIType; }
            set { _CurUIType = value; }
        }

        #region 重载方法

        /// <summary>
        /// 显示状态
        /// </summary>
        public virtual void show()
        {
            this.gameObject.SetActive(true);
            //设置模态窗体调用(必须是弹出窗体)
            if (_CurUIType.UIForms_Type == UIFormType.PopUp)
            {

            }
        }

        /// <summary>
        /// 隐藏状态
        /// </summary>
        public virtual void hide()
        {
            this.gameObject.SetActive(false);
        }

        /// <summary>
        /// 重新显示状态
        /// </summary>
        public virtual void redisplay()
        {
            this.gameObject.SetActive(true);
            //设置模态窗体调用(必须是弹出窗体)
            if (_CurUIType.UIForms_Type == UIFormType.PopUp)
            {
                //UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject, _CurrentUIType.UIForm_LucencyType);
            }
        }

        /// <summary>
        /// 冻结状态
        /// </summary>
	    public virtual void freeze()
        {
            this.gameObject.SetActive(true);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 注册按钮事件
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_handle"></param>
        protected void RegisterButtonObjectEvent(string _name, EventTriggerListener.VoidDelegate _handle)
        {
            GameObject btn = ToolUtils.seekNodeByName(this.gameObject, _name).gameObject;
            //给按钮注册事件方法
            if (btn != null)
            {
                EventTriggerListener.Get(btn).onClick = _handle;
            }
        }

        /// <summary>
        /// 打开UI窗体
        /// </summary>
        /// <param name="uiFormName"></param>
        protected void OpenUIForm(string uiFormName)
        {
            UIManager.GetInstance().ShowUIForms(uiFormName);
        }

        /// <summary>
        /// 关闭当前UI窗体
        /// </summary>
        protected void CloseUIForm()
        {
            string strUIFromName = string.Empty;            //处理后的UIFrom 名称
            int intPosition = -1;

            strUIFromName = GetType().ToString();             //命名空间+类名
            intPosition = strUIFromName.IndexOf('.');
            if (intPosition != -1)
            {
                //剪切字符串中“.”之间的部分
                strUIFromName = strUIFromName.Substring(intPosition + 1);
            }

            UIManager.GetInstance().CloseUIForms(strUIFromName);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgType">消息的类型</param>
        /// <param name="msgName">消息名称</param>
        /// <param name="msgContent">消息内容</param>
        //protected void SendMessage(string msgType, string msgName, object msgContent)
        //{
        //    KeyValuesUpdate kvs = new KeyValuesUpdate(msgName, msgContent);
        //    MessageCenter.SendMessage(msgType, kvs);
        //}

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="messagType">消息分类</param>
        /// <param name="handler">消息委托</param>
        //public void ReceiveMessage(string messagType, MessageCenter.DelMessageDelivery handler)
        //{
        //    MessageCenter.AddMsgListener(messagType, handler);
        //}

        /// <summary>
        /// 显示语言
        /// </summary>
        /// <param name="id"></param>
        //public string Show(string id)
        //{
        //    string strResult = string.Empty;

        //    strResult = LauguageMgr.GetInstance().ShowText(id);
        //    return strResult;
        //}


        #endregion
    }
}
