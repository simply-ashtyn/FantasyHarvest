using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playerController : MonoBehaviour, IDamageable
{
    [Header("----Player Attributes----")]
    [SerializeField] CharacterController controller;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Animator animator;
    [SerializeField] float playerHP;
    [SerializeField] float playerSpeed;
    [SerializeField] float playerJumpHeight;
    [SerializeField] float gravityValue;
    float originalHP;

    [Header("----Spell Attributes----")]
    [SerializeField] GameObject spell;
    [SerializeField] ParticleSystem spellEffect;
    [SerializeField] float spellUseRate;
    [SerializeField] int spellRange;
    [SerializeField] int spellDamage;

    [Header("----Sword Attributes----")]
    [SerializeField] Collider swordCollider;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject staff;
    [SerializeField] float swingRate;

    [Header("----Trigger Action----")]
    [SerializeField] string onSwing;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    Vector3 move;
    bool spellActive;
    private bool isSwinging;

    private void Start()
    {
        originalHP = playerHP; // Bug potential with save system, will need to update
    }

    void Update()
    {
        Movement();
        StartCoroutine(ActivateSpell());
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

    public void PlayerRespawn()
    {
        controller.enabled = false;
        transform.position = gameManager.instance.playerSpawnPoint.transform.position;
        controller.enabled = true;
        if (playerHP == 0)
            playerHP = originalHP;
       // gameManager.instance.CursorLock();
    }

    private void TriggerAction(string action)
    {
        if (action == "")
        {
            return;
        }

        //foreach (DialogTrigger trigger in GetComponents<DialogTrigger>())
        //{
        //    trigger.Trigger(action);
        //}
    }

    /// FOR WORK RELATED SPELLS, NOT FOR "SHOOTING" ENEMIES
    IEnumerator ActivateSpell()
    {
        if (!spellActive && Input.GetButton("Shoot"))
        {
            spellActive = true;

            //Did it hit something
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, spellRange))
            {
                /// LONG DIST SHOOTING FOR ATTACKS
                if (hit.collider.GetComponent<IDamageable>() != null)
                {
                    hit.collider.GetComponent<IDamageable>().TakeDamage(spellDamage);
                    //spellEffect.Play();
                }




                /// DETERMINE ACTION BASED ON ITEM HIT
                //if (!hit.transform.CompareTag("")) ; // plant, enemy, grass, etc
                //else  Destroy(hit.transform.gameObject); // used for harvesting/cutting grass


                //create object where the player is pointing and is facing
                Instantiate(spell, hit.point, transform.rotation); // spell.transform.rotation - uses the items rotation not the players (needed for spells?)
            }

            yield return new WaitForSeconds(spellUseRate);
            spellActive = false;
        }
    }

    public void TakeDamage(int dmg)
    {
        playerHP -= dmg;
        StartCoroutine(DamageFlash());

        if (playerHP <= 0)
        {
            gameManager.instance.isDead = true;
        }
    }

    IEnumerator DamageFlash()
    {
        gameManager.instance.playerDamaged.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gameManager.instance.playerDamaged.SetActive(false);
    }

    IEnumerator Swing()
    {
        if (!isSwinging && playerInput.actions["Attack"].triggered)
        {
            isSwinging = true;
            swordCollider.enabled = true;
            animator.SetInteger("SwordAttack", Random.Range(1, 4));

            yield return new WaitForSeconds(swingRate);
            isSwinging = false;
            animator.SetInteger("SwordAttack", 0);

            TriggerAction(onSwing);
        }
    }
}