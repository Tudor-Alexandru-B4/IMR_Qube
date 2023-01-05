using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    GameObject selectedObject;
    RaycastHit hit;
    Ray ray;

    void Start()
    {
        camera = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    [Button]
    void PlayLatestLevel()
    {
        SceneManager.LoadScene("level1");
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ray = camera.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                selectedObject = hit.collider.gameObject;
                string selectedName = selectedObject.name.ToString();
                ExecuteIfLevel(selectedName);
                ExecuteIfArrowButton(selectedName);
                ExecuteIfPlayButton(selectedName);
            }
        }
    }

    void ExecuteIfArrowButton(string name)
    {
        if (name.StartsWith("arrow"))
        {
            selectedObject.gameObject.GetComponent<ArrowButtonScript>().ButtonPress();
        }
    }

    void ExecuteIfLevel(string name)
    {
        if (name.StartsWith("level") && selectedObject.transform.childCount == 0)
        {
            SceneManager.LoadScene(name);
        }
    }

    void ExecuteIfPlayButton(string name)
    {
        if (name.StartsWith("play"))
        {
            SceneManager.LoadScene("EngineTestScene");
        }
    }
}
