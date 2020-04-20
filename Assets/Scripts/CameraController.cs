using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField]
    float mouseSensitivity = 100f;
    [SerializeField]
    bool allowCameraMovment = true;

    Transform Player;

    float xRotation = 0f;

    private void Start()
    {
        Player = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        if(allowCameraMovment)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);


            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            Player.Rotate(Vector3.up * mouseX);
        }
    }

    public void EnableCameraMovement()
    {
        allowCameraMovment = true;
    }

    public void DisableCameraMovement()
    {
        allowCameraMovment = false;
    }


}