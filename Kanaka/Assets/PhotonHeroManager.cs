using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PhotonHeroManager : MonoBehaviourPun
{
    public static GameObject LocalPlayerInstance;
    [SerializeField] private GameObject model;

    // Start is called before the first frame update
    void Start()
    {
        PhotonView photonview = GetComponent<PhotonView>();
        PhotonTransformView aux = model.AddComponent<PhotonTransformView>();
        photonview.ObservedComponents.Add(aux);

        if (photonView.IsMine)
        {
            LocalPlayerInstance = this.gameObject;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
