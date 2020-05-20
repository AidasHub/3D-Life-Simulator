using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidequest_Exam : MonoBehaviour
{
    ParticleSystem TriggerParticles = null;
    SpriteRenderer TriggerSprite = null;
    AudioSource AudioSource = null;
    CameraController PlayerCamera = null;
    PlayerController PlayerController = null;
    [SerializeField]
    TextController TextController = null;
    GameObject Exam = null;
    [SerializeField]
    TaskController TaskController;
    [SerializeField]
    GameObject ExamPrefab;
    [SerializeField]
    AudioClip ExamMusic;
    [SerializeField]
    AudioClip StreetMusic;

    // Start is called before the first frame update
    void Start()
    {
        TriggerParticles = GetComponentInChildren<ParticleSystem>();
        TriggerSprite = GetComponentInChildren<SpriteRenderer>();
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TaskController.CompleteTask(2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioSource.Play();
            PlayerCamera = other.gameObject.GetComponentInChildren<CameraController>();
            PlayerController = other.gameObject.GetComponentInChildren<PlayerController>();

            //Disable the sidequest marker
            TriggerParticles.gameObject.SetActive(false);
            TriggerSprite.gameObject.SetActive(false);
            PlayerCamera.DisableCameraMovement();
            PlayerController.enabled = false;
            GetComponent<BoxCollider>().enabled = false;

            Exam = Instantiate(ExamPrefab, GameObject.Find("Canvas").transform);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameObject.Find("GlobalAudio").GetComponent<AudioSource>().clip = ExamMusic;
            GameObject.Find("GlobalAudio").GetComponent<AudioSource>().Play();
        }
    }

    public void ExamPass()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerController.enabled = true;
        PlayerCamera.EnableCameraMovement();
        TaskController.CompleteTask(2);
        TextController.UpdateMonologue("01000101 01100001 01110011 01111001");
        GameObject.Find("GlobalAudio").GetComponent<AudioSource>().clip = StreetMusic;
        GameObject.Find("GlobalAudio").GetComponent<AudioSource>().Play();
        Destroy(Exam);
    }
}
