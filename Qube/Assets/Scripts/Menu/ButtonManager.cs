using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager raycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    new Camera camera;
    GameObject selectedObject;

    void Start()
    {
        camera = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.GetTouch(0).position);

        if (raycastManager.Raycast(Input.GetTouch(0).position, hits))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    selectedObject = hit.collider.gameObject;
                    string selectedName = selectedObject.name.ToString();
                    executeIfLevel(selectedName);
                    executeIfArrowButton(selectedName);
                    executeIfPlayButton(selectedName);
                    executeIfBackToMenu(selectedName);
                }
            }
        }
    }

    void executeIfBackToMenu(string name)
    {
        if (name.CompareTo("backToMenu") == 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void executeIfArrowButton(string name)
    {
        if (name.StartsWith("arrow"))
        {
            selectedObject.gameObject.GetComponent<ArrowButtonScript>().ButtonPress();
        }
    }

    void executeIfLevel(string name)
    {
        if (name.StartsWith("level"))
        {
            SceneManager.LoadScene(name);
        }
    }

    void executeIfPlayButton(string name)
    {
        if (name.StartsWith("play"))
        {
            SceneManager.LoadScene("EngineTestScene");
        }
    }
}
