using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MareasAgresivas : MonoBehaviourPun
{
    public GameObject cube;
    public GameObject player;
    public List<GameObject> points;
    private Animator animator;
    private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void FirstCall()
    {
        if (!photonView.IsMine)
            return ;
        
        animator.SetBool("Attack", false);
        FirstSpawn();
        //Debug.Log("hola");
        player.GetComponent<Player>().setCanMove(false);//El personaje no se puede mover
    }

    private void FirstSpawn()
    {
        if (!photonView.IsMine)
            return;

        Debug.Log("Lo estoy intentando seriamente");
        Vector3 aux= player.transform.position + player.transform.TransformDirection(Vector3.forward)*2;
        GameObject q;
        if (!PhotonNetwork.IsConnected)
        {
             q= Instantiate(cube, aux, player.transform.rotation);
        }
        else
        {
            q = PhotonNetwork.Instantiate("CubeMareas", aux, player.transform.rotation);
        }
        q.GetComponent<Mareas1>().setPlayer(player.GetComponent<Player>(),-1);
    }

    public void SecondCall()
    {
        if (!photonView.IsMine)
            return;

        SecondSpawn();
        //Debug.Log("Segundohola");
        GetComponent<Animator>().ResetTrigger("Attack");
    }

    private void SecondSpawn()
    {
        Vector3 aux = player.transform.position + player.transform.TransformDirection(Vector3.left) * 2;
        GameObject q;
        if (!PhotonNetwork.IsConnected)
        {
            q= Instantiate(cube, aux, player.transform.rotation);
        }
        else
        {
            q = PhotonNetwork.Instantiate("CubeMareas", aux, player.transform.rotation);
        }  
        q.GetComponent<Mareas1>().setPlayer(player.GetComponent<Player>(),0);
        q.GetComponent<Mareas1>().setPoints(this.points);
    }
    
    public void LastCall()
    {
        if (!photonView.IsMine)
            return;
        player.GetComponent<Player>().setCanMove(true);//El personaje puede volver a moverse;
    }


}
