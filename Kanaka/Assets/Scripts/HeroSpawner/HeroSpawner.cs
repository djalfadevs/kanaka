using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] private Transform Hero;
    [SerializeField] private bool isBusy;
    [SerializeField] private int team;
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
}
