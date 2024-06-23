using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] int sensHori;
    [SerializeField] int sensVert;

    [SerializeField] int lockVertMin;
    [SerializeField] int lockVettMax;
    [SerializeField] bool invert;

    float xRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (gameManager.instance.isPaused == false)
        {
            //Get input
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensHori;
            float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensVert;

            if (invert)
            {
                xRotation += mouseY;
            }
            else
            {
                xRotation -= mouseY;
            }

            //Clamp Rotation
            xRotation = Mathf.Clamp(xRotation, lockVertMin, lockVettMax);

            //Rotate Camera on X Axis
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

            //Rotate the player
            transform.parent.Rotate(Vector3.up * mouseX); 
        }
    }
}
