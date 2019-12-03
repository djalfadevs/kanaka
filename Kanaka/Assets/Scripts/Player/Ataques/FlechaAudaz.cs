﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class FlechaAudaz : MonoBehaviour
{
    public GameObject Cube;
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
    public void CallFlecha()
    {
        SpawnFlecha();
    }
    private void SpawnFlecha()
    {
        GameObject q;
        Vector3 aux = player.transform.position + player.transform.TransformDirection(Vector3.forward) * 2;
        q = Instantiate(Cube,aux,player.transform.rotation);
        q.GetComponent<Flecha>().setTeam(player.GetComponent<Player>().GetTeam());
    }
    public void LastCallFlecha()
    {
        if (photonView.IsMine)
        {
            animator.SetBool("Attack",false);
        }
    }
}