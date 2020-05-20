using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Settings
    //public float GameSoundVolume = 0.5f;
    //public float MusicVolume = 0.5f;

    int GameTasksN = 0;
    public GameTask[] GameTasks;

    static GameController instance;

    [SerializeField]
    public bool GamePaused = false;

    GameObject EscapeMenu;

    [SerializeField]
    TextController TextController = null;

    [SerializeField]
    bool HungerEnabled = true;

    [SerializeField]
    StatusController PlayerStatus = null;

    [SerializeField]
    UIController UI_Controller = null;

    [SerializeField]
    float HungerRate = 0;

    [SerializeField]
    int MirrorsShattered = 0;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        InitializeGameTasks();

        if (HungerEnabled)
        {
            UI_Controller.DisplayHunger(true);
            StartCoroutine(HungerTick());
        }
    }

    private void Update()
    {
    }

    public void InitializeGameTasks()
    {
        GameTasks = new GameTask[3];
        GameTasks[GameTasksN] = new GameTask(GameTasksN++, "Cockroaches!", "Evict the nasty house guests");
        GameTasks[GameTasksN] = new GameTask(GameTasksN++, "Nothing to do with luck", "Win a game that involves toes");
        GameTasks[GameTasksN] = new GameTask(GameTasksN++, "Exam", "Pass the IT exam");
    }

    IEnumerator HungerTick()
    {
        while(true)
        {
            PlayerStatus.ChangeHunger(-1);
            UI_Controller.UpdateHunger(PlayerStatus);
            yield return new WaitForSeconds(HungerRate);
        }
    }

    public void IncreaseMirrorsShattered()
    {
        MirrorsShattered += 1;
        TextController.UpdateMirrors("Mirrors shattered: " + MirrorsShattered);
    }

    public void CheckTasks()
    {
        bool complete = true;
        foreach(GameTask T in GameTasks)
        {
            if (!T.Completed)
                complete = false;
        }
        if (complete)
        {
            SceneManager.LoadScene(3);
        }
    }
}
