
using UnityEngine;

namespace Project
{
    public class GameManager : MonoBehaviour
    {

        ////数据读取管理类
        //[HideInInspector]
        //public DBManager dbManager;

        ////页面管理器
        //[HideInInspector]
        //public PageManager pageManager;

        //设置按键管理器
        [HideInInspector]
        public SettingManager _SettingManager;

        private static object _lock = new object();
        private static GameManager _Instance;
        public static GameManager GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new GameObject("_GameManager").AddComponent<GameManager>();
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

            _SettingManager = gameObject.AddComponent<SettingManager>();
            _SettingManager.Init();

        }
    }
}
