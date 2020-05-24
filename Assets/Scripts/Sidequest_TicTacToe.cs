using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidequest_TicTacToe : MonoBehaviour
{
    ParticleSystem TriggerParticles = null;
    SpriteRenderer TriggerSprite = null;
    CameraController PlayerCamera = null;
    PlayerController PlayerController = null;
    [SerializeField]
    TextController TextController = null;
    TicTacToe TicTacToeGame;
    [SerializeField]
    TaskController TaskController;
    [SerializeField]
    Animator NPC_Animator;
    AudioSource AudioSource;

    void Start()
    {
        TicTacToeGame = transform.parent.GetComponentInChildren<TicTacToe>();
        TriggerParticles = GetComponentInChildren<ParticleSystem>();
        TriggerSprite = GetComponentInChildren<SpriteRenderer>();
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioSource.Play();
            PlayerCamera = other.gameObject.GetComponentInChildren<CameraController>();
            PlayerController = other.gameObject.GetComponentInChildren<PlayerController>();
            NPC_Animator.enabled = true;

            //Disable the sidequest marker
            TriggerParticles.gameObject.SetActive(false);
            TriggerSprite.gameObject.SetActive(false);
            PlayerCamera.DisableCameraMovement();
            PlayerController.enabled = false;
            StartCoroutine(ReturnControlAfter(3));
            TextController.UpdateMonologue("New objective! Tic That Toe");
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void PlayerWon()
    {
        TaskController.CompleteTask(1);
        TextController.UpdateMonologue("Winning really makes you tic");
        NPC_Animator.SetTrigger("NPCLost");
    }

    public void PlayerLost()
    {
        TriggerParticles.gameObject.SetActive(true);
        TriggerSprite.gameObject.SetActive(true);
        TextController.UpdateMirrors("You lost!");
        GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator ReturnControlAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        PlayerController.enabled = true;
        PlayerCamera.EnableCameraMovement();
        TicTacToeGame.StartGame();
    }
}
