using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class CharacterSelect : MonoBehaviour
{
    private string path;
    private string path2;

    private User u;
    private OnlineUser ou;
    private string un;
    private bool im;



    IEnumerator getRequest2(string uri,int ch)
    {
        UnityWebRequest request = UnityWebRequest.Get("https://api.myjson.com/bins/asgog");
        yield return request.SendWebRequest();
        string text = request.downloadHandler.text;
        u = JsonUtility.FromJson<User>(text);
        un = u.name;

        UnityWebRequest request2 = UnityWebRequest.Get("https://api.myjson.com/bins/88as0");
        yield return request2.SendWebRequest();
        string text2 = request2.downloadHandler.text;

        ou = JsonUtility.FromJson<OnlineUser>(text2);
        un = ou.userName;
        im = ou.ismobile;
        ou = new OnlineUser(un, ch, im, (int)Random.Range(0.0f, 1.0f));

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

    public void CharSelected(int ch)
    {
        if(true)
        {
            StartCoroutine(getRequest2(path2,ch));
        }
        else
        {
            if (System.IO.File.Exists(path))
            {
                FileInfo fileinfo = new FileInfo(path);
                StreamReader reader = fileinfo.OpenText();
                string text = reader.ReadLine();
                u = JsonUtility.FromJson<User>(text);
                un = u.name;
                reader.Close();

                string text2 = File.ReadAllText(path2);
                if (text != null)
                {
                    ou = JsonUtility.FromJson<OnlineUser>(text);
                    un = ou.userName;
                    im = ou.ismobile;
                    ou = new OnlineUser(un, ch, im, (int)Random.Range(0.0f, 1.0f));
                    File.WriteAllText(path2, JsonUtility.ToJson(ou));
                }
            }
        }
        
    }
}
