    using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class offlinegm : MonoBehaviour
{
   
    [SerializeField] private bool matchIsFinished = false;
    [SerializeField] public bool recalculateAliveTotems = false;
    [SerializeField] private GameObject cameraController;
    private bool StartTimers = false;
    [SerializeField] private Text TimerText;//Debug
    [SerializeField] private Text TotalTotemsText;//Debug
    [SerializeField] private static int totemsDestroyed;
    [SerializeField] private static double matchduration = 15;
    [SerializeField] private double prematch=3;
    public GameObject menu;
    public List<GameObject> herolist;
    // Start is called before the first frame update
    void Start()
    {
        InstanciateHero();
        matchIsFinished = false;
        prematch = 3;
        matchduration = 15;
        totemsDestroyed = 0;
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
        matchduration += 3;
    }

    void TimeManager()
    {
        if (matchduration>0)
        {
            TimerText.text = "Time left: " + Math.Round(matchduration);
            matchduration -= Time.deltaTime;
        }
        else
        {
            Debug.Log("match is finished");
            matchIsFinished = true;
            //END MATCH
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
