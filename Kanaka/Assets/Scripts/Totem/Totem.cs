using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    [SerializeField] private int team;
    [SerializeField] private float hp = 1;
    [SerializeField] private Color teamColor;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetTeam()
    {
        return team;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = teamColor;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.2f,0.2f,0.2f));
    }

    public void setTeamColor(Color color)
    {
        teamColor = color;
    }
}
