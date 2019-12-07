using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebGlFileLoader : MonoBehaviour
{

public static IEnumerator loadStreamingAsset(string fileName)
{
    string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

    string result;
    if (filePath.Contains("://") || filePath.Contains(":///"))
    {
        UnityWebRequest www = new UnityWebRequest(filePath);
        yield return www.SendWebRequest();
    }
    else
        result = System.IO.File.ReadAllText(filePath);
}
}
