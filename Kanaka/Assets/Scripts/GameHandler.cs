using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{   /// <summary>
    /// Guardamos dos variables de equipos que guardaran referencias a los personajes de la escena.
    /// </summary>

    [SerializeField] private IList<Team> teamList = new List<Team>();

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
        

        DetectTeamsSpawner();
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
    void DetectTeamsSpawner()
    {
        GameObject[] heroSpawners = GameObject.FindGameObjectsWithTag("Respawn"); 
       for(int i = 0; i < teamList.Count; i++)
        {
            DetectTeamSpawner(i,heroSpawners);
        }
    }

    
    void DetectTeamSpawner(int team,GameObject [] heroSpawners)
    {
       foreach(GameObject s in heroSpawners)
        {
          HeroSpawner aux =  s.gameObject.GetComponent<HeroSpawner>();
            if(aux!=null)
            {
                if(aux.Getteam() == team)
                {
                    (teamList[team]).GetSpawnsTeam().Add(aux.gameObject);
                }
            }
        }
    }
}
