using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume() => this.gameObject.SetActive(false);

    public void MainMenu()
    {
       GameObject gameHandler = GameObject.FindGameObjectWithTag("GameController");
        if (gameHandler.GetComponent<PhotonGameManager>() != null)
        {
            PhotonGameManager photonGameManager = gameHandler.GetComponent<PhotonGameManager>();
            photonGameManager.mainMenu();
        }
    }
}
