using UnityEngine;

public class ZoomOutLevelIII : MonoBehaviour
{
    [SerializeField]
    public float minimumDistance;

    public GameObject Cube;
    new Camera camera;
    public Engine Engine;

    void Start()
    {
        camera = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    void Update()
    {
        ExecuteIfFarEnough();
    }

    public void ExecuteIfFarEnough()
    {
        float currentDistance = Vector3.Distance(Cube.transform.position, camera.transform.position);

        if(currentDistance >= minimumDistance)
        {
            Debug.Log("Close enough");
            //Engine.TriggerLevelSolved();
        }
        else
        {
            Debug.Log("Too far");
        }
    }
}


