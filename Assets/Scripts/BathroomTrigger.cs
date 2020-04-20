using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomTrigger : MonoBehaviour
{
    [SerializeField]
    TextController TextController = null;
    
    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        TextController.UpdateMonologue("This is MY bathroom. There are many like it, but this one's mine.");
        transform.gameObject.SetActive(false);
    }
}
