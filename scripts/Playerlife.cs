using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // 添加UI命名空间
using static AllControl;
using static item_collector;
using static PlayerMovement;
using static Gun;
public class Playerlife : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    public GameObject life01, life02, life03;
    public GameObject Gun;
    public int life = 3;
   
    [SerializeField] private AudioSource deathSoundEffect;
      // 保留但可能不再使用

    // 新增UI覆盖层相关变量
    public Image blinkOverlay;  // 拖入UI Image组件
    public float blinkInterval = 0.1f;  // 闪烁间隔

    // 闪烁相关参数
    public float blinkDuration = 1f;  // 闪烁持续时间
    public int blinkCount = 5;        // 闪烁次数
    private bool isBlinking = false;  // 是否正在闪烁

    // 无敌状态相关参数
    private bool isInvincible = false;  // 是否处于无敌状态
    public float invincibleDuration = 0.5f;  // 无敌持续时间

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // 初始化渲染器组件

        // 确保UI覆盖层初始状态为禁用
        if (blinkOverlay != null)
        {
            blinkOverlay.gameObject.SetActive(false);
        }
    }

    PlayerMovement movement = new PlayerMovement();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查是否处于无敌状态
        if (isInvincible)
            return;

        if (collision.gameObject.CompareTag("Trap")||collision.gameObject.CompareTag("Enemy"))
        {
            DamagePlayer();
            Life();
            if(life!=0)
            StartCoroutine(InvincibleCoroutine());
        }
        else if (collision.gameObject.CompareTag("BigTrap"))
        {
            life01.SetActive(false);
            life02.SetActive(false);
            life03.SetActive(false);
            deathSoundEffect.Play();
            Die();
        }
    }

    private void Die()
    {

        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");
        Destroy(Gun);
        Gamemanager.Instance.score = 0;
        Gamemanager.Instance.enemydeath = 0;
        Gamemanager.Instance.num = 0;
        Gamemanager.Instance.deathCount++; // 增加死亡次数
        Debug.Log("当前死亡次数: " + Gamemanager.Instance.deathCount);
    }

    public int DamagePlayer()
    {
        life--;
        return 0;
    }

    void Life()
    {
        if (life == 3)
        {
            life01.SetActive(true);
            life02.SetActive(true);
            life03.SetActive(true);
        }
        if (life == 2)
        {
            life01.SetActive(false);
            life02.SetActive(true);
            life03.SetActive(true);
        }
        if (life == 1)
        {
            life01.SetActive(false);
            life02.SetActive(false);
            life03.SetActive(true);
        }
        if (life == 0)
        {
            life01.SetActive(false);
            life02.SetActive(false);
            life03.SetActive(false);
            deathSoundEffect.Play();
            Die();
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// 无敌状态协程
    /// </summary>
    private IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;
        Debug.Log("进入无敌状态");

        // 启动UI覆盖层闪烁效果
        StartCoroutine(BlinkCoroutine());

        // 等待无敌时间结束
        yield return new WaitForSeconds(invincibleDuration);

        isInvincible = false;
        Debug.Log("无敌状态结束");
    }

    /// <summary>
    /// UI覆盖层闪烁效果协程
    /// </summary>
    private IEnumerator BlinkCoroutine()
    {
        if (blinkOverlay == null)
        {
            Debug.LogWarning("blinkOverlay未赋值，请在Inspector中拖入Image组件");
            yield break;
        }

        isBlinking = true;
        blinkOverlay.gameObject.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < blinkDuration)
        {
            blinkOverlay.enabled = !blinkOverlay.enabled;
            elapsedTime += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        // 确保结束时覆盖层被禁用
        blinkOverlay.enabled = false;
        blinkOverlay.gameObject.SetActive(false);
        isBlinking = false;
    }
}