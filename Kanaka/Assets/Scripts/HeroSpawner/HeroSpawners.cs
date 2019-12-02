using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawners : MonoBehaviour
{
    [SerializeField] private bool isBusy;
    [SerializeField] private Vector3 BusyBoxSize;
    [SerializeField] private int team;
    [SerializeField] private Color teamColor;
    [SerializeField] private float radious;

    private static List<Transform> transformSpawner = new List<Transform>();
    private static List<Transform> transformSpawnerTeam1 = new List<Transform>();
    private static List<Transform> transformSpawnerTeam2 = new List<Transform>();

    private Vector3 spawnPos;
    
    void Awake()
    {
        transformSpawner.Add(this.transform);
        //Guardamos en otras variables aquellas que pertenecen en exclusiva al equipo uno y dos.
        if (this.team.Equals(0))
        {
            transformSpawnerTeam1.Add(this.transform);
        }

        if (this.team.Equals(1))
        {
            transformSpawnerTeam2.Add(this.transform);
        }
        //Debug.LogError("Los resultados son " + transformSpawner.Count + " " + transformSpawnerTeam1.Count + " " + transformSpawnerTeam2.Count);
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void RespawnHero()
    {

    }

    public int GetTeam()
    {
        return team;
    }

    public void setTeamColor(Color color)
    {
        teamColor = color;
        ColorAplication();
    }

    private void ColorAplication()
    {
        
    }

 
    public Vector3 Spawn(Player player,int teamMembership)
    {
        CalculateHeroSpawnPoint(player,teamMembership);
        return spawnPos;
    }

    //Calcula el punto exacto donde debe aparecer el jugador para que salga tocando el suelo directamente
    //Ademas tiene en cuenta si ese punto ya esta en uso.
    public void CalculateHeroSpawnPoint(Player player,int teamMembership)
    {
        
        List<Transform> auxL = SelectTransformList(teamMembership);
        bool isBusyaux = false;
        int remaining_attempts = 5;
        //Debug.LogError("Result: " + " " + auxL.Count);
        do
        {
        //Debug.LogError("Result: " + isBusyaux + " " + auxL.Count);
        Transform auxT = auxL[(int) Mathf.Round(UnityEngine.Random.Range(0.0f, auxL.Count - 1.0f))]; //Se elige un sitio de despliegue aleatorio dentro de la lista.
        Vector3 spawnPointXZ = RandomCircle(auxT.position, radious); 
        isBusyaux = (CheckIsBusy(spawnPointXZ));
        spawnPos = CalculateExactPoint(spawnPointXZ, player);
        remaining_attempts--;
        } while (isBusyaux && remaining_attempts>0);
    }

    private List<Transform> SelectTransformList(int teamMembership)
    {
        if(teamMembership == 0)
        {
            return transformSpawnerTeam1;
        }
        if (teamMembership == 1)
        {
            return transformSpawnerTeam2;
        }
        else
        {
            return transformSpawner;
        }
    }

    private Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = UnityEngine.Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad) * UnityEngine.Random.Range(0.0f, 1.0f);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad) * UnityEngine.Random.Range(0.0f, 1.0f);
        return pos;
    }

    //Comprueba si el punto de aparicion ya esta en uso
    private bool CheckIsBusy(Vector3 pos)
    {
        isBusy = false; //Inicializacion
        Vector3 initicalPos = pos;
        Vector3 checkBoxHalfHeight = new Vector3(0, BusyBoxSize.y / 2, 0);

        Collider[] coll = Physics.OverlapBox(initicalPos + checkBoxHalfHeight, BusyBoxSize, Quaternion.identity);

        foreach (Collider c in coll)
        {
            //Si choca con un jugador esq hay alguien encima y por tanto esta ocupado
            if (c.gameObject.tag == "Player")
            {
                isBusy = true;
            }
        }

        return isBusy;
    }


    private Vector3 CalculateExactPoint(Vector3 spawnPointXZ, Player player)
    {
        RaycastHit hit;
        if (Physics.Raycast(spawnPointXZ, Vector3.down, out hit))
        {
            spawnPointXZ.y = hit.point.y;
            spawnPointXZ += new Vector3(0, (player.gameObject.GetComponent<Collider>().bounds.size.y) / 2, 0);
        }

        return spawnPointXZ;
    }

    private void OnDrawGizmos()
    {
        if (radious > 0)
        {
            Gizmos.color = teamColor;
            Gizmos.DrawWireSphere(this.GetComponent<Transform>().position, radious);
        }  
    }
}
