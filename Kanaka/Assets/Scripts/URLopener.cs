using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLopener : MonoBehaviour
{
    public string url;
    public void openurl()
    {
        Application.OpenURL(url);
    }
}
