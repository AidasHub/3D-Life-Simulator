using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public PlayerController PlayerController;
    public CameraController CameraController;
    public Transform PlayerCamera;
    public Transform Player;
    float speed = 1.0f;
    Vector3 OldDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    bool rotating = false;
    // Update is called once per frame
    void Update()
    {
        if(!rotating)
        {
            if (Input.GetKey(KeyCode.O))
            {
                print("Dialogue initiated");
                PlayerController.enabled = false;
                CameraController.enabled = false;
                rotating = true;
                CameraController.DisableCameraMovement();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                print("Dialogue ended");
                PlayerController.enabled = true;
                CameraController.enabled = true;
                rotating = false;
                CameraController.EnableCameraMovement();
            }
        }
        else
        {
            // Determine which direction to rotate towards
            Vector3 targetDirection = transform.position - PlayerCamera.transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(PlayerCamera.transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(PlayerCamera.transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            PlayerCamera.transform.rotation = Quaternion.LookRotation(newDirection);

            if(OldDirection != null && OldDirection == newDirection)
            {
                rotating = false;
            }
            else
            {
                OldDirection = newDirection;
            }
        }
    }
}
