using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap8Times : MonoBehaviour, TapActionScript
{
    [SerializeField]
    Engine engine;

    int numberOfTap = 0;

    [Button]
    public void TapAction()
    {
        numberOfTap++;
        if(numberOfTap==8)
        {
            engine.TriggerLevelSolved();
        }
    }
}
