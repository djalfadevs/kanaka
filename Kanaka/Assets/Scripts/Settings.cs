using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer ax;
    public bool mb;
    public Toggle tg;
    private OnlineUser ou;
    private string path = Application.streamingAssetsPath + "/UsersData/MatchInput.json";

    // Start is called before the first frame update

    private void Awake()
    {
        FileInfo fileinfo = new FileInfo(path);
        StreamReader reader = fileinfo.OpenText();
        string text = reader.ReadLine();
        ou = JsonUtility.FromJson<OnlineUser>(text);
        reader.Close();
        tg.isOn = ou.ismobile;
    }
    public void SetVolume(float vol)
    {
        ax.SetFloat("MasterVol", vol);
    }
    public void SetQuality(int qi)
    {
        QualitySettings.SetQualityLevel(qi);
    }
    public void SetMobile()
    {
        mb = tg.isOn;
        FileInfo fileinfo = new FileInfo(path);
        StreamReader reader = fileinfo.OpenText();
        string text = reader.ReadLine();
        ou = JsonUtility.FromJson<OnlineUser>(text);
        reader.Close();
        ou.ismobile = mb;
        File.WriteAllText(path, JsonUtility.ToJson(ou));
    }
}
