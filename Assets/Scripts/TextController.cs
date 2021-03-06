﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI MirrorsText = null;
    [SerializeField]
    TextMeshProUGUI Monologue = null;
    string TextBuffer = null;
    [SerializeField]
    float timeBetweenCharacters = 0.3f;

    public bool isMonologuing = false;

    public void UpdateMonologue(string text, bool fast=false)
    {
        StopAllCoroutines();
        MirrorsText.text = "";
        Monologue.text = "";
        isMonologuing = true;
        //Monologue.text = text;
        Monologue.enabled = true;
        //StopCoroutine("Wait");
        //StartCoroutine(Wait(5, Monologue, true));
        if (fast == false)
            StartCoroutine(Wait(text, 5, Monologue, false, true));
        else
            StartCoroutine(Wait(text, 2.5f, Monologue, false, true));
    }

    public void UpdateMirrors(string text)
    {
        MirrorsText.enabled = true;
        StartCoroutine(Wait(text, 5, MirrorsText, true));
    }


    IEnumerator Wait(string text, float seconds, TextMeshProUGUI Component, bool DisableAfter, bool Gradual = false)
    {
        if(Gradual)
        {
            TextBuffer = text;
            Component.text = "";
            for (int i = 0; i < TextBuffer.Length; i++)
            {
                Component.text += TextBuffer[i];
                yield return new WaitForSeconds(timeBetweenCharacters);
            }
            isMonologuing = false;
            yield return new WaitForSeconds(seconds);
            Component.text = "";
            TextBuffer = "";
            Component.enabled = DisableAfter;
        }
        else
        {
            Component.text = text;
            yield return new WaitForSeconds(seconds);
            Component.text = "";
            Component.enabled = DisableAfter;
        }
    }
}
