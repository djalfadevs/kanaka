using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Corriente : MonoBehaviour, IPunObservable
{
    private int dmg;
    [SerializeField] private float radius;
    [SerializeField] private float duracion;
    private float p;
    private PhotonView photonView;
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
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
         if (!photonView.IsMine)
             return;
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
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
         PhotonView photonView = collider.GetComponent<PhotonView>();
          if (!PhotonNetwork.IsMasterClient)
            return;
        Debug.Log(collider.gameObject.name);
        if (collider.gameObject.CompareTag("Player"))
        {
            if (collider.gameObject.GetComponent<Player>().GetTeam() != p)
            {
                collider.gameObject.GetComponent<Player>().Hit(this.GetComponent<Collider>());
                if (Vector3.Distance(this.transform.position,collider.gameObject.GetComponent<Player>().transform.position)<radius)
                {
                    Debug.Log("cRINGe");
                }
            }
        }
        
    }
    public void setPlayer(float p2)
    {
        this.p = p2;
    }
    public int getDmg()
    {
        return this.dmg;
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
