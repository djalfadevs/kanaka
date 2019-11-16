using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facecam : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }
}
