using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    private string path;
    private string path2;

    private User u;
    private OnlineUser ou;
    private string un;
    private bool im;
    


    void Awake()
    {
        path = Application.streamingAssetsPath + "/UsersData/User.json";
        path2 = Application.streamingAssetsPath + "/UsersData/MatchInput.json";

    }

    public void CharSelected(int ch)
    {
        if (System.IO.File.Exists(path))
        {
            FileInfo fileinfo = new FileInfo(path);
            StreamReader reader = fileinfo.OpenText();
            string text = reader.ReadLine();
            u = JsonUtility.FromJson<User>(text);
            un = u.name;
            reader.Close();

            string text2 = File.ReadAllText(path2);
            if (text != null)
            {
                ou = JsonUtility.FromJson<OnlineUser>(text);
                un = ou.userName;
                im = ou.ismobile;
                ou = new OnlineUser(un, ch, im,  (int) Random.Range(0.0f,1.0f));
                File.WriteAllText(path2, JsonUtility.ToJson(ou));
            }
        }
    }
}
