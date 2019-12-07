using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedTotemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject totem;
    [SerializeField] private float radius = 1;
    [SerializeField] private float respawnTime;
    [SerializeField] private bool keepSpawning;
    [SerializeField] private int numberOfTotems;
    [SerializeField] private float raycastDepth = 10;
    [SerializeField] private int MaxTotems;
    private int SpawnedTotems;
    private Transform transformSpawner;
    private float currentRespawnTime;
    // Start is called before the first frame update
    void Start()
    {
        transformSpawner = this.gameObject.GetComponent<Transform>();
        currentRespawnTime = respawnTime;
        SpawnedTotems = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimeHandler();
    }

    public void SpawnTotems(int numberOfTotems)
    {
        if (SpawnedTotems <= MaxTotems)
        {
            SpawnedTotems++;
            Vector3 center = transformSpawner.position;
            for (int i = 0; i < numberOfTotems; i++)
            {
                Vector3 pos = RandomCircle(center, radius);
                float auxY = FindPositionInY(pos);
                //Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
                if (auxY != -1)
                {
                    pos.y = auxY;//Cambiamos la posicion en Y
                    //La rotacion del totem en Y es aleatoria
                    GameObject q=Instantiate(totem, pos, Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0.0f, 360.0f), 0)));
                    q.GetComponent<CorruptedTotem>().setSpawner(this);
                }
            }
        }
    }
    public void DespawnTotems()
    {
        SpawnedTotems--;
    }

    //Devuelve -1 si ya hay un totem en esa posicion
    //Si no devuelve el valor para colocar el totemcorrupto de forma correcta en el mapa
    private float FindPositionInY(Vector3 pos)
    {
        float auxPosSpawnerY = this.GetComponent<Transform>().position.y;
        RaycastHit hit;
        float positionY = -1;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(new Vector3(pos.x,auxPosSpawnerY,pos.z), Vector3.down, out hit, raycastDepth))
        {
            Debug.DrawRay(new Vector3(pos.x, auxPosSpawnerY, pos.z), Vector3.down * hit.distance, Color.yellow,100);
            Collider auxColl = totem.GetComponent<Collider>();
            //Si no choca con ningun totem ya intanciado y tampoco choca con el jugador
            if (!Physics.CheckBox(hit.point, auxColl.bounds.size / 2))
            {
                Debug.DrawRay(pos, Vector3.up * hit.distance, Color.yellow, 100);
                Debug.DrawLine(pos + (auxColl.bounds.size / 2), pos - (auxColl.bounds.size / 2), Color.red, 100);
                positionY = hit.point.y + (auxColl.bounds.size.y / 2);
            }
             
        }
        return positionY;
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = UnityEngine.Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad)* UnityEngine.Random.Range(0.0f,1.0f);
        pos.y = transformSpawner.position.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad)* UnityEngine.Random.Range(0.0f, 1.0f);
        return pos;
    }

    private void OnDrawGizmos()
    {
        if (radius > 0)
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(this.GetComponent<Transform>().position, radius);
    }

    void SpawnTimeHandler()
    {
        currentRespawnTime -= Time.deltaTime;
        if (currentRespawnTime <= 0 && respawnTime>1)//Medida de seguridad el segundo punto
        {
            if (keepSpawning)
            {
                SpawnTotems(numberOfTotems);
            }
            currentRespawnTime = respawnTime;
        }
    }
}
