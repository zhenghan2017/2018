using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIForm
{
    public class UILogin : UIObject
    {
        public void Awake()
        {
            RegisterButtonObjectEvent("Btn_OK",
                p => OpenUIForm(UIDefine.SELECT_HERO_FORM)
                );
        }

    }
}

