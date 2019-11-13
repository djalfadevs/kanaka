using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuScript
{
    [MenuItem("Tools/SpawnTotems")]
    public static void AssignTileScript()
    {
        GameObject[] spawnTotems = GameObject.FindGameObjectsWithTag("TotemSpawner");
        foreach (GameObject t in spawnTotems)
        {
            t.GetComponent<TotemSpawner>().SpawnTotems(3);
        }
    }
}
