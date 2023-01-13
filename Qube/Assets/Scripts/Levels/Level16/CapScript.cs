using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapScript : MonoBehaviour, TapActionScript
{
    [SerializeField]
    Engine engine;


    public void TapAction()
    {
        transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
    }
}

