using UnityEngine;

public class ARPersistance : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
