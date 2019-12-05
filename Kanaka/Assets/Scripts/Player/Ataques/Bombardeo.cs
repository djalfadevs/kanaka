using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombardeo : Attack
{
    [SerializeField] private float duracion;
    [SerializeField] private int distancia;
    [SerializeField] private int speed = 2;
    [SerializeField] private float team;
    private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Timemanager();
    }

    private void Timemanager()
    {
        if (duracion > 0)
        {
            duracion -= Time.deltaTime;
            if (duracion < 0) duracion = 0;
        }
        if (duracion <= 0)
        {
            Destroy(this.gameObject, 0.1f);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //PARTE ONLINE PLAYERS
        PhotonView photonview2 = collider.GetComponent<PhotonView>();
        if (photonview2 != null && PhotonNetwork.IsConnected)
        {
            if (photonview2.IsMine)
            {
                if (collider.gameObject.CompareTag("Player"))
                {
                    if (collider.gameObject.GetComponent<Player>().GetTeam() != team)
                    {
                        collider.gameObject.GetComponent<Player>().Hit(this.GetComponent<Collider>());
                        collider.gameObject.GetComponent<Player>().changeSpeed(0.8f,1f);

                    }
                    else
                    {
                        collider.gameObject.GetComponent<Player>().Heal(this.dmg);
                        collider.gameObject.GetComponent<Player>().changeSpeed(0.8f, 1f);
                    }
                }
            }
        }

        //PARTE ONLINE TOTEMS
        if (PhotonNetwork.IsConnected)
        {
            if (collider.gameObject.CompareTag("Totem"))
            {
                //Debug.Log(photonView.GetInstanceID() + " " + collider.gameObject.ToString());
                if (collider.gameObject.GetComponent<Totem>().GetTeam() != team)
                {
                    collider.gameObject.GetComponent<Totem>().Hit(this.GetComponent<Collider>());
                }
            }
        }

        //Parte OFFLINE
        if (!PhotonNetwork.IsConnected)
        {
            //totem
            if (collider.gameObject.CompareTag("Totem"))
            {
                //Debug.Log(photonView.GetInstanceID() + " " + collider.gameObject.ToString());
                if (collider.gameObject.GetComponent<Totem>().GetTeam() != team)
                {
                    collider.gameObject.GetComponent<Totem>().Hit(this.GetComponent<Collider>());
                }
            }
            //player
            if (collider.gameObject.CompareTag("Player"))
            {
                if (collider.gameObject.GetComponent<Player>().GetTeam() != team)
                {
                    collider.gameObject.GetComponent<Player>().Hit(this.GetComponent<Collider>());
                }
            }

            //Corrupted Totem
            if (collider.gameObject.CompareTag("CorruptedTotem"))
            {
                collider.gameObject.GetComponent<CorruptedTotem>().Hit(this.GetComponent<Collider>());
            }
        }


    }
    public void setDamage(int damage)
    {
        this.dmg = damage;
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(team);
            stream.SendNext(dmg);
        }
        else
        {
            // Network player, receive data
            this.team = (float)stream.ReceiveNext();
            this.dmg = (int)stream.ReceiveNext();
        }
    }

}
