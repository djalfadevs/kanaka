using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LlamaradaImpulsiva : MonoBehaviour
{
    public GameObject cube;
    public Player player;
    private Animator animator;
    private PhotonView photonView;
    public GameObject effect;
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
    public void CallLlamarada()
    {
        if (PhotonNetwork.IsConnected && photonView.IsMine)
        {
            player.GetComponent<Player>().setCanMove(false);//El personaje no se puede mover
        }
        else if (!PhotonNetwork.IsConnected)
        {
            player.GetComponent<Player>().setCanMove(false);//El personaje no se puede mover
        }
        SpawnLlamarada();
    }

    private void SpawnLlamarada()
    {
        Vector3 aux = player.transform.position + player.transform.TransformDirection(Vector3.forward);
        GameObject q;
        Instantiate(effect, player.transform.position, player.transform.rotation);
        q = Instantiate(cube, aux, player.transform.rotation,player.transform);
        
        q.GetComponent<Llamarada>().setPlayer(this.player,this);
    }
    public void LastCallLlamarada()
    {
        if (PhotonNetwork.IsConnected && photonView.IsMine)
        {
            Debug.Log("holaRRR");
            animator.SetBool("Habilidad", false);
            player.GetComponent<Player>().setCanMove(true);


        }
        else if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("holaRRR");
            animator.SetBool("Habilidad", false);
            player.GetComponent<Player>().setCanMove(true);
        }
    }
}
