using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllControl : MonoBehaviour
{
    // 确保场景切换时不被销毁
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public class Gamemanager
    {
        private static Gamemanager _instance;
        public static Gamemanager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Gamemanager();
                return _instance;
            }
        }
        public int score = 0;
        public int deathCount = 0;
        public int enemydeath = 0;
        public int num = 0;// 死亡次数统计变量
    }
}