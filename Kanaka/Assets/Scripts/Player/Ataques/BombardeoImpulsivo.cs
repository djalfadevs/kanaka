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
        if (photonView.IsMine)
        {
            player.GetComponent<Player>().setCanMove(false);//El personaje no se puede mover
        }
        SpawnBomba();
    }

    private void SpawnBomba()
    {
        Vector3 aux = player.transform.position + player.transform.TransformDirection(Vector3.forward) * 10;
        GameObject q;
        q = Instantiate(cube2, aux, player.transform.rotation);
    }
    public void LastCallBomba()
    {
        if (photonView.IsMine)
        {
            animator.SetBool("Habilidad", false);
            player.GetComponent<Player>().setCanMove(true);//El personaje puede volver a moverse;
        }
    }
}