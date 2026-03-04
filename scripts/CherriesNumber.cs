using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CherriesNumber : MonoBehaviour
{
    public static CherriesNumber Instance;

    [Header("UI 引用")]
    [SerializeField] private Text totalCherriesText;

    public int totalCherriesCount = 0;

    private void Awake()
    {
        // 单例模式确保场景中只有一个 GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // 监听场景加载事件，确保每个场景都能统计樱桃
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // 初始统计当前场景的樱桃数量
        CountCherries();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 场景加载完成后重新统计樱桃
        CountCherries();
    }

    private void CountCherries()
    {
        // 查找所有带有 Cherry 标签的对象
        GameObject[] cherries = GameObject.FindGameObjectsWithTag("Cherry");
        totalCherriesCount = cherries.Length;

        // 更新 UI 显示
        UpdateCherriesUI();

        Debug.Log("当前场景樱桃总数: " + totalCherriesCount);
    }

    private void UpdateCherriesUI()
    {
        if (totalCherriesText != null)
        {
            totalCherriesText.text = "樱桃总数：" + totalCherriesCount;
        }
    }
}