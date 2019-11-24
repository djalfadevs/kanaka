using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;

public class HeroGroundCircle : MonoBehaviourPun
{
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer TeamAllyIndicator; //Indica en que direccion se encuentra tu aliado
    [SerializeField] float radius = 1; //Indica en que direccion se encuentra tu aliado 
    private Transform Ally;
    [SerializeField] private float TeamAllyIndicatorLenght = 1;
    [SerializeField] private float TeamAllyIndicatorWidth = 1;
    [SerializeField] private float TeamAllyIndicatorHeightAdapt = 0.01f;
    private int numberOfAliesInRoom = 0;

    private GameObject child;
    private PhotonView ph;
    // Start is called before the first frame update
    void Start()
    {
        ph = GetComponentInParent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x,player.gameObject.GetComponent<CapsuleCollider>().bounds.min.y,player.position.z);  //Momentaneo , habra que ajustar bien las posiciones
        setSpriteColor();
        setHeroLine();
    }

    void setSpriteColor()
    {
        Color auxColor = player.GetComponent<Player>().getTeamColor();
        sprite.color = auxColor;
        if (player.GetComponent<PlayerController>()!=null && ph.IsMine)
        {
            sprite.color = Color.yellow;
        }
        
    }

    void setHeroLine()
    {
        numberOfAliesInRoom = 0;
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Player"))
           {
                if(p != player.gameObject && player.GetComponent<Player>().GetTeam()==p.GetComponent<Player>().GetTeam())
                {
                    numberOfAliesInRoom++;
                    Ally = p.transform;
                   
           }
        }

        if (numberOfAliesInRoom >= 1)
        {
            TeamAllyIndicator.enabled = true;
            Vector3 fin = new Vector3(Ally.transform.position.x, player.gameObject.GetComponent<CapsuleCollider>().bounds.min.y + TeamAllyIndicatorHeightAdapt, Ally.transform.position.z);
            Vector3 ini = new Vector3(player.transform.position.x, player.gameObject.GetComponent<CapsuleCollider>().bounds.min.y + TeamAllyIndicatorHeightAdapt, player.transform.position.z);
            Vector3 AuxDirUnit = Vector3.Normalize(fin - ini);
            TeamAllyIndicator.gameObject.transform.position = ini + AuxDirUnit * radius;
            TeamAllyIndicator.material.color = player.GetComponent<Player>().getTeamColor();
        }
        else
        {
            if (TeamAllyIndicator.enabled)
            {
                TeamAllyIndicator.enabled = false;
            }
        }
    }
}
