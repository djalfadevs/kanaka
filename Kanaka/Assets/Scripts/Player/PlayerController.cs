 ﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviourPun
{
    [SerializeField] private Player player;
    private PhotonView ph;

    private string path = Application.streamingAssetsPath + "/UsersData/MatchInput.json";
    private OnlineUser ou;
    private GameObject fj;


    private void Awake()
    {
        ph = GetComponentInParent<PhotonView>();

    }

    void Start()
    {
        fj = GameObject.FindGameObjectWithTag("MobileInput").transform.GetChild(0).gameObject;
        player = this.GetComponent<Player>();
        string text = File.ReadAllText(path);
        if (text != null)
        {
            ou = JsonUtility.FromJson<OnlineUser>(text);
        }
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

        if (ou.ismobile)
        {
            fj.SetActive(true);
            float auxAxisHorizontal = fj.GetComponent<FixedJoystick>().Horizontal;
            float auxAxisVertical = fj.GetComponent<FixedJoystick>().Vertical;
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