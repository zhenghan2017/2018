using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIForm
{
    #region 系统枚举类型

    /// <summary>
    /// UI窗体（位置）类型
    /// </summary>
    public enum UIFormType
    {
        //普通窗体
        Normal,
        //固定窗体                              
        Fixed,
        //弹出窗体
        PopUp
    }

    /// <summary>
    /// UI窗体的显示类型
    /// </summary>
    public enum UIFormShowMode
    {
        //普通
        Normal,
        //反向切换
        ReverseChange,
        //隐藏其他
        HideOther
    }

    /// <summary>
    /// UI窗体透明度类型
    /// </summary>
    public enum UIFormLucenyType
    {
        //完全透明，不能穿透
        Lucency,
        //半透明，不能穿透
        Translucence,
        //低透明度，不能穿透
        ImPenetrable,
        //可以穿透
        Pentrate
    }

    #endregion

    public class UIDefine
    {
        /* 路径常量 */
        public const string UI_PATH_CANVAS = "Canvas";
        public const string UI_PATH_UIFORMS_CONFIG_INFO = "UIFormsConfigInfo";
        public const string UI_PATH_CONFIG_INFO = "UIConfigInfo";

        /* 标签常量 */
        public const string UI_TAG_CANVAS = "_TagCanvas";
        /* 节点常量 */
        public const string UI_NORMAL_NODE = "Normal";
        public const string UI_FIXED_NODE = "Fixed";
        public const string UI_POPUP_NODE = "PopUp";
        public const string UI_SCRIPTMANAGER_NODE = "_ScriptMgr";
        /* 遮罩管理器中，透明度常量 */
        public const float UI_UIMASK_LUCENCY_COLOR_RGB = 255 / 255F;
        public const float UI_UIMASK_LUCENCY_COLOR_RGB_A = 0F / 255F;

        public const float UI_UIMASK_TRANS_LUCENCY_COLOR_RGB = 220 / 255F;
        public const float UI_UIMASK_TRANS_LUCENCY_COLOR_RGB_A = 50F / 255F;

        public const float UI_UIMASK_IMPENETRABLE_COLOR_RGB = 50 / 255F;
        public const float UI_UIMASK_IMPENETRABLE_COLOR_RGB_A = 200F / 255F;

        /* 摄像机层深的常量 */

        /* 全局性的方法 */
        //Todo...

        /* 委托的定义 */
        //Todo....
    }

    public class UIType
    {
        //是否清空“栈集合”
        public bool IsClearStack = false;
        //UI窗体（位置）类型
        public UIFormType UIForms_Type = UIFormType.Normal;
        //UI窗体显示类型
        public UIFormShowMode UIForms_ShowMode = UIFormShowMode.Normal;
        //UI窗体透明度类型
        public UIFormLucenyType UIForm_LucencyType = UIFormLucenyType.Lucency;

    }
}

