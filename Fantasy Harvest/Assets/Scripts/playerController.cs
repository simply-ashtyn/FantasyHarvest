using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
//using TMPro;

public class playerController : MonoBehaviour
{
    [Header("----Player Attributes----")]
    [SerializeField] CharacterController controller;
    [SerializeField] float playerSpeed;
    [SerializeField] float playerJumpHeight;
    [SerializeField] float gravityValue;
    [SerializeField] float turnSmoothTime;

    [Header("----Spell Attributes----")]
    [SerializeField] GameObject spell;
    [SerializeField] float spellUseRate;
    [SerializeField] int spellRange;
    [SerializeField] int spellDamage;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    Vector3 move;
    bool spellActive;

    private void Start()
    {

    }

    void Update()
    {
        if (gameManager.instance.isPaused == false)
        {
            Movement();
            StartCoroutine(Shoot()); 
        }
    }

    void Movement()
    {
        if (controller.transform.transform.position.y < 0.01)
        {
            groundedPlayer = true;
        }

        if (groundedPlayer && playerVelocity.y < 0.5)
        {
            playerVelocity.y = 0f;
        }

        move = (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y = playerJumpHeight;
            groundedPlayer = false;
        }

        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    IEnumerator Shoot()
    {
        if (!spellActive && Input.GetButton("Shoot"))
        {
            spellActive = true;

            //Did it hit something
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, spellRange))
            {
                /// LONG DIST SHOOTING FOR ATTACKS
                if (hit.collider.GetComponent<IDamageable>() != null)
                {
                    hit.collider.GetComponent<IDamageable>().TakeDamage(spellDamage);
                }




                /// DETERMINE ACTION BASED ON ITEM HIT
                //if (!hit.transform.CompareTag("")) ; // plant, enemy, grass, etc
                //else  Destroy(hit.transform.gameObject); // used for harvesting/cutting grass


                //create object where the player is pointing and is facing
                // Instantiate(spell, hit.point, transform.rotation); // spell.transform.rotation - uses the items rotation not the players (needed for spells?)
            }

            yield return new WaitForSeconds(spellUseRate);
            spellActive = false;
        }
    }
}