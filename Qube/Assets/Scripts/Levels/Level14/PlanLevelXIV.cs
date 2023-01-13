using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanLevelXIV : MonoBehaviour
{
    [SerializeField]
    Engine engine;
    
    public GameObject correctCube;
    public GameObject wrongCube;

    public Vector3 firstPosition;
    public Vector3 secondPosition;
    
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider collider){
        if(collider.tag == "Cube14"){
            if(collider.gameObject == wrongCube){
                secondPosition=wrongCube.transform.position;
                firstPosition=correctCube.transform.position;
            }
            if(collider.gameObject == correctCube){
                collider.tag = "Untagged";
                wrongCube.transform.position=firstPosition;
                collider.transform.position=secondPosition;
                collider.tag = "Untagged";
                Destroy(gameObject);
            }
        }
    }
}
