using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    private SaveToDB sv;

    // Start is called before the first frame update
    void Start()
    {
        sv = new SaveToDB();
        sv.savetoDB();
    }
}
