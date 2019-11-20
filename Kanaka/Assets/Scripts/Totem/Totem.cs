using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    [SerializeField] private int team;
    [SerializeField] private float maxHp;
    [SerializeField] private float hp;
    [SerializeField] private Color teamColor;
    [SerializeField] private int stage; //Solo sirve para ver cambios , la logica de stage es inherente a los eventos de las animaciones
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
       
        animator = GetComponent<Animator>();
        hp = maxHp;
        animator.SetFloat("HP%", hp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetTeam()
    {
        return team;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = teamColor;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.2f,0.2f,0.2f));
    }

    public void setTeamColor(Color color)
    {
        teamColor = color;
    }

    public void Dead()
    {
        hp = 0;
        animator.SetFloat("HP%", hp);
        animator.SetBool("isDead",true);

        //Actualizamos los totems vivos de los equipos ya que uno de ellos ha muerto
        GameObject gameHandler = GameObject.FindGameObjectWithTag("GameController");
        if(gameHandler!=null)
        gameHandler.GetComponent<GameHandler>().CountAliveTotemsinTeams();
    }

    public void Hit(Collider collider)
    {
        float damage = collider.gameObject.GetComponent<Mareas1>().getDmg();
        Debug.Log("He recibido " + damage + " puntos de dmg");
        //Dead();

        if (!(hp - damage <= 0))//Si con el golpe no muere
        {
            animator.SetBool("isDamaged",true);
            hp -= damage;
            float hpPorcentaje = hp / maxHp;
            animator.SetFloat("HP%",hpPorcentaje);
            CalculateState((hp+damage)/maxHp,(hp/maxHp));
        }
        else//Si se muere
        {
            animator.SetBool("isDamaged", true);
            CalculateState((hp + damage) / maxHp, (hp / maxHp));
            Dead();
        }
       
    }

    private void CalculateState(float currentValue, float nextValue)
    {
        if(currentValue>=0.6 && nextValue <= 0.6)//En el punto del 0.6
        {
            animator.SetInteger("State",1);
        }
        else if (currentValue >= 0.3 && nextValue <= 0.3)//En el punto del 0.6
        {
            animator.SetInteger("State", 2);
        }
        else if (nextValue <= 0)//En el punto del 0.6
        {
            animator.SetInteger("State", 3);
        }
    }

    public float GetHp()
    {
        return hp;
    }

    //Se utiliza  cuando se realizan cambios de estados que seran dados por el uso de ciertas animaciones.
    /*
    public void setStage(int stage)
    {
        animator.SetInteger("Stage", stage);
        this.stage = stage; //Solo como informacion para ver posibles fallos
    }
    */

    public void SetBoolDamageg(int i)
    {
        if (i == 0)
            animator.SetBool("isDamaged", false);
        else if(i==1)
        {
            animator.SetBool("isDamaged", true);
        }
    }
}
