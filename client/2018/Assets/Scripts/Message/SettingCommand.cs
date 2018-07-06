using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;
using Define;

delegate void KeyCmd();

namespace Project
{
    enum CMD_KEY
    {
        CMD_KEY_UP = 0,
        CMD_KEY_RIGHT,
        CMD_KEY_DOWN,
        CMD_KEY_LEFT,
        
    }


    public class SettingCommand : MonoBehaviour
    {
        //索引匹配对应的方法功能
        List<KeyCode> keyList;

        Dictionary<KeyCode,bool> keyDownList;
        //当前的按键
        KeyCode currentKey;

        private static SettingCommand _Instance = null;

        private SettingCommand() { }

        public static SettingCommand GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new SettingCommand();
            }

            return _Instance;
        }

        //Dictionary<int, List<int>> SecneKeyDic;

        public void Init()
        {
            _Instance = this;
            //SecneKeyDic = new Dictionary<int, List<int>>();
            keyList = new List<KeyCode>();

            keyDownList = new Dictionary<KeyCode, bool>();

            //读取 本地保存的配置文件
            Dictionary<string, string> configMgr = ToolUtils.getConfigByJson(ResourcesPath.KEY_SETTING_CONFIG);
            foreach (var temp in configMgr)
            {
                if (temp.Value == null) continue;
                keyList.Add((KeyCode)int.Parse(temp.Value));
                //int index = int.Parse(temp.Key);
                //keyList[index] = temp.Value;
            }

            MessageCenter.AddListener<string>(MsgDefine.Msg_KEYSETTING, ExcuteCmd);
        }

        private void Awake()
        {

            Init();
        }

        //private void FixedUpdate()
        //{
        //    //if (keyDownList.Count == 0) return;
        //    //List<KeyCode> removeKeyList = new List<KeyCode>();
        //    //foreach (var temp in keyDownList)
        //    //{
        //    //    if(keyList.Contains(temp.Key.ToString()) && temp.Value)
        //    //    {
        //    //       // ExcuteCmd(temp.Key.ToString());
        //    //    }

        //    //    if (Input.GetKeyUp(temp.Key))
        //    //    {
        //    //        removeKeyList.Add(temp.Key);
        //    //    }
        //    //}

        //    ////删除
        //    //foreach (var rItem in removeKeyList)
        //    //{
        //    //    keyDownList.Remove(rItem);
        //    //}


        //    //Debug.Log(keyDownList.Count);

           
        //}

        public void UpdatePlayerCmd()
        {
            //CMD_KEY_UP = 0,
            //CMD_KEY_RIGHT,
            //CMD_KEY_DOWN,
            //CMD_KEY_LEFT,

             //写死
            Vector2 dir = new Vector2(0,0); // 下:-1 上:1 左：-3 右：3 静止：0
            if(Input.GetKey(keyList[0])) 
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
            //移动的实现
            MoveCmd(dir);

          //  return (int)(dir.x * 3 + dir.y);
        }


        private void ExcuteCmd(string key)
        {
            int index = keyList.FindIndex(item=>item.Equals(key));

            if (index == null) return;

            switch ((CMD_KEY)index)
            {
                case CMD_KEY.CMD_KEY_UP:
                case CMD_KEY.CMD_KEY_DOWN:
                case CMD_KEY.CMD_KEY_LEFT:
                case CMD_KEY.CMD_KEY_RIGHT:
                   // MoveCmd(index);
                    break;
            }

        }

        private void MoveCmd(Vector2 dir)
        {
            if (GlobalData.playerCpn.transform == null) return;

            int movedir = (int)(dir.x * 3 + dir.y);
            //设置朝向精灵
            //GlobalData.playerCpn.spriteRender.sprite
            //设置动画
            Debug.Log(movedir);
            GlobalData.playerCpn.setPlayerAnim(movedir);
            

            if (dir == Vector2.zero) return;

            Vector2 targetPos = new Vector2(
                GlobalData.playerCpn.transform.position.x, GlobalData.playerCpn.transform.position.y
                ) + dir;

            GlobalData.playerCpn.rigidbody.MovePosition(
                Vector2.Lerp(GlobalData.playerCpn.rigidbody.position, targetPos, GlobalData.playerCpn.getSpeed() * Time.deltaTime));
        }


        void OnGUI()
        {
            if (Input.anyKey)
            {
                Event e = Event.current;
                if (e.isKey )
                {
                    //if (!keyList.Contains(e.keyCode.ToString())) return;
                    if(!keyDownList.ContainsKey(e.keyCode))
                    {
                        keyDownList.Add(e.keyCode, true);
                    }
                    else
                    {
                        keyDownList[e.keyCode] = true;
                    }
                }
            }
        }
    }
}

