using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    [SerializeField]
    bool Drive = false;
    float SpeedModifier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Drive == true)
        {
            transform.position += Vector3.left * 0.35f * Mathf.Log(SpeedModifier);
        }
        if(transform.position.x < -65)
        {
            Drive = false;
            gameObject.SetActive(false);
        }
    }

    public void SpawnCar(float magnitude)
    {
        SpeedModifier = magnitude;
        transform.position = transform.parent.position;
        Drive = true;
    }
}
