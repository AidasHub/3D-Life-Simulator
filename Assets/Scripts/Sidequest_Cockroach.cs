using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidequest_Cockroach : MonoBehaviour
{
    ParticleSystem TriggerParticles = null;
    SpriteRenderer TriggerSprite = null;
    CameraController PlayerCamera = null;
    PlayerController PlayerController = null;
    [SerializeField]
    TextController TextController = null;

    [SerializeField]
    List<GameObject> Cockroaches;

    int CockroachesKilled;
    int CockroachTotal;

    // Start is called before the first frame update
    void Start()
    {
        TriggerParticles = GetComponentInChildren<ParticleSystem>();
        TriggerSprite = GetComponentInChildren<SpriteRenderer>();
        CockroachTotal = Cockroaches.Count;
        print("Total cockroaches " + CockroachTotal);
    }

    // Update is called once per frame
    void Update()
    {
        if(CockroachesKilled == CockroachTotal)
        {
            TextController.UpdateMonologue("Task Complete! Your reward: nothing; that's just life");
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerCamera = other.gameObject.GetComponentInChildren<CameraController>();
            PlayerController = other.gameObject.GetComponentInChildren<PlayerController>();

            //Disable the sidequest marker
            TriggerParticles.gameObject.SetActive(false);
            TriggerSprite.gameObject.SetActive(false);
            PlayerCamera.DisableCameraMovement();
            PlayerController.enabled = false;
            StartCoroutine(ReturnControlAfter(5));
            TextController.UpdateMonologue("New objective! Destroy all the pesky house intruders");
            foreach(GameObject GO in Cockroaches)
            {
                GO.SetActive(true);
            }
            CockroachesKilled = 0;
            GetComponent<BoxCollider>().enabled = false;

        }
    }

    IEnumerator ReturnControlAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        PlayerController.enabled = true;
        PlayerCamera.EnableCameraMovement();
    }

    public void IncreaseCockroachKillCount()
    {
        CockroachesKilled += 1;
        print("Added 1 cockroach");
    }
}
