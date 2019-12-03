using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer ax;
    // Start is called before the first frame update
   public void SetVolume(float vol)
    {
        ax.SetFloat("MasterVol", vol);
    }
    public void SetQuality(int qi)
    {
        QualitySettings.SetQualityLevel(qi);
    }
    public void SetFullScreen(bool fs)
    {
        Screen.fullScreen = fs;
    }
}
