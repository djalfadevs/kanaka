using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CheckSkins : MonoBehaviour
{
    public GameObject skin1;
    public GameObject skin2;
    public GameObject skin3;
    public GameObject skin4;
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
        List<int> l = u.skinsIDList;
        if (l.Contains(0))
        {
            skin1.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            skin1.GetComponent<Button>().enabled = true;
        }
        if (l.Contains(1))
        {
            skin2.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            skin2.GetComponent<Button>().enabled = true;
        }
        if (l.Contains(2))
        {
            skin3.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            skin3.GetComponent<Button>().enabled = true;
        }
        if (l.Contains(3))
        {
            skin4.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            skin4.GetComponent<Button>().enabled = true;
        }
    }
}
