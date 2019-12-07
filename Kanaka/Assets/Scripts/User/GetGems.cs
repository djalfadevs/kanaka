using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class GetGems : MonoBehaviour
{
    private string path;
    private User u;
    public TextMeshProUGUI t;


    IEnumerator getRequest(string uri)
    {
        UnityWebRequest request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();
        string text = request.downloadHandler.text;
        u = JsonUtility.FromJson<User>(text);
        t.text = u.realMoney.ToString();

    }

    void Awake()
    {
        path = Application.streamingAssetsPath + "/UsersData/User.json";
    }
    void Start()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            StartCoroutine(getRequest(path));
        }
        else
        {
            if (System.IO.File.Exists(path))
            {
                FileInfo fileinfo = new FileInfo(path);
                StreamReader reader = fileinfo.OpenText();
                string text = reader.ReadLine();
                u = JsonUtility.FromJson<User>(text);
                t.text = u.realMoney.ToString();
                reader.Close();

            }
        }
     
    }
}
