using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettersLevelIV : MonoBehaviour
{
    [SerializeField]
    private Engine engine;
    
    public GameObject myLetter;
    private GameObject myPlane;
    private int values;

    // Start is called before the first frame update
    void Start()
    {
        myPlane = GetComponent<GameObject>();
        values = 6;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter(Collider collider){
        if(collider.tag == "Letter"){
            if(collider.gameObject == myLetter){
                Destroy(myPlane);
                values = values-1;
                collider.tag = "Untagged";
            }
        }
        if(values == 0){
            engine.TriggerLevelSolved();
        } 
    }
}
