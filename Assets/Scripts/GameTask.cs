using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTask
{
    public int Id;
    public string Name;
    public string Description;
    public bool Completed;

    public GameTask(int id, string n, string d)
    {
        Id = id;
        Name = n;
        Description = d;
        Completed = false;
    }
}
