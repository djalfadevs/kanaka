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

    private Transform transformSpawner;
    private float currentRespawnTime;
    // Start is called before the first frame update
    void Start()
    {
        transformSpawner = this.gameObject.GetComponent<Transform>();
        currentRespawnTime = respawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimeHandler();
    }

    public void SpawnTotems(int numberOfTotems)
    {
        Vector3 center = transformSpawner.position;
        for (int i = 0; i < numberOfTotems; i++)
        {
            Vector3 pos = RandomCircle(center, radius);
            //Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            Instantiate(totem, pos,Quaternion.Euler(new Vector3(0,0,0)));
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad)* Random.Range(0.0f,1.0f);
        pos.y = transformSpawner.position.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad)* Random.Range(0.0f, 1.0f);
        return pos;
    }

    private void OnDrawGizmos()
    {
        if(radius>0 && transformSpawner!=null)
        Gizmos.DrawWireSphere(transformSpawner.position, radius);
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
