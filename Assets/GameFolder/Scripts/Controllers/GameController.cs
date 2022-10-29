using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    public List<OffenderController> OffenderControllers;
    void Start()
    {
        Singleton();
    }
}
