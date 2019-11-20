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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        cam = FindObjectOfType<Camera>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void Hit(Collider collider)
    {
        Debug.Log("He recibido "+collider.gameObject.GetComponent<Mareas1>().getDmg()+" puntos de dmg");
    }

    public int GetTeam()
    {
        return team;
    }

    public void setTeamColor(Color color)
    {
        teamColor = color;
    }

    public Color getTeamColor()
    {
        return teamColor; 
    }

    public void moveAndroid(float auxAxisHorizontal,float auxAxisVertical)
    {
        if (canMove)
        {
            Vector3 move = new Vector3(auxAxisHorizontal * MoveSpeed, 0f, auxAxisVertical * MoveSpeed);
            Vector3 v3 = new Vector3(auxAxisHorizontal, 0.0f, auxAxisVertical);
            Quaternion qTo = Quaternion.LookRotation(v3);
            transform.rotation = Quaternion.Slerp(transform.rotation, qTo, MoveSpeed * Time.deltaTime);

            cc.Move(move * Time.deltaTime);
            cc.SimpleMove(Physics.gravity);
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
            cc.Move(move * Time.deltaTime);
            cc.SimpleMove(Physics.gravity);
        }
        
    }

    public void attack()
    {
        if (attackCD<=0)
        {
            animator.SetBool("Attack",true);
            attackCD = baseAttackCD;
           // Attack.GetComponent<Attack>().use(this.gameObject);
        }
       
   
    }

    public void ability()
    {
        if (abilityCD<=0)
        {
            Debug.Log("bolsa");
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
            // We own this player: send the others our data
        }
        else
        {
            // Network player, receive data
            //this.IsFiring = (bool)stream.ReceiveNext();
        }
    }
}
