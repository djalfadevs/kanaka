using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] private Transform Hero;
    [SerializeField] private bool isBusy;
    [SerializeField] private int team;
    [SerializeField] private Color teamColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RespawnHero()
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
}
