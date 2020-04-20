using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CockroachBehaviour : MonoBehaviour
{
    SpriteRenderer SpriteRenderer = null;
    [SerializeField]
    Sprite SplatSprite;
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    Sidequest_Cockroach CockroachController;
    [SerializeField]
    float WanderRadius = 3;
    [SerializeField]
    float WanderTimer = 3;
    float Timer;


    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Timer = WanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= WanderTimer)
        {
            Vector3 newPos = Random.insideUnitSphere * WanderRadius;
            agent.SetDestination(newPos + transform.position);
            //agent.SetDestination(GameObject.FindWithTag("Player").transform.position); //Debugging
            //print(newPos);
            Timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!this.enabled)
            return;

        if(other.tag == "Player")
        {
            SpriteRenderer.sprite = SplatSprite;
            agent.enabled = false;
            CockroachController.IncreaseCockroachKillCount();
            this.enabled = false;
        }
    }
}
