using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{   /// <summary>
    /// Guardamos dos variables de equipos que guardaran referencias a los personajes de la escena.
    /// </summary>
    [SerializeField] private List<GameObject> Team1; //En la escena , referencia a los personajes del equipo uno
    [SerializeField] private List<GameObject> TotemsTeam1;
    [SerializeField] private List<GameObject> SpawnsTeam1;
    private int AliveTotemTeam1;

    [SerializeField] private List<GameObject> Team2;// En la escena , referencia a los personajes del equipo dos
    [SerializeField] private List<GameObject> TotemsTeam2;
    [SerializeField] private List<GameObject> SpawnsTeam2;
    private int AliveTotemTeam2;

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
        Team1 = new List<GameObject>();//Se inicializan vacias y luego se les guardaran las referencias a estos.
        Team2 = new List<GameObject>();
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
}
