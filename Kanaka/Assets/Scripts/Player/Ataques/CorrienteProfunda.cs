using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CorrienteProfunda : MonoBehaviour
{
    public GameObject Sphere;
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
    public void CallCorriente()
    {
        if (!PhotonNetwork.IsMasterClient)
           return ;
        SpawnCorriente();
        player.GetComponent<Player>().setCanMove(false);//El personaje no se puede mover
        Debug.Log("holaR");
    }

    private void SpawnCorriente()
    {
         if (!PhotonNetwork.IsMasterClient)
             return;
        GameObject q;
         if (!PhotonNetwork.IsConnected)
         {
            q = Instantiate(Sphere, player.transform.position, player.transform.rotation);
         }
        else
         {
             q = PhotonNetwork.Instantiate("Sphere", player.transform.position, player.transform.rotation);
        }
        q.GetComponent<Corriente>().setPlayer(player.GetComponent<Player>().GetTeam());
        animator.SetBool("Habilidad",false);
    }
    public void LastCallCorriente()
    {
        if (!PhotonNetwork.IsMasterClient)
             return;
        player.GetComponent<Player>().setCanMove(true);//El personaje puede volver a moverse;
    }
}
