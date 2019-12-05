using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Flecha : Attack
{
    [SerializeField] private float duracion = 30;
    [SerializeField] private int speed;
    private float t;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timemanager();
        moveManager();

    }

    public void setDir(Vector3 d)
    {
        this.dir = d;
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

            Debug.Log(collider.gameObject.name);
            if (collider.gameObject.CompareTag("Player"))
            {
                if (collider.gameObject.GetComponent<Player>().GetTeam() != t)
                {   
                    if (photonView.IsMine)
                    {
                        collider.gameObject.GetComponent<Player>().Hit(this.GetComponent<Collider>());
                    }
                    Destroy(this.gameObject);
                }
            }
            if (collider.gameObject.CompareTag("Totem"))
            {
                if (collider.gameObject.GetComponent<Totem>().GetTeam() != t)
                { 
                    if (photonView.IsMine)
                    {
                        collider.gameObject.GetComponent<Totem>().Hit(this.GetComponent<Collider>());
                    }
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            //totem
            if (collider.gameObject.CompareTag("Totem"))
            {
                //Debug.Log(photonView.GetInstanceID() + " " + collider.gameObject.ToString());
                if (collider.gameObject.GetComponent<Totem>().GetTeam() != t)
                {
                    collider.gameObject.GetComponent<Totem>().Hit(this.GetComponent<Collider>());
                    Destroy(this.gameObject);

                }
            }
            //player
            if (collider.gameObject.CompareTag("Player"))
            {
                if (collider.gameObject.GetComponent<Player>().GetTeam() != t)
                {      
                    collider.gameObject.GetComponent<Player>().Hit(this.GetComponent<Collider>());
                    Destroy(this.gameObject);

                }
            }

            //Corrupted Totem
            if (collider.gameObject.CompareTag("CorruptedTotem"))
            {    
                collider.gameObject.GetComponent<CorruptedTotem>().Hit(this.GetComponent<Collider>());
                Destroy(this.gameObject);
            }
        }

    }
    public void setTeam(float p2)
    {
        this.t = p2;
    }

    private void moveManager()
    {
        
        transform.Translate(this.transform.InverseTransformDirection(dir) * speed * Time.deltaTime);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(t);
        }
        else
        {
            // Network player, receive data
            this.t = (float)stream.ReceiveNext();
        }
    }

}
