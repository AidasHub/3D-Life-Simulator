using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
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

    private void Start()
    {
        if(HungerEnabled)
        {
            UI_Controller.DisplayHunger(true);
            StartCoroutine(HungerTick());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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
}
