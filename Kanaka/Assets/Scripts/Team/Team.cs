using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team 
{
    [SerializeField] private IList<GameObject> team
    {
        get { return team; }
        set { team = value; }
    } //En la escena , referencia a los personajes del equipo uno
    [SerializeField]
    private IList<GameObject> totemsTeam
    {
        get { return totemsTeam; }
        set { totemsTeam = value; }
    }
    [SerializeField] private IList<GameObject> spawnsTeam
    {
        get { return spawnsTeam; }
        set { spawnsTeam = value; }
    }

    private int AliveTotemTeam1;
        
    Team()
    {
    team = new List<GameObject>(); //En la escena , referencia a los personajes del equipo uno
    totemsTeam = new List<GameObject>();
    spawnsTeam = new List<GameObject>();
    }

    public IList<GameObject> GetTeam()
    {
        return team;
    }

    public IList<GameObject> GetTotemsTeam()
    {
        return totemsTeam;
    }

    public IList<GameObject> GetSpawnsTeam()
    {
        return spawnsTeam;
    }
}
