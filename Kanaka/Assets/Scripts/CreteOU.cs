using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class CreteOU : MonoBehaviour
{
    private string path;
    private string path2;

    private User u;
    private OnlineUser ou;

    IEnumerator getRequest(string uri)
    {
        UnityWebRequest request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();
        string text = request.downloadHandler.text;
        u = JsonUtility.FromJson<User>(text);
        ou = new OnlineUser(u.name, u.charactersID[0], false, (int)Random.Range(0.0f, 1.0f));
    }

    IEnumerator UploadFile(string formData)
    {
        UnityWebRequest www = UnityWebRequest.Put(path2, formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    void Awake()
    {
        path = Application.streamingAssetsPath + "/UsersData/User.json";
        path2 = Application.streamingAssetsPath + "/UsersData/MatchInput.json";

    }
    // Start is called before the first frame update
    void Start()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            StartCoroutine(getRequest(path));
            StartCoroutine(UploadFile(JsonUtility.ToJson(ou)));
        }
        else
        {
        if (System.IO.File.Exists(path))
        {
             FileInfo fileinfo = new FileInfo(path);
             StreamReader reader = fileinfo.OpenText();
             string text = reader.ReadLine();
             u = JsonUtility.FromJson<User>(text);

             string text2 = File.ReadAllText(path2);
             if (text != null)
             {
             ou = new OnlineUser(u.name, u.charactersID[0], false, (int)Random.Range(0.0f, 1.0f));
             File.WriteAllText(path2, JsonUtility.ToJson(ou));
             }
             reader.Close();

        }
            
            
        }
        
    }
}
