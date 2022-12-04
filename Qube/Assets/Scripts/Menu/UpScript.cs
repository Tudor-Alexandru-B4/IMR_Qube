using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpScript : MonoBehaviour
{
    public GameObject grid1;
    public GameObject grid2;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Click!");

        grid2.SetActive(false);

        grid1.SetActive(true);

    }
}
