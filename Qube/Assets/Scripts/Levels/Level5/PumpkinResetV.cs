using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinResetV : MonoBehaviour
{
    [SerializeField]
    float stopFallingOnY = -1;
    [SerializeField]
    float destroyOnY = -15;
    [SerializeField]
    GameObject pumpkin;

    Rigidbody rb;
    bool isReseting = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;

        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

        foreach(Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name != "Cube-Pumpkin" && GameObject.Find("Cube-Pumpkin") == null)
        {
            gameObject.name = "Cube-Pumpkin";
        }

        if (!isReseting && rb.useGravity && gameObject.transform.position.y <= stopFallingOnY)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        if (isReseting && gameObject.transform.position.y <= destroyOnY)
        {
            Destroy(gameObject);
        }
    }

    public void Reset()
    {
        isReseting = true;
        Vector3 spawnPosition = gameObject.transform.position;
        spawnPosition.y += 15;
        Instantiate(pumpkin, spawnPosition, Quaternion.identity);
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}
