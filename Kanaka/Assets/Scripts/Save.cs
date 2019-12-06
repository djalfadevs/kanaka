using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        SaveToDB.savetoDB(); 
    }
}
