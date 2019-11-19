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
        Debug.Log("He recibido " + collider.gameObject.GetComponent<Mareas1>().getDmg() + " puntos de dmg");
        animator.SetTrigger("isDamaged");
        //Dead();
        float damage = collider.gameObject.GetComponent<Mareas1>().getDmg();

        if (!(hp - damage <= 0))//Si con el golpe no muere
        {
            hp -= damage;
            float hpPorcentaje = hp / maxHp;
            animator.SetFloat("HP%",hpPorcentaje);
            
        }
        else//Si se muere
        {
            Dead();
        }
       
    }

    public float GetHp()
    {
        return hp;
    }

    //Se utiliza  cuando se realizan cambios de estados que seran dados por el uso de ciertas animaciones.
    public void setStage(int stage)
    {
        animator.SetInteger("Stage", stage);
        this.stage = stage; //Solo como informacion para ver posibles fallos
    }
}
