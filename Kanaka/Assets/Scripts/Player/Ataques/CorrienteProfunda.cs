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
        if (photonView.IsMine && PhotonNetwork.IsConnected)
        {
            player.GetComponent<Player>().setCanMove(false);//El personaje no se puede mover
        }
        else if (!PhotonNetwork.IsConnected)
        {
            player.GetComponent<Player>().setCanMove(false);//El personaje no se puede mover
        }
        SpawnCorriente();
        Debug.Log("holaR");
    }

    private void SpawnCorriente()
    {
        GameObject q;
        q = Instantiate(Sphere, player.transform.position, player.transform.rotation);
        q.GetComponent<Corriente>().setPlayer(player.GetComponent<Player>().GetTeam());
    }
    public void LastCallCorriente()
    {
        Debug.Log("holaRR");
        if (photonView.IsMine && PhotonNetwork.IsConnected)
        {
            Debug.Log("holaRRR");
            animator.SetBool("Habilidad", false);
            player.GetComponent<Player>().setCanMove(true);
           

        }
        else if(!PhotonNetwork.IsConnected)
        {
            Debug.Log("holaRRR");
            animator.SetBool("Habilidad", false);
            player.GetComponent<Player>().setCanMove(true);
        }
    }
}
