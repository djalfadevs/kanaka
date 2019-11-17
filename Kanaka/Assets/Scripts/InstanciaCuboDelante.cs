using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciaCuboDelante : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ataque(GameObject player)
    {
        GameObject aux = GameObject.CreatePrimitive(PrimitiveType.Cube);
        aux.transform.position = player.transform.position;
    }
}
