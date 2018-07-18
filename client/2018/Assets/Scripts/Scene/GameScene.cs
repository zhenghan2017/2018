using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
using UIForm;

namespace Project
{
    public class GameScene : MonoBehaviour
    {
        void Awake()
        {
            //清除UI缓存中的所有窗体(需要保存UI缓存么?)
            UIManager.GetInstance().HideAllUIForms();

            //生成地图


            //生成玩家
            GameObject obj = Resources.Load(ResourcesPath.KEY_PLAYER_PREFAB) as GameObject;
            GameObject creObj = Instantiate(obj, Vector2.zero, Quaternion.identity);
            GlobalData._Controller = creObj.GetComponent<PlayerController>();


            //发送游戏心跳包

        }

    }
}
