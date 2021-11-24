using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPAP : MonoBehaviour
{
    public static PPAP Instance;
    public GameObject player;

    private void Awake()
    {
        Instance = this;
        
    }
}
