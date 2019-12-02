using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupturaSismica : MonoBehaviour
{
    public GameObject cube2;
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
    public void Call()
    {
        Spawn();
        Debug.Log("holaR");
    }

    private void Spawn()
    {
        Vector3 aux = player.transform.position + player.transform.TransformDirection(Vector3.forward) * 2;
        GameObject q = Instantiate(cube2, aux, player.transform.rotation);
        q.GetComponent<Ruptura>().setPlayer(player.GetComponent<Player>());
        GetComponent<Animator>().ResetTrigger("Attack");
    }
}
