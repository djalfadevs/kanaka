using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruptura : MonoBehaviour
{
    private int dmg;
    [SerializeField] private float duracion;
    [SerializeField] private int distancia;
    [SerializeField] private int speed = 2;
    private Player p;
    // Start is called before the first frame update
    void Start()
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
            Destroy(this.gameObject, 0.1f);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject.name);
        if (collider.gameObject.CompareTag("Player"))
        {
            if (collider.gameObject.GetComponent<Player>().GetTeam() != p.GetTeam())
            {
                collider.gameObject.GetComponent<Player>().Hit(this.GetComponent<Collider>());
            }
        }
        if (collider.gameObject.CompareTag("Totem"))
        {
            if (collider.gameObject.GetComponent<Totem>().GetTeam() != p.GetTeam())
            {
                collider.gameObject.GetComponent<Totem>().Hit(this.GetComponent<Collider>());
            }
        }

    }
    public void setPlayer(Player p2)
    {
        this.p = p2;
    }
    public int getDmg()
    {
        return this.dmg;
    }
}
