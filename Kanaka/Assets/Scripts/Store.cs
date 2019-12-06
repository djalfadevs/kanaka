using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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


    public void moneyUpdate(int moneydelta)
    {
        FileInfo fileinfo = new FileInfo(path);
        StreamReader reader = fileinfo.OpenText();
        string text = reader.ReadLine();
        u = JsonUtility.FromJson<User>(text);
        reader.Close();
        u.gameMoney += moneydelta;
        File.WriteAllText(path, JsonUtility.ToJson(u));
        goldtext.GetComponent<GetGold>().updateGold();
    }

    public void addChar(int newchar)
    {
        FileInfo fileinfo = new FileInfo(path);
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
        }
    }
}
