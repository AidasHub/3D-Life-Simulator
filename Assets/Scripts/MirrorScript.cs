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

    private MeshRenderer mesh;
    private bool IsShattered = false;

    private void Start()
    {
        mesh = GetComponentsInChildren<MeshRenderer>()[1];
    }

    public void Shatter()
    {
        if (!IsShattered)
        {
            mesh.material = CrackedGlassMaterial;
            print("crack!");
            IsShattered = true;
            TextController.UpdateMonologue("You talkin' to me?");
            GameController.IncreaseMirrorsShattered();
        }
    }

}
