using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Umbrella : MonoBehaviour
{
    RainScript RainScript;
    [SerializeField]
    float RotationSpeed;

    [SerializeField]
    GameObject OpenUmbrellaPrefab;
    // Start is called before the first frame update
    void Start()
    {
        RainScript = GameObject.Find("Rain").GetComponent<RainScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAroundLocal(Vector3.up, 1 * RotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("PLAYER?");
        if(other.tag == "Player")
        {
            print("PLAYER!");
            RainScript.StopRain(3);
            GameObject OpenUmbrella = Instantiate(OpenUmbrellaPrefab, other.transform);
            OpenUmbrella.transform.localPosition += (((Vector3.forward*2/3) + (Vector3.right/3))+(Vector3.up*0.5f)); 
            Destroy(this.gameObject);
            Destroy(OpenUmbrella.gameObject, 5);
        }
    }
}
