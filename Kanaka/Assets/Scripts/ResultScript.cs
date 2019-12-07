using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{

    static public string LastScene;
    private double secondsLeft;
    [SerializeField] TextMeshProUGUI textQ;
    [SerializeField] TextMeshProUGUI textA;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayLoadLevel(3));

    }
    IEnumerator DelayLoadLevel(float seconds)
    {
        secondsLeft = seconds;
        if (LastScene.Equals("Survival"))
        {
            textQ.text = "Seconds Survived";
            textA.text = Math.Round(offlinegmlife.matchduration).ToString();
        }
        else
        {
            textQ.text = "Totems Destroyed";
            textA.text = offlinegm.totemsDestroyed.ToString();
        }
        do
        {
            yield return new WaitForSeconds(1);
        } while (--secondsLeft > 0);
        SceneManager.LoadScene("MainMenu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
