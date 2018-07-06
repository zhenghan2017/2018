
using UnityEngine;

namespace Project
{
    public class GameRoot : MonoBehaviour
    {

        ////数据读取管理类
        //[HideInInspector]
        //public DBManager dbManager;

        ////页面管理器
        //[HideInInspector]
        //public PageManager pageManager;

        //设置按键管理器
        [HideInInspector]
        public SettingCommand settingCommand;

        private static object _lock = new object();
        private static GameRoot _Instance;
        public static GameRoot GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new GameObject("_GameRoot").AddComponent<GameRoot>();
            }
            return _Instance;
        }
        private void Awake()
        {
            if (_Instance == null)
            {
                _Instance = this;
                _Instance.Initialize();
            }
            else
            {
                Destroy(this);
                _Instance = null;
            }
            DontDestroyOnLoad(this);
        }

        void Initialize()
        {
            //场景初始化的时候需要添加的脚本
            //dbManager = gameObject.AddComponent<SqlManager>();
            //dbManager.Init();

            settingCommand = gameObject.AddComponent<SettingCommand>();
            settingCommand.Init();

        }
    }
}
