using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveLevelIX : MonoBehaviour, TapActionScript
{

    public GameObject yellow1, yellow2;
    public GameObject blue1, blue2;
    public GameObject new1, new2;
    public int number;

    void Start()
    {
        yellow1.SetActive(false);
        yellow2.SetActive(false);
        blue1.SetActive(false);
        blue2.SetActive(false);
        new1.SetActive(false);
        new2.SetActive(false);
    }

    public void TapAction()
    {
        if (number==4){
            yellow1.SetActive(true);
            yellow1.transform.DOMoveY(1,0.5f).SetEase(Ease.InOutSine).SetLoops(4,LoopType.Yoyo);
            number=number-1;
        }
        else
        if (number==3){
            blue1.SetActive(true);
            blue1.transform.DOMoveY(1,0.5f).SetEase(Ease.InOutSine).SetLoops(4,LoopType.Yoyo);
            number=number-1;
        }
        else
        if (number==2){
            yellow2.SetActive(true);
            yellow2.transform.DOMoveY(1,0.5f).SetEase(Ease.InOutSine).SetLoops(4,LoopType.Yoyo);
            number=number-1;
        }
        else
        if (number==1){
            blue2.SetActive(true);
            blue2.transform.DOMoveY(1,0.5f).SetEase(Ease.InOutSine).SetLoops(4,LoopType.Yoyo);
            number=number-1;
        }

    }
}
