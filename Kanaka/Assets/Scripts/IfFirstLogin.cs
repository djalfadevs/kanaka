using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class IfFirstLogin : MonoBehaviour
{
    private string first;
    private string not;

    private string path;
    private User u;
    public GameSignUpInManager gsm;
    List<int> l;

    void Awake()
    {
        path = Application.streamingAssetsPath + "/UsersData/User.json";
        first = "Select First Chapter";
        not = "MainMenu";

    }

    IEnumerator getRequest(string uri)
    {
        UnityWebRequest request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();
        string text2 = request.downloadHandler.text;
        u = JsonUtility.FromJson<User>(text2);
        l = u.charactersID;

    }

    public void loadScene()
    {
        if (gsm.correctlog)
        {
            l = new List<int>();
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                StartCoroutine(getRequest(path));
            }
            else
            {
                //Debug.LogError("LLEGA AQUI");
                if (System.IO.File.Exists(path))
                {
                    FileInfo fileinfo = new FileInfo(path);
                    StreamReader reader = fileinfo.OpenText();
                    string text = reader.ReadLine();
                    u = JsonUtility.FromJson<User>(text);
                    l = u.charactersID;
                    Debug.Log(l.Count);
                    reader.Close();
                }
            }

            if (l.Count == 0)
            {
                GameObject objs = GameObject.FindGameObjectWithTag("Music");
                Destroy(objs);
                SceneManager.LoadScene(first);
            }
            else
            {
                GameObject objs = GameObject.FindGameObjectWithTag("Music");
                Destroy(objs);
                SceneManager.LoadScene(not);
            }

        }
    }
}
