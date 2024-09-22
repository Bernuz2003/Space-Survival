using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play_button : MonoBehaviour
{
    
    public void ClickPlay()
    {
        GameController.gameover = false;
        SceneManager.LoadScene(1);
    }
}
