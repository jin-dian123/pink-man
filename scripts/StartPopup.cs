using UnityEngine;
using UnityEngine.UI;

public class StartPopup : MonoBehaviour
{
    // 弹出框Panel引用
    public GameObject popupPanel;

    // 确认按钮引用
    public Button confirmButton;

    // 可选：弹出框消息文本引用
    public Text messageText;

    // 可选：自定义消息内容
  

    void Start()
    {
        // 确保引用已设置
        if (popupPanel == null)
        {
            Debug.LogError("请在Inspector中设置popupPanel引用！");
            return;
        }

        if (confirmButton == null)
        {
            Debug.LogError("请在Inspector中设置confirmButton引用！");
            return;
        }

        // 设置消息文本（如果有）
      

        // 添加确认按钮点击事件监听
        confirmButton.onClick.AddListener(OnConfirmButtonClick);

        // 场景加载时自动显示弹出框
        ShowPopup();
    }

    // 显示弹出框
    private void ShowPopup()
    {
        popupPanel.SetActive(true);
        // 可选：暂停游戏
        Time.timeScale = 0;
    }

    // 隐藏弹出框
    private void HidePopup()
    {
        popupPanel.SetActive(false);
         Time.timeScale = 1;
    }

    // 确认按钮点击处理
    public void OnConfirmButtonClick()
    {
        HidePopup();
        // 这里可以添加确认后的其他逻辑
        Debug.Log("用户已确认，游戏继续");
    }
}