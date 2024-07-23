using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    public GameObject player;
    public playerController playerController;
    public GameObject playerSpawnPoint;

    public GameObject menuManager;

    public GameObject playerDamaged;

    // PAUSE MENU ITMES
    public GameObject pauseMenu;
    public bool isPaused;
    float timeScaleOrig;

    // DEATH MENU ITEMS
    public GameObject deathMenu;
    public bool isDead;

    // INVENTORY MENU ITEMS
    public GameObject inventoryMenu;
    public bool inventoryOpen;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<playerController>();
        playerSpawnPoint = GameObject.Find("PlayerSpawnPoint");
        playerController.PlayerRespawn();

        timeScaleOrig = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Input.GetButtonDown("Cancel") && !inventoryOpen)
            {
                isPaused = !isPaused;
                menuManager = pauseMenu;
                menuManager.SetActive(isPaused);

                if (isPaused)
                {
                    CursorUnlock();
                }
                else
                    CursorLock();
            }

            if (Input.GetButtonDown("Inventory") && !isPaused)
            {
                inventoryOpen = !inventoryOpen;
                menuManager = inventoryMenu;
                menuManager.SetActive(inventoryOpen);

                if (inventoryOpen)
                {
                    CursorUnlock();
                }
                else
                    CursorLock();
            }
        }

        else
        {
            menuManager = deathMenu;
            menuManager.SetActive(true);
            CursorUnlock();
        }
    }

    public void CursorUnlock()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }

    public void CursorLock()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = timeScaleOrig;
        if (menuManager != null)
        {
            menuManager.SetActive(false);
            menuManager = null;
        }
    }
}
