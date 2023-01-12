using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine;
using UnityEngine.SceneManagement;
using cakeslice;
using NaughtyAttributes;

public class Engine : MonoBehaviour
{
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
        raycastManager = GameObject.Find("AR Session Origin").GetComponent<ARRaycastManager>();
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

        if (Physics.Raycast(ray, out hit))
        {
            InstantiateHitObject();
        }

        if (selectedObject != null)
        {
            ExitToMenuIfDoor();

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

    [Button]
    private void ExitToMenuIfDoor()
    {
        if (selectedObject.tag == "door")
        {
            SceneManager.LoadScene(1);
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
            if (movable && createdPoint != null)
            {
                Destroy(createdPoint);
                createdPoint = null;
                movableParented = false;
            }
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
            if (createdPoint != null)
            {
                Destroy(createdPoint);
                createdPoint = null;
                movableParented = false;
            }
            if (selectedObject.GetComponent<Outline>() != null)
            {
                selectedObject.GetComponent<Outline>().eraseRenderer = true;
            }
            ExecuteIfTappable();
            selectedObject = null;
            movable = scalable = rotatable = justTappable = movableParented = false;
            holdingTime = 0;
        }
    }

    public void ForceReset()
    {
        if (createdPoint != null)
        {
            Destroy(createdPoint);
            createdPoint = null;
            movableParented = false;
        }
        scaling = false;
        if (selectedObject.GetComponent<Outline>() != null)
        {
            selectedObject.GetComponent<Outline>().eraseRenderer = true;
        }
        ExecuteIfTappable();
        selectedObject = null;
        movable = scalable = rotatable = justTappable = movableParented = false;
        holdingTime = 0;
    }

    public static void GetInterfaces<T>(out List<T> resultList, GameObject objectToSearch) where T : class
    {
        if (objectToSearch == null)
        {
            resultList = new List<T>();
            return;
        }
        
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

    private string GetLevelNumber(string level)
    {
        string number = "";
        foreach (char c in level)
        {
            if (c >= '0' && c <= '9')
            {
                number += c;
            }
        }

        return number;
    }

    public void TriggerLevelSolved(string text = null)
    {
        if(text != null)
        {
            Debug.Log(text);
        }
        int nextLevel = int.Parse(GetLevelNumber(SceneManager.GetActiveScene().name)) + 1;
        GameObject.Find("PersistentDataManager").GetComponent<PersistentDataManager>().SaveData(nextLevel.ToString());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
