using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIForm
{
    public class UISelectHeroForm : UIObject
    {

        public void Awake()
        {
            //窗体的性质
            CurUIType.UIForms_ShowMode = UIFormShowMode.HideOther;

            //注册进入主城的事件
            RegisterButtonObjectEvent("BtnConfirm",
                p =>
                {
                    OpenUIForm(UIDefine.MAIN_CITY_UIFORM);
                    OpenUIForm(UIDefine.HERO_INFO_UIFORM);
                }

                );

            //注册返回上一个页面
            RegisterButtonObjectEvent("BtnClose",
                m => CloseUIForm()
                );
        }
    }
}
