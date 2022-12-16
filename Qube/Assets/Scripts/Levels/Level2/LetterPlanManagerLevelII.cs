using UnityEngine;

public class LetterPlanManagerLevelII : MonoBehaviour
{
    [SerializeField]
    Engine engine;

    void Update()
    {
        if (gameObject.transform.childCount <= 0)
        {
            engine.TriggerLevelSolved();
        }
    }
}
