using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderPositionScript : MonoBehaviour
{
    enum SliderType
    {
        Music,
        GameSound
    }

    GameController GameController;
    [SerializeField]
    SliderType _SliderType;
    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.Find("GameController").GetComponent<GameController>();
        if(_SliderType == SliderType.GameSound)
        {
            GetComponent<Slider>().value = GameController.GameSoundLevel;
        }
        if(_SliderType == SliderType.Music)
        {
            GetComponent<Slider>().value = GameController.MusicSoundLevel;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
