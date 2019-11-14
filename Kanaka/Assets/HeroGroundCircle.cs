using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroGroundCircle : MonoBehaviour
{
    [SerializeField] private Transform player;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();  
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x,player.position.y-1.0f,player.position.z);  //Momentaneo , habra que ajustar bien las posiciones
        setSpriteColor();
    }

    void setSpriteColor()
    {
        Color auxColor = player.GetComponent<Player>().getTeamColor();
        sprite.color = auxColor;
        if (player.GetComponent<PlayerController>()!=null)
        {
            sprite.color = Color.yellow;
        }
        
    }
}
