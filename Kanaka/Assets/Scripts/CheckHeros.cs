using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CheckHeros : MonoBehaviour
{
    public GameObject hero1;
    public GameObject hero2;
    public GameObject hero3;
    public GameObject hero4;
    private string path;
    private User u;

    void Awake()
    {
        path = Application.dataPath + "/UsersData/User.json";
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
            hero1.GetComponent<Image>().color = new Color(1,1,1,1);
            hero1.GetComponent<Button>().enabled = true;
        }
        if (l.Contains(1))
        {
            hero2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            hero2.GetComponent<Button>().enabled = true;
        }
        if (l.Contains(2))
        {
            hero3.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            hero3.GetComponent<Button>().enabled = true;
        }
        if (l.Contains(3))
        {
            hero4.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            hero4.GetComponent<Button>().enabled = true;
        }
    }
}
