using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExamScript : MonoBehaviour
{
    [SerializeField]
    TMP_Text ErrorText;
    TMP_InputField[] Inputs;
    AudioSource AudioSource;
    [SerializeField]
    AudioClip clip;
    string[] answers = 
        {
        "GetName",
        "names[id]",
        "void",
        "PrintAllNames()",
        "string",
        "names"
        };
    // Start is called before the first frame update
    void Start()
    {
        Inputs = GetComponentsInChildren<TMP_InputField>();
        AudioSource = GetComponent<AudioSource>();
    }

    public void CheckInputs()
    {
        int i = 0;
        bool passed = true;
        foreach(TMP_InputField Input in Inputs)
        {
            if(Input.text != answers[i])
            {
                print("Wrong on" + Input.text + " should be " + answers[i]);
                passed = false;
                break;
            }
            i++;
        }
        if (passed)
        {
            CorrectAnswers();
        }
        else
        {
            WrongAnswers();
        }
    }

    void CorrectAnswers()
    {
        GameObject.Find("Exam").GetComponentInChildren<Sidequest_Exam>().ExamPass();
    }

    void WrongAnswers()
    {
        DisplayError = true;
        Color color = ErrorText.color;
        color.a = 1;
        ErrorText.color = color;
    }

    bool DisplayError = false;
    float currentTime = 0f;
    float timeToFlash = 1f;

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            AudioSource.PlayOneShot(clip);
        }
        if (DisplayError)
        {
            if(currentTime >= timeToFlash)
            {
                Color color = ErrorText.color;
                color.a = 0;
                ErrorText.color = color;
                DisplayError = false;
                currentTime = 0f;
            }
            currentTime += Time.deltaTime;
        }
    }
}
