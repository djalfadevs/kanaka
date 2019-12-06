using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaObjetos : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private List<GameObject> objetosSpawn; //Lista de objetos que puede spawnear 
    private Animator animator;
    private PhotonView ph;
    private bool open = false;
    // Start is called before the first frame update
    void Start()
    {
        ph = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Hit()
    {
        
        open = true;
        ItemSpawner.numCajas--;
        GameObject objetoSpawn = objetosSpawn[Random.Range(0, objetosSpawn.Count)];
        if (!PhotonNetwork.IsConnected)//OFFLINE
        {
            animator.SetTrigger("IsHit");
            Instantiate(objetoSpawn, transform.position, Quaternion.Euler(0, 0, 0));
        }
        else//ONLINE
        {
            ph.RpcSecure("ActiveAnim", RpcTarget.All, false);
            if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate(objetoSpawn.name, transform.position, Quaternion.Euler(0, 0, 0));
        }
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null && !open)//Si choca contra un player
        {
            Hit();
            
        }
    }

    void DestroyBox()
    {
        Destroy(this.gameObject); 
    }

    [PunRPC]
    void ActiveAnim()
    {
        animator.SetTrigger("IsHit");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(open);
          
        }
        else
        {
            open = (bool) stream.ReceiveNext();
            
        }
    }
}
