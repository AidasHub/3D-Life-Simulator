using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    [SerializeField]
    int directionInt = 1;
    [SerializeField]
    bool Drive = false;
    float SpeedModifier;
    GameObject Player;
    [SerializeField]
    GameObject BlackPanel;
    AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Drive == true)
        {
            transform.position += Vector3.left * 0.35f * Mathf.Log(SpeedModifier) * directionInt;
        }
        if(transform.position.x < -65 * directionInt)
        {
            Drive = false;
            gameObject.SetActive(false);
        }
    }

    public void SpawnCar(float magnitude)
    {
        SpeedModifier = magnitude;
        transform.position = transform.parent.position;
        Drive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Drive = false;
            GameObject.Find("GlobalAudio").GetComponent<AudioSource>().Stop();
            Player = GameObject.Find("Player");
            Player.GetComponent<PlayerController>().enabled = false;
            Player.GetComponentInChildren<CameraController>().DisableCameraMovement();
            Player.transform.position = GameObject.Find("GameOverPlayerPosition").transform.position;
            BlackPanel.SetActive(true);
            GameObject.Find("TaskList").SetActive(false);
            AudioSource = GetComponent<AudioSource>();
            AudioSource.Play();
            StartCoroutine(WaitForCrash());
        }
    }

    IEnumerator WaitForCrash()
    {
        while (AudioSource.isPlaying)
        {
            yield return null;
        }
        BlackPanel.SetActive(false);
        Player.GetComponentInChildren<CameraController>().transform.SetParent(GameObject.Find("GameOverCameraPos").transform);
        Camera.main.transform.position = GameObject.Find("GameOverCameraPos").transform.position;
        Camera.main.transform.rotation = GameObject.Find("GameOverCameraPos").transform.rotation;
    }
}
