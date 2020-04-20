using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField]
    List<CarScript> Cars;
    [SerializeField]
    GameObject Player;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float CurrentTime = 1f;
    [SerializeField]
    float CarCooldown = 1f;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (CurrentTime >= CarCooldown)
            {
                if (index >= Cars.Count - 1)
                {
                    index = 0;
                }
                Cars[index].gameObject.SetActive(true);
                Cars[index].SpawnCar((transform.position - Player.transform.position).magnitude);
                index++;
                CurrentTime = 0f;
            }
            else
            {
                CurrentTime += Time.deltaTime;
            }
        }
    }
}
