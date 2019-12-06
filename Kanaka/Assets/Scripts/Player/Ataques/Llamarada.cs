using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llamarada : Attack
{
    [SerializeField] private int duracion;
    [SerializeField] private int speed = 2;
    [SerializeField] private float team;
    private int time0=0;
    private LlamaradaImpulsiva a;
    private PhotonView photonView;
    private Player player;
    private bool dash;
    // Start is called before the first frame update
    void Start()
    {
        
        dash = true;
    }
    void Awake()
    {
    }
    public void setPlayer(Player p,LlamaradaImpulsiva aa)
    {
        this.player = p;
        this.team = p.GetComponent<Player>().GetTeam();
        this.a = aa;
    }
    // Update is called once per frame
    void Update()
    {
            while (dash && time0 < duracion)
            {
                time0+=1;
                Movemanager();
            }
        float aa = 0;
            if (time0 >= duracion)
            {
            do
            {
                aa+=Time.deltaTime;
               
            } while (aa<2);
            a.LastCallLlamarada();
            Destroy(this.gameObject);
        }
        

    }

    private void Movemanager()
    {
        this.player.GetComponent<CharacterController>().Move(this.player.transform.TransformDirection(Vector3.forward*speed*Time.deltaTime));
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
                        time0 = duracion;
                        collider.gameObject.GetComponent<Player>().Hit(this.GetComponent<Collider>());
                       

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
                    time0 = duracion;
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
                    time0 = duracion;
                    collider.gameObject.GetComponent<Totem>().Hit(this.GetComponent<Collider>());
                   
                }
            }
            //player
            if (collider.gameObject.CompareTag("Player"))
            {
                if (collider.gameObject.GetComponent<Player>().GetTeam() != team)
                {
                    time0 = duracion;
                    collider.gameObject.GetComponent<Player>().Hit(this.GetComponent<Collider>());
                    
                }
            }

            //Corrupted Totem
            if (collider.gameObject.CompareTag("CorruptedTotem"))
            {
                time0 = duracion;
                collider.gameObject.GetComponent<CorruptedTotem>().Hit(this.GetComponent<Collider>());
               
            }
        }


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
