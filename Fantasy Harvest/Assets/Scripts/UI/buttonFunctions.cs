using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{
    /// PAUSE MENU
    public void Resume()
    {
        if (gameManager.instance.isPaused)
        {
            gameManager.instance.isPaused = false;
            gameManager.instance.CursorLock();
        }
    }

    public void Options()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
