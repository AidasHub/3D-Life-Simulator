using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainScript : MonoBehaviour
{
    Transform PlayerTransform = null;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(PlayerTransform.position.x, transform.position.y, PlayerTransform.position.z);
        transform.position = newPos;

    }
}
