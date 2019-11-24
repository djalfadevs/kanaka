using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] private bool isBusy;
    [SerializeField] private Vector3 BusyBoxSize;
    [SerializeField] private int team;
    [SerializeField] private Color teamColor;
    [SerializeField] private float radious;
    private Transform transformSpawner;
    private Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        transformSpawner = GetComponent<Transform>();
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

    //Comprueba si el punto de aparicion ya esta en uso
    public bool CheckIsBusy(Vector3 pos)
    {
        isBusy = false; //Inicializacion
        Vector3 initicalPos = pos;
        Vector3 checkBoxHalfHeight = new Vector3(0, BusyBoxSize.y / 2, 0);

        Collider[] coll = Physics.OverlapBox(initicalPos + checkBoxHalfHeight, BusyBoxSize, Quaternion.identity);
        
        foreach(Collider c in coll)
        {
            //Si choca con un jugador esq hay alguien encima y por tanto esta ocupado
            if(c.gameObject.tag == "Player")
            {
                isBusy = true;
            }
        }

        return isBusy;
    }

    //Calcula el punto exacto donde debe aparecer el jugador para que salga tocando el suelo directamente
    //Ademas tiene en cuenta si ese punto ya esta en uso.
    public bool CalculateHeroSpawnPoint(Player player)
    {
        
        Vector3 spawnPointXZ = RandomCircle(transformSpawner.position, radious);
        if (!CheckIsBusy(spawnPointXZ))
        {
            spawnPos =CalculateExactPoint(spawnPointXZ,player);
            return true;
        }
        return false;
    }

    public Vector3 CalculateExactPoint(Vector3 spawnPointXZ,Player player)
    {
        RaycastHit hit;
        if (Physics.Raycast(spawnPointXZ, Vector3.down, out hit))
        {
            spawnPointXZ.y = hit.point.y;
            spawnPointXZ += new Vector3(0, (player.gameObject.GetComponent<Collider>().bounds.size.y) / 2, 0);
        }

        return spawnPointXZ;
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = UnityEngine.Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad) * UnityEngine.Random.Range(0.0f, 1.0f);
        pos.y = transformSpawner.position.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad) * UnityEngine.Random.Range(0.0f, 1.0f);
        return pos;
    }

    //Cuando spawnea un heroe en este spawner podra realizar las acciones determinadas en esta funcion
    public void SpawnAction()
    {

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
