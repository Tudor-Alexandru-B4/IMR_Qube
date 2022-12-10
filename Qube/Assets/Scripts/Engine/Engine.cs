using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine;
using UnityEngine.SceneManagement;
using cakeslice;

public class Engine : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager raycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    RaycastHit hit;
    Ray ray;

    new Camera camera;
    GameObject selectedObject;

    float initialDistance;
    Vector3 initialScale;
    bool scaling = false;

    [SerializeField]
    List<string> canDoAll;
    [SerializeField]
    List<string> canMove;
    [SerializeField]
    List<string> canScale;
    [SerializeField]
    List<string> canRotate;

    bool movable;
    bool scalable;
    bool rotatable;

    void Start()
    {
        selectedObject = null;
        camera = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        ray = camera.ScreenPointToRay(Input.GetTouch(0).position);

        if (raycastManager.Raycast(Input.GetTouch(0).position, hits))
        {
            InstantiateHitObject();

            ExecuteIfMovingAMovable();
            ExecuteIfScalingAScalable();
            ExecuteIfRotatingARotatable();

            ResetWhenInputEnds();
        }
    }

    private void InstantiateHitObject()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began && selectedObject == null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                selectedObject = hit.collider.gameObject;
                if (selectedObject != null)
                {
                    string objectTag = selectedObject.tag;
                    if (canDoAll.Contains(selectedObject.tag))
                    {
                        movable = scalable = rotatable = true;
                    }
                    else
                    {
                        movable = canMove.Contains(objectTag);
                        scalable = canScale.Contains(objectTag);
                        rotatable = canRotate.Contains(objectTag);
                    }

                    if(movable || scalable || rotatable)
                    {
                        Outline outline = selectedObject.GetComponent<Outline>();
                        if(outline != null)
                        {
                            outline.eraseRenderer = false;
                        }
                        else
                        {
                            selectedObject.AddComponent<Outline>();
                        }
                    }
                }
            }
        }
    }

    private void ExecuteIfMovingAMovable()
    {
        if (movable && Input.GetTouch(0).phase == TouchPhase.Moved && selectedObject != null && Input.touchCount == 1)
        {
            selectedObject.transform.position = hits[0].pose.position;
        }
    }

    private void ExecuteIfScalingAScalable()
    {
        if (selectedObject != null && scalable && Input.touchCount == 2)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
            {
                return;
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
                    return;
                }

                var factor = currentDistance / initialDistance;
                selectedObject.transform.localScale = initialScale * factor;
            }
        }
    }

    private void ExecuteIfRotatingARotatable()
    {
        if (selectedObject != null && rotatable && Input.touchCount == 3)
        {
            selectedObject.transform.rotation = camera.transform.rotation;
        }
    }

    private void ResetWhenInputEnds()
    {
        if (Input.touchCount != 2 || Input.GetTouch(1).phase == TouchPhase.Ended || Input.GetTouch(1).phase == TouchPhase.Canceled)
        {
            scaling = false;
        }

        if (Input.touchCount < 1 || Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
        {
            selectedObject.GetComponent<Outline>().eraseRenderer = true;
            selectedObject = null;
            movable = scalable = rotatable = false;
        }
    }

    public void TriggerLevelSolved(string text = null)
    {
        if(text != null)
        {
            Debug.Log(text);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
