using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mareas1 : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private int dmg;
    [SerializeField] private float duracion;
    [SerializeField] private int distancia;
    [SerializeField] private int speed=2;

    private List<GameObject> points=new List<GameObject>();
    private List<Vector3> pointsPos = new List<Vector3>();

    private float team;
    private int fase;
    private PhotonView photonView;
    //0 no
    //1 si
    //2 slow
    //3 mas rapido

    // Start is called before the first frame update
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        //Debug.Log(photonView.InstantiationData[0].ToString());
        float aux;
        float.TryParse(photonView.InstantiationData[0].ToString(), out aux);
        team = aux;
    }
    void Start()
    {
        
    }
    public void setPoints(List<GameObject>l)
    {
        this.points = l;
        pointsPos.Add(new Vector3(points[0].GetComponent<Transform>().position.x,
            points[0].GetComponent<Transform>().position.y,
            points[0].GetComponent<Transform>().position.z));
        pointsPos.Add(new Vector3(points[1].GetComponent<Transform>().position.x,
            points[1].GetComponent<Transform>().position.y,
            points[1].GetComponent<Transform>().position.z));
    }
    public void setDamage(int damage)
    {
        this.dmg = damage;
    }

    public void setTeam(float team)
    {
        this.team = team;
    }

    public void setPlayer(float p2,int f)
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
        this.team = p2;
        
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
            //Photon.
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {

        //PARTE ONLINE PLAYERS
        //Debug.Log(collider.gameObject.name);
        //Debug.Log(team.ToString());
        PhotonView photonview2 = collider.GetComponent<PhotonView>();
        if (photonview2 != null && PhotonNetwork.IsConnected)
        {
            if (photonview2.IsMine)
            {
                if (collider.gameObject.CompareTag("Player"))
                {
                    if (collider.gameObject.GetComponent<Player>().GetTeam() != team)
                    {
                        collider.gameObject.GetComponent<Player>().Hit(this.GetComponent<Collider>());
                    }
                }
            }
        }
    
        //PARTE ONLINE TOTEMS
        //Caso Objeto de escena (se regula por la vista del usuario que lanza dicho cubo)
        if (PhotonNetwork.IsConnected)
        {
            if (collider.gameObject.CompareTag("Totem"))
            {
                //Debug.Log(photonView.GetInstanceID() + " " + collider.gameObject.ToString());
                if (collider.gameObject.GetComponent<Totem>().GetTeam() != team)
                {
                    collider.gameObject.GetComponent<Totem>().Hit(this.GetComponent<Collider>());
                }
            }
        }

        //Parte OFFLINE
        if (!PhotonNetwork.IsConnected)
        {
            //totem
            if (collider.gameObject.CompareTag("Totem"))
            {
                //Debug.Log(photonView.GetInstanceID() + " " + collider.gameObject.ToString());
                if (collider.gameObject.GetComponent<Totem>().GetTeam() != team)
                {
                    collider.gameObject.GetComponent<Totem>().Hit(this.GetComponent<Collider>());
                }
            }
            //player
            if (collider.gameObject.CompareTag("Player"))
            {
                if (collider.gameObject.GetComponent<Player>().GetTeam() != team)
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

    public void moveManager()
    {
        if (fase !=-1)
        {
            if (fase == 0)
            {
                if (this.pointsPos.Count != 0)
                {
                    if (Vector3.Distance(this.gameObject.transform.position, this.pointsPos[0]) > 0.5f)
                    {
                        Vector3 move = Vector3.Normalize(this.pointsPos[0] - this.gameObject.transform.position);
                        transform.Translate(this.transform.InverseTransformDirection(move * speed * Time.deltaTime));
                        //Debug.Log(move);
                    }
                    else
                    {
                        fase = 1;
                    }
                }
            }
            else if (fase == 1)
            {
                if (this.pointsPos.Count != 0)
                {
                    //Debug.Log("inicio fase1");
                    if (Vector3.Distance(this.gameObject.transform.position, this.pointsPos[1]) > 0.5f)
                    {
                        Vector3 move = Vector3.Normalize(this.pointsPos[1] - this.gameObject.transform.position);
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(team);
            stream.SendNext(dmg);
        }
        else
        {
            // Network player, receive data
            this.team = (float)stream.ReceiveNext();
            this.dmg = (int)stream.ReceiveNext();
        }
    }
}
