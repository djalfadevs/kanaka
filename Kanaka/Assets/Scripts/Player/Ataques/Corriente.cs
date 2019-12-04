using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Corriente : Attack
{
    [SerializeField] private float radius;
    [SerializeField] private float duracion;
    private float p;
    // Start is called before the first frame update
    void Start()
    {
        if (radius==0f)
        {
            radius = 1f;
        }
        this.gameObject.transform.localScale=new Vector3(radius,radius,radius);
    }

    void Awake()
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
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonView photonView = collider.GetComponent<PhotonView>();
            //if (!PhotonNetwork.IsMasterClient)
            //    return;
            Debug.Log(collider.gameObject.name);
            if (collider.GetComponent<PhotonView>() == null)
            {
                return;
            }

            if (!photonView.IsMine)
            {
                return;
            }
            if (collider.gameObject.CompareTag("Player"))
            {
                if (collider.gameObject.GetComponent<Player>().GetTeam() != p)
                {
                    if (Vector3.Distance(this.transform.position, collider.gameObject.GetComponent<Player>().transform.position) < radius)
                    {
                        Vector3 dir = (collider.gameObject.transform.position - this.transform.position);
                        Vector3 aux = Vector3.Normalize(dir) * (Vector3.Distance((this.radius * Vector3.Normalize(dir)+this.transform.position),collider.gameObject.transform.position));
                        collider.gameObject.GetComponent<Player>().Hit(this.GetComponent<Collider>(),aux);
                    }


                }
            }
        }
        else
        {
            //totem
            if (collider.gameObject.CompareTag("Totem"))
            {
                //Debug.Log(photonView.GetInstanceID() + " " + collider.gameObject.ToString());
                if (collider.gameObject.GetComponent<Totem>().GetTeam() !=p)
                {
                    collider.gameObject.GetComponent<Totem>().Hit(this.GetComponent<Collider>());
                }
            }
            //player
            if (collider.gameObject.CompareTag("Player"))
            {
                if (collider.gameObject.GetComponent<Player>().GetTeam() != p)
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
    public void setPlayer(float p2)
    {
        this.p = p2;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(p);
        }
        else
        {
            // Network player, receive data
            this.p = (float)stream.ReceiveNext();
        }
    }
}
