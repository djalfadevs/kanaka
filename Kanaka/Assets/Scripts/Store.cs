using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject hero1;
    public GameObject hero2;
    public GameObject hero3;
    public GameObject hero4;
    public GameObject goldtext;
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
        reader.Close();
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

    public void moneyUpdate(int moneydelta)
    {
        FileInfo fileinfo = new FileInfo(path);
        StreamReader reader = fileinfo.OpenText();
        string text = reader.ReadLine();
        u = JsonUtility.FromJson<User>(text);
        reader.Close();
        u.gameMoney += moneydelta;
        goldtext.GetComponent<GetGold>().updateGold();
        File.WriteAllText(path, JsonUtility.ToJson(u));
    }

    public void addChar(int newchar)
    {
        FileInfo fileinfo = new FileInfo(path);
        StreamReader reader = fileinfo.OpenText();
        string text = reader.ReadLine();
        u = JsonUtility.FromJson<User>(text);
        reader.Close();
        if (u.gameMoney >= 100)
        {
            u.charactersID.Add(newchar);
            u.gameMoney += -100;
            File.WriteAllText(path, JsonUtility.ToJson(u));
            goldtext.GetComponent<GetGold>().updateGold();
            this.gameObject.SetActive(false);
        }
    }
}
