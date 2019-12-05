using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Photon.Pun;

public class MenuScript
{
    [MenuItem("Tools/SpawnTotems")]
    public static void SpawnTotemsScript()
    {
        GameObject[] spawnTotems = GameObject.FindGameObjectsWithTag("EnemySpawner");
        foreach (GameObject t in spawnTotems)
        {
            if(t.GetComponent<CorruptedTotemSpawner>()!=null)
            t.GetComponent<CorruptedTotemSpawner>().SpawnTotems(3);
        }
    }

    [MenuItem("Tools/HitTotems")]
    public static void HitTotemsScript()
    {
        GameObject[] Totems = GameObject.FindGameObjectsWithTag("Totem");
        foreach (GameObject t in Totems)
        {
            if (t.GetComponent<Totem>() != null)
                t.GetComponent<Totem>().Hit(null);
        }
    }

    [MenuItem("Tools/InstanceBoxMaster")]
    public static void InstanceBoxScript()
    {
       if(PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("caja", new Vector3(12, 0, 12), Quaternion.identity);
        }
    }
}
