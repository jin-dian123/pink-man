using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AllControl;

public class EndMenu : MonoBehaviour
{
  public void ReloadGame()
    {
        SceneManager.LoadScene(0);
        Gamemanager.Instance.score = 0;
    }
}
