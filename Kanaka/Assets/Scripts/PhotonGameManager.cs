using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class PhotonGameManager : MonoBehaviourPunCallbacks , IPunObservable 
{
    /// <summary>
    /// Guardamos dos variables de equipos que guardaran referencias a los personajes de la escena.
    /// </summary>

    /// El readonly es como el final de java ///
    [TextArea]
    [SerializeField] private string MyTextArea = "El gameHandler tambien tiene acceso a una List<Team> con la info de los equipos";

    /*
     * Vamos a realizar un sistema mixto respecto a la gestion de la informacion que guarda la clase Team
     * 
     * Gestion propia de cada Vista:
     * -1 El numero total de totems y su referencia los gestiona cada vista
     * -2 El numero total de spawners y su referencia los gestiona cada vista
     * -3 Los totems vivos y su calculo 
     * 
     * Gestion por parte del Master y envio de info al resto
     * -1 Le corresponde al master indicar cuando recalcular los totems vivos
     * -
     */
    public IList<Team> teamList = new List<Team>();//Guarda referencias a datos de una clase con datos de los equipos 

    [SerializeField] private List<Color> teamColorList;
    [SerializeField] private int maxItemsSpawneds;
    [SerializeField] private List<GameObject> ItemsSpawns; //Referencia a la lista de items

    [SerializeField] private bool gameStarted = false;
    [SerializeField] private int winnerTeam = -1;
    [SerializeField] private int loserTeam = -1;
    [SerializeField] private bool matchIsFinished = false;
    [SerializeField] public bool recalculateAliveTotems = false;

    public List<GameObject> herolist = new List<GameObject>();

    private bool StartTimers = false;
    //Timer
    private double incTimer = 0;
    private double startTime = 0;
    private double decTimer = 111;
    [SerializeField] private double matchDuration = 120000;

    //Timer 2
    private double incTimer2 = 0;
    private double prestartTime = 0;
    private double decTimer2 = 111;
    [SerializeField] private double preMatchDuration = 60;

    [SerializeField] private Text TimerText;//Debug
    [SerializeField] private Text Team1TotalTotemsText;//Debug
    [SerializeField] private Text Team1AliveTotemsText;//Debug
    [SerializeField] private Text Team2TotalTotemsText;//Debug
    [SerializeField] private Text Team2AliveTotemsText;//Debug

    public float TimeBox= 5f;

    public GameObject playerPrefab;
    public GameObject menu;
    //public static GameObject LocalPlayerInstance;

    // Start is called before the first frame update
    void Awake()
    {
     
    }
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        InitializeTeamsInfo();
        InstanciateHero();
        InitializeTeamsInfo();
        InitializeMatch();
    }

    void SetTimers()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("PreStartTime"))
        {
            prestartTime = (double) PhotonNetwork.CurrentRoom.CustomProperties["PreStartTime"];
        }

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("StartTime"))
        {
            startTime = (double) PhotonNetwork.CurrentRoom.CustomProperties["StartTime"];
        }
    }

    void InstanciateHero()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {

            //LEEMOS LOS DATOS FIJADOS EN LA ANTERIOR PANTALLA Y GUARDADOS EN MATCHINPUT
            if (System.IO.File.Exists(Application.streamingAssetsPath + "/UsersData/MatchInput.json"))
            {
                string text2 = File.ReadAllText(Application.streamingAssetsPath + "/UsersData/MatchInput.json");
                OnlineUser ou = JsonUtility.FromJson<OnlineUser>(text2);
                int aux = ou.team;
                Debug.LogError(aux + "team i support");
                object[] instanceData = new object[2];
                instanceData[0] = aux;
                instanceData[1] = "#"+ ColorUtility.ToHtmlStringRGBA(teamColorList[aux]);
                
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);
                Player p = this.herolist[ou.selchar].GetComponentInChildren<Player>();
                Vector3 spawnPoint = (teamList[0].GetSpawnsTeam()[0]).GetComponent<HeroSpawners>().Spawn(p, aux);

                //Debug.DrawLine(spawnPoint,spawnPoint+Vector3.up,Color.yellow);
                //Debug.LogError(spawnPoint);
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                GameObject a = PhotonNetwork.Instantiate(this.herolist[ou.selchar].name, spawnPoint, Quaternion.identity, 0,instanceData);
            }          
            
        }
    }
    void InitializeTeamsInfo()
    {
        //Añadimos dos equipos al comienzo
        for (var j = 0; j < 2; j++)
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

        //Guardamos los totems (total y vivos) , heroes y spawners de heroes 
        AddTotemsTeams();
        AddTeamsSpawner();
        CountAliveTotemsinTeams();
        AddHerosTeams();

        //DEBUG
        Team1TotalTotemsText.text = "TotalTotems: " + teamList[0].TotalTotemsTeam;
        Team1AliveTotemsText.text = "AliveTotems: " + teamList[0].AliveTotemTeam;

        Team2TotalTotemsText.text = "TotalTotems: " + teamList[1].TotalTotemsTeam;
        Team2AliveTotemsText.text = "AliveTotems: " + teamList[1].AliveTotemTeam;
        //DEBUG
    }

    void InitializeMatch()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable { { "PreStartTime", PhotonNetwork.Time } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(ht);

            ExitGames.Client.Photon.Hashtable ht2 = new ExitGames.Client.Photon.Hashtable { { "StartTimers", true } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(ht2);
        }
        SetTimers();
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("StartTimers"))
        {
            StartTimers = (bool)PhotonNetwork.CurrentRoom.CustomProperties["StartTimers"];
        }
            


    }
    void SetStartTime()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable { { "StartTime", PhotonNetwork.Time } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(ht);
            ExitGames.Client.Photon.Hashtable ht2 = new ExitGames.Client.Photon.Hashtable { { "GameStarted", true } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(ht2);    
        }
        SetTimers();
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("GameStarted"))
        {
            gameStarted = (bool)PhotonNetwork.CurrentRoom.CustomProperties["GameStarted"];
        }
        


    }

    // Update is called once per frame
    void Update()
    {
         if (!PhotonNetwork.IsConnected)
         {
            if (TimeBox <= 0)
            {
                ItemSpawner.Spawn();
                TimeBox = UnityEngine.Random.Range(5.0f, 10.0f);
            }
            else
            {
                TimeBox -= Time.deltaTime;
            }
            return;
          }

         RecalculateAliveTotems();
         if (StartTimers)
            { 
             CountTimer();
             CountTimer2();
         }
            if (gameStarted && PhotonNetwork.IsMasterClient)
            {
                if (TimeBox <= 0)
                {
                    ItemSpawner.Spawn();
                    TimeBox = UnityEngine.Random.Range(5.0f, 10.0f);
                }
                else
                {
                    TimeBox -= Time.deltaTime;
                }
            }
        

        if (matchIsFinished)
        {
            FinDePartida();
        } 
        
    }

    public void RecalculateAliveTotems()
    {
        //DEBUG
        if (recalculateAliveTotems)
        {
            this.CountAliveTotemsinTeams();
            Team1TotalTotemsText.text = "TotalTotems: " + teamList[0].TotalTotemsTeam;
            Team1AliveTotemsText.text = "AliveTotems: " + teamList[0].AliveTotemTeam;

            Team2TotalTotemsText.text = "TotalTotems: " + teamList[1].TotalTotemsTeam;
            Team2AliveTotemsText.text = "AliveTotems: " + teamList[1].AliveTotemTeam;

            if (!PhotonNetwork.IsMasterClient)
            {
                recalculateAliveTotems = false;
            }
        }
        /////
    }

    //LLeva el tiempo de la partida
    public void CountTimer()
    {
        if (!gameStarted)
        {
            return;
        }
        // Example for a increasing timer
        incTimer = PhotonNetwork.Time - startTime;
        // Example for a decreasing timer
        decTimer = matchDuration - incTimer;

        if (decTimer < 0)
            decTimer = 0;

        //Debug
        TimerText.text = "Time: " +  TimeSpan.FromSeconds(Math.Round(decTimer)).ToString(@"m\:ss"); ;
        //////

        if (decTimer<=0)
        {
            Debug.Log("Fin de Partida");
            matchIsFinished = true;
        }
    }

    //LLeva el tiempo Pre Partida
    public void CountTimer2()
    {
        if (gameStarted)
        {
            return;
        }

        incTimer2 = PhotonNetwork.Time - prestartTime;
        decTimer2 = preMatchDuration - incTimer2;

        if (decTimer2 < 0)
            decTimer2 = 0;

        TimerText.text = "Game Starts in: " + Math.Round(decTimer2);

        if (decTimer2 <= 0)
        {
            //Debug.Log("Fin de Pre-Partida");
            SetStartTime();
        }
    }

    public void FinDePartida()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //PhotonNetwork.LoadLevel("Escena Photon Resultados");
        }

        //FALTA GESTIONAR TODO EL TEMA DE VICTORIA DERROTA
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
    public void AddTeamsSpawner()
    {
        GameObject[] heroSpawners = GameObject.FindGameObjectsWithTag("Respawn");
        for (int i = 0; i < teamList.Count; i++)
        {
            Team auxT = teamList[i];
            auxT.AddTeamSpawner(i, heroSpawners);
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
    public void AddTotemsTeams()
    {
        GameObject[] totems = GameObject.FindGameObjectsWithTag("Totem");
        for (int i = 0; i < teamList.Count; i++)
        {
            Team auxT = teamList[i];
            auxT.AddTotemsTeam(i, totems);
        }
    }

    //Lo mismo que en los anteriores pero para los heroes
    public void AddHerosTeams()
    {
        GameObject[] heros = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < teamList.Count; i++)
        {
            Team auxT = teamList[i];
            auxT.AddHerosTeam(i, heros);
        }
    }

    //Calcula los totems vivos en cada equipo
    public void CountAliveTotemsinTeams()
    {
        
        for (int i = 0; i < teamList.Count; i++)
        {
            Team auxT = teamList[i];
            auxT.CountAliveTotems();

            //FIN DE PARTIDA si un equipo tiene todos sus totems muertos;
            if (auxT.AliveTotemTeam == 0 && PhotonNetwork.IsMasterClient)
            {
                matchIsFinished = true;
                loserTeam = i;
            }
        }

        
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(gameStarted);

            stream.SendNext(recalculateAliveTotems);
            if (recalculateAliveTotems)
            {
                recalculateAliveTotems = false;
            }

            stream.SendNext(loserTeam);

            stream.SendNext(matchIsFinished);

            //stream.SendNext(StartTimers);
        }
        else
        {
            this.gameStarted = (bool)stream.ReceiveNext();

            bool auxrecalculate = (bool)stream.ReceiveNext();
            if (auxrecalculate)
            {
                this.recalculateAliveTotems = auxrecalculate;
            }

            this.loserTeam = (int) stream.ReceiveNext();

            this.matchIsFinished = (bool)stream.ReceiveNext();

            //this.StartTimers = (bool)stream.ReceiveNext();
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        //Debug.Log("Ha cambiado algo y se ha detectado");
        object propsTime;
        if (propertiesThatChanged.TryGetValue("StartTime", out propsTime))
        {
            startTime = (double)propsTime;
            // Debug.Log(startTime);
        }
        if (propertiesThatChanged.TryGetValue("PreStartTime", out propsTime))
        {
            prestartTime = (double)propsTime;
            //Debug.Log(prestartTime +" aa");
        }
        if (propertiesThatChanged.TryGetValue("GameStarted", out propsTime))
        {
            gameStarted = (bool)propsTime;
            //Debug.Log(prestartTime +" aa");
        }
        if (propertiesThatChanged.TryGetValue("StartTimers", out propsTime))
        {
            StartTimers = (bool)propsTime;
            //Debug.Log(prestartTime +" aa");
        }
    }

    public void OnPauseButt()
    {
        menu.SetActive(true);
    }

    public void mainMenu()
    {
        PhotonNetwork.LeaveRoom();
        //SceneManager.LoadScene();
        Debug.LogError("Se cargaria la escena de menu principal");
    }

    public override void OnLeftRoom()
    {
        //PhotonNetwork.LoadLevel("");
        Debug.LogError("Se cargaria un mensaje de que alguien ha abandonado la sala y al aceptar se permitiria volver al menu principal");
    }
}
