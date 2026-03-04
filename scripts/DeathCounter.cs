using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static AllControl;

public class DeathCounterUI : MonoBehaviour
{
    public Text deathCountText; // 用于显示死亡次数的UI文本

    void Start()
    {
        UpdateDeathCountUI();
    }

    // 更新死亡次数UI
    public void UpdateDeathCountUI()
    {
        if (deathCountText != null&& SceneManager.GetActiveScene().buildIndex!=4)
        {
            deathCountText.text = "你已死亡: " + Gamemanager.Instance.deathCount+"次";
        }
        else if(deathCountText != null && SceneManager.GetActiveScene().buildIndex== 4)
        {
            deathCountText.text = "你的死亡次数是: " + Gamemanager.Instance.deathCount;
        }

    }
}