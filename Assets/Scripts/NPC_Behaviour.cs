using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Behaviour : MonoBehaviour
{
    NavMeshAgent Agent;
    Animator AgentAnimator;
    [SerializeField]
    List<Transform> Checkpoints;
    int NextCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        AgentAnimator = GetComponentInChildren<Animator>();
        Agent.SetDestination(Checkpoints[0].position);
        NextCheckpoint = 1;

    }

    float CurrentTime = 0f;
    [SerializeField]
    float RestTime = 3f;

    // Update is called once per frame
    void Update()
    {
        AgentAnimator.SetFloat("Speed", Agent.velocity.magnitude);
        if(Agent.remainingDistance == 0)
        {
            if(CurrentTime >= RestTime)
            {
                CurrentTime = 0f;
                Agent.destination = Checkpoints[NextCheckpoint].position;
                if (NextCheckpoint == Checkpoints.Count - 1)
                    NextCheckpoint = 0;
                else
                    NextCheckpoint++;
            }
            else
            {
                CurrentTime += Time.deltaTime;
            }

        }
    }
}
