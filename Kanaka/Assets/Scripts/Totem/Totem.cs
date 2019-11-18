using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    [SerializeField] private int team;
    [SerializeField] private float hp;
    [SerializeField] private Color teamColor;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
       
        animator = GetComponent<Animator>();
        animator.SetFloat("HP", hp);
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
        animator.SetFloat("HP", hp);

        //Actualizamos los totems vivos de los equipos ya que uno de ellos ha muerto
        GameObject gameHandler = GameObject.FindGameObjectWithTag("GameController");
        gameHandler.GetComponent<GameHandler>().CountAliveTotemsinTeams();
    }

    public void Hit(Collider collider)
    {
        Debug.Log("He recibido " + collider.gameObject.GetComponent<Mareas1>().getDmg() + " puntos de dmg");
        Dead();
        animator.SetFloat("HP", hp);
    }

    public float GetHp()
    {
        return hp;
    }
}
