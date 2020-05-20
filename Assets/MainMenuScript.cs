using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuScript : MonoBehaviour
{
    GameController GameController;
    [SerializeField]
    GameObject MainMenu;

    [SerializeField]
    AudioMixer AudioMixer;

    private void Start()
    {
        GameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameController.GamePaused == false)
        {
            MainMenu.SetActive(true);
            Pause();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        
    }

    public void SaveMusicAudio(float soundLevel)
    {
        AudioMixer.SetFloat("Music", soundLevel);
    }

    public void SaveGameSoundAudio(float soundLevel)
    {
        AudioMixer.SetFloat("Sounds", soundLevel);
    }

    public void About()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        GameController.GamePaused = false;
         
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
        GameController.GamePaused = true;
    }
}
