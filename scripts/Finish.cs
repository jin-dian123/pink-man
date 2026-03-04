using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AllControl;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private bool levelCompleted=false;
    void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" &&!levelCompleted&&CherriesNumber.Instance.totalCherriesCount == Gamemanager.Instance.score)
        {
            finishSound.Play();
            levelCompleted= true;
            Invoke("CompleteLevel", 2f);
        }
    }
    // Update is called once per frame
  private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Gamemanager.Instance.score = 0;
    }
}
