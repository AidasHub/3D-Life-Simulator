using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskController : MonoBehaviour
{
    GameController GameController;
    List<string> TaskText;
    TMP_Text[] NotepadTexts;
    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.Find("GameController").GetComponent<GameController>();
        TaskText = new List<string>();
        NotepadTexts = GetComponentsInChildren<TMP_Text>();
        for(int i = 0; i < GameController.GameTasks.Length; i++)
        {
            string s = GameController.GameTasks[i].Description;
            TaskText.Add(s);
        }
        DisplayTasks();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CompleteTask(int id)
    {
        GameController.GameTasks[id].Completed = true;
        NotepadTexts[id].fontStyle = FontStyles.Strikethrough;
        NotepadTexts[id].text = TaskText[id];
        GameController.CheckTasks();
    }

    public void DisplayTasks()
    {
        for(int i = 0; i < TaskText.Count; i++)
        {
            NotepadTexts[i].text = TaskText[i];
            if(GameController.GameTasks[i].Completed)
            {
                NotepadTexts[i].fontStyle = FontStyles.Strikethrough;
            }
        }
    }
}
