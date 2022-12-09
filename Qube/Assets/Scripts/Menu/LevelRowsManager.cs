using System.Collections.Generic;
using UnityEngine;

public class LevelRowsManager : MonoBehaviour
{

    [SerializeField]
    List<LevelRow> levelRows;

    [System.NonSerialized]
    public int topRowIndex;

    // Start is called before the first frame update
    void Start()
    {
        UpdateRows(0);
    }

    public bool UpdateRows(int newTopRowIndex)
    {
        if(newTopRowIndex < 0 || newTopRowIndex >= levelRows.Count - 1)
        {
            return false;
        }

        topRowIndex = newTopRowIndex;
        for (int i = 0; i < levelRows.Count; i++)
        {
            bool makeActive = false;
            if (i == topRowIndex || i == topRowIndex + 1)
            {
                makeActive = true;
            }
            levelRows[i].ToggleActivity(makeActive);
        }

        return true;
    }
}
