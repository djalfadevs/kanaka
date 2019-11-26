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
        //PhotonTransformView aux = model.GetComponent<PhotonTransformView>();
        //aux.m_SynchronizePosition = false;
        Player aux2 = model.GetComponent<Player>();
        //photonview.ObservedComponents.Add(aux);
        photonview.ObservedComponents.Add(aux2);
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
