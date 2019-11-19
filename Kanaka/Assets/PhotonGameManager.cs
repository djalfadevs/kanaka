using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonGameManager : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Awake()
    {
     
    }
    void Start()
    {
        if (PhotonHeroManager.LocalPlayerInstance == null)
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            PhotonNetwork.Instantiate("Hero Photon", new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        }
        else
        {
            Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
