using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team 
{
    private IList<GameObject> team { get; set; }
   //En la escena , referencia a los personajes del equipo uno

    
    private IList<GameObject> totemsTeam{ get; set; }
   
    private IList<GameObject> spawnsTeam {get; set; }

    private int AliveTotemTeam1;
        
    public Team()
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
