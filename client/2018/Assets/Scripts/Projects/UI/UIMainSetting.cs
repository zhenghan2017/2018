using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIForm
{
    public class UIMainSetting : UIObject
    {
        void Awake()
        {
            //窗体的性质
            CurUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
        }
    }
}
