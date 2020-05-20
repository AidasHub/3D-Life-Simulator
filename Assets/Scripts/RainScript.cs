using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainScript : MonoBehaviour
{
    [SerializeField]
    GameObject CloudSprite;
    RectTransform CloudTransform;
    [SerializeField]
    float CloudSpriteMoveSpeed;
    Transform PlayerTransform = null;
    ParticleSystem particles;
    [SerializeField]
    AudioSource AudioSource;
    [SerializeField]
    AudioClip RainSound;

    bool StartRaining = false;
    bool ReachedPos1 = false;
    bool ReachedPos2 = false;
    float Pos0;
    float Pos1 = 0.8f; //609 Y
    float Pos2 = 1000f; //0.8 X
    // Start is called before the first frame update
    void Start()
    {
        Pos0 = CloudSprite.transform.position.x;
        particles = GetComponent<ParticleSystem>();
        CloudTransform = CloudSprite.GetComponent<RectTransform>();
        PlayerTransform = GameObject.FindWithTag("Player").transform;
        ReachedPos1 = false;
        ReachedPos2 = false;
        particles.Stop();
        StartCoroutine(StartDelay(2));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(PlayerTransform.position.x, transform.position.y, PlayerTransform.position.z);
        transform.position = newPos;

        if (StartRaining && ReachedPos1 == false)
        {
            if (CloudTransform.localPosition.x <= Pos1)
            {
                Vector3 cloudPos = CloudTransform.position;
                var speed = ((Pos1 - Pos0) / 2) * Time.deltaTime;
                cloudPos.x += speed * CloudSpriteMoveSpeed;
                CloudTransform.position = cloudPos;
            }
            else
            {
                StartRaining = false;
                AudioSource.clip = RainSound;
                AudioSource.Play();
                StartCoroutine(WaitBeforeY(1));
            }
        }
        else if(StartRaining && ReachedPos2 == false)
        {
            if(CloudTransform.localPosition.y <= Pos2)
            {
                Vector3 cloudPos = CloudTransform.position;
                var speed = ((Pos2 - 609) / 2) * Time.deltaTime;
                cloudPos.y += speed * CloudSpriteMoveSpeed;
                CloudTransform.position = cloudPos;
            }
            else
            {
                ReachedPos2 = true;
                StartRaining = false;
                Destroy(CloudTransform.gameObject);
            }
        }

    }

    public void StopRain(float s)
    {
        print("Destroying");
        Destroy(this.gameObject, s);
    }

    IEnumerator StartDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        AudioSource.Play();
        StartRaining = true;
    }

    IEnumerator WaitBeforeY(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ReachedPos1 = true;
        StartRaining = true;
        particles.Play();
    }
}
