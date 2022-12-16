using UnityEngine;

public class LettersLevelIV : MonoBehaviour
{
    [SerializeField]
    private Engine engine;
    
    public GameObject myLetter;

    void OnTriggerEnter(Collider collider){
        if(collider.tag == "Letter"){
            if(collider.gameObject == myLetter){
                engine.ForceReset();
                collider.transform.position = gameObject.transform.position;
                collider.transform.rotation = gameObject.transform.rotation;
                collider.transform.localScale = gameObject.transform.localScale;
                collider.tag = "Untagged";
                Destroy(gameObject);
            }
        }
    }
}
