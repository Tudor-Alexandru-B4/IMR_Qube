using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChooseCubeLevelVI : MonoBehaviour, TapActionScript
{
    [SerializeField]
    Engine engine;

    public void TapAction()
    {
        transform.DOMoveY(1,0.5f).SetEase(Ease.InOutSine).SetLoops(5,LoopType.Yoyo).OnComplete(Win);
    }

    private void Win()
    {
        engine.TriggerLevelSolved();
    }
}
