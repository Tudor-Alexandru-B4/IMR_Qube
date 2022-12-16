using UnityEngine;

public class ChooseMeLevel4 : MonoBehaviour, TapActionScript
{
    [SerializeField]
    Engine engine;
    
    public void TapAction()
    {
        engine.TriggerLevelSolved();
    }
}
