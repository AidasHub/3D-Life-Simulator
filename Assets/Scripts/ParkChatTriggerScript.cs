using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkChatTriggerScript : MonoBehaviour
{
    [SerializeField]
    TextController TextController;

    private void OnTriggerEnter(Collider other)
    {
        if(!enabled)
        {
            return;
        }
        if(other.tag == "Player")
        {
            TextController.UpdateMonologue("The simulation, unfortunately, does not have enough RAM to load the seemingly intriguing group conversation");
            enabled = false;
        }
    }
}
