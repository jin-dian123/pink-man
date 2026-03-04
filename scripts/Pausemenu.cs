using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AllControl;

public class Pausemenu : MonoBehaviour
{
    [SerializeField] GameObject pausemenu;
    public void Pause()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
    }
   public void Resume()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Gamemanager.Instance.score = 0;
    }
    public void Home()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        Gamemanager.Instance.score = 0;
    }
}
