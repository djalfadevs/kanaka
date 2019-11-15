using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
}
