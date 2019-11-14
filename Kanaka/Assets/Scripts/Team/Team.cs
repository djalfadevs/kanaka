using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team 
{
    private IList<GameObject> herosTeam { get; set; }
   //En la escena , referencia a los personajes del equipo uno

    
    private IList<GameObject> totemsTeam{ get; set; }
   
    private IList<GameObject> spawnsTeam {get; set; }

    private int AliveTotemTeam{ get; set; }

    private Color teamColor { get; set; }

    public Team()
    {
    herosTeam = new List<GameObject>(); //En la escena , referencia a los personajes del equipo uno
    totemsTeam = new List<GameObject>();
    spawnsTeam = new List<GameObject>();
    teamColor = new Color(0, 0, 0);//Color por defecto
    AliveTotemTeam = 0;
    }

    public Team(Color color)
    {
        herosTeam = new List<GameObject>(); //En la escena , referencia a los personajes del equipo uno
        totemsTeam = new List<GameObject>();
        spawnsTeam = new List<GameObject>();
        teamColor = color;
        AliveTotemTeam = 0;
    }

    public IList<GameObject> GetTeam()
    {
        return herosTeam;
    }

    public IList<GameObject> GetTotemsTeam()
    {
        return totemsTeam;
    }

    public IList<GameObject> GetSpawnsTeam()
    {
        return spawnsTeam;
    }

    public void AddTotemsTeam(int team, GameObject[] totems)
    {
        this.totemsTeam.Clear();//Limpiamos la lista antes de añadir
        foreach (GameObject s in totems)
        {
            Totem aux = s.gameObject.GetComponent<Totem>();
            if (aux != null)
            {
                if (aux.GetTeam() == team)
                {
                    this.totemsTeam.Add(aux.gameObject);
                    aux.setTeamColor(teamColor);//Se le pasa el color del equipo establecido desde el gamehandler
                    Debug.Log("Added Totem to team " + team);
                }
            }
        }
    }

    public void AddTeamSpawner(int team, GameObject[] heroSpawners)
    {
        this.spawnsTeam.Clear();//Limpiamos la lista antes de añadir
        foreach (GameObject s in heroSpawners)
        {
            HeroSpawner aux = s.gameObject.GetComponent<HeroSpawner>();
            if (aux != null)
            {
                if (aux.GetTeam() == team)
                {
                    this.spawnsTeam.Add(aux.gameObject);
                    aux.setTeamColor(teamColor);//Se le pasa el color del equipo establecido desde el gamehandler
                    Debug.Log("Added Spawn to team " + team + "and set color" + teamColor);
                }
            }
        }
    }

    public void AddHerosTeam(int team, GameObject[] herosTeam)
    {
        this.herosTeam.Clear();//Limpiamos la lista antes de añadir
        foreach (GameObject s in herosTeam)
        {
            Player aux = s.gameObject.GetComponent<Player>();
            if (aux != null)
            {
                if (aux.GetTeam() == team)
                {
                    this.herosTeam.Add(aux.gameObject);
                    aux.setTeamColor(teamColor);//Se le pasa el color del equipo establecido desde el gamehandler
                    Debug.Log("Added Team to team " + team);
                }
            }
        }
    }
}


