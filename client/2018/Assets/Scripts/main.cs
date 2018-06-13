using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIForm;
using Define;

public class main : MonoBehaviour {

    void Start()
    {
        //加载登陆窗体
        UIManager.GetInstance().ShowUIForms(UIDefine.LOGON_FROMS);
    }
}
