using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarvingScoreV : MonoBehaviour
{
    [SerializeField]
    int passingScore = 14;
    int score = 0;

    public void AddScore(int update)
    {
        if(update < 0)
        {
            gameObject.GetComponent<PumpkinResetV>().Reset();
        }

        score += update;
        if (score == passingScore)
        {
            GameObject.Find("Engine").GetComponent<Engine>().TriggerLevelSolved();
        }
    }
}
