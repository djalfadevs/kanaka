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
    


    void Awake()
    {
        path = Application.dataPath + "/UsersData/User.json";
        path2 = Application.dataPath + "/StreamingAssets/UsersData/MatchInput.json";

    }

    public void CharSelected(int ch)
    {
        if (System.IO.File.Exists(path))
        {
            FileInfo fileinfo = new FileInfo(path);
            StreamReader reader = fileinfo.OpenText();
            string text = reader.ReadLine();
            u = JsonUtility.FromJson<User>(text);
            un = u.userName;
            reader.Close();

            string text2 = File.ReadAllText(path2);
            if (text != null)
            {
                ou = new OnlineUser(un, ch);
                File.WriteAllText(path2, JsonUtility.ToJson(ou));
                SceneManager.LoadScene("Escena Photon Prueba");
            }
        }
    }
}
