using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Room CurrentRoom { get; set; }

    private void Awake()
    {
        instance = this;
    }
}
