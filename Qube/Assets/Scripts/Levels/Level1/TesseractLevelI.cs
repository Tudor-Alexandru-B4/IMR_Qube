using NaughtyAttributes;
using UnityEngine;

public class TesseractLevelI : MonoBehaviour, TapActionScript
{
    [SerializeField]
    Engine engine;

    [SerializeField]
    int numberOfNeededTaps = 5;
    [SerializeField]
    float teleportRange = 5.0f;

    int currentTaps = 0;

    [Button]
    private void TesseractTapped()
    {
        currentTaps += 1;
        if (currentTaps >= numberOfNeededTaps)
        {
            engine.TriggerLevelSolved();
        }
        gameObject.transform.position = new Vector3(GenerateRandomInRange(), GenerateRandomInRange(), GenerateRandomInRange());
    }

    private float GenerateRandomInRange()
    {
        return Random.RandomRange(-teleportRange, teleportRange);
    }

    public void TapAction()
    {
        TesseractTapped();
    }
}
