﻿ ﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviourPun
{
    [SerializeField] private Player player;
    private PhotonView ph;
  

    private RuntimePlatform platform
    {
        get
        {
            #if UNITY_ANDROID
                return RuntimePlatform.Android;
            #elif UNITY_IOS
                return RuntimePlatform.IPhonePlayer;
            #elif UNITY_STANDALONE_OSX
                return RuntimePlatform.OSXPlayer;
            #elif UNITY_STANDALONE_WIN
                return RuntimePlatform.WindowsPlayer;
            #else
            return RuntimePlatform.WebGLPlayer;

            #endif
        }
    }

    private void Awake()
    {
        ph = GetComponentInParent<PhotonView>();
    }

    void Start()
    {
        player = this.GetComponent<Player>();
    }

    void attackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.attack();
            //Debug.Log("Atacamos");
        }
        if (Input.GetMouseButtonUp(1))
        {
            player.ability();
        }

    }

    void Update()
    {

        if (PhotonNetwork.IsConnected == true)
        {
            if (ph.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }
        }

        if (platform == RuntimePlatform.Android)
        {
            float auxAxisHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float auxAxisVertical = CrossPlatformInputManager.GetAxis("Vertical");

            player.moveAndroid(auxAxisHorizontal,auxAxisVertical);
            
        }

        else
        {
            Vector3 mouse_pos;
            mouse_pos = Input.mousePosition;
            mouse_pos.z = 5; //The distance between the camera and object
            float vertical = Input.GetAxis("Vertical");
            if (mouse_pos != null)
            {
                player.move(mouse_pos, vertical);
                attackInput();
            }
        }
        
    }
}