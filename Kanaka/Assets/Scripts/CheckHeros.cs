using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CheckHeros : MonoBehaviour
{
    public Button hero1;
    public Button hero2;
    public Button hero3;
    public Button hero4;
    private string path;
    private User u;


    void Awake()
    {
        path = Application.streamingAssetsPath + "/UsersData/User.json";
    }

    // Start is called before the first frame update
    void Start()
    {
        FileInfo fileinfo = new FileInfo(path);
        StreamReader reader = fileinfo.OpenText();
        string text = reader.ReadLine();
        u = JsonUtility.FromJson<User>(text);
        List<int> l = u.charactersID;
        if (l.Contains(0))
        {
            hero1.interactable = true;
        }
        if (l.Contains(1))
        {
            hero2.interactable = true;

        }
        if (l.Contains(2))
        {
            hero3.interactable = true;

        }
        if (l.Contains(3))
        {
            hero4.interactable = true;

        }
    }
}
