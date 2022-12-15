using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMeLevel4 : MonoBehaviour
{
    [SerializeField]
    Engine engine;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        engine.TriggerLevelSolved();
    }
}
