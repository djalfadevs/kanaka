﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombardeoImpulsivo : MonoBehaviour
{
    public GameObject cube2;
    public GameObject player;
    private Animator animator;
    private PhotonView photonView;
    public GameObject effect;
    public GameObject effect2;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CallBomba()
    {
        if (PhotonNetwork.IsConnected&&photonView.IsMine)
        {
            player.GetComponent<Player>().setCanMove(false);//El personaje no se puede mover
        }
        else if (!PhotonNetwork.IsConnected)
        {
            player.GetComponent<Player>().setCanMove(false);//El personaje no se puede mover
        }
        SpawnBomba();
    }

    private void SpawnBomba()
    {
        Vector3 aux = player.transform.position + player.transform.TransformDirection(Vector3.forward) * 10;
        GameObject q;
        GameObject qq;
        Instantiate(effect, player.transform.position, player.transform.rotation);
        qq=Instantiate(effect2, aux, player.transform.rotation);
        q = Instantiate(cube2, aux, player.transform.rotation);
        q.GetComponent<Bombardeo>().setPlayer(player.GetComponent<Player>());
    }
    public void LastCallBomba()
    {
        if (PhotonNetwork.IsConnected && photonView.IsMine)
        {
            animator.SetBool("Habilidad", false);
            player.GetComponent<Player>().setCanMove(true);
        }
        else if (!PhotonNetwork.IsConnected)
        {
            animator.SetBool("Habilidad", false);
            player.GetComponent<Player>().setCanMove(true);
        }
    }
}
