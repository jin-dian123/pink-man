using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static AllControl;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Text enemyText;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource deathSoundEffect;
    public int health;
    
   public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
   public void Update()
    {
       if(health <= 0)
        {
            
            deathSoundEffect.Play();
            anim.SetTrigger("Death");
        }
    }
    public void TakeDamage()
    {
        health--;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Trap")|| collision.gameObject.CompareTag("Bullet"))
        {
            
            anim.SetTrigger("Death");
            Gamemanager.Instance.enemydeath++;
            enemyText.text = "已击败：" + Gamemanager.Instance.enemydeath;
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
