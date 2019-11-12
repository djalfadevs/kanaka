using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   /// <summary>
    /// Guardamos dos variables de equipos que guardaran referencias a los personajes de la escena.
    /// </summary>
    [SerializeField] private List<GameObject> Team1; //En la escena , referencia a los personajes del equipo uno
    [SerializeField] private List<GameObject> TotemsTeam1;
    [SerializeField] private List<GameObject> SpawnsTeam1;
    private float VictoryPointsTeam1;

    [SerializeField] private List<GameObject> Team2;// En la escena , referencia a los personajes del equipo dos
    [SerializeField] private List<GameObject> TotemsTeam2;
    [SerializeField] private List<GameObject> SpawnsTeam2;
    private float VictoryPointsTeam2;

    [SerializeField] private List<GameObject> ItemsSpawns; //Referencia a la lista de items

    // Start is called before the first frame update
    void Start()
    {
        Team1 = new List<GameObject>();//Se inicializan vacias y luego se les guardaran las referencias a estos.
        Team2 = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FinDePartida()
    {

    }
}
