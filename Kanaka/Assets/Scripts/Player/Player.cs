using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{
    public float HP;
    public float MaxHP;
    [SerializeField] private GameObject Item;
    [SerializeField] private int team;
    [SerializeField] private Color teamColor;
    public float MoveSpeed;
    [SerializeField] private bool canMove = true;
    CharacterController cc;
    Vector3 mouse_pos;
    public Transform target; //Assign to the object you want to rotate
    Vector3 object_pos;
    private Camera cam;
    public float deadzone;
    private Vector3 rotvec;
    [SerializeField] private float attackCD;
    [SerializeField] private float baseAttackCD;
    [SerializeField] private float abilityCD;
    [SerializeField] private float baseAbilityCD;
    [SerializeField] private Animator animator;
    private PhotonView ph;
    private Vector3 movement;
    private Vector3 networkPosition;
    private Quaternion networkRotation;
    private float changeDuration;
    [SerializeField] private float baseSpeed;
    public GameObject healEffect;
    public GameObject buffEffect;
    public GameObject deathEffect;
    public GameObject slowEffect;


    // Start is called before the first frame update
    void Awake()
    {
        ph = GetComponentInParent<PhotonView>();
        photonView.RpcSecure("RPC_MoveExactlyOnTransform", RpcTarget.Others, false, transform.position);
        //if(ph!=null)
        //ph.ObservedComponents.Add(this);
    }
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        cam = FindObjectOfType<Camera>();
        cc = GetComponent<CharacterController>();

        int aux;
        Color aux2;

        if (PhotonNetwork.IsConnected)
        {
            if (photonView ?? null)
            {
               int.TryParse(photonView.InstantiationData[0].ToString(), out aux);
                team = aux;
                ColorUtility.TryParseHtmlString(photonView.InstantiationData[1].ToString(), out aux2);
                teamColor = aux2;
           }
        }
        
    }

    [PunRPC]
    void RPC_MoveExactlyOnTransform(Vector3 p)
    {
            transform.position = p;
            Debug.LogError("Se actualiza la posicion de golpe");
    }


    // Update is called once per frame
    void Update()
    {
        if (!ph.IsMine && PhotonNetwork.IsConnected)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, networkPosition, Time.deltaTime * baseSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * 30);
        }

        if (PhotonNetwork.IsConnected)
        {
            if (!ph.IsMine)
            {
                return;
            }
        }

        if (attackCD >= 0)
        {
            attackCD -= Time.deltaTime;
            if (attackCD<0)
            {
                attackCD = 0;
            }
        }
        if (abilityCD>=0)
        {
            abilityCD -= Time.deltaTime;
            if (abilityCD<0)
            {
                abilityCD = 0;
            }
        }
        if (this.MoveSpeed != this.baseSpeed)
        {
            if (changeDuration <= 0)
            {
                this.MoveSpeed = this.baseSpeed;
            }
            else
            {
                this.changeDuration -= Time.deltaTime;
            }
        }
    }

    public void changeSpeed(float percentage, float d )
    {
        if (this.MoveSpeed!=this.baseSpeed) return;
        if (percentage<1)
        {
            Instantiate(slowEffect, this.transform.position, this.transform.rotation);
        }
        else
        {
            Instantiate(buffEffect, this.transform.position, this.transform.rotation);
        }
        this.changeDuration = d;
        this.MoveSpeed *= percentage;
    }

    [PunRPC]
    public void Heal(int amount)
    {
        Instantiate(healEffect,this.transform.position,this.transform.rotation);   
        if (!PhotonNetwork.IsConnected)//offline
        {
            HP += amount;
            if (HP >= MaxHP) HP = MaxHP;
        }
        else//ONLINE
        {
            if (ph.IsMine)//Solo me curo yo mismo
            {
                HP += amount;
                if (HP >= MaxHP) HP = MaxHP;
            }
        }
    }

    public void Hit(Collider collider)
    {
        
        float damage = collider.gameObject.GetComponent<Attack>().getDmg();
        Debug.Log("He recibido "+ damage +" puntos de dmg");
        if (HP - damage>0)
        {
            HP -= damage;
        }
        else
        {
            HP = 0;
            animator.SetBool("IsDead", true);
        }
    }

    public void Hit(float dmg)
    {
        if (HP - dmg > 0)
        {
            HP -= dmg;
        }
        else
        {
            HP = 0;
            animator.SetBool("IsDead", true);
        }
    }


    public void DeadFinish()
    {

        if (ph.IsMine && PhotonNetwork.IsConnected)
        {
            Debug.LogError("Estoy Intentando Resucitar");
            //Restauramos la vida
            animator.SetBool("IsDead", false);
            HP = MaxHP;
            //Lo colocamos en otra posicion
            GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
            Vector3 newSpawnPoint = (gameManager.GetComponent<PhotonGameManager>().teamList[0].GetSpawnsTeam()[0]).GetComponent<HeroSpawners>().Spawn(this, team);
            photonView.RpcSecure("RPC_MoveExactlyOnTransform", RpcTarget.All, false, newSpawnPoint);
        }

    }

    public void Hit(Collider collider,Vector3 destino)
    {
        float damage = collider.gameObject.GetComponent<Attack>().getDmg();
        Debug.Log("He recibido " + damage + " puntos de dmg");
        if (HP - damage > 0)
        {
            HP -= damage;
        }
        else
        {
            HP = 0;
            Instantiate(deathEffect, this.transform.position, this.transform.rotation);
            animator.SetBool("IsDead", true);
        }
       
            cc.Move(Vector3.Normalize(destino-transform.position)*2);
        
       
    }

    public int GetTeam()
    {
        return team;
    }

    public Color getTeamColor()
    {
        return teamColor; 
    }

    public float getAtackCD()
    {
        return attackCD;
    }

    public float getAbilityCD()
    {
        return abilityCD;
    }

    public void moveAndroid(float auxAxisHorizontal,float auxAxisVertical)
    {
        if (canMove)
        {
            Vector3 move = new Vector3(auxAxisHorizontal * MoveSpeed, 0f, auxAxisVertical * MoveSpeed);
            Vector3 v3 = new Vector3(auxAxisHorizontal, 0.0f, auxAxisVertical);
            if (auxAxisHorizontal != 0.0f && auxAxisVertical != 0.0f)
            {
                Quaternion qTo = Quaternion.LookRotation(v3);
                transform.rotation = Quaternion.Slerp(transform.rotation, qTo, MoveSpeed * Time.deltaTime);
            }
            cc.Move(move * Time.deltaTime);
            if(auxAxisHorizontal!=0 && auxAxisVertical != 0)
            {
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
            cc.SimpleMove(new Vector3(0,-98f,0));
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
       
    }

    public void move(Vector3 mouse_pos,float vertical)
    {
        if (canMove)//Si puede moverse
        {
            object_pos = cam.WorldToScreenPoint(target.position);
            rotvec.x = mouse_pos.x - object_pos.x;
            rotvec.y = mouse_pos.y - object_pos.y;

            //if distancia entre mouse y object
            if (Vector3.Distance(mouse_pos, object_pos) > deadzone)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(rotvec.x, 0, rotvec.y));

                //Debug.Log(Vector3.Distance(mouse_pos, object_pos));
            }

            Vector3 move = vertical * transform.TransformDirection(Vector3.forward) * MoveSpeed;
            if (vertical != 0)
            {
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
            cc.Move(move * Time.deltaTime);
            cc.SimpleMove(Physics.gravity);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    public void attack()
    {
        if (attackCD<=0 && !animator.GetBool("Attack"))
        {
            animator.SetBool("Attack",true);
            attackCD = baseAttackCD;
           // Attack.GetComponent<Attack>().use(this.gameObject);
        }
       
   
    }

    public void ability()
    {
        if (abilityCD<=0 && !animator.GetBool("Habilidad"))
        {
            animator.SetBool("Habilidad",true);
            abilityCD = baseAbilityCD;
            //Ability.GetComponent<Ability>().use();
        }


    }

    public void setCanMove(bool b)
    {
        canMove = b;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(attackCD);
            stream.SendNext(team);
            //Debug.LogError("Color send " + "#"+teamColor + " "+ph.GetInstanceID() );
            stream.SendNext("#"+ColorUtility.ToHtmlStringRGBA(teamColor));
            stream.SendNext(HP);

            stream.SendNext(this.transform.position);
            stream.SendNext(this.transform.rotation);

        }
        else
        {
            // Network player, receive data
            this.attackCD = (float)stream.ReceiveNext();
            this.team = (int)stream.ReceiveNext();

            Color aux;
            ColorUtility.TryParseHtmlString((string) stream.ReceiveNext(),out aux);
            this.teamColor = aux;

            this.HP = (float)stream.ReceiveNext();

            this.networkPosition = (Vector3)stream.ReceiveNext();
            this.networkRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
