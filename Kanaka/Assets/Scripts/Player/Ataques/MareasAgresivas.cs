using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MareasAgresivas : MonoBehaviour
{
    public GameObject cube;
    public GameObject player;
    public List<GameObject> points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FirstCall()
    {
        FirstSpawn();
        //Debug.Log("hola");
        player.GetComponent<Player>().setCanMove(false);//El personaje no se puede mover
    }

    private void FirstSpawn()
    {
        Vector3 aux= player.transform.position + player.transform.TransformDirection(Vector3.forward)*2;
        GameObject q=Instantiate(cube, aux, player.transform.rotation);
        q.GetComponent<Mareas1>().setPlayer(player.GetComponent<Player>(),-1);
    }

    public void SecondCall()
    {
        SecondSpawn();
        //Debug.Log("Segundohola");
        GetComponent<Animator>().ResetTrigger("Attack");
    }

    private void SecondSpawn()
    {
        Vector3 aux = player.transform.position + player.transform.TransformDirection(Vector3.left) * 2;
        GameObject q=Instantiate(cube, aux, player.transform.rotation);
        q.GetComponent<Mareas1>().setPlayer(player.GetComponent<Player>(),0);
        q.GetComponent<Mareas1>().setPoints(this.points);
    }
    
    public void LastCall()
    {
        player.GetComponent<Player>().setCanMove(true);//El personaje puede volver a moverse;
    }


}
