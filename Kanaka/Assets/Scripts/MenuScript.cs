using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuScript
{
    [MenuItem("Tools/SpawnTotems")]
    public static void AssignTileScript()
    {
        GameObject[] spawnTotems = GameObject.FindGameObjectsWithTag("EnemySpawner");
        foreach (GameObject t in spawnTotems)
        {
            if(t.GetComponent<CorruptedTotemSpawner>()!=null)
            t.GetComponent<CorruptedTotemSpawner>().SpawnTotems(3);
        }
    }
}
