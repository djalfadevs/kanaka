using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RupturaSismica : MonoBehaviour
{
    public GameObject cube2;
    public GameObject player;
    public List<GameObject> points;
    private Animator animator;
    private PhotonView photonView;
    [SerializeField] private int dmg;
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
    public void CallRuptura()
    {
        if (photonView.IsMine)
        {
            player.GetComponent<Player>().setCanMove(false);//El personaje no se puede mover
        }
        SpawnRuptura();
    }

    private void SpawnRuptura()
    {
        Vector3 aux = player.transform.position + player.transform.TransformDirection(Vector3.forward) * 2;
        GameObject q;
        q =Instantiate(cube2, aux, player.transform.rotation);
        q.GetComponent<Ruptura>().setDamage(dmg);
    }
    public void LastCallRuptura()
    {
        if (PhotonNetwork.IsConnected&&photonView.IsMine)
        {
            Debug.Log("holaRRR");
            animator.SetBool("Attack", false);
            player.GetComponent<Player>().setCanMove(true);


        }
        else if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("holaRRR");
            animator.SetBool("Attack", false);
            player.GetComponent<Player>().setCanMove(true);
        }
    }
}
