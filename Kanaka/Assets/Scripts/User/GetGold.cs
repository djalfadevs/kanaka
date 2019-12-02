using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class GetGold : MonoBehaviour
{
    private string path;
    private User u;
    public TextMeshProUGUI t;

    void Awake()
    {
        path = Application.dataPath + "/UsersData/User.json";
    }

    void Start()
    {
        if (System.IO.File.Exists(path))
        {
            FileInfo fileinfo = new FileInfo(path);
            StreamReader reader = fileinfo.OpenText();
            string text = reader.ReadLine();
            u = JsonUtility.FromJson<User>(text);
            t.text = u.gameMoney.ToString();
            reader.Close();
        }
    }
}
