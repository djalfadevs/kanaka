using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RupturaSismica : MonoBehaviour
{
    public GameObject cube2;
    public GameObject player;
    public List<GameObject> points;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Awake()
    {
        //photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CallRuptura()
    {
        //if (!photonView.IsMine)
        //   return ;
        SpawnRuptura();
        player.GetComponent<Player>().setCanMove(false);//El personaje no se puede mover
        Debug.Log("holaR");
    }

    private void SpawnRuptura()
    {
        // if (!photonView.IsMine)
        //     return;
        Vector3 aux = player.transform.position + player.transform.TransformDirection(Vector3.forward) * 2;
        GameObject q;
        // if (!PhotonNetwork.IsConnected)
        // {
        q =Instantiate(cube2, aux, player.transform.rotation);
        //  }
        //else
        // {
        //     q = PhotonNetwork.Instantiate("CubeMareas", aux, player.transform.rotation);
        // }
        
        q.GetComponent<Ruptura>().setPlayer(player.GetComponent<Player>());
        animator.SetBool("Attack", false);
    }
    public void LastCallRuptura()
    {
        //if (!photonView.IsMine)
        //     return;
        player.GetComponent<Player>().setCanMove(true);//El personaje puede volver a moverse;
    }
}
