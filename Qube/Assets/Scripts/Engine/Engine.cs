using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine;
using UnityEngine.SceneManagement;
using cakeslice;
using NaughtyAttributes;

public class Engine : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager raycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    RaycastHit hit;
    RaycastHit hitMovable;
    Ray ray;

    new Camera camera;
    GameObject selectedObject;

    float initialDistance;
    Vector3 initialScale;
    bool scaling = false;

    [SerializeField]
    float maxTapHoldTime = 1.0f;

    [SerializeField]
    List<string> canDoAll;
    [SerializeField]
    List<string> canMove;
    [SerializeField]
    List<string> canScale;
    [SerializeField]
    List<string> canRotate;
    [SerializeField]
    List<string> justTap;

    bool movable;
    bool scalable;
    bool rotatable;
    bool justTappable = false;

    float holdingTime = 0;

    float lastPositionX;
    float lastPositionY;
    float lastPositionZ;

    bool movableParented = false;

    [SerializeField]
    GameObject movablePoint;
    GameObject createdPoint = null;

    void Start()
    {
        selectedObject = null;
        camera = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        if (holdingTime <= maxTapHoldTime + 1.0f)
        {
            holdingTime += Time.deltaTime;
        }

        ray = camera.ScreenPointToRay(Input.GetTouch(0).position);

        //TODO: Raycast Unity
        if (Physics.Raycast(ray, out hit))
        {
            InstantiateHitObject();
        }

        if (selectedObject != null)
        {
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
            if (selectedObject == null)
            {
                selectedObject = hit.collider.gameObject;
                lastPositionX = Input.GetTouch(0).position.x;
                lastPositionY = Input.GetTouch(0).position.y;
                lastPositionZ = camera.transform.position.z;
                InitiateAvailableMoves();
            }
        }
    }

    [Button]
    private void InitiateAvailableMoves()
    {
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
            justTappable = justTap.Contains(selectedObject.tag);

            if (movable || scalable || rotatable || justTappable)
            {
                Outline outline = selectedObject.GetComponent<Outline>();
                if (outline != null)
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

    private void ExecuteIfMovingAMovable()
    {
        if (movable && Input.GetTouch(0).phase == TouchPhase.Moved && selectedObject != null && Input.touchCount == 1)
        {
            if (!movableParented)
            {
                createdPoint = Instantiate(movablePoint, hit.transform.position, selectedObject.transform.rotation) as GameObject;
                movableParented = true;
                createdPoint.transform.parent = camera.transform;
            }
            else
            {
                selectedObject.transform.position = createdPoint.transform.position;
                selectedObject.transform.rotation = createdPoint.transform.rotation;
            }

            //selectedObject.transform.position = hits[0].pose.position;

            //selectedObject.transform.position = new Vector3(
            //        selectedObject.transform.position.x + (Input.GetTouch(0).position.x - lastPositionX),
            //        selectedObject.transform.position.y + (Input.GetTouch(0).position.y - lastPositionY),
            //        selectedObject.transform.position.z
            //    );
            //lastPositionX = Input.GetTouch(0).position.x;
            //lastPositionY = Input.GetTouch(0).position.y;
            //lastPositionZ = camera.transform.position.z;
            //Debug.Log(selectedObject.transform.position.x);
            //Debug.Log(selectedObject.transform.position.y);
            //Debug.Log(selectedObject.transform.position.z);
            //Debug.Log(Input.GetTouch(0).position.x);
            //Debug.Log(Input.GetTouch(0).position.y);
            //Debug.Log("---------------------------");

            //selectedObject.transform.position = Vector3.MoveTowards(selectedObject.transform.position, Input.GetTouch(0).position, Time.deltaTime);
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

    [Button]
    private void ExecuteIfTappable()
    {
        if (holdingTime <= maxTapHoldTime)
        {
            List<TapActionScript> interfaceList;
            GetInterfaces<TapActionScript>(out interfaceList, selectedObject);
            foreach (TapActionScript tappable in interfaceList)
            {
                tappable.TapAction();
            }
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
            if (movablePoint != null)
            {
                Destroy(createdPoint);
                movableParented = false;
            }
            selectedObject.GetComponent<Outline>().eraseRenderer = true;
            ExecuteIfTappable();
            selectedObject = null;
            movable = scalable = rotatable = justTappable = movableParented = false;
            holdingTime = 0;
        }
    }

    public void ForceReset()
    {
        scaling = false;
        selectedObject.GetComponent<Outline>().eraseRenderer = true;
        ExecuteIfTappable();
        selectedObject = null;
        movable = scalable = rotatable = justTappable = false;
        holdingTime = 0;
    }

    public static void GetInterfaces<T>(out List<T> resultList, GameObject objectToSearch) where T : class
    {
        MonoBehaviour[] list = objectToSearch.GetComponents<MonoBehaviour>();
        resultList = new List<T>();
        foreach (MonoBehaviour mb in list)
        {
            if (mb is T)
            {
                resultList.Add((T)((System.Object)mb));
            }
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
