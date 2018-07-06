using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIForm
{
    public class UIMainScene : UIObject
    {
        void Awake()
        {
            RegisterButtonObjectEvent("Btn_Setting",
                p => OpenUIForm(UIDefine.UI_MAIN_SETTING_FORM)
            );

            RegisterButtonObjectEvent("Btn_JoinGame",
                changeScene
            );
        }

        //切换场景
        private void changeScene(GameObject go)
        {
            SceneManager.LoadScene(1);
        }

    }

}
