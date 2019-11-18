using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mareas1 : MonoBehaviour
{
    private int dmg;
    [SerializeField] private float duracion;
    [SerializeField] private int distancia;
    [SerializeField] private int speed=2;

    private List<GameObject> points=new List<GameObject>();
    
    private Player p;
    private int fase;
    //0 no
    //1 si
    //2 slow
    //3 mas rapido

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void setPoints(List<GameObject>l)
    {
        this.points = l;
    }
    public void setPlayer(Player p2,int f)
    {
        this.fase = f;
        switch (f)
        {
            case -1:
                dmg = 20;
                break;
            default:
                dmg = 15;
                break;
        }
        this.p = p2;
        
    }
    // Update is called once per frame
    void Update()
    {
        Timemanager();
        moveManager();

    }

    public int getDmg()
    {
        return this.dmg;
    }

    private void Timemanager (){
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

    public void moveManager()
    {
        if (fase !=-1)
        {
            if (fase == 0)
            {
                if (Vector3.Distance(this.gameObject.transform.position,this.points[0].transform.position)>0.5f)
                {
                    Vector3 move =  Vector3.Normalize(this.points[0].transform.position-this.gameObject.transform.position);
                    transform.Translate(this.transform.InverseTransformDirection(move *speed* Time.deltaTime));
                    Debug.Log(move);
                }
                else
                {
                    fase = 1;
                }
            }
            else if (fase == 1)
            {
                Debug.Log("inicio fase1");
                if (Vector3.Distance(this.gameObject.transform.position, this.points[1].transform.position) > 0.5f)
                {
                    Vector3 move = Vector3.Normalize(this.points[1].transform.position - this.gameObject.transform.position);
                    transform.Translate(this.transform.InverseTransformDirection(move * speed * Time.deltaTime));
                }
                else
                {
                    fase = 2;
                }
            }
        }       
    }
}
