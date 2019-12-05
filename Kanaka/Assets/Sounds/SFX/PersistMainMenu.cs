using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistMainMenu : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            Destroy(this);
        }
    }
}
