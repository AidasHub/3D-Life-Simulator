using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Behaviour : MonoBehaviour
{
    enum AI_Type
    {
        Patrol,
        FollowPlayer
    }

    [SerializeField]
    AI_Type AIType;
    NavMeshAgent Agent;
    Animator AgentAnimator;
    [SerializeField]
    List<Transform> Checkpoints;
    int NextCheckpoint;

    GameObject Player;
    [SerializeField]
    GameObject Item;

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        AgentAnimator = GetComponentInChildren<Animator>();
        if(AIType == AI_Type.Patrol)
        {
            Agent.SetDestination(Checkpoints[0].position);
            NextCheckpoint = 1;
        }
        if(AIType == AI_Type.FollowPlayer)
        {
            Player = GameObject.Find("Player");
        }

    }

    float CurrentTime = 0f;
    [SerializeField]
    float RestTime = 3f;

    // Update is called once per frame
    void Update()
    {
        switch (AIType)
        {
            case (AI_Type.Patrol):
                AgentAnimator.SetFloat("Speed", Agent.velocity.magnitude);
                if (Agent.remainingDistance == 0)
                {
                    if (CurrentTime >= RestTime)
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
                break;
            case (AI_Type.FollowPlayer):
                AgentAnimator.SetFloat("Speed", Agent.velocity.magnitude);
                Agent.SetDestination(Player.transform.position);
                if((Agent.transform.position - Player.transform.position).magnitude < 5f)
                {
                    Agent.isStopped = true;
                    Item.transform.position = Agent.transform.position;
                }
                break;
        }
        
    }
}
