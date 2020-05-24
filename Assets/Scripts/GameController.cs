using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    public static int MirrorsShattered;

    public float MusicSoundLevel = 0.5f;
    public float GameSoundLevel = 0.5f;

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
        print("Lets go");
        InitializeGameTasks();
        SceneManager.sceneLoaded += OnLevelLoad;
        MirrorsShattered = 0;

        if (HungerEnabled)
        {
            UI_Controller.DisplayHunger(true);
            StartCoroutine(HungerTick());
        }

    }


    private void OnLevelLoad(Scene scene, LoadSceneMode mode)
    {

        if(scene.buildIndex == 1)
        { 
            GameObject Player = GameObject.Find("Player");
            Player.GetComponent<PlayerController>().enabled = false;
            GameObject.Find("Main Camera").GetComponent<CameraController>().DisableCameraMovement();
            StartCoroutine(StartGame1());
            //GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
            //GameObject.Find("Main Camera").GetComponent<CameraController>().EnableCameraMovement();
            //After everything
            Player.transform.position = new Vector3(-2.07f, 1.558f, 19.67f);
            Player.transform.rotation = Quaternion.Euler(0, 90, 0);
            print(Player.transform.rotation);

            
        }
    }

    string[] StartMonologue = { 
        "The year is 2389",
        "Humanity is on the brink of exctinction",
        "The human population is practically non-existent",
        "And the remainder is in hiding because of aliens or underground monsters or something",
        "You, Subject Omega, codename: 'Narrative Cliche', have been selected to partake in an experiment",
        "You will be placed in a life simulation, thought to be a 100% accurate representation of the human life in the Year 2020",
        "Complete the tasks given and the simulation will be complete",
        "If you die, however...",
        "...well, nothing really, it's only a simulation, no need to get dramatic",
        "Simulation begins in 3...",
        "2...",
        "1..." 
    };

    IEnumerator StartGame1()
    {
        var TC = GameObject.Find("TextController").GetComponent<TextController>();
        yield return new WaitForSeconds(3);
        foreach(string s in StartMonologue)
        {
            TC.UpdateMonologue(s, true);
            while (true)
            {
                if(!TC.isMonologuing && Input.anyKey)
                {
                    break;
                }
                yield return null;
            }
            yield return null;
        }
        //GameObject.Find("BlackPanel").GetComponent<Animator>().enabled = true;
        GameObject.Find("BlackPanel").GetComponent<Animator>().SetTrigger("FadeOut");
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<CameraController>().EnableCameraMovement();
    }

    public void ResetTasks()
    {
        StopAllCoroutines();
        MirrorsShattered = 0;
        GameTasksN = 0;
        InitializeGameTasks();
    }

    public void InitializeGameTasks()
    {
        GameTasks = new GameTask[5];
        GameTasks[GameTasksN] = new GameTask(GameTasksN++, "Cockroaches!", "Evict the nasty house guests");
        GameTasks[GameTasksN] = new GameTask(GameTasksN++, "Nothing to do with luck", "Win a game that involves toes");
        GameTasks[GameTasksN] = new GameTask(GameTasksN++, "Exam", "Pass the IT exam");
        GameTasks[GameTasksN] = new GameTask(GameTasksN++, "Football", "Score a goal");
        GameTasks[GameTasksN] = new GameTask(GameTasksN++, "Mirrors", "Smash 5 mirrors");
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
        if(TextController == null)
        {
            TextController = GameObject.Find("TextController").GetComponent<TextController>();
        }
        MirrorsShattered = MirrorsShattered + 1;
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
            var Panel = GameObject.Find("BlackPanel");
            Panel.GetComponent<Animator>().enabled = true;
            Panel.GetComponent<Animator>().SetTrigger("FadeIn");
            StartCoroutine(GameWon());
        }
    }

    IEnumerator GameWon()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(3);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
