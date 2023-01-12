using UnityEngine;

public class LockManager : MonoBehaviour
{
    [SerializeField]
    GameObject levels;
    [SerializeField]
    GameObject lockPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PersistentDataManager dataManager = GameObject.Find("PersistentDataManager").GetComponent<PersistentDataManager>();
        dataManager.SaveData("20");
        int lastUnlockedLevel = int.Parse(dataManager.LoadData());

        foreach (Transform row in levels.transform)
        {
            foreach (Transform level in row)
            {
                if (int.Parse(GetLevelNumber(level.gameObject.name)) > lastUnlockedLevel)
                {
                    GameObject newLock = Instantiate(lockPrefab, level);
                    newLock.transform.parent = level;
                }
            }
        }

    }

    private string GetLevelNumber(string level)
    {
        string number = "";
        foreach (char c in level)
        {
            if (c >= '0' && c <= '9')
            {
                number += c;
            }
        }

        return number;
    }
}
