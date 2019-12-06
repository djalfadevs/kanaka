using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private bool isBusy;
    [SerializeField] private Vector3 BusyBoxSize;
    [SerializeField] private float boxHeight;
    private static List<Transform> spawners = new List<Transform>();
    [SerializeField] private GameObject toSpawn;
    private Vector3 spawnPos;
    [SerializeField] private static int MaxCajasInScene = 5;
    public static int numCajas = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        spawners.Add(this.transform);
    }

    public static void Spawn()
    {
        int aux=Mathf.RoundToInt(UnityEngine.Random.Range(0.0f, (float)spawners.Count-1));
        spawners[aux].GetComponent<ItemSpawner>().CalculateItemSpawnPoint(spawners[(int)aux]);
        
        if(numCajas< MaxCajasInScene)
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.Instantiate("caja",
             spawners[(int)aux].GetComponent<ItemSpawner>().spawnPos, spawners[(int)aux].GetComponent<ItemSpawner>().toSpawn.transform.rotation);
                numCajas++;
            }
            else
            {
                Instantiate(spawners[(int)aux].GetComponent<ItemSpawner>().toSpawn,
                   spawners[(int)aux].GetComponent<ItemSpawner>().spawnPos, spawners[(int)aux].GetComponent<ItemSpawner>().toSpawn.transform.rotation);
                numCajas++;
            }
        }
    }

    private void CalculateItemSpawnPoint(Transform pos)
    {
        bool isBusyaux = false;
        int remaining_attempts = 5;
        //Debug.LogError("Result: " + " " + auxL.Count);
        do
        {
            Vector3 spawnPointXZ = RandomCircle(pos.position, radius);
            isBusyaux = (CheckIsBusy(spawnPointXZ));
            spawnPos = CalculateExactPoint(spawnPointXZ);
            remaining_attempts--;
        } while (isBusyaux && remaining_attempts > 0);
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

    private bool CheckIsBusy(Vector3 pos)
    {
        isBusy = false; //Inicializacion
        Vector3 initicalPos = pos;
        Vector3 checkBoxHalfHeight = new Vector3(0, BusyBoxSize.y / 2, 0);

        Collider[] coll = Physics.OverlapBox(initicalPos + checkBoxHalfHeight, BusyBoxSize, Quaternion.identity);

        foreach (Collider c in coll)
        {
            //Si choca con un jugador esq hay alguien encima y por tanto esta ocupado
            if (c.gameObject.tag == "Player"||c.gameObject.tag=="CajaObjetos")
            {
                isBusy = true;
            }
        }

        return isBusy;
    }

    private Vector3 CalculateExactPoint(Vector3 spawnPointXZ)
    {
        RaycastHit hit;
        if (Physics.Raycast(spawnPointXZ, Vector3.down, out hit, 20))
        {
            spawnPointXZ.y = hit.point.y;
            spawnPointXZ += new Vector3(0, boxHeight / 2, 0);
        }

        return spawnPointXZ;
    }

    private void OnDrawGizmos()
    {
        if (radius > 0)
        {
            Gizmos.DrawWireSphere(this.GetComponent<Transform>().position, radius);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
