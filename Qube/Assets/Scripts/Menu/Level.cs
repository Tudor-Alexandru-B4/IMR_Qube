using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        Debug.Log("Click!");
        Debug.Log(_renderer.gameObject.name);
        SceneManager.LoadScene(_renderer.gameObject.name.ToString());
    }
}

