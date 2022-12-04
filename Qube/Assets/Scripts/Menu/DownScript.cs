using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownScript : MonoBehaviour
{
    private Renderer _renderer;
    public GameObject grid1;
    public GameObject grid2;

    void Start()
    {
        _renderer = GetComponent<Renderer>();

    }

    private void OnMouseDown()
    {
        Debug.Log("Click!");
        Debug.Log(_renderer.gameObject.name);

        grid1.SetActive(false);

        grid2.SetActive(true);
    }
}
