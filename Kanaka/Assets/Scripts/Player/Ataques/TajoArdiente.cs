using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TajoArdiente : MonoBehaviour
{
    public GameObject CubeI;
    public GameObject CubeD;
    public GameObject player;
    private Animator animator;
    private PhotonView photonView;
    public GameObject point1;
    public GameObject point2;
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

    public void CallDa()
    {
        if (PhotonNetwork.IsConnected && photonView.IsMine)
        {
            if (Input.GetMouseButton(0))
            {
                animator.SetInteger("NumAttack", 2);
                SpawnCubeDcha();
            }
            else
            {
                EndTajo();
            }
        }
        else if (!PhotonNetwork.IsConnected)
        {
            if (Input.GetMouseButton(0))
            {
                animator.SetInteger("NumAttack", 2);
                SpawnCubeDcha();
            }
            else
            {
                EndTajo();
            }
        }
    }

    public void ReturnIz()
    {
        if (PhotonNetwork.IsConnected&&photonView.IsMine)
        {
            if (Input.GetMouseButton(0))
            {
                animator.SetInteger("NumAttack", 1);
                SpawnCubeIz();
            }
            else
            {
                EndTajo();
            }
        }
        else if (!PhotonNetwork.IsConnected)
        {
            if (Input.GetMouseButton(0))
            {
                animator.SetInteger("NumAttack", 1);
                SpawnCubeIz();
            }
            else
            {
                EndTajo();
            }
        }
    }

    public void CallIz()
    {
        if (PhotonNetwork.IsConnected&&photonView.IsMine)
        {
            animator.SetInteger("NumAttack", 1);
            player.GetComponent<Player>().setCanMove(false);//El personaje no se puede mover
        }
        else if (!PhotonNetwork.IsConnected)
        {
            animator.SetInteger("NumAttack", 1);
            player.GetComponent<Player>().setCanMove(false);//El personaje no se puede mover
        }
        SpawnCubeIz();
        Debug.Log("holaR");
    }

    private void SpawnCubeIz()
    {
        GameObject q;
        q = Instantiate(CubeI, this.point1.transform.position+ this.player.transform.TransformDirection(Vector3.forward) * 1.5f,
            player.transform.rotation);
        Instantiate(effect,player.transform.position + this.player.transform.TransformDirection(Vector3.forward) * 3, player.transform.rotation);
        q.GetComponent<TajoIzq>().setItems(player.GetComponent<Player>().GetTeam(),
            this,this.point2.transform.position+this.player.transform.TransformDirection(Vector3.forward)*3);
    }

    public void SpawnCubeDcha()
    {
        GameObject q;
        Instantiate(effect, player.transform.position + this.player.transform.TransformDirection(Vector3.forward) * 3, player.transform.rotation);
        q = Instantiate(CubeD, this.point2.transform.position + this.player.transform.TransformDirection(Vector3.forward) * 1.5f
            , player.transform.rotation);  
        q.GetComponent<TajoDcha>().setItems(player.GetComponent<Player>().GetTeam(), this,
            this.point1.transform.position+ this.player.transform.TransformDirection(Vector3.forward) * 3);
    }

    public void EndTajo()
    {
        if (PhotonNetwork.IsConnected&&photonView.IsMine)
        {
            animator.SetBool("Attack", false);
            animator.SetInteger("NumAttack",0);
            player.GetComponent<Player>().setCanMove(true);

        }
        else if (!PhotonNetwork.IsConnected)
        {
            animator.SetBool("Attack", false);
            animator.SetInteger("NumAttack", 0);
            player.GetComponent<Player>().setCanMove(true);
        }
    }
}
