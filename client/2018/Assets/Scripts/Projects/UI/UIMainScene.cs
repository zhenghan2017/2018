using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Define.Socket;
using Define;

namespace UIForm
{
    public class UIMainScene : UIObject
    {
        void Awake()
        {
            RegisterButtonObjectEvent("Btn_Set",
                p => OpenUIForm(UIDefine.UI_MAIN_SETTING_FORM)
            );

            RegisterButtonObjectEvent("Btn_Join",
                ReqJoinRoom
            );

            RegisterButtonObjectEvent("Btn_Create",
                ReqCreateRoom
            );

        }

        private void ReqJoinRoom(GameObject go)
        {
            MessageCenter.AddListener<sEvent_NetMessageData>(MsgDefine.Msg_RespNetMessage, RespRoomInfoCall);
            CMD_GAME_REQJOINROOM _data = new CMD_GAME_REQJOINROOM();
            _data.playerid = 111111; //
            _data.roomid = 123456;
            SocketManager.Instance.SendMsg(GameLogicPro.CMD_GAME_REQJOINROOM, _data);
        }


        private void ReqCreateRoom(GameObject go)
        {
            MessageCenter.AddListener<sEvent_NetMessageData>(MsgDefine.Msg_RespNetMessage, RespRoomInfoCall);
            CMD_GAME_REQCREATEROOM _data = new CMD_GAME_REQCREATEROOM();
            _data.playerid = 111111; //
            SocketManager.Instance.SendMsg(GameLogicPro.CMD_GAME_REQCREATEROOM, _data);
        }

        private void RespRoomInfoCall(sEvent_NetMessageData data)
        {
            if((GameLogicPro)data._eventType == GameLogicPro.CMD_GAME_RESPJOINROOM)
            {
                

            }
            else if((GameLogicPro)data._eventType == GameLogicPro.CMD_GAME_RESPCREATEROOM)
            {

            }
            else
            {
                return;
            }

            MessageCenter.RemoveListener<sEvent_NetMessageData>(MsgDefine.Msg_RespNetMessage, RespRoomInfoCall);
            JoinRoom();
        }

        //切换场景
        private void JoinRoom()
        {
            SceneManager.LoadScene(1);
        }

    }

}
