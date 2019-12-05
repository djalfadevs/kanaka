using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Botella : MonoBehaviourPun
{
    private Animator animator;
    private PhotonView photonView;
    private bool isTake = false;
    private bool canTake = false;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        animator.SetTrigger("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)//Si colisiona con un jugador
        {
            if (!PhotonNetwork.IsConnected)//OFFLINE
            {
                if (!isTake && canTake)
                {
                    other.GetComponent<Player>().Heal((int)other.GetComponent<Player>().MaxHP / 2);
                    isTake = true;
                    Destroy(this.gameObject);
                }
            }
            else
            {
                if (!isTake && canTake && PhotonNetwork.IsMasterClient)//online
                {
                    other.GetComponent<Player>().photonView.RpcSecure("Heal", RpcTarget.All,false,(int) other.GetComponent<Player>().MaxHP/2);//Manda el mensaje de curar a todos.
                    isTake = true;
                    PhotonNetwork.Destroy(this.gameObject);
                }
            }
            
        }
    }

    public void DestroyBottle()
    {
       //Destroy(this.gameObject);
    }

    public void CanTake()
    {
        canTake = true;
    }

}
