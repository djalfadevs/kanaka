using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TajoIzq : Attack
{
    private float t;
    private TajoArdiente tajo;
    [SerializeField]private int speed;
    private Vector3 destino;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveManager();
    }

    private void moveManager()
    {
        if (Vector3.Distance(this.gameObject.transform.position,this.destino)>0.5f)
        {
            Vector3 aux = this.transform.TransformDirection(Vector3.right);
            this.transform.Translate(this.transform.InverseTransformDirection(aux*speed*Time.deltaTime));
        }
        else
        {
            tajo.CallDa();
            Destroy(this.gameObject);
        }
    }

    public void setItems(float tt,TajoArdiente tA,Vector3 d)
    {
        this.t = tt;
        this.tajo = tA;
        this.destino = d;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonView photonView = collider.GetComponent<PhotonView>();
            if (!PhotonNetwork.IsMasterClient)
                return;
            Debug.Log(collider.gameObject.name);
            if (collider.gameObject.CompareTag("Player"))
            {
                if (collider.gameObject.GetComponent<Player>().GetTeam() != t)
                {
                    collider.gameObject.GetComponent<Player>().Hit(this.GetComponent<Collider>());
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
                }
            }
            //player
            if (collider.gameObject.CompareTag("Player"))
            {
                if (collider.gameObject.GetComponent<Player>().GetTeam() != t)
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
