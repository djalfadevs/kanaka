using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{

    private IList<GameObject> herosTeam { get; set; }
    //En la escena , referencia a los personajes del equipo uno


    private IList<GameObject> totemsTeam { get; set; }

    private IList<GameObject> spawnsTeam { get; set; }

    public int AliveTotemTeam;

    public int TotalTotemsTeam;

    private Color teamColor { get; set; }

    public Team()
    {
        herosTeam = new List<GameObject>(); //En la escena , referencia a los personajes del equipo uno
        totemsTeam = new List<GameObject>();
        spawnsTeam = new List<GameObject>();
        teamColor = new Color(0, 0, 0);//Color por defecto
        AliveTotemTeam = 0;
        TotalTotemsTeam = 0;
    }

    public Team(Color color)
    {
        herosTeam = new List<GameObject>(); //En la escena , referencia a los personajes del equipo uno
        totemsTeam = new List<GameObject>();
        spawnsTeam = new List<GameObject>();
        teamColor = color;
        AliveTotemTeam = 0;
        TotalTotemsTeam = 0;
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
        TotalTotemsTeam = 0;
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
                    TotalTotemsTeam++;//
                    Debug.Log("Added Totem to team " + team + " and set color " + teamColor);
                }
            }
        }
    }

    public void AddTeamSpawner(int team, GameObject[] heroSpawners)
    {
        this.spawnsTeam.Clear();//Limpiamos la lista antes de añadir
        foreach (GameObject s in heroSpawners)
        {
            HeroSpawners aux = s.gameObject.GetComponent<HeroSpawners>();
            if (aux != null)
            {
                if (aux.GetTeam() == team)
                {
                    this.spawnsTeam.Add(aux.gameObject);
                    aux.setTeamColor(teamColor);//Se le pasa el color del equipo establecido desde el gamehandler
                    Debug.Log("Added Spawn to team " + team + " and set color " + teamColor);
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
                    //aux.setTeamColor(teamColor);//Se le pasa el color del equipo establecido desde el gamehandler
                    Debug.Log("Added Team to team " + team + " and set color " + teamColor);
                }
            }
        }
    }

    public void CountAliveTotems()
    {
        int auxAliveTotems = 0;
        foreach (GameObject t in totemsTeam)
        {
            if (t.GetComponent<Totem>() != null)
            {
                if (t.GetComponent<Totem>().GetHp() > 0)
                {
                    auxAliveTotems++;
                }
            }
        }
        //Debug.Log("Recalculando totems vivos " + auxAliveTotems);
        AliveTotemTeam = auxAliveTotems;

        //A partir de aqui si los totems vivos son 0 se llama a fin de partida
        //if (auxAliveTotems <= 0)
        //{
        //    Lose();
        //}
    }
    
    /*
    public void Lose()
    {
        GameObject gameHandler = GameObject.FindGameObjectWithTag("GameController");
        gameHandler.GetComponent<GameHandler>().FinDePartida();
    }
    */
}


