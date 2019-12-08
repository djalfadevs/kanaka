using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer ax;
    public bool mb;
    public Toggle tg;
    private OnlineUser ou;
    private string path = Application.streamingAssetsPath + "/UsersData/MatchInput.json";

    // Start is called before the first frame update

    IEnumerator getRequest1()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://api.myjson.com/bins/88as0");
        yield return request.SendWebRequest();
        ou = JsonConvert.DeserializeObject<OnlineUser>(request.downloadHandler.text);

        tg.isOn = ou.ismobile;
    }

    IEnumerator getRequest2()
    {
        mb = tg.isOn;

        UnityWebRequest request = UnityWebRequest.Get("https://api.myjson.com/bins/88as0");
        yield return request.SendWebRequest();
        ou = JsonConvert.DeserializeObject<OnlineUser>(request.downloadHandler.text);

        ou.ismobile = mb;

        var uwr = UnityWebRequest.Put("https://api.myjson.com/bins/88as0", JsonConvert.SerializeObject(ou));
        uwr.SetRequestHeader("Content-Type", "application/json; charset=utf-8");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
            Debug.LogError(uwr.error);
        else
        {
            // file data successfully sent
        }
    }

    private void Awake()
    {
        getRequest1();
        /*FileInfo fileinfo = new FileInfo(path);
        StreamReader reader = fileinfo.OpenText();
        string text = reader.ReadLine();
        ou = JsonUtility.FromJson<OnlineUser>(text);
        reader.Close();
        tg.isOn = ou.ismobile;*/
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
        getRequest2();/*
        mb = tg.isOn;
        FileInfo fileinfo = new FileInfo(path);
        StreamReader reader = fileinfo.OpenText();
        string text = reader.ReadLine();
        ou = JsonUtility.FromJson<OnlineUser>(text);
        reader.Close();
        ou.ismobile = mb;
        File.WriteAllText(path, JsonUtility.ToJson(ou));*/
    }
}
