using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap8Times : MonoBehaviour
{
    [SerializeField]
    Engine engine;

    [SerializeField]
    int numberOfTap = 0;

    public void TapAction()
    {
        numberOfTap++;
        if(numberOfTap==0)
        {
            engine.TriggerLevelSolved();
        }
    }
}
