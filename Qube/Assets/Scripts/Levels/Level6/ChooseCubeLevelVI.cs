using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCubeLevelVI : MonoBehaviour, TapActionScript
{
    [SerializeField]
    Engine engine;
    
    public void TapAction()
    {
        engine.TriggerLevelSolved();
    }
}
