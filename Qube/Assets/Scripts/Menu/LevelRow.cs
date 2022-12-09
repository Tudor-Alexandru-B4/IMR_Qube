using UnityEngine;

public class LevelRow : MonoBehaviour
{
    public void ToggleActivity(bool makeActive)
    {
        gameObject.SetActive(makeActive);
    }
}
