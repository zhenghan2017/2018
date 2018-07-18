using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Define
{
    public class GameConst
    {
        public const string IP = "127.0.0.1";
        public const int Port = 9876;
    }

    public class MsgDefine
    {
        public const string Msg_RespNetMessage = "Msg_RespNetMessage";
    }

    public class ResourcesPath
    {
        public const string KEY_SETTING_CONFIG = "JsonConfig/KeySettingConfig"; //动态绑定的数据


        //游戏场景中
        public const string KEY_PLAYER_PREFAB  = "Prefabs/PlayerPrefab"; //玩家预制件
    }

}