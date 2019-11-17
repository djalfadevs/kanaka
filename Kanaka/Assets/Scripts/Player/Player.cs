using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float HP;
    public float MaxHP;
    [SerializeField] private GameObject Ability;
    [SerializeField] private GameObject Attack;
    [SerializeField] private GameObject Item;
    [SerializeField] private int team;
    [SerializeField] private Color teamColor;
    public float MoveSpeed;
    CharacterController cc;
    Vector3 mouse_pos;
    public Transform target; //Assign to the object you want to rotate
    Vector3 object_pos;
    float angle;
    private Camera cam;
    public float deadzone;
    private Vector3 rotvec;
    [SerializeField] private float attackCD;
    [SerializeField] private float baseAttackCD;
    [SerializeField] private float abilityCD;
    [SerializeField] private float baseAbilityCD;
    // Start is called before the first frame update
    void Start()
    {
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

    void Hit(Collider collider)
    {

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
        Vector3 move = new Vector3(auxAxisHorizontal * MoveSpeed, 0f, auxAxisVertical * MoveSpeed);
        Vector3 v3 = new Vector3(auxAxisHorizontal, 0.0f, auxAxisVertical);
        Quaternion qTo = Quaternion.LookRotation(v3);
        transform.rotation = Quaternion.Slerp(transform.rotation, qTo, MoveSpeed * Time.deltaTime);

        cc.Move(move * Time.deltaTime);
        cc.SimpleMove(Physics.gravity);
    }

    public void move(Vector3 mouse_pos,float vertical)
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

        Vector3 move = vertical* transform.TransformDirection(Vector3.forward) * MoveSpeed;
        cc.Move(move * Time.deltaTime);
        cc.SimpleMove(Physics.gravity);
    }

    public void attack()
    {
        if (attackCD<=0)
        {
            attackCD = baseAttackCD;
            Attack.GetComponent<Attack>().use(this.gameObject);
        }
       
   
    }

    public void ability()
    {
        if (abilityCD<=0)
        {
            abilityCD = baseAbilityCD;
            //Ability.GetComponent<Ability>().use();
        }


    }
}
