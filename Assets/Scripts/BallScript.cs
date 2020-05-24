using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    TaskController TaskController;
    [SerializeField]
    bool isBall = false;
    [SerializeField]
    float FlyForce = 1f;
    [SerializeField]
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if(isBall)
        {
            rb = GetComponent<Rigidbody>();
        }
        else
        {
            TaskController = GameObject.Find("TaskList").GetComponent<TaskController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PunchBall(Vector3 force)
    {
        if(!isActiveAndEnabled)
        {
            return;
        }
        rb.AddForce(new Vector3(force.x, force.y*-1, force.z) * FlyForce);
        print("Ball punched with force");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActiveAndEnabled)
        {
            return;
        }
        if(!isBall && other.tag == "Ball")
        {
            TaskController.CompleteTask(3);
            GetComponent<AudioSource>().Play();
            other.GetComponent<BallScript>().enabled = false;
        }
        if(isBall && other.tag == "Player")
        {
            Vector3 MoveVector = other.transform.TransformDirection(Vector3.forward);
            print(MoveVector);
            rb.AddForce(MoveVector * FlyForce);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isActiveAndEnabled)
        {
            return;
        }
        if(isBall && other.tag == "Player")
        {
            Vector3 MoveVector = other.transform.TransformDirection(Vector3.forward);
            print(MoveVector);
            rb.AddForce(MoveVector * FlyForce/10);
        }
    }

}
