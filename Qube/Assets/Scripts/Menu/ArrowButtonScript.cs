using NaughtyAttributes;
using UnityEngine;

public class ArrowButtonScript : MonoBehaviour
{
    [SerializeField]
    GameObject levels;

    [SerializeField]
    bool upArrow;

    int sgn = 1;

    LevelRowsManager levelRowsManager;

    void Start()
    {
        levelRowsManager = levels.GetComponent<LevelRowsManager>();
        if (upArrow)
        {
            sgn = -1;
        }
    }

    [Button]
    public void ButtonPress()
    {
        if (levelRowsManager.UpdateRows(levelRowsManager.topRowIndex + 1 * sgn))
        {
            var updatedPosition = levels.transform.position;
            updatedPosition.y += 3.5f * sgn;
            levels.transform.position = updatedPosition;
        }
    }
}
