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
  
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    Vector3 move;

    private void Start()
    {

    }

    void Update()
    {       
        if (controller.transform.transform.position.y < 0.01) {
           groundedPlayer = true;
        }

        if (groundedPlayer && playerVelocity.y < 0.5)
        {
            playerVelocity.y = 0f;
        }

        move = (transform.right * Input.GetAxis("Horizontal") + transform.forward* Input.GetAxis("Vertical"));
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
}