using System.Collections;
using UnityEngine;

public class CubeVisibleLevelIII : MonoBehaviour
{
    [SerializeField]
    GameObject Explosion;

    [SerializeField]
    Engine Engine;

    [SerializeField]
    float minimumDistance;

    [SerializeField]
    bool isFirstTime;
    
    new Camera camera;

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
        float currentDistance = Vector3.Distance(gameObject.transform.position, camera.transform.position);

        if(currentDistance >= minimumDistance && isFirstTime)
        {
            isFirstTime = false;
            Instantiate(Explosion, transform.position, Quaternion.identity);
            StartCoroutine(TriggerSolve());
        }
    }
    IEnumerator TriggerSolve()
    {
        yield return new WaitForSecondsRealtime(2);
        Engine.TriggerLevelSolved();
    }
}


