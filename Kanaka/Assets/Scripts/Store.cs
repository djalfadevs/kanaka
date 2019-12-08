using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Store : MonoBehaviour
{

    public GameObject goldtext;
    private string path;
    private User u;

    void Awake()
    {
        path = Application.streamingAssetsPath + "/UsersData/User.json";
    }
    // Start is called before the first frame update
    IEnumerator getRequestmoney(int moneydelta)
    {
        UnityWebRequest request = UnityWebRequest.Get("https://api.myjson.com/bins/asgog");
        yield return request.SendWebRequest();
        u = JsonConvert.DeserializeObject<User>(request.downloadHandler.text);
        u.gameMoney += moneydelta;
        var uwr = UnityWebRequest.Put("https://api.myjson.com/bins/asgog", JsonConvert.SerializeObject(u));
        uwr.SetRequestHeader("Content-Type", "application/json; charset=utf-8");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
            Debug.LogError(uwr.error);
        else
        {
            // file data successfully sent
        }
        goldtext.GetComponent<GetGold>().updateGold();
    }

    IEnumerator getRequestaddchar(int newchar)
    {
        UnityWebRequest request = UnityWebRequest.Get("https://api.myjson.com/bins/asgog");
        yield return request.SendWebRequest();
        u = JsonConvert.DeserializeObject<User>(request.downloadHandler.text);
        if (u.gameMoney >= 120)
        {
            u.charactersID.Add(newchar);
            u.gameMoney += -120;

            var uwr = UnityWebRequest.Put("https://api.myjson.com/bins/asgog", JsonConvert.SerializeObject(u));
            uwr.SetRequestHeader("Content-Type", "application/json; charset=utf-8");
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError)
                Debug.LogError(uwr.error);
            else
            {
                // file data successfully sent
            }

            goldtext.GetComponent<GetGold>().updateGold();
            this.gameObject.SetActive(false);
        }  
    }



    public void moneyUpdate(int moneydelta)
    {
        StartCoroutine(getRequestmoney(moneydelta));
        /*FileInfo fileinfo = new FileInfo(path);
        StreamReader reader = fileinfo.OpenText();
        string text = reader.ReadLine();
        u = JsonUtility.FromJson<User>(text);
        reader.Close();
        u.gameMoney += moneydelta;
        File.WriteAllText(path, JsonUtility.ToJson(u));
        goldtext.GetComponent<GetGold>().updateGold();*/
    }

    public void addChar(int newchar)
    {
        StartCoroutine( getRequestaddchar(newchar));
        /*FileInfo fileinfo = new FileInfo(path);
        StreamReader reader = fileinfo.OpenText();
        string text = reader.ReadLine();
        u = JsonUtility.FromJson<User>(text);
        reader.Close();
        if (u.gameMoney >= 120)
        {
            u.charactersID.Add(newchar);
            u.gameMoney += -120;
            File.WriteAllText(path, JsonUtility.ToJson(u));
            goldtext.GetComponent<GetGold>().updateGold();
            this.gameObject.SetActive(false);
        }*/
    }
}
