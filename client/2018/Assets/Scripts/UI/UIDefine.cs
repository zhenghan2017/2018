using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIForm
{
    public class UIDefine
    {
        /* 路径常量 */
        public const string UI_PATH_CANVAS = "UICanvas";
        public const string UI_PATH_UIFORMS_CONFIG_INFO = "JsonConfig/UIFormsConfigInfo";
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

        /* 常量定义： UI窗体名称 */
        //测试用
        public const string LOGON_FROMS = "UILoginForm";
        public const string MAIN_CITY_UIFORM = "MainCityUIForm";
        public const string HERO_INFO_UIFORM = "HeroInfoUIForm";
        public const string SELECT_HERO_FORM = "UISelectHeroForm";
        public const string PRO_DETAIL_UIFORM = "PropDetailUIForm";
        public const string MARKET_UIFORM = "MarketUIFrom";
        //正式
        public const string UI_MAIN_SCENE_FORM = "UIMainSceneForm";   //主场景UI
        public const string UI_MAIN_SETTING_FORM = "UIMainSettingForm";  //主场景设置界面



        /* 摄像机层深的常量 */

        /* 全局性的方法 */
        //Todo...

        /* 委托的定义 */
        //Todo....
    }
}
