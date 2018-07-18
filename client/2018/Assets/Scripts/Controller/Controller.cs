using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;


public abstract class Controller : MonoBehaviour
{
    //移动命令模块
    public abstract void onCmd_Controller_Move(Vector2 dir);
}