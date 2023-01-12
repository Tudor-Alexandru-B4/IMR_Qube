using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarveCubeV : MonoBehaviour, TapActionScript
{
    [SerializeField]
    bool shouldBeCarved = false;
    
    public void TapAction()
    {
        gameObject.transform.parent.GetComponent<CarvingScoreV>().AddScore(shouldBeCarved?1:-1);
        gameObject.SetActive(false);
    }
}
