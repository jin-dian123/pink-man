using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static AllControl;

public class item_collector : MonoBehaviour
{
    
    [SerializeField] private Text cherriesText;
    [SerializeField] private AudioSource collectSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            Gamemanager.Instance.score++;
            cherriesText.text = "已收集:" + Gamemanager.Instance.score;

        }

    }
  
}
