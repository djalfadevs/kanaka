using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IfFirstLogin : MonoBehaviour
{
    private string first;
    private string not;

    private string path;
    private User u;
    public GameSignUpInManager gsm;

    void Awake()
    {
        path = Application.dataPath + "/UsersData/User.json";
        first = "Select First Chapter";
        not = "MainMenu";

    }

    public void loadScene()
    {
        if (gsm.correctlog)
        {
            if (System.IO.File.Exists(path))
            {
                FileInfo fileinfo = new FileInfo(path);
                StreamReader reader = fileinfo.OpenText();
                string text = reader.ReadLine();
                u = JsonUtility.FromJson<User>(text);
                List<int> l = u.charactersID;
                Debug.Log(l.Count);
                if (l.Count == 0)
                {
                    SceneManager.LoadScene(first);
                }
                else
                {
                    SceneManager.LoadScene(not);
                }
                reader.Close();
            }
        }
    }
}
