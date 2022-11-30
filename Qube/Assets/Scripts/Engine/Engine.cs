using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager raycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    new Camera camera;
    GameObject selectedObject;

    float initialDistance;
    Vector3 initialScale;
    bool scaling = false;

    [SerializeField]
    List<string> canMove;
    [SerializeField]
    List<string> canScale;
    [SerializeField]
    List<string> canRotate;

    bool movable;
    bool scalable;
    bool rotatable;


    // Start is called before the first frame update
    void Start()
    {
        selectedObject = null;
        camera = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.GetTouch(0).position);

        if (raycastManager.Raycast(Input.GetTouch(0).position, hits))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && selectedObject == null)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "SpawnableObjectTag")
                    {
                        selectedObject = hit.collider.gameObject;
                        string objectTag = selectedObject.tag;
                        movable = canMove.Contains(objectTag);
                        scalable = canScale.Contains(objectTag);
                        rotatable = canRotate.Contains(objectTag);
                    }
                }
            }
            else if (movable && Input.GetTouch(0).phase == TouchPhase.Moved && selectedObject != null)
            {
                selectedObject.transform.position = hits[0].pose.position;
            }

            if (scalable && Input.touchCount == 2)
            {
                var touchZero = Input.GetTouch(0);
                var touchOne = Input.GetTouch(1);

                if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
                {
                    goto lable_continue;
                }

                if (!scaling && selectedObject != null)
                {
                    scaling = true;
                    initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    initialScale = selectedObject.transform.localScale;
                }
                else
                {
                    var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                    if (Mathf.Approximately(initialDistance, 0))
                    {
                        goto lable_continue;
                    }

                    var factor = currentDistance / initialDistance;
                    selectedObject.transform.localScale = initialScale * factor;

                }

            }

        lable_continue:

            if (Input.touchCount < 2 || Input.GetTouch(1).phase == TouchPhase.Ended || Input.GetTouch(1).phase == TouchPhase.Canceled)
            {
                scaling = false;
            }

            if (Input.touchCount < 1 || Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                selectedObject = null;
                movable = scalable = rotatable = false;
            }

        }


    }
}
