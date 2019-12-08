using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public string toLoad;

    public void loadScene()
    {
        SceneManager.LoadScene(toLoad);
    }

    public void leftRoom()
    {
        Photon.Pun.PhotonNetwork.Disconnect();
    }
}
