using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{
    /// PAUSE MENU
    public void Resume()
    {
        gameManager.instance.inventoryOpen = false;
        gameManager.instance.isPaused = false;
        gameManager.instance.CursorLock();
    }

    public void Options()
    {

    }

    public void Respawn()
    {
        gameManager.instance.playerController.PlayerRespawn();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void DebugDeath()
    {
        gameManager.instance.isDead = true;
    }
}
