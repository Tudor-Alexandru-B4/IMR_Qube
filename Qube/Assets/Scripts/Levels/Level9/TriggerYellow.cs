using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TriggerYellow : MonoBehaviour
{
    public GameObject yellow1,yellow2,new2;
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag=="Yellow9")
        {
            yellow1.SetActive(false);
            yellow2.SetActive(false);
            new2.SetActive(true);
            new2.transform.DOMoveY(1,0.5f).SetEase(Ease.InOutSine).SetLoops(4,LoopType.Yoyo);
        }
    }
}
