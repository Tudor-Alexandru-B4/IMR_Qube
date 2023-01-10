using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerSphere : MonoBehaviour
{
    [SerializeField]
    Engine engine;
    public GameObject new1,new2;
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag=="Sphere9")
        {
            new1.SetActive(false);
            new2.SetActive(false);
            engine.TriggerLevelSolved();
        }
    }
}
