using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using Define;
using System;
using System.IO;

delegate void KeyCmd();


enum CMD_KEY
{
    CMD_KEY_UP = 0,
    CMD_KEY_RIGHT,
    CMD_KEY_DOWN,
    CMD_KEY_LEFT,

}

//需要将保存本地的json文件合成一个文件
public class SettingManager : MonoBehaviour
{
    //索引匹配对应的方法功能
    List<KeyCode> keyList;

    Dictionary<string, string> configMgr;
    //当前的按键
    KeyCode currentKey;

    private static SettingManager _Instance = null;

    private SettingManager() { }

    public static SettingManager GetInstance()
    {
        if (_Instance == null)
        {
            _Instance = new SettingManager();
        }

        return _Instance;
    }

    //Dictionary<int, List<int>> SecneKeyDic;

    public void Init()
    {
        _Instance = this;
        //SecneKeyDic = new Dictionary<int, List<int>>();
        keyList = new List<KeyCode>();

        //读取 本地保存的配置文件
        configMgr = ToolUtils.getConfigByJson(ResourcesPath.KEY_SETTING_CONFIG);
        foreach (var temp in configMgr)
        {
            if (temp.Value == null) continue;
            keyList.Add((KeyCode)int.Parse(temp.Value));
            //int index = int.Parse(temp.Key);
            //keyList[index] = temp.Value;
        }
        //  MessageCenter.AddListener<string>(MsgDefine.Msg_KEYSETTING, ExcuteCmd);
    }

    private void Awake()
    {

        Init();
    }

    public void UpdatePlayerCmd()
    {
        //CMD_KEY_UP = 0,
        //CMD_KEY_RIGHT,
        //CMD_KEY_DOWN,
        //CMD_KEY_LEFT,

        //写死
        Vector2 dir = new Vector2(0, 0); // 下:-1 上:1 左：-3 右：3 静止：0
        int curCmdKey = 0;
        if (Input.GetKey(keyList[0]))
        {
            dir.y += 1;
        }
        if (Input.GetKey(keyList[1]))
        {
            dir.x += 1;
        }
        if (Input.GetKey(keyList[2]))
        {
            dir.y += -1;
        }
        if (Input.GetKey(keyList[3]))
        {
            dir.x += -1;
        }

        // Debug.Log(Convert.ToInt32(Input.GetKey(keyList[0])));

        switch (curCmdKey)
        {
            case 0: //移动的实现
                GlobalData._Controller.onCmd_Controller_Move(dir);
                break;
        }


        //  return (int)(dir.x * 3 + dir.y);
    }

    public bool SaveSetFile()
    {
        string filePath = Application.dataPath + @"/Resources/Key.json";
        return ToolUtils.SetFile(configMgr, filePath);
    }

    //private void ExcuteCmd(string key)
    //{
    //    int index = keyList.FindIndex(item=>item.Equals(key));

    //    if (index == null) return;

    //    switch ((CMD_KEY)index)
    //    {
    //        case CMD_KEY.CMD_KEY_UP:
    //        case CMD_KEY.CMD_KEY_DOWN:
    //        case CMD_KEY.CMD_KEY_LEFT:
    //        case CMD_KEY.CMD_KEY_RIGHT:
    //           // MoveCmd(index);
    //            break;
    //    }

    //}


    //void OnGUI()
    //{
    //    if (Input.anyKey)
    //    {
    //        Event e = Event.current;
    //        if (e.isKey )
    //        {
    //            //if (!keyList.Contains(e.keyCode.ToString())) return;
    //            if(!keyDownList.ContainsKey(e.keyCode))
    //            {
    //                keyDownList.Add(e.keyCode, true);
    //            }
    //            else
    //            {
    //                keyDownList[e.keyCode] = true;
    //            }
    //        }
    //    }
    //}
}

