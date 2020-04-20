using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI HungerText = null;
    

    public void DisplayHunger(bool display)
    {
        HungerText.transform.gameObject.SetActive(display);
    }

    public void UpdateHunger(StatusController controller)
    {
        var text = "Hunger: " + controller.GetHunger();
        HungerText.text = text;
    }

}
