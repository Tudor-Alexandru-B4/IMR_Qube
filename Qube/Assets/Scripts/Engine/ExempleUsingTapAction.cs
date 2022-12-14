using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExempleUsingTapAction : MonoBehaviour, TapActionScript
{
    public void TapAction()
    {
        Debug.Log("Tapped");
    }
}
