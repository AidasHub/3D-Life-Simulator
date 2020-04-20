using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Level_House : MonoBehaviour
{
    [SerializeField]
    TextController TextController;
    bool FirstEnter;
    // Start is called before the first frame update
    void Start()
    {
        FirstEnter = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(FirstEnter)
        {
            FirstEnter = false;
            TextController.UpdateMonologue("Press 'E' to go outside");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(1);
        }
    }
}
