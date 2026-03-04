using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static AllControl;
public class enemydeathnum : MonoBehaviour
{
    [SerializeField] private Text enemyText;
   
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamemanager.Instance.enemydeath != Gamemanager.Instance.num)
        {
            enemyText.text = "已击败：" + Gamemanager.Instance.enemydeath;
            Gamemanager.Instance.num = Gamemanager.Instance.enemydeath;
        }
    }
}
