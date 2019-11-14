using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{   /// <summary>
    /// Guardamos dos variables de equipos que guardaran referencias a los personajes de la escena.
    /// </summary>
 
    public IList<Team> teamList = new List<Team>();//Guarda referencias a datos de una clase con datos de los equipos 

    [SerializeField] private List<Color> teamColorList;
    [SerializeField] private List<GameObject> ItemsSpawns; //Referencia a la lista de items

    //Timer
    [SerializeField] private float mainTimer;
    private float timer;
    private bool doOnce = false;
    private bool canCount = true;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Tiempo Empezado");
        timer = mainTimer;

        //Test
        for(var j = 0; j<2; j++)
        {
            if (teamColorList[j] != null)
            {
                teamList.Add(new Team(teamColorList[j]));
            }
            else
            {
                teamList.Add(new Team());
            }
        }
        
        AddTotemsTeams();
        AddTeamsSpawner();
        AddHerosTeams();
        ///////////////
    }

    // Update is called once per frame
    void Update()
    {
        CountTimer();
    }

    void FinDePartida()
    {

    }

    void CountTimer()
    {
        if(timer>=0.0f && canCount)
        {
            timer -= Time.deltaTime;
        }
        else if (timer<= 0.0f && !doOnce)
        {
            Debug.Log("Tiempo Acabado");
            doOnce = true;
            canCount = false;
            timer = 0.0f;
        }
    }
    /// <summary>
    /// Detectamos los spawns pertenecientes a cada equipo
    /// Los gameobject que son spawns de los heroes estan identificados mediante 
    /// -- un componente HeroSpawner
    /// -- un tag Respawn
    /// Una vez hayados todos los spawners en el mapa activos mediante un gameobject.findtag
    /// Detectamos que spawners son de cada equipo, para eso el script que tienen los gameobject HeroSpawners llamado tambien HeroSpawner
    /// tienen un atributo propio int que indica a que equipo (en funcion del orden de la lista) pertenece.
    /// </summary>
    void AddTeamsSpawner()
    {
       GameObject[] heroSpawners = GameObject.FindGameObjectsWithTag("Respawn"); 
       for(int i = 0; i < teamList.Count; i++)
        {
            Team auxT = teamList[i];
            auxT.AddTeamSpawner(i,heroSpawners);
        }
    }

    /// <summary>
    /// Detectamos los totems pertenecientes a cada equipo
    /// Los gameobject que son totems de los heroes estan identificados mediante 
    /// -- un componente Totem
    /// -- un tag Totem
    /// Una vez hayados todos los totems en el mapa activos mediante un gameobject.findtag
    /// Detectamos que totems son de cada equipo, para eso el script que tienen los gameobject Totem llamado tambien Totem
    /// tienen un atributo propio int que indica a que equipo (en funcion del orden de la lista) pertenece.
    /// Se ha movido parte de la funcionalidad a la clase Team.cs
    /// </summary>
    void AddTotemsTeams()
    {
        GameObject[] totems = GameObject.FindGameObjectsWithTag("Totem");
        for (int i = 0; i < teamList.Count; i++)
        {
            Team auxT = teamList[i];
            auxT.AddTotemsTeam(i, totems);
        }
    }

    //Lo mismo que en los anteriores pero para los heroes
    void AddHerosTeams()
    {
        GameObject[] heros = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < teamList.Count; i++)
        {
            Team auxT = teamList[i];
            auxT.AddHerosTeam(i, heros);
        }
    }

}
