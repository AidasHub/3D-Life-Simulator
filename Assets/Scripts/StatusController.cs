using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour
{

    [SerializeField]
    int MaxHunger = 0;
    [SerializeField]
    int MaxSanity = 0;

    [Space(20)]

    [SerializeField]
    int Hunger = 0;
    [SerializeField]
    int Sanity = 0;

    public int GetHunger()
    {
        return Hunger;
    }

    public int GetSanity()
    {
        return Sanity;
    }

    public void ChangeHunger(int value)
    {
        int NewHunger = Hunger + value;
        if(NewHunger <= 0)
        {
            Hunger = 0;
            //GAME END
        }
        else if(NewHunger >= MaxHunger)
        {
            Hunger = MaxHunger;
        }
        else
        {
            Hunger += value;
        }
    }

    public void ChangeSanity(int value)
    {
        int NewSanity = Sanity + value;
        if (NewSanity <= 0)
        {
            Sanity = 0;
            //GAME END
        }
        else if (NewSanity >= MaxSanity)
        {
            Sanity = MaxSanity;
        }
        else
        {
            Sanity += value;
        }
    }

}
