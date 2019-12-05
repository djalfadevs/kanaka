using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistSound : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            Destroy(this);
        }
    }
}
