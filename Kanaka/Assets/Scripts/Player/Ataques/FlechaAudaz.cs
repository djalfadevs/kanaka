using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class FlechaAudaz : MonoBehaviour
{
    public GameObject Cube;
    public GameObject player;
    private Animator animator;
    private PhotonView photonView;
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
    public void CallFlecha()
    {
        SpawnFlecha();
    }
    private void SpawnFlecha()
    {
        GameObject q;
        Vector3 dir = player.transform.TransformDirection(Vector3.forward);
        
        Vector3 aux = player.transform.position + dir * 2;
        q = Instantiate(Cube, aux, Quaternion.identity);    //(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.y));
        Vector3 dir2 = q.transform.InverseTransformDirection(dir);
        q.transform.rotation = Quaternion.LookRotation(dir2, Vector3.right);
        q.transform.Rotate(-90,0,0,Space.Self);
        q.GetComponent<Flecha>().setTeam(player.GetComponent<Player>().GetTeam());
        q.GetComponent<Flecha>().setDir(dir);
    }
    public void LastCallFlecha()
    {
        if (PhotonNetwork.IsConnected&&photonView.IsMine)
        {
            animator.SetBool("Attack",false);
        }
        else if (!PhotonNetwork.IsConnected)
        {
            animator.SetBool("Attack", false);
        }
    }
}
