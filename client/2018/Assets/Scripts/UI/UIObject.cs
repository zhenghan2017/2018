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

        #endregion
    }
}
