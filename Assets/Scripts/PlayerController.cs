using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    private Rigidbody rb;
    private GameObject PlayerAnimatorGameObject;
    private Animator PlayerAnimator;
    [SerializeField]
    private new Camera camera;
    private RaycastHit hit;
    [SerializeField]
    float reachDistance = 0;

    [SerializeField]
    float movementSpeed = 1f;
    [SerializeField]
    float gravity = -9.81f;
    [SerializeField]
    float jumpHeight = 3f;

    Vector3 velocity;
    public LayerMask LayerMask;

    private void Start()
    {
        camera = Camera.main;
        rb = GetComponent<Rigidbody>();
        PlayerAnimatorGameObject = GameObject.Find("FirstPersonAnimations");
        PlayerAnimator = PlayerAnimatorGameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;

        controller.Move(movement * movementSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerAnimator.Play("Punch");
            if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, reachDistance, LayerMask))
            {
                if(hit.collider.tag == "Mirror")
                {
                    hit.collider.gameObject.GetComponentInChildren<MirrorScript>().Shatter();
                }
            }
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log(hit.collider.name);
    }
}