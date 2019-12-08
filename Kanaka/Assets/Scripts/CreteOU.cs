using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class CreteOU : MonoBehaviour
{
    private string path;
    private string path2;

    private User u;
    private OnlineUser ou;

    IEnumerator getRequest(string uri)
    {
        UnityWebRequest request = UnityWebRequest.Get("https://api.myjson.com/bins/asgog");
        yield return request.SendWebRequest();
        string text = request.downloadHandler.text;
        u = JsonUtility.FromJson<User>(text);

        UnityWebRequest request2 = UnityWebRequest.Get("https://api.myjson.com/bins/88as0");
        yield return request2.SendWebRequest();
        string text2 = request.downloadHandler.text;
        OnlineUser AuxOu = JsonConvert.DeserializeObject<OnlineUser>(text2);

        if (AuxOu == null)
        ou = new OnlineUser(u.name, u.charactersID[0], false, (int)Random.Range(0.0f, 1.0f));

        var uwr = UnityWebRequest.Put("https://api.myjson.com/bins/88as0", JsonConvert.SerializeObject(ou));
        uwr.SetRequestHeader("Content-Type", "application/json; charset=utf-8");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
            Debug.LogError(uwr.error);
        else
        {
            // file data successfully sent
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
        if(true)
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
