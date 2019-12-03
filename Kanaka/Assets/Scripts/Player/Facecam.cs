using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facecam : MonoBehaviour
{
    private Transform mainCamera;
    // Update is called once per frame

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        this.transform.Rotate(0, 180, 0);
    }
    void Update()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}
