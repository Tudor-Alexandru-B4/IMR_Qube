using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLevelXIV : MonoBehaviour
{
    [SerializeField]
    Engine engine;

    void Update()
    {
        if (gameObject.transform.childCount <= 0)
        {
            engine.TriggerLevelSolved();
        }
    }
}
