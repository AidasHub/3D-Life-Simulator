using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MirrorScript : MonoBehaviour
{
    //[SerializeField]
    //Material GlassMaterial = null;
    [SerializeField]
    Material CrackedGlassMaterial = null;
    [SerializeField]
    TextController TextController = null;
    [SerializeField]
    GameController GameController = null;
    TaskController TaskController;

    private MeshRenderer mesh;
    private bool IsShattered = false;

    private void Start()
    {
        mesh = GetComponentsInChildren<MeshRenderer>()[1];
        TaskController = GameObject.Find("TaskList").GetComponent<TaskController>();
    }

    string[] MirrorResponses = {
        "You talkin' to me?", 
        "Damn, I look good",
        "What's cookin', good lookin?",
        "HULK SMASH",
        "This would really hurt if these hands were real",
        "See THIS",
        "Smashing!",
        "Mirror mirror on the wall, who's the toughest of them all",
        "I hear this mirror was a smash hit"
    };

    public void Shatter()
    {
        if (!IsShattered)
        {
            mesh.material = CrackedGlassMaterial;
            print("crack!");
            IsShattered = true;
            string monologue = MirrorResponses[Random.Range(0, MirrorResponses.Length)];
            TextController.UpdateMonologue(monologue);
            GameController.IncreaseMirrorsShattered();
            if(GameController.MirrorsShattered == 5)
            {
                TaskController.CompleteTask(4);
            }
        }
    }

}
