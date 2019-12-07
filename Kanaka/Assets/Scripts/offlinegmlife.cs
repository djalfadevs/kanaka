    using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class offlinegmlife : MonoBehaviour
{
   
    [SerializeField] private bool matchIsFinished = false;
    [SerializeField] public bool recalculateAliveTotems = false;
    [SerializeField] private GameObject cameraController;
    private bool StartTimers = false;
    [SerializeField] private Text TimerText;//Debug
    [SerializeField] private Text TotalTotemsText;//Debug
    [SerializeField] private static int totemsDestroyed;
    [SerializeField] private double matchduration = 0;
    [SerializeField] private double prematch=3;
    private float TimeLastHit;
    public GameObject menu;
    public List<GameObject> herolist;
    private static Player p2;
    // Start is called before the first frame update
    void Start()
    {
        InstanciateHero();
        matchIsFinished = false;
        prematch = 3;
        matchduration = 0;
        totemsDestroyed = 0;
        TimeLastHit = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        RecalculateTotems();
        if (prematch>0)
        {
            TimeManager();
            if (matchIsFinished)
            {
                FinDePartida();
            }
        }
        else
        {
            TimerText.text = "Game Starts in: " + Math.Round(prematch);
            prematch -= Time.deltaTime;
        }
        
    }

    void RecalculateTotems()
    {
        TotalTotemsText.text = "Totems killed: " + totemsDestroyed;
    }

    public static void destroyTotem()
    {
        totemsDestroyed++;
        if(p2!=null)
        p2.Heal(4);
    }

    void TimeManager()
    {
        matchduration += Time.deltaTime;
        TimerText.text = "Time Survived: " + Math.Round(matchduration);

        if (TimeLastHit+1<=Time.time)
        {
            TimeLastHit = Time.time;
            if (p2 != null)
                p2.Hit(5);
        }
        if (p2!=null&&p2.HP<=0)
        {
            matchIsFinished = true;
        }
    }


    void InstanciateHero()
    {
        //LEEMOS LOS DATOS FIJADOS EN LA ANTERIOR PANTALLA Y GUARDADOS EN MATCHINPUT
         if (System.IO.File.Exists(Application.streamingAssetsPath + "/UsersData/MatchInput.json"))
         {
                string text2 = File.ReadAllText(Application.streamingAssetsPath + "/UsersData/MatchInput.json");
                OnlineUser ou = JsonUtility.FromJson<OnlineUser>(text2);
                Player p = this.herolist[ou.selchar].GetComponentInChildren<Player>();
                //Debug.DrawLine(spawnPoint,spawnPoint+Vector3.up,Color.yellow);
                //Debug.LogError(spawnPoint);
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                GameObject a = Instantiate(this.herolist[ou.selchar], this.transform.position, Quaternion.identity);
            p2 = a.GetComponentInChildren<Player>();
            Debug.Log(a);
                cameraController.GetComponent<CinemachineVirtualCamera>().Follow = a.GetComponentInChildren<Player>().transform;
           }

        
    }

    public void OnPauseButt()
    {
        menu.SetActive(true);
    }

    public void mainMenu()
    {
        //SceneManager.LoadScene();
        Debug.LogError("Se cargaria la escena de menu principal");
    }

    public void FinDePartida()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
