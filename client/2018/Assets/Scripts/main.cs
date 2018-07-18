using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIForm;
using Define;

namespace Project
{
    public class main : MonoBehaviour
    {

        private void Awake()
        {
            
        }

        void Start()
        {
            //Messenger.Broadcast("Send"); 调用的方式 
            //Messenger.Broadcast("Send",GameObject); 带参数的调用方式
            //MessageCenter.AddListener("Msg_GameStart", DoSomething);

            //MessageCenter.AddListener<GameObject>("1212", DoSomething);
            //加载主场景页面
            UIManager.GetInstance().ShowUIForms(UIDefine.UI_MAIN_SCENE_FORM);
            GameManager.GetInstance();
            SocketManager.Instance.Connect(GameConst.IP, GameConst.Port);
        }

    }
}
