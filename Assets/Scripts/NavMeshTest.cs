using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]
    float WanderRadius = 3;
    [SerializeField]
    float WanderTimer = 3;
    float Timer;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        Timer = WanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= WanderTimer)
        {
            Vector3 newPos = Random.insideUnitSphere * WanderRadius;
            agent.SetDestination(newPos + transform.position);
            print(newPos);
            Timer = 0;
        }
    }
}
