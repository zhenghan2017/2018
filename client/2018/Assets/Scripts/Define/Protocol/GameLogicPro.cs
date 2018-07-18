using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Define.Socket
{
    public enum GameLogicPro
    {
        CMD_GAME_FIRST             = 10000, // 协议开始
        //请求的协议 c->s
        CMD_GAME_REQCREATEROOM     = CMD_GAME_FIRST + 1,    // 玩家请求建房
        CMD_GAME_REQJOINROOM       = CMD_GAME_FIRST + 3,    // 请求加入房间
        CMD_GAME_REQPLAYERDATA     = CMD_GAME_FIRST + 3,    // 请求玩家数据
        CMD_GAME_REQMOVE           = CMD_GAME_FIRST + 5,    // 玩家请求移动
        CMD_GAME_REQRELINK         = CMD_GAME_FIRST + 7,    // 玩家请求重连

        //接受的协议 s->c
        CMD_GAME_RESPCREATEROOM    = CMD_GAME_FIRST + 2,  
        CMD_GAME_RESPJOINROOM      = CMD_GAME_FIRST + 4,
        CMD_GAME_RESPPLAYERDATA    = CMD_GAME_FIRST + 6,
        CMD_GAME_RESPMOVE          = CMD_GAME_FIRST + 8,
        CMD_GAME_RESPRELINK        = CMD_GAME_FIRST + 10,

        CMD_GAME_REQHEART          = CMD_GAME_FIRST + 1111, // 请求游戏心跳
        CMD_GAME_RESPHEART         = CMD_GAME_REQHEART + 1, 

        CMD_GAME_LAST              = 11112,//协议结束
    }
}

//协议部分
//玩家请求建房 ->  建房成功后 ，开启游戏心跳，请求玩家相关数据 -> 接受到玩家数据的时候，游戏中显示相关的玩家信息