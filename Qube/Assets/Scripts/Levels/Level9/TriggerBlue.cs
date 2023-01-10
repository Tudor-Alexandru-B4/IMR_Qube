using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TriggerBlue : MonoBehaviour
{
    public GameObject blue1,blue2,new1;
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag=="Blue9")
        {
            blue1.SetActive(false);
            blue2.SetActive(false);
            new1.SetActive(true);
            new1.transform.DOMoveY(1,0.5f).SetEase(Ease.InOutSine).SetLoops(4,LoopType.Yoyo);
        }
    }
}
