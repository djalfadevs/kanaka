using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class StoreStart : MonoBehaviour
{
    public GameObject hero1;
    public GameObject hero2;
    public GameObject hero3;
    public GameObject hero4;
    private string path;
    private User u;


    IEnumerator getRequest(string uri)
    {
        UnityWebRequest request = UnityWebRequest.Get("https://api.myjson.com/bins/asgog");
        yield return request.SendWebRequest();
        u = JsonConvert.DeserializeObject<User>(request.downloadHandler.text);

        List<int> l = u.charactersID;
        if (l.Contains(0))
        {
            hero1.SetActive(false);
        }
        if (l.Contains(1))
        {
            hero2.SetActive(false);

        }
        if (l.Contains(2))
        {
            hero3.SetActive(false);

        }
        if (l.Contains(3))
        {
            hero4.SetActive(false);

        }
    }

    void Awake()
    {
        path = Application.streamingAssetsPath + "/UsersData/User.json";
    }
    // Start is called before the first frame update
    void Start()
    {
        if(true){
            StartCoroutine(getRequest(path));
        }
        else
        {
            FileInfo fileinfo = new FileInfo(path);
            StreamReader reader = fileinfo.OpenText();
            string text = reader.ReadLine();
            u = JsonUtility.FromJson<User>(text);
            reader.Close();
        }

        List<int> l = u.charactersID;
        if (l.Contains(0))
        {
            hero1.SetActive(false);
        }
        if (l.Contains(1))
        {
            hero2.SetActive(false);

        }
        if (l.Contains(2))
        {
            hero3.SetActive(false);

        }
        if (l.Contains(3))
        {
            hero4.SetActive(false);

        }

    }
}
