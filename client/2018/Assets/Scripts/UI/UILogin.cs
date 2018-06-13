using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
using UIForm;

namespace UITest
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

