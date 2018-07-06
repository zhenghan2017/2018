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
            GlobalData.playerCpn = GameObject.Find("PlayerPrefab").GetComponent<Player>();

            //Messenger.Broadcast("Send"); 调用的方式 
            //Messenger.Broadcast("Send",GameObject); 带参数的调用方式
            MessageCenter.AddListener("Msg_GameStart", DoSomething);

            MessageCenter.AddListener<GameObject>("1212", DoSomething);
            //加载主场景页面
            UIManager.GetInstance().ShowUIForms(UIDefine.UI_MAIN_SCENE_FORM);
            GameRoot.GetInstance();

        }

        public void DoSomething()
        {
            Debug.Log("进入主场景界面");
        }

        public void DoSomething(GameObject obj)
        {

        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                MessageCenter.Broadcast("Msg_GameStart");
            }
        }
    }
}
