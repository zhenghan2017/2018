using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using UnityEngine.UI;
using Project;

namespace UIForm
{
    public class UISettingForm : UIObject
    {
        Transform SelectPannel;


        Transform SetPannel;

        void Awake()
        {
            SelectPannel = null;
            //窗体的性质
            CurUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;

            RegisterButtonObjectEvent("Btn_KeySetting",
                 openKeyPannel
            );
        }

        /// <summary>
        /// 按键设置的界面
        /// </summary>
        /// <param name="go"></param>
        private void openKeyPannel(GameObject go)
        {
            if (SelectPannel != null)
                SelectPannel.gameObject.SetActive(false);
            bool isFirstOpenPannel = SetPannel == null;
            SetPannel = ToolUtils.seekNodeByName(this.gameObject, "KeySetPannel");
            SetPannel.gameObject.SetActive(true);
            SelectPannel = SetPannel;

            if (isFirstOpenPannel)
                RegisterSetCancelBtn(SetPannel);
        }




        private void RegisterSetCancelBtn(Transform _panrent)
        {
            //绑定确定取消事件
            Transform SetBtn = ToolUtils.seekNodeByName(_panrent.gameObject, "Btn_Set");
            Transform CancelBtn = ToolUtils.seekNodeByName(_panrent.gameObject, "Btn_Cancel");

            CancelBtn.gameObject.GetComponent<Button>().onClick.AddListener(delegate() 
            { 
                SelectPannel.gameObject.SetActive(false);
            });

            SetBtn.gameObject.GetComponent<Button>().onClick.AddListener(delegate()
            {
                //先不考虑(弹出对应的消息框) 直接保存到本地 
                if(SettingManager.GetInstance().SaveSetFile())
                {
                    Debug.Log("设置成功");
                    SelectPannel.gameObject.SetActive(false);
                }
                
            });
        }

    }


}
